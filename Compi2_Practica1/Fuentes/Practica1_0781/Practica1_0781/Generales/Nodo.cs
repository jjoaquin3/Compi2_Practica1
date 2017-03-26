using Practica1_0781.ParteA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Practica1_0781.Generales
{
    [Serializable]
    public class Nodo
    {
        public String name;
        public int num;
        public List<Transicion> transiciones;
        public Boolean aceptacion;
        public Boolean enlazado;

        public Nodo()
        {
            transiciones = new List<Transicion>();
            num = -1;
            aceptacion = false;
            enlazado = false;
        }

        public Nodo(String n)
        {
            name = n.Replace("\"","");
            transiciones = new List<Transicion>();
            num = -1;
            aceptacion = false;
            enlazado = false;
        }

    }
}
