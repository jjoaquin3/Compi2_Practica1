using Practica1_0781.Generales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica1_0781.ParteB
{
    [Serializable]
    public class Thompson
    {
        public String estado, cadena, subconjuntos, movimientos;

        public Thompson() { }

        public void setEstado(int number)
        {
            this.estado = "S" + number.ToString();
            this.cadena = "Cerradura(" + number.ToString() + ")";
        }

        public void setSubconjunto(Dictionary<String, Nodo> subconj)
        {
            StringBuilder t = new StringBuilder("{ ");
            Boolean primero=false;
            foreach (String item in subconj.Keys)
            {
                if(!primero)
                {
                    t.Append(item);
                    primero = true;
                    continue;
                }
                t.Append(", " + item);
            }
            t.Append(" }");
            subconjuntos = t.ToString();
        }

        public void setMovimientos(Dictionary<String, Move> m)
        {
            StringBuilder t = new StringBuilder();
            foreach (Move mov in m.Values)
            {                
                t.Append("Mover[" + estado + ", " + mov.simbolo + "] = ");
                if (mov.particion == null)
                {
                    t.Append("{ }");
                    t.Append(Environment.NewLine);
                    continue;
                }
                t.Append("{");
                Boolean primero = false;
                foreach (String item in mov.transiciones.Keys)
                {
                    if (!primero)
                    {
                        t.Append(item);
                        primero = true;
                        continue;
                    }
                    t.Append(", " + item);
                }
                t.Append("}");
                t.Append(" => S" + mov.particion.num);
                t.Append(Environment.NewLine);
            }
            movimientos = t.ToString();
        }

    }
}
