using Irony.Interpreter.Ast;
using Irony.Parsing;
using Practica1_0781.Generales;
using Practica1_0781.ParteB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Practica1_0781.ParteA
{
    [Serializable]
    public class AnalizadorP1
    {
        public Archivo arch;

        public AnalizadorP1() { }               

        public void compilar(String entrada, Archivo arch)
        {
            this.arch = arch;
            GrammarP1 gramatica = new GrammarP1();
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parser = new Parser(lenguaje);

            ParseTree arbol = parser.Parse(entrada);
            ParseTreeNode raiz = arbol.Root;

            if (raiz == null || arbol.ParserMessages.Count > 0 || arbol.HasErrors())
            {                
                reporteErrores_de_Irony(arbol);
            }
            else
            {
                Accciones(arbol);
                arch.desplegarErrores();
            }
        }


        #region "Otros"

        private void reporteErrores_de_Irony(ParseTree arbol)
        {
            String tipo_error, descripcion;
            foreach (var item in arbol.ParserMessages)
            {
                if (item.Message.Contains("Invalid character:"))
                {
                    tipo_error = "Lexico"; descripcion = item.Message;
                }
                else
                {
                    tipo_error = "Sintactico"; descripcion = item.Message.Replace("Syntax error, expected:","Se Esperaba: ");
                }
                this.arch.guardarError("",item.Location.Line, item.Location.Column, tipo_error, descripcion);
            }
            arch.desplegarErrores();
        }

        //Grafico conjuntos
        private void graficarAFN_Conjuntos()
        {
            Graficador g = new Graficador();
            int i = 1;
            foreach (var item in arch.conjuntos)
            {
                contador = 0;
                Automata tempo = item.Value.getAFND_Conjuntos();
                tempo.listaEstados = new Dictionary<String, Nodo>();
                this.enumerarEstadosAFN(tempo.start, tempo.listaEstados);
                g.AFND_graficar("Conjunto_" + i + "_" + item.Value.nombre, tempo.listaEstados);
                i++;
            }
        }

        private void graficarAFN_Conjuntos_si()
        {
            Graficador g = new Graficador();

            int i = 1;
            foreach (var item in arch.conjuntos)
            {
                contador = 0;
                Automata tempo = item.Value.getAFND_Conjuntos();
                g.AFND_graficar("Conjunto_" + i + "_" + item.Value.nombre, tempo.start);
                i++;
            }
        }

        //Grafico las Reglas Lexicas -> AFND
        private void graficarAFN_Reglas()
        {
            Graficador g = new Graficador();
            foreach (var item in arch.reglas)
            {
                g.AFND_graficar(item.ruta_relativa, item.afn.listaEstados);
            }
        }

        #endregion


        #region "Acciones"

        private void Accciones(ParseTree arbol)
        {
            //------------------------> Objeto que grafica            
            Graficador g = new Graficador();
            g.Arbol_graficar(arbol);

            F1_Clasificar(arbol.Root.ChildNodes[0]);

            if (arch.errores.Count > 0)
                return;
            //------------------------> Aplicar Metodo Subconjuntos
            F2_A_MetodoSubconjuntos();

            //------------------------> Generar AFD's


        }

        //Guardo conjuntos y creo afnd
        private void F1_Clasificar(ParseTreeNode nodo)
        {
            switch (nodo.Term.Name)
            {
                case "INICIO":
                    F1_Clasificar(nodo.ChildNodes[0]);
                    break;

                case "CODIGO":
                    foreach (ParseTreeNode hijo in nodo.ChildNodes)
                    {
                        F1_Clasificar(hijo);
                    }
                    break;

                case "CONJUNTO":
                    //Seria el de guardar conjuntos
                    this.guardarConjunto(nodo);
                    break;

                case "REGLA":
                    //Seria armar el afnd de la regla
                    //Seria de guardar el metodo de la regla
                    //Seria de guardar la lista de reservadas, si se solicita
                    this.guardarRegla(nodo);
                    break;

                case "ERROR":
                    //Seria de armar el afnd de error
                    //Seria de activar que ya vino esta sentencia
                    this.guardarError(nodo);
                    break;

                default:
                    Console.WriteLine("! =============================> Case else recorridoDX");
                    break;
            }
        }        
       
        //Guardo la Regla de ERROR
        private void guardarError(ParseTreeNode nodo)
        {
            String id = nodo.ChildNodes[0].Token.Text;

            //Validar que no haya venido antes
            if (arch.regla_error != null)
            {
                AstNode ad = (AstNode)nodo.AstNode;
                arch.guardarError(id, ad.Location.Line, ad.Location.Column, "Semantico", "Metodo Error ya vino");
                return;
            }

            //Validar que no exista ya en conjuntos de archivo            
            if (arch.existeRegla(id))
            {
                AstNode ad = (AstNode)nodo.AstNode;
                arch.guardarError(id, ad.Location.Line, ad.Location.Column, "Semantico", "Regla error con id ya existe");
                return;
            }

            //Obtener nombre de regla
            //ParseTreeNode h0 = nodo.ChildNodes[1];
            Regla regla = new Regla(id, arch.reglas.Count);

            //Obtener el AFN que tendra
            ParseTreeNode h1 = nodo.ChildNodes[1];
            regla.afn = this.obtenerAFN(h1);

            //Obtener el metodo
            regla.soy_error = true;

            arch.regla_error = regla;
            arch.reglas.Add(regla);
        }

        //Guardo las reglas
        private void guardarRegla(ParseTreeNode nodo)
        {
            //Validar que no exista ya en conjuntos de archivo
            String id = nodo.ChildNodes[0].Token.Text;
            if (arch.existeRegla(id))
            {
                AstNode ad = (AstNode)nodo.AstNode;
                arch.guardarError(id, ad.Location.Line, ad.Location.Column, "Semantico", "Regla con id ya existe");
                return;
            }

            //Obtener nombre de regla
            //ParseTreeNode h0 = nodo.ChildNodes[1];
            Regla regla = new Regla(id, arch.reglas.Count);

            //Obtener el AFN que tendra
            ParseTreeNode h1 = nodo.ChildNodes[1];
            regla.afn = this.obtenerAFN(h1);
            regla.afn.end.aceptacion = true;

            //Obtener el metodo
            ParseTreeNode h2 = nodo.ChildNodes[2];
            regla.metodo = this.obtenerMetodo(h2);

            //Si tiene 4 hijos, el 4to hijo, guarda todas las reservadas
            if (nodo.ChildNodes.Count == 4)
            {
                ParseTreeNode h3 = nodo.ChildNodes[3];
                foreach (ParseTreeNode item in h3.ChildNodes)
                {
                    String name_reservada = item.ChildNodes[1].Token.Text;
                    if (regla.reservadas.ContainsKey(name_reservada))
                    {
                        arch.guardarError(name_reservada, h3.Token.Location.Line, h3.Token.Location.Column, "Semantico", "id de palabra reservada ya existe");
                        continue;
                    }
                    String valor_lexema = item.ChildNodes[0].Token.Text.Replace("\"", "");
                    regla.reservadas.Add(name_reservada, valor_lexema);
                }
            }

            arch.reglas.Add(regla);
        }

        //Obtengo el metodo que ha de aplicar x Regla
        private Metodo obtenerMetodo(ParseTreeNode nodo)
        {
            if (nodo.Term.Name.Equals("M2"))
            {
                Metodo meto = new Metodo(nodo.ChildNodes[0].Token.Text, nodo.ChildNodes[2].Term.Name);
                return meto;
            }
            Metodo metos = new Metodo(nodo.ChildNodes[0].Token.Text);
            return metos;
        }

        #endregion


        #region "Conjuntos"

        private void guardarConjunto(ParseTreeNode nodo)
        {
            //Validar que no exista ya en conjuntos de archivo
            String id = nodo.ChildNodes[0].Token.Text;
            if (arch.conjuntos.ContainsKey(id))
            {
                AstNode ad = (AstNode)nodo.AstNode;
                arch.guardarError(id, ad.Location.Line, ad.Location.Column, "Semantico", "id ya existe");
                return;
            }

            //Crear el conjunto dependiendo su definicion
            ParseTreeNode h1 = nodo.ChildNodes[1];
            if (nodo.ChildNodes[1].Term.Name.Equals("SERIE"))
            {
                arch.conjuntos.Add(id, this.crearConjuntoSerie(id, h1));
            }
            else if (nodo.ChildNodes[1].Term.Name.Equals("LISTA"))
            {
                arch.conjuntos.Add(id, this.crearConjuntoLista(id, h1));
            }

            Console.WriteLine("Hola");
        }

        private Conjunto crearConjuntoSerie(String id, ParseTreeNode nodo)
        {
            //nodo = SERIE
            //h0 = inicio
            //h1 = fin
            String start = "0";
            String end = "8";
            ParseTreeNode h0 = nodo.ChildNodes[0];
            ParseTreeNode h1 = nodo.ChildNodes[1];
            switch (h0.Term.Name)
            {
                case "ascii":
                    start = h0.Token.Text;
                    break;

                case "numero":
                    if(h0.Token.Text.Length>1)
                        arch.guardarError(h0.Token.Text, h0.Token.Location.Line, h0.Token.Location.Column, "Semantico", "Numero con mas de 1 caracter");

                    start = h0.Token.Text[0].ToString();
                    break;

                case "tstring":
                    String ts = h0.Token.Text.Replace("\"", "");
                    if (ts.Length > 1)
                        arch.guardarError(ts, h0.Token.Location.Line, h0.Token.Location.Column, "Semantico", "Cadena con mas de 1 caracter");
                    start = ts[0].ToString();
                    break;

                case "tchar":
                    String tc = h0.Token.Text.Replace("'", "");
                    if (tc.Length > 1)
                        arch.guardarError(tc, h0.Token.Location.Line, h0.Token.Location.Column, "Sintactico", "Char solo es 1 caracter");
                    start = tc[0].ToString();
                    break;

                case "id":
                    if (h0.Token.Text.Length > 1)
                        arch.guardarError(h0.Token.Text, h0.Token.Location.Line, h0.Token.Location.Column, "Semantico", "CONJ: Serie no puede hacer referencia a un conjunto declarado");

                    start = h0.Token.Text[0].ToString();
                    break;

                default:
                    Console.WriteLine("! =============================> Case else guardarSerie");
                    break;
            }

            switch (h1.Term.Name)
            {
                case "ascii":
                    end = h1.Token.Text;
                    break;

                case "numero":
                    if (h1.Token.Text.Length > 1)
                        arch.guardarError(h1.Token.Text, h1.Token.Location.Line, h1.Token.Location.Column, "Semantico", "Numero con mas de 1 caracter");

                    end = h1.Token.Text[0].ToString();
                    break;

                case "tstring":
                    String ts = h1.Token.Text.Replace("\"", "");
                    if (ts.Length > 1)
                        arch.guardarError(ts, h1.Token.Location.Line, h1.Token.Location.Column, "Semantico", "Cadena con mas de 1 caracter");
                    end = ts[0].ToString();
                    break;

                case "tchar":
                    String tc = h1.Token.Text.Replace("'", "");
                    if (tc.Length > 1)
                        arch.guardarError(tc, h1.Token.Location.Line, h1.Token.Location.Column, "Sintactico", "Char solo es 1 caracter");
                    end = tc[0].ToString();
                    break;

                case "id":
                    if (h1.Token.Text.Length > 1)
                        arch.guardarError(h1.Token.Text, h1.Token.Location.Line, h1.Token.Location.Column, "Semantico", "CONJ: Serie no puede hacer referencia a un conjunto declarado");
                    end = h1.Token.Text[0].ToString();
                    break;

                default:
                    Console.WriteLine("! =============================> Case else guardarSerie");
                    break;
            }
            //arch.conjuntos.Add(id, new Conjunto(id, start, end, arch.expandir));
            Conjunto c =  new Conjunto(id, start, end, arch.expandir);
            return c;
        }

        private Conjunto crearConjuntoLista(String id, ParseTreeNode nodo)
        {
            //nodo = LISTA
            //hijos = Lo que haya que ingresar en el diccinario de simbolos
            Conjunto conj = new Conjunto(id, 2, arch.expandir);
            foreach (ParseTreeNode h1 in nodo.ChildNodes)
            {
                String simbol = "";
                switch (h1.Term.Name)
                {
                    case "ascii":
                        simbol = h1.Token.Text;
                        if (!conj.lista.ContainsKey(simbol))
                        {
                            conj.lista.Add(simbol, simbol);
                        }
                        break;

                    case "numero":
                        if (h1.Token.Text.Length > 1)
                            arch.guardarError(h1.Token.Text, h1.Token.Location.Line, h1.Token.Location.Column, "Semantico", "Numero con mas de 1 caracter");

                        simbol = h1.Token.Text[0].ToString();
                        if (!conj.lista.ContainsKey(simbol))
                        {
                            conj.lista.Add(simbol, simbol);
                        }
                        break;

                    case "tstring":
                        String ts = h1.Token.Text.Replace("\"", "");
                        if (ts.Length > 1)
                            arch.guardarError(ts, h1.Token.Location.Line, h1.Token.Location.Column, "Semantico", "Cadena con mas de 1 caracter");
                        simbol = ts[0].ToString();
                        if (!conj.lista.ContainsKey(simbol))
                        {
                            conj.lista.Add(simbol, simbol);
                        }
                        break;

                    case "tchar":
                        String tc = h1.Token.Text.Replace("'", "");
                        if (tc.Length > 1)
                            arch.guardarError(tc, h1.Token.Location.Line, h1.Token.Location.Column, "Sintactico", "Char solo es 1 caracter");
                        simbol = tc[0].ToString();
                        if (!conj.lista.ContainsKey(simbol))
                        {
                            conj.lista.Add(simbol, simbol);
                        }
                        break;

                    case "id":
                        //validar que exista un conjunto con nombre = id.lexval()
                        String buscado = h1.Token.Text;
                        Conjunto recurso = arch.getConjunto(buscado);
                        if (recurso == null)
                        {
                            arch.guardarError(buscado, h1.Token.Location.Line, h1.Token.Location.Column, "Semantico", "Conjunto no existe");
                        }
                        else
                        {
                            //Recorrer la lista del conjunto recurso y agregarlo a la lista del nuevo conjuntos
                            foreach (String item in recurso.lista.Values)
                            {
                                if (!conj.lista.ContainsKey(item))
                                {
                                    conj.lista.Add(item, item);
                                }
                            }
                        }                        
                        break;

                    case "SERIE":
                        //Obtener un conjunto serie
                        //Del conjunto serie,sacar la lista de simbolos
                        //Agregar la lista de simbolos al conjunto conj
                        Conjunto resource = this.crearConjuntoSerie("solicitado", h1);
                        foreach (String item in resource.lista.Values)
                        {
                            if (!conj.lista.ContainsKey(item))
                            {
                                conj.lista.Add(item, item);
                            }
                        }
                        break;

                    default:
                        Console.WriteLine("! =============================> Case else guardarLista");
                        break;
                }
            }
            return conj;
        }

        #endregion


        #region "Construir AFN"

        private Automata obtenerAFN(ParseTreeNode nodo)
        {
            if(nodo.ChildNodes.Count == 3)
            {                
                //Es un and, or (|, . )
                ParseTreeNode h0 = nodo.ChildNodes[0];
                ParseTreeNode h1 = nodo.ChildNodes[1];
                ParseTreeNode h2 = nodo.ChildNodes[2];
                if(h2.Token.Text.Equals("."))   //es un and
                {
                    return this.obtenerAFN_and(h0, h1, h2);                    
                }
                else //Es un or
                {
                    return this.obtenerAFN_or(h0, h1, h2);
                }
            }
            else if(nodo.ChildNodes.Count==2)
            {
                ParseTreeNode h0 = nodo.ChildNodes[0];
                ParseTreeNode h1 = nodo.ChildNodes[1];
                if(h1.Token.Text.Equals("*"))
                {
                    return this.obtenerAFN_nadamuchos(h0, h1);
                }
                else if(h1.Token.Text.Equals("+"))
                {
                    return this.obtenerAFN_muchos(h0, h1);
                }
                if (h1.Token.Text.Equals("?"))
                {
                    return this.obtenerAFN_nada(h0, h1);
                }
            }
            else if(nodo.ChildNodes.Count==1)
            {
                ParseTreeNode h0 = nodo.ChildNodes[0];
                if(h0.Term.Name.Equals("ESCAPE"))
                {
                    return obtenerAFN_escape(h0);
                }
                else if(h0.Term.Name.Equals("id"))
                {
                    return obtenerAFN_id(h0);
                }
                else if (h0.Term.Name.Equals("numero"))
                {
                    return obtenerAFN_numero(h0);
                }
                else if (h0.Term.Name.Equals("tstring"))
                {
                    return obtenerAFN_string(h0);
                }
                else if (h0.Term.Name.Equals("tchar"))
                {
                    return obtenerAFN_char(h0);
                }
                else
                {
                    Nodo n = new Nodo();
                    Transicion t = new Transicion("ELSE", n, n);
                    n.transiciones.Add(t);
                    Automata afn = new Automata(n, n);
                    return afn;
                }
            }

            return null;
        }

        private Automata obtenerAFN_or(ParseTreeNode h0, ParseTreeNode h1, ParseTreeNode h2)
        {
            Automata afn0 = this.obtenerAFN(h0);
            Automata afn1 = this.obtenerAFN(h1);

            Nodo start = new Nodo();
            Nodo end = new Nodo();

            Automata reto = new Automata(start, end);

            Transicion start_afn0 = new Transicion("ε", start, afn0.start);
            Transicion start_afn1 = new Transicion("ε", start, afn1.start);
            Transicion afn0_end = new Transicion("ε", afn0.end, end);
            Transicion afn1_end = new Transicion("ε", afn1.end, end);

            start.transiciones.Add(start_afn0);
            start.transiciones.Add(start_afn1);

            afn0.end.transiciones.Add(afn0_end);
            afn1.end.transiciones.Add(afn1_end);
            return reto;
        }

        private Automata obtenerAFN_and(ParseTreeNode h0, ParseTreeNode h1, ParseTreeNode h2)
        {
            //AFN afn0 = this.obtenerAFN(h0);
            //AFN afn1 = this.obtenerAFN(h1);
            //AFN reto = new AFN(afn0.start, afn1.end);
            //Transicion afn0_end_afn1_start = new Transicion("ε", afn0.end, afn1.start);
            //afn0.end.transiciones.Add(afn0_end_afn1_start);
            //return reto;

            Automata afn0 = this.obtenerAFN(h0);
            Automata afn1 = this.obtenerAFN(h1);

            //Hacer que todas las transiciones que salgan de S2
            //Ahora salgan de E1
            foreach (Transicion item in afn1.start.transiciones)
            {
                afn0.end.transiciones.Add(item);
            }

            //S1 A E1        S2 B E2                   S1 A E1 B E2

            return new Automata(afn0.start, afn1.end);
        }

        private Automata obtenerAFN_nadamuchos(ParseTreeNode h0, ParseTreeNode h1)
        {
            Automata sub_afn = this.obtenerAFN(h0);

            Nodo start = new Nodo();
            Nodo end = new Nodo();

            Automata reto = new Automata(start, end);

            Transicion e1 = new Transicion("ε", start, sub_afn.start);
            Transicion e2 = new Transicion("ε", sub_afn.end, sub_afn.start);
            Transicion e3   = new Transicion("ε", sub_afn.end, end);
            Transicion e4 = new Transicion("ε", start, end);

            start.transiciones.Add(e1);
            sub_afn.end.transiciones.Add(e2);
            sub_afn.end.transiciones.Add(e3);
            start.transiciones.Add(e4);
                                
            return reto;
        }

        private Automata obtenerAFN_muchos(ParseTreeNode h0, ParseTreeNode h1)
        {
            Automata sub_afn = this.obtenerAFN(h0);

            Nodo start = new Nodo();
            Nodo end = new Nodo();

            Automata reto = new Automata(start, end);

            Transicion e1 = new Transicion("ε", start, sub_afn.start);
            Transicion e2 = new Transicion("ε", sub_afn.end, sub_afn.start);
            Transicion e3 = new Transicion("ε", sub_afn.end, end);            

            start.transiciones.Add(e1);
            sub_afn.end.transiciones.Add(e2);
            sub_afn.end.transiciones.Add(e3);

            return reto;
        }

        private Automata obtenerAFN_nada(ParseTreeNode h0, ParseTreeNode h1)
        {
            Automata sub_afn = this.obtenerAFN(h0);

            Nodo start = new Nodo();
            Nodo end = new Nodo();

            Automata reto = new Automata(start, end);

            Transicion e1 = new Transicion("ε", start, sub_afn.start);
            //Transicion e2 = new Transicion("ε", sub_afn.end, sub_afn.start);
            Transicion e3 = new Transicion("ε", sub_afn.end, end);
            Transicion e4 = new Transicion("ε", start, end);

            start.transiciones.Add(e1);
            //sub_afn.end.transiciones.Add(e2);
            sub_afn.end.transiciones.Add(e3);
            start.transiciones.Add(e4);

            return reto;
        }

        private Automata obtenerAFN_id(ParseTreeNode h0)
        {
            String buscado = h0.Token.Text;

            //Buscar primero en los conjuntos
            Conjunto conjunto = arch.getConjunto(buscado);
            if(conjunto!=null)
                return conjunto.getAFND_Conjuntos();

            //Buscar segundo en las reglas
            Regla regla = arch.getRegla(buscado);
            if(regla!=null)
            {
                //Serializo el AFN de la regla y la retorno
                this.obtenerAFN_id_serializar(regla);
                return obtenerAFN_id_deserializar();                           
            }

            //Armar un And con todos los caracteres del id
            return obtenerAFN_caracteres(buscado);
        }

        private Automata obtenerAFN_numero(ParseTreeNode h0)
        {
            return this.obtenerAFN_caracteres(h0.Token.Text);
        }

        private Automata obtenerAFN_string(ParseTreeNode h0)
        {
            String data = h0.Token.Text.Replace("\"","");
            if(data.Length == 0)
            {
                arch.guardarError(h0.Token.Text, h0.Token.Location.Line, h0.Token.Location.Column, "Semantico", "String con 0 caracteres");
                return this.obtenerAFN_caracteres("0");
            }
            return this.obtenerAFN_caracteres(data);
        }

        private Automata obtenerAFN_char(ParseTreeNode h0)
        {
            String data = h0.Token.Text.Replace("'", "");
            if(data.Length == 0 || data.Length>1)
            {
                arch.guardarError(h0.Token.Text, h0.Token.Location.Line, h0.Token.Location.Column, "Semantico", "Char solo puede tener 1 caracter");
                return this.obtenerAFN_caracteres("0");
            }
            return this.obtenerAFN_caracteres(data);
        }

        private Automata obtenerAFN_escape(ParseTreeNode h0)
        {
            String name = "a";
            switch (h0.ChildNodes[0].Token.Text)
            {
                case "\\n":
                    name = "salto";
                    break;

                case "\\r":
                    name = "retorno";
                    break;

                case "\\t":
                    name = "tab";
                    break;

                case "\\'":
                    name = "comilla_s";
                    break;

                case "\\\"":
                    name = "comilla_d";
                    break;

                case "[:blanco:]":
                    //or entre espacio, tab, salto, retorno
                    Nodo ns1 = new Nodo();
                    Nodo ne1 = new Nodo();
                    Nodo ns2 = new Nodo();
                    Nodo ne2 = new Nodo();
                    Nodo ns3 = new Nodo();
                    Nodo ne3 = new Nodo();
                    Nodo ns4 = new Nodo();
                    Nodo ne4 = new Nodo();

                    ns1.transiciones.Add(new Transicion("espacio", ns1, ne1));
                    ns2.transiciones.Add(new Transicion("tab", ns2, ne2));
                    ns3.transiciones.Add(new Transicion("salto", ns3, ne3));
                    ns4.transiciones.Add(new Transicion("retorno", ns4, ne4));

                    Automata afn1 = new Automata(ns1, ne1);
                    Automata afn2 = new Automata(ns2, ne2);
                    Automata afn3 = new Automata(ns3, ne3);
                    Automata afn4 = new Automata(ns4, ne4);

                    Nodo start = new Nodo();
                    Nodo end = new Nodo();

                    start.transiciones.Add(new Transicion("ε", start, ns1));
                    start.transiciones.Add(new Transicion("ε", start, ns2));
                    start.transiciones.Add(new Transicion("ε", start, ns3));
                    start.transiciones.Add(new Transicion("ε", start, ns4));
                    ne1.transiciones.Add(new Transicion("ε", ne1, end));
                    ne2.transiciones.Add(new Transicion("ε", ne2, end));
                    ne3.transiciones.Add(new Transicion("ε", ne3, end));
                    ne4.transiciones.Add(new Transicion("ε", ne4, end));
                   
                    return new Automata(start, end);

                case "[:todo:]":
                    name = "todo";
                    break;

                default:
                    Console.WriteLine("abc");
                    Nodo n = new Nodo();
                    Transicion t = new Transicion("ESCAPE", n, n);
                    n.transiciones.Add(t);
                    Automata afn = new Automata(n, n);
                    return afn;
            }

            Nodo starts = new Nodo();
            Nodo ends = new Nodo();
            Transicion tra = new Transicion(name, starts, ends);
            starts.transiciones.Add(tra);
            return new Automata(starts, ends);
        }

        private void obtenerAFN_id_serializar(Regla regla)
        {
            FileStream flujo_archivo;
            BinaryFormatter formato_binario = new BinaryFormatter(); ;
            //Serializo el AFN
            flujo_archivo = File.Create("sourceRegla.bin");
            //Application.DoEvents();                       
            formato_binario.Serialize(flujo_archivo, regla.afn);
            //Thread.Sleep(5000);
            flujo_archivo.Close();
        }

        private Automata obtenerAFN_id_deserializar()
        {
            FileStream flujo_archivo;
            BinaryFormatter formato_binario = new BinaryFormatter();
            //DESERIALIZO el AFN
            flujo_archivo = File.OpenRead("sourceRegla.bin");
            Automata reto = (Automata)formato_binario.Deserialize(flujo_archivo);
            flujo_archivo.Close();
            return reto;
        }

        private Automata obtenerAFN_caracteres(String cadena)
        {
            //Armar un And con todos los caracteres de la cadena
            if (cadena.Length == 1)
            {
                //Solo es 1
                Nodo start = new Nodo();
                Nodo end = new Nodo();
                Transicion trans = new Transicion(cadena, start, end);
                start.transiciones.Add(trans);
                return new Automata(start, end);
            }
            else
            {
                Nodo start = new Nodo();
                Nodo end = new Nodo();
                for (int i = 0; i < cadena.Length; i++)
                {
                    if (i == 0)
                    {
                        Transicion trans = new Transicion(cadena[i].ToString(), start, end);
                        start.transiciones.Add(trans);
                    }
                    else
                    {
                        Nodo nuevo_end = new Nodo();
                        Transicion trans = new Transicion(cadena[i].ToString(), end, nuevo_end);
                        end.transiciones.Add(trans);
                        end = nuevo_end;
                    }
                }
                return new Automata(start, end);
            }
        }

        #endregion


        #region "Metodo Subconjuntos"

        private int contador;
        private void F2_A_MetodoSubconjuntos()
        {
            //------------------------> Enumerar Estados, Listas Simbolos
            this.listar_Estados_y_Simbolos();

            //------------------------> Graficar conjuntos
            this.graficarAFN_Conjuntos();
            
            //------------------------> Graficar Reglas
            this.graficarAFN_Reglas();

            //------------------------> Algoritmo Subconjuntos
            foreach (Regla regla in arch.reglas)
            {
                //Aplicar el Algoritmo de SubConjuntos
                algoritmoSubconjuntos(regla);

                //Armar el Resumen
                regla.resumirThompson();

                //Armar el AFD
                regla.armarAFD();
            }                        
            Console.WriteLine(":D");
        }

        private void listar_Estados_y_Simbolos()
        {
            foreach (Regla item in this.arch.reglas)
            {
                //Enumerar el AFN de C/regla
                contador = 0;
                //Llenar la lista de estados
                item.afn.listaEstados = new Dictionary<String, Nodo>();
                this.enumerarEstadosAFN(item.afn.start, item.afn.listaEstados);

                //LLenar la lista de simbolos
                //Eso se hace ya con la lista de estados
                //c/estado tiene n transiciones, c/transicion se mueve con x simbolo
                item.afn.listaSimbolos = new Dictionary<String, String>();
                foreach (Nodo estado in item.afn.listaEstados.Values)
                {
                    foreach (Transicion tra in estado.transiciones)
                    {
                        if (tra.simbolo.Equals("ε"))
                            continue;
                        if (item.afn.listaSimbolos.ContainsKey(tra.simbolo) == false)
                            item.afn.listaSimbolos.Add(tra.simbolo, tra.simbolo);
                    }
                }
            }
        }

        private void enumerarEstadosAFN(Nodo nodo, Dictionary<String, Nodo> dic)
        {
            if (nodo.num != -1)
                return;

            nodo.num = contador;
            contador++;
            dic.Add(nodo.num.ToString(), nodo);            

            foreach (Transicion item in nodo.transiciones)
            {
                //if (dic.ContainsKey(item.simbolo) == false)
                enumerarEstadosAFN(item.destino, dic);
            }
        }

        EstadoD estado_T;
        EstadoD estado_T_igual;
        private void algoritmoSubconjuntos(Regla regla)
        {
            //Crear la lista de estadosD -> de AF"D"
            //LinkedList<EstadoD> estados_D = regla.estadosD;
            List<EstadoD> estados_D = regla.estadosD;

            //Crear la particion 0 + cerraduraS
            //estados_D.AddFirst(this.crearEstado0(regla.afn));
            estados_D.Add(this.crearEstado0(regla.afn));

            while (hay_estado_T_sin_marcar(regla.estadosD))
            {
                estado_T.marcado = true;
                foreach (String simbolo_a in regla.afn.listaSimbolos.Values)
                {
                    //U =  ε_cerradura(mover(T,  a));
                    EstadoD U = e_cerradura_move_T_a(estado_T, simbolo_a, estados_D.Count);

                    //si U no esta en estadosD
                    if (esta_U_en_estadosD(U, estados_D)==false)
                    {
                        if (U.suconjunto.Count == 0)
                            continue;

                        //U = no marcado
                        U.marcado = false;

                        //Move[.....] = U
                        Move m;
                        estado_T.movimientos.TryGetValue(simbolo_a, out m);                        
                        m.particion = U;

                        //Meter U a estadosD
                        //estados_D.AddLast(U);
                        estados_D.Add(U);
                    }
                    else
                    {
                        Move m;
                        estado_T.movimientos.TryGetValue(simbolo_a, out m);
                        m.particion = estado_T_igual;
                        //Move[.....] = particion que ya esta en estadosD
                    }
                }
            }
            buscarAceptacion(estados_D, regla.afn.end.num);
        }

        private void buscarAceptacion(LinkedList<EstadoD> estados_D, int numero_end)
        {
            foreach (EstadoD estado in estados_D)
            {
                if (estado.suconjunto.ContainsKey(numero_end.ToString()))
                    estado.aceptacion = true;
            }            
        }

        private void buscarAceptacion(List<EstadoD> estados_D, int numero_end)
        {
            foreach (EstadoD estado in estados_D)
            {
                if (estado.suconjunto.ContainsKey(numero_end.ToString()))
                    estado.aceptacion = true;
            }
        }

        public Boolean esta_U_en_estadosD(EstadoD pu, LinkedList<EstadoD> estados_D)
        {
            //Ir por cada estado de estados_D
            //Comparar si ese estado == estado U
            //si son iguales retonar true y guardar en estado_T_igual = estado
            foreach (EstadoD estado in estados_D)
            {
                if (this.subconjuntosIguales(estado, pu))
                {
                    //Si existe un estado igual
                    this.estado_T_igual = estado;
                    return true;
                }
            }
            return false;
        }

        public Boolean esta_U_en_estadosD(EstadoD pu, List<EstadoD> estados_D)
        {
            //Ir por cada estado de estados_D
            //Comparar si ese estado == estado U
            //si son iguales retonar true y guardar en estado_T_igual = estado
            foreach (EstadoD estado in estados_D)
            {
                if (this.subconjuntosIguales(estado, pu))
                {
                    //Si existe un estado igual
                    this.estado_T_igual = estado;
                    return true;
                }
            }
            return false;
        }

        public Boolean subconjuntosIguales(EstadoD vs, EstadoD U)
        {
            if(U.suconjunto.Count == vs.suconjunto.Count)
            {
                foreach (var item in U.suconjunto.Keys)
                {
                    if (vs.suconjunto.ContainsKey(item) == false)
                        return false;
                }
                return true;
            }
            return false;
        }

        public EstadoD e_cerradura_move_T_a(EstadoD T, String a, int numero)
        {
            //Crear la particion U (Estado posible a agregar)
            EstadoD U = new EstadoD(numero);

            Move move = new Move(T, a);

            //Armando el Move[estado_T, simbolo_a] = {n1, n2, n3, ...., nn}
            foreach (Nodo nodo in T.suconjunto.Values)
            {
                foreach (Transicion transis in nodo.transiciones)
                {
                    String s = transis.simbolo;                 
                    if (s.Equals(a))
                    {
                        //Evalua si la transicion tiene el mismo simbolo
                        move.addTransicion(transis.destino);
                        continue;
                    }
                    if(s.Contains("[:")==true && s.Contains("~"))
                    {
                        //Evaluar series    //evaluar si contiene [:, ~, :]
                        String s2 = s.Replace("[", "").Replace(":", "").Replace("~", ",");
                        String[] t = s2.Split(',');
                        int iserie = this.toAscii(t[0]);
                        int fserie = this.toAscii(t[1]);
                        int sserie = this.toAscii(a);

                        if(sserie>=iserie && sserie<=fserie)
                        {
                            move.addTransicion(transis.destino);
                            continue;
                        }
                    }
                    if(a.Equals("salto") || a.Equals("retorno") || a.Equals("tab")|| a.Equals("espacio"))
                    {
                        if(s.Equals("blanco"))
                        {
                            move.addTransicion(transis.destino);
                            continue;
                        }
                    }
                    if (a.Equals("blanco"))
                    {
                        if (s.Equals("salto") || s.Equals("retorno") || s.Equals("tab") || s.Equals("espacio"))
                        {
                            move.addTransicion(transis.destino);
                            continue;
                        }
                    }
                    if(!a.Equals("salto") && !a.Equals("retorno"))
                    {
                        if(s.Equals("todo"))
                        {
                            move.addTransicion(transis.destino);
                            continue;
                        }
                    }
                }
            }

            if(T.movimientos.ContainsKey(a)==false)
                T.movimientos.Add(a,move);

            //U = particion con subconjunto que se llega con Move[.....]
            this.cerraduraT(move.listaTransiciones, U);

            return U;
        }

        /*
        public EstadoD e_cerradura_move_T_a(EstadoD T, String a, int numero)
        {
            //Crear la particion U (Estado posible a agregar)
            EstadoD U = new EstadoD(numero);

            Move move = new Move(T, a);

            //Armando el Move[estado_T, simbolo_a] = {n1, n2, n3, ...., nn}
            foreach (Nodo nodo in T.suconjunto.Values)
            {
                foreach (Transicion transis in nodo.transiciones)
                {
                    if (transis.simbolo.Equals(a))
                    {
                        move.addTransicion(transis.destino);
                    }
                }
            }

            if (T.movimientos.ContainsKey(a) == false)
                T.movimientos.Add(a, move);

            //U = particion con subconjunto que se llega con Move[.....]
            this.cerraduraT(move.listaTransiciones, U);

            return U;
        }
        */

        public Boolean hay_estado_T_sin_marcar(LinkedList<EstadoD> estadosD)
        {
            foreach (EstadoD item in estadosD)
            {
                if (item.marcado == false)
                {
                    this.estado_T = item;
                    return true;
                }
            }
            return false;
        }

        public Boolean hay_estado_T_sin_marcar(List<EstadoD> estadosD)
        {
            foreach (EstadoD item in estadosD)
            {
                if (item.marcado == false)
                {
                    this.estado_T = item;
                    return true;
                }
            }
            return false;
        }

        private EstadoD crearEstado0(Automata afn)
        {
            EstadoD p = new EstadoD(0);
            this.cerraduraS(afn, p);
            return p;
        }

        private void cerraduraS(Automata afn, EstadoD p)
        {
            //Crear la pila
            LinkedList<Nodo> pila = new LinkedList<Nodo>();
            //Agregar el estado 0 a la pila;
            pila.AddFirst(afn.start);
            //Agregar el estado 0 a el subconjunto
            p.suconjunto.Add(afn.start.num+"", afn.start);

            while (pila.Count!=0)
            {
                Nodo superior = pila.First.Value;
                pila.RemoveFirst();
                foreach (Transicion t in superior.transiciones)
                {
                    //Solo transiciones con ε
                    if(t.simbolo.Equals("ε"))
                    {
                        //Tiene que no estar en subconjunto el estado
                        if(p.suconjunto.ContainsKey(t.destino.num+"")==false)
                        {
                            p.suconjunto.Add(t.destino.num + "", t.destino);
                            pila.AddLast(t.destino);
                        }
                    }
                }
            }
        }

        private void cerraduraT(List<Nodo> estadosT,EstadoD p)
        {
            //Crear la pila
            LinkedList<Nodo> pila = new LinkedList<Nodo>();
            //Meter todos los estados T en la pila
            //Meter todos los estados T en suconjuntos de P
            foreach (Nodo item in estadosT)
            {
                pila.AddLast(item);
                if (!p.suconjunto.ContainsKey(item.num.ToString()))
                    p.suconjunto.Add(item.num.ToString(), item);
            }                        

            while (pila.Count != 0)
            {
                Nodo superior = pila.First.Value;
                pila.RemoveFirst();
                foreach (Transicion t in superior.transiciones)
                {
                    //Solo transiciones con ε
                    if (t.simbolo.Equals("ε"))
                    {
                        //Tiene que no estar en subconjunto el estado
                        if (p.suconjunto.ContainsKey(t.destino.num + "") == false)
                        {
                            p.suconjunto.Add(t.destino.num + "", t.destino);
                            pila.AddLast(t.destino);
                        }
                    }
                }
            }
        }

        #endregion





        public int toAscii(Char c)
        {
            return Encoding.ASCII.GetBytes(c.ToString())[0];
        }

        public int toAscii(String s)
        {
            return Encoding.ASCII.GetBytes(s)[0];
        }

        public String toSCaracter(int c)
        {
            return (Convert.ToChar(c)).ToString();
        }



    }
}

