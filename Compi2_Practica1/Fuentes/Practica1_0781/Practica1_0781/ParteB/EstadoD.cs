using Practica1_0781.Generales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica1_0781.ParteB
{
    [Serializable]
    public class EstadoD
    {
        public int num;
        public Dictionary<String, Nodo> suconjunto;
        public Boolean marcado;
        /// <summary>
        /// Key     = String        Es el simbolos de transicion
        /// Value   = Move          Guarda la info del movimiento con el simoblo de transicion
        /// </summary>
        public Dictionary<String, Move> movimientos;
        public Boolean aceptacion;
        public Nodo nodo;
        
        /// <summary>
        /// Key     = String        Es el simbolo de la transicion
        /// Value   = List<Nodo>    Es la lista de los nodos a donde se llega con el simbolo
        /// </summary>
        //public Dictionary<String, List<Nodo>> Transiciones;

        public EstadoD()
        {
            marcado = false;
            this.suconjunto = new Dictionary<String, Nodo>();
            this.movimientos = new Dictionary<String, Move>();
            this.aceptacion = false;
            //Transiciones = new Dictionary<String, List<Nodo>>();
        }

        public EstadoD(int n)
        {
            marcado = false;
            this.num = n;
            this.suconjunto = new Dictionary<String, Nodo>();
            this.movimientos = new Dictionary<String, Move>();
            this.aceptacion = false;
            //Transiciones = new Dictionary<String, List<Nodo>>();
        }



    }
}
