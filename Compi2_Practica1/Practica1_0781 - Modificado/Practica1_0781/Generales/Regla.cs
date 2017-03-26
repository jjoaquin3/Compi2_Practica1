using Practica1_0781.ParteA;
using Practica1_0781.ParteB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica1_0781.Generales
{
    [Serializable]
    public class Regla
    {
        public String nombre;       //nombre de la ER
        public int num;             //id de ER => posición en la lista de ERs
        public Automata afn;            //Automata Finito no Deterministico
        public Automata afd;            //Automata Finito Deterministico
        public Metodo metodo;           //Metodo asociado
        public Boolean soy_error;       //Es regla de error?
        public String ruta_relativa;    //Ruta donde se graficara su afn
        public Dictionary<String, String> reservadas;   //Si se separa en palabras reservadas (key=id token, value = lexema token)
        //public LinkedList<EstadoD> estadosD;          //Critico, lleva los estadosD es decir para el AFD
        public List<EstadoD> estadosD;          //Critico, lleva los estadosD es decir para el AFD
        public List<Thompson> resumen_subconjuntos;

        public Regla()
        {
            soy_error = false;
            reservadas = new Dictionary<String, String>();
            //estadosD = new LinkedList<EstadoD>();
            estadosD = new List<EstadoD>();
        }

        public Regla(String name, int n)
        {
            this.nombre = name;
            num = n;
            ruta_relativa = "Regla_" + num + "_" + name;
            soy_error = false;
            reservadas = new Dictionary<String, String>();
            //estadosD = new LinkedList<EstadoD>();
            estadosD = new List<EstadoD>();
        }

        public void armarAFD()
        {
            Dictionary<String, Nodo> listaEstados=new Dictionary<String, Nodo>();
            
            //Enumerar Nodos y marcar aceptacion
            foreach (EstadoD estado in this.estadosD)
            {
                estado.nodo = new Nodo("S"+estado.num.ToString());
                estado.nodo.num = estado.num;
                listaEstados.Add(estado.nodo.num.ToString(), estado.nodo);
                if (estado.aceptacion)
                    estado.nodo.aceptacion = true;
            }
                                
            //Enlazar
            foreach(EstadoD estado in this.estadosD)
            {
                foreach (Move movimiento in estado.movimientos.Values)
                {
                    if(movimiento.particion!=null)
                    {
                        Transicion tra = new Transicion(movimiento.simbolo, estado.nodo, movimiento.particion.nodo);
                        estado.nodo.transiciones.Add(tra);
                    }                    
                }
            }

            //Automata afd = new Automata(this.estadosD.First.Value.nodo, this.estadosD.Last.Value.nodo);
            Automata afd = new Automata(this.estadosD[0].nodo, this.estadosD[this.estadosD.Count - 1].nodo);
            afd.listaEstados = listaEstados;
            afd.listaSimbolos = afn.listaSimbolos;

            //Graficar
            Graficador g = new Graficador();
            //Regla_0_Str
            g.AFD_graficar(ruta_relativa, listaEstados);
        }

        public void resumirThompson()
        {
            this.resumen_subconjuntos = new List<Thompson>();

            foreach (EstadoD estado in this.estadosD)
            {
                Thompson t = new Thompson();
                t.setEstado(estado.num);
                t.setSubconjunto(estado.suconjunto);
                t.setMovimientos(estado.movimientos);
                this.resumen_subconjuntos.Add(t);
            }
        }

    }
}
