using Practica1_0781.Generales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica1_0781.ParteA
{
    [Serializable]
    public class Automata
    {
        public Nodo start;
        public Nodo end;
        public Dictionary<String, Nodo> listaEstados;
        public Dictionary<String, String> listaSimbolos;

        public Automata() { }

        public Automata(Nodo s, Nodo e)
        {
            this.start = s;
            this.end = e;
        }


        
    }
}
