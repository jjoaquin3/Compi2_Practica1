using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica1_0781.ParteC
{
    [Serializable]
    public class TokenD
    {        
        public String nombre_token, valor, tipo;
        public int linea, columna, itipo;
        //public Boolean es_reservada, aceptado, recorriendo;
        public Boolean aceptado;

        public TokenD()
        {
            this.nombre_token = "";this.valor = "";this.tipo = "";
        }

        public TokenD(int l, int c)
        {
            this.nombre_token = ""; this.valor = ""; this.tipo = "";
            this.linea = l;this.columna = c;
        }

        public void setPosicion(int l, int c)
        {
            this.linea = l;
            this.columna = c;
        }

        //Guardar reservada
        //public String html;
        public TokenD(String ntk, String value, int l, int c)
        {
            this.nombre_token = ntk;
            this.valor = value;
            this.tipo = ntk;
            //this.html = value;
            this.linea = l;
            this.columna = c;
        }

        //Guardar token con metodo = 2
        public TokenD(String ntk, String value, String tipo, int l, int c)
        {
            this.nombre_token = ntk;
            this.valor = value;
            this.tipo = tipo;
            this.linea = l;
            this.columna = c;
        }

    }
}
