using Practica1_0781.Generales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica1_0781.ParteA
{
    [Serializable]
    public class Conjunto
    {
        public String nombre;
        public int tipo;
        public Boolean expandir;

        //Para Series: 0~9
        public int inicio, final;
        public String sinicio, sfinal;

        //Para Listas: a, b, c, Para Series
        public Dictionary<String, String> lista;

        public Conjunto()
        {
            this.expandir = false;
            this.lista = new Dictionary<String, String>();
        }

        public Conjunto(String nom, int ptipo, Boolean expand)
        {
            this.nombre = nom;
            this.tipo = ptipo;
            this.expandir = expand;
            this.lista = new Dictionary<String, String>();
        }

        public Conjunto(String nom, String si, String sf, Boolean exp)
        {
            this.nombre = nom;            
            this.lista = new Dictionary<String, String>();
            this.expandir = exp;
            this.tipo = 1;

            int ii = toAscii(si);
            int ff = toAscii(sf);

            if (ii == ff)
            {
                this.sinicio = si; this.inicio = toAscii(si);
                this.sfinal = sf; this.final = toAscii(sf);
                this.lista.Add(sinicio, sfinal);
            }
            else if (ii < ff)
            {
                this.sinicio = si; this.inicio = toAscii(si);
                this.sfinal = sf; this.final = toAscii(sf);
                for (int i = inicio; i <= final; i++)
                {
                    //lista.Add(toCaracter(i).ToString());
                    String ssave = toSCaracter(i);
                    if (this.lista.ContainsKey(ssave) == false)
                        this.lista.Add(ssave, ssave);
                }
            }
            else//final < inicio
            {
                this.sinicio = sf; this.inicio = toAscii(sf);
                this.sfinal = si; this.final = toAscii(si);
                for (int i = final; i <= inicio; i++)
                {
                    //lista.Add(toCaracter(i).ToString());
                    String ssave = toSCaracter(i);
                    if (this.lista.ContainsKey(ssave) == false)
                        this.lista.Add(ssave, ssave);
                }
            }                       
        }

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

        public Automata getAFND_Conjuntos()
        {
            if(this.expandir)
            {
                return this.getAFND_con_expandir();
            }
            return this.getAFND_sin_expandir();
        }

        public Automata getAFND_sin_expandir()
        {
            //Si tengo 1 simbolo
            if (this.lista.Count == 1)
            {
                Nodo start = new Nodo();
                Nodo end = new Nodo();
                foreach (String item in this.lista.Values)
                {
                    Transicion trans = new Transicion(item + "", start, end);
                    start.transiciones.Add(trans);
                }
                return new Automata(start, end);
                //return start;
            }
            else
            {
                if(this.tipo==1)
                {
                    //    Nodo start = new Nodo();
                    //    Nodo end = new Nodo();
                    //    Transicion trans = new Transicion("["+sinicio+"-"+sfinal+"]", start, end);
                    //    start.transiciones.Add(trans);
                    //    return start;
                    Nodo start_t1 = new Nodo();
                    Nodo end_t1 = new Nodo();
                    Transicion trans_t1 = new Transicion("[" + sinicio + "~" + sfinal + "]", start_t1, end_t1);
                    start_t1.transiciones.Add(trans_t1);

                    //Limpiar los simbolos
                    this.lista = new Dictionary<String, String>();
                    this.lista.Add("[" + sinicio + "~" + sfinal + "]", "[" + sinicio + "~" + sfinal + "]");
                    return new Automata(start_t1, end_t1);
                }

                //start = nodo de donde derivan todos
                //end = nodo en donde llegan todos
                //s1 = nodo de start de simbolo
                //s2 = nodo de end de simbolo
                //start [epsilon] s1 [SIMBOLO] s2 [epsilon] end
                Nodo start = new Nodo();
                Nodo end = new Nodo();

                foreach (String item in this.lista.Values)
                {
                    Nodo s1 = new Nodo();
                    Nodo s2 = new Nodo();
                    Transicion e1 = new Transicion("ε", start, s1);
                    Transicion e2 = new Transicion("ε", s2, end);
                    Transicion trans = new Transicion(item + "", s1, s2);
                    start.transiciones.Add(e1);
                    s1.transiciones.Add(trans);
                    s2.transiciones.Add(e2);
                }
                return new Automata(start, end);
                //return start;
            }
        }

        public Automata getAFND_con_expandir()
        {
            //Si tengo 1 simbolo
            if (this.lista.Count == 1)
            {
                Nodo start = new Nodo();
                Nodo end = new Nodo();
                foreach (String item in this.lista.Values)
                {
                    Transicion trans = new Transicion(item + "", start, end);
                    start.transiciones.Add(trans);
                }
                return new Automata(start, end);
                //return start;
            }
            else
            {
                //start = nodo de donde derivan todos
                //end = nodo en donde llegan todos
                //s1 = nodo de start de simbolo
                //s2 = nodo de end de simbolo
                //start [epsilon] s1 [SIMBOLO] s2 [epsilon] end
                Nodo start = new Nodo();
                Nodo end = new Nodo();

                foreach (String item in this.lista.Values)
                {
                    Nodo s1 = new Nodo();
                    Nodo s2 = new Nodo();
                    Transicion e1 = new Transicion("ε", start, s1);
                    Transicion e2 = new Transicion("ε", s2, end);
                    Transicion trans = new Transicion(item + "", s1, s2);
                    start.transiciones.Add(e1);
                    s1.transiciones.Add(trans);
                    s2.transiciones.Add(e2);
                }
                return new Automata(start, end);
                //return start;
            }
        }

        

        /*public Nodo getAFND2()
        {
            //Es una serie
            if(tipo==1)
            {
                //if(!expandir)
                //{
                //    Nodo start = new Nodo();
                //    Nodo end = new Nodo();
                //    Transicion trans = new Transicion("["+sinicio+"-"+sfinal+"]", start, end);
                //    start.transiciones.Add(trans);
                //    return start;
                //}
                //Asi grande
                //Si tengo 1 simbolo
                if(this.lista.Count==1)
                {
                    Nodo start = new Nodo();
                    Nodo end = new Nodo();
                    foreach (String item in this.lista.Values)
                    {
                        Transicion trans = new Transicion(item, start, end);
                        start.transiciones.Add(trans);
                    }
                    return start;
                }
                else
                {
                    //start = nodo de donde derivan todos
                    //end = nodo en donde llegan todos
                    //s1 = nodo de start de simbolo
                    //s2 = nodo de end de simbolo
                    //start [epsilon] s1 [SIMBOLO] s2 [epsilon] end
                    Nodo start = new Nodo();
                    Nodo end = new Nodo();

                    foreach (String item in this.lista.Values)
                    {
                        Nodo s1 = new Nodo();
                        Nodo s2 = new Nodo();
                        Transicion e1 = new Transicion("[:e:]", start, s1);
                        Transicion e2 = new Transicion("[:e:]", s2, end);
                        Transicion trans = new Transicion(item, s1, s2);
                    }
                    return start;
                }
            }
            else
            {

            }
            return null;
        }*/

    }
}
