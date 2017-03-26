using Practica1_0781.Generales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica1_0781.ParteB
{
    [Serializable]
    public class Move
    {
        public String simbolo;
        /// <summary>
        /// Key     = String -> numero del nodo a donde llega
        /// Value   = Nodo ->   nodo
        /// </summary>
        public Dictionary<String, Nodo> transiciones;
        public List<Nodo> listaTransiciones;
        public EstadoD particion;

        public Move(EstadoD estado_t, String simbolo_a)
        {
            this.simbolo = simbolo_a;
            this.transiciones = new Dictionary<String, Nodo>();
            this.listaTransiciones = new List<Nodo>();
        }

        public void addTransicion(Nodo n)
        {
            if (this.transiciones.ContainsKey(n.num.ToString()) == true)
                return;
            this.transiciones.Add(n.num.ToString(), n);
            this.listaTransiciones.Add(n);
        }

    }
}
