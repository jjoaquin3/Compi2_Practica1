using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica1_0781.Generales
{
    [Serializable]
    public class Metodo
    {
        public int nivel; //1 = solo token, 2 = token, valor, tipo
        public int itipo;
        public String stipo;
        public String nombre_token;
        public String valor;
        //public Object lexema;

        public Metodo()
        {

        }

        //Tipo 2
        public Metodo(String id_tok, String stip)
        {
            this.nombre_token = id_tok;
            this.stipo = stip;
            this.nivel = 2;            
        }

        //Tipo 1
        public Metodo(String id_tok)
        {
            this.nombre_token = id_tok;
            this.stipo = "token";
            this.nivel = 1;
        }


    }
}
