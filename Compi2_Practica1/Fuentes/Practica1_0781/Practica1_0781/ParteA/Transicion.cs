using Practica1_0781.Generales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica1_0781.ParteA
{
    [Serializable]
    public class Transicion
    {
        public Nodo origen;
        public Nodo destino;
        public String simbolo;

        public Transicion() { }

        public Transicion(String s, Nodo o, Nodo d)
        {
            this.simbolo = s;
            this.origen = o;
            this.destino = d;
        }

        

    }
}
