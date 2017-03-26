using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Irony.Parsing;
using Irony.Ast;
using Practica1_0781.ParteA;

namespace Practica1_0781.Generales
{
    [Serializable]
    public class Graficador
    {

        private String desktop;
        StringBuilder graphivz;
        private int contador;

        public Graficador()
        {
            desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            graphivz = new StringBuilder();
        }

        private void generarDOT_PNG(String rdot, String rpng)
        {
            System.IO.File.WriteAllText(rdot, graphivz.ToString());
            String comandodot = "dot.exe -Tpng " + rdot + " -o " + rpng + " ";
            var command = string.Format(comandodot);
            var procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/C" + command);
            var proc = new System.Diagnostics.Process();
            proc.StartInfo = procStartInfo;
            proc.Start();
            proc.WaitForExit();
        }

        private void autoAbrir(String ruta)
        {
            try
            {
                System.Diagnostics.Process.Start(ruta);
            }
            catch (Exception ex)
            {
            }
        }

        public void Arbol_graficar(ParseTree arbol)
        {
            graphivz.Clear();contador = 0;
            String rdot = desktop + "\\Files\\Arbol\\arbol.dot";
            String rpng = desktop + "\\Files\\Arbol\\arbol.png";
            graphivz.Append("digraph G {\r\n node[shape=doublecircle, style=filled, color=khaki1, fontcolor=black]; \r\n");
            Arbol_listar_enlazar(arbol.Root, contador);
            graphivz.Append("}");
            this.generarDOT_PNG(rdot, rpng);
            //this.autoAbrir(rpng);
        }

        private void Arbol_listar_enlazar(ParseTreeNode nodo, int num)
        {
            if (nodo != null)
            {
                //graphivz += "node" + num + " [ label = \"" + nodo.Term.ToString() + "\"];\r\n";
                graphivz.Append("node" + num + " [ label = \"" + nodo.Term.ToString() + "\"];\r\n");
                int tam = nodo.ChildNodes.Count;
                int actual;
                for (int i = 0; i < tam; i++)
                {
                    contador = contador + 1;
                    actual = contador;
                    Arbol_listar_enlazar(nodo.ChildNodes[i], contador);
                    //graphivz += "\"node" + num + "\"->\"node" + actual + "\";\r\n";
                    graphivz.Append("\"node" + num + "\"->\"node" + actual + "\";\r\n");
                }
            }
        }       

        public void AFND_graficar(String id, Dictionary<String, Nodo> dicc)
        {
            graphivz.Clear(); contador = 0;
            String rdot = desktop + "\\Files\\AFND\\" + id + ".dot";
            String rpng = desktop + "\\Files\\AFND\\" + id + ".png";
            graphivz.Append("digraph G {\r\n rankdir=LR\n node[shape=doublecircle, style=filled, color=khaki1, fontcolor=black]; \r\n");
            AFND_listar(dicc);
            AFND_enlazar(dicc);            
            graphivz.Append("}");
            this.generarDOT_PNG(rdot, rpng);
            //this.autoAbrir(rpng);
        }

        public void AFD_graficar(String id, Dictionary<String, Nodo> dicc)
        {
            graphivz.Clear(); contador = 0;
            String rdot = desktop + "\\Files\\AFD\\" + id + ".dot";
            String rpng = desktop + "\\Files\\AFD\\" + id + ".png";
            graphivz.Append("digraph G {\r\n rankdir=LR\n node[shape=doublecircle, style=filled, color=khaki1, fontcolor=black]; \r\n");
            AFND_listar(dicc);
            AFND_enlazar(dicc);
            graphivz.Append("}");
            this.generarDOT_PNG(rdot, rpng);
            //this.autoAbrir(rpng);
        }

        //private int contador = 0;
        //public void AFND_listar(Nodo nodo)
        //{
        //    if (nodo.num != -1)
        //        return;

        //    nodo.num = contador;
        //    contador++;
        //    graphivz.Append("node" + nodo.num.ToString() + "[label=\"s" + nodo.num.ToString() + "\"];\n");

        //    foreach (Transicion item in nodo.transiciones)
        //    {
        //        AFND_listar(item.destino);
        //    }
        //}

        public void AFND_listar(Dictionary<String, Nodo> lista_nodos)
        {
            foreach (Nodo nodo in lista_nodos.Values)
            {
                if(nodo.aceptacion)
                    graphivz.Append("node" + nodo.num.ToString() + "[label=\"s" + nodo.num.ToString() + "\", shape=doublecircle, color=limegreen];\n");
                else
                    graphivz.Append("node" + nodo.num.ToString() + "[label=\"s" + nodo.num.ToString() + "\"];\n");
            }
        }

        //public void AFND_enlazar(Nodo nodo)
        //{
        //    if (nodo.enlazado)
        //        return;

        //    foreach (Transicion item in nodo.transiciones)
        //    {
        //        graphivz.Append("\"node" + nodo.num + "\" ->");
        //        graphivz.Append("\"node" + item.destino.num + "\" ");
        //        String tempo = item.simbolo;
        //        if (tempo == "\\")
        //            tempo = "\\\\";
        //        else if (tempo == "\"")
        //            tempo = "\\\"";
        //        graphivz.Append("[label=\"" + tempo + "\"];\n");                                                                                                                                                                         
        //    }

        //    nodo.enlazado = true;

        //    foreach (Transicion item in nodo.transiciones)
        //    {
        //        AFND_enlazar(item.destino);
        //    }
        //}
        public void AFND_enlazar(Dictionary<String, Nodo> lista_nodos)
        {
            foreach (Nodo nodo in lista_nodos.Values)
            {
                foreach (Transicion item in nodo.transiciones)
                {
                    graphivz.Append("\"node" + nodo.num + "\" ->");
                    graphivz.Append("\"node" + item.destino.num + "\" ");
                    String tempo = item.simbolo;
                    if (tempo == "\\")
                        tempo = "\\\\";
                    else if (tempo == "\"")
                        tempo = "\\\"";
                    graphivz.Append("[label=\"" + tempo + "\"];\n");
                }
            }
        }







        public void AFND_graficar(String id, Nodo nodo)
        {
            graphivz.Clear(); contador = 0;
            String rdot = desktop + "\\Files\\AFND\\" + id + ".dot";
            String rpng = desktop + "\\Files\\AFND\\" + id + ".png";
            graphivz.Append("digraph G {\r\n rankdir=LR\n node[shape=doublecircle, style=filled, color=khaki1, fontcolor=black]; \r\n");
            //Sgrafo(arbol.Root, contador);            
            AFND_listar(nodo);
            AFND_enlazar(nodo);
            graphivz.Append("}");
            this.generarDOT_PNG(rdot, rpng);
            this.autoAbrir(rpng);
            AFND_reiniciarListar(nodo);
            AFND_reiniciarEnlazar(nodo);
        }

        public void AFND_reiniciarListar(Nodo nodo)
        {
            if (nodo.num == -1)
                return;

            //nodo.enlazado = false;
            nodo.num = -1;

            foreach (Transicion item in nodo.transiciones)
            {
                AFND_reiniciarListar(item.destino);
            }
        }

        public void AFND_reiniciarEnlazar(Nodo nodo)
        {
            if (!nodo.enlazado)
                return;

            nodo.enlazado = false;

            foreach (Transicion item in nodo.transiciones)
            {
                AFND_enlazar(item.destino);
            }
        }


        //private int contador = 0;
        public void AFND_listar(Nodo nodo)
        {
            if (nodo.num != -1)
                return;

            nodo.num = contador;
            contador++;
            graphivz.Append("node" + nodo.num.ToString() + "[label=\"s" + nodo.num.ToString() + "\"];\n");

            foreach (Transicion item in nodo.transiciones)
            {
                AFND_listar(item.destino);
            }
        }

        public void AFND_enlazar(Nodo nodo)
        {
            if (nodo.enlazado)
                return;

            foreach (Transicion item in nodo.transiciones)
            {
                graphivz.Append("\"node" + nodo.num + "\" ->");
                graphivz.Append("\"node" + item.destino.num + "\" ");
                String tempo = item.simbolo;
                if (tempo == "\\")
                    tempo = "\\\\";
                else if (tempo == "\"")
                    tempo = "\\\"";
                graphivz.Append("[label=\"" + tempo + "\"];\n");
            }

            nodo.enlazado = true;

            foreach (Transicion item in nodo.transiciones)
            {
                AFND_enlazar(item.destino);
            }
        }


    }
}

