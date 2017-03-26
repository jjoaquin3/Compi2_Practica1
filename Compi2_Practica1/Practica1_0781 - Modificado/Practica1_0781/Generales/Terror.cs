using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica1_0781.Generales
{
    [Serializable]
    public class Terror
    {
        public String simbolo, tipo, descripcion;
        public int linea, columna;

        public Terror() { }

        public Terror(String n, int l, int c, String t, String d)
        {
            this.simbolo = n;
            this.linea = l;
            this.columna = c;
            this.tipo = t;
            this.descripcion = d;
        }


    }
}
