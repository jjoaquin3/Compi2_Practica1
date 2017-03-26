using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica1_0781.Generales
{
    [Serializable]
    public class Token
    {
        public String nombre, tipo;
        public int linea, columna;
        public Object valor;

        public Token() { }

    }
}
