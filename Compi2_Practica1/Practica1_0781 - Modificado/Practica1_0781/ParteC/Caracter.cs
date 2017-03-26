using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica1_0781.ParteC
{
    [Serializable]
    public class Caracter
    {
        public String caracter;
        public int linea, columna;

        public Caracter(String carac, int l, int c)
        {
            this.caracter = carac;
            this.linea = l;
            this.columna = c;
        }
    }
}
