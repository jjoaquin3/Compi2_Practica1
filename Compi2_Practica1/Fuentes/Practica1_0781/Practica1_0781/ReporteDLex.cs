using Practica1_0781.Generales;
using Practica1_0781.ParteB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practica1_0781
{
    public partial class ReporteDLex : Form
    {
        public ReporteDLex(List<Regla> lista_reglas)
        {
            InitializeComponent();
            desktop  = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            r_afn = desktop + "\\Files\\AFND\\";
            r_afd = desktop + "\\Files\\AFD\\";

            foreach (Regla rrr in lista_reglas)
            {
                TabPage ttt = new TabPage(rrr.nombre);
                TabControl los4 = this.crearSuperTabControl(rrr);
                ttt.Controls.Add(los4);
                this.tc.TabPages.Add(ttt);
            }
        }

        public TabControl crearSuperTabControl(Regla rule)
        {
            TabPage tabla = this.crearTab_tabla(rule);
            TabPage afn = this.crearTab_afn(rule);
            TabPage afd = this.crearTab_afd(rule);
            TabPage operaciones = this.crearTab_resumen(rule);

            //TabPage st = new TabPage(rule.nombre);
            TabControl st = new TabControl();
            st.Dock = DockStyle.Fill;
            st.TabPages.Add(tabla);
            st.TabPages.Add(afn);
            st.TabPages.Add(afd);
            st.TabPages.Add(operaciones);
            return st;
        }

        public String desktop, r_afn, r_afd;
        public TabPage crearTab_tabla(Regla r)
        {
            TabPage t = new TabPage("Tabla");
            DataGridView dgv = new DataGridView();
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToOrderColumns = true;
            dgv.ReadOnly = true;
            dgv.Dock = System.Windows.Forms.DockStyle.Fill;

            //1 = estado
            //n = simbolos
            //n+1= total columnas
            dgv.Columns.Add(new DataGridViewColumn());
            dgv.Columns[0].Name = "Estado";
            dgv.Columns[0].CellTemplate = new DataGridViewTextBoxCell();
            dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[0].DefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Italic);
            //columT.CellTemplate = cell;
            //columT.Name = "TRABAJADOR";

            List<String> ss = new List<String>();
            foreach (String item in r.afn.listaSimbolos.Values)
            {
                DataGridViewColumn col = new DataGridViewColumn();
                col.Name = item;                
                col.CellTemplate = new DataGridViewTextBoxCell();
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                ss.Add(item);
                dgv.Columns.Add(col);
            }
            Console.WriteLine(dgv.Columns.Count);
            foreach (EstadoD estado in r.estadosD)
            {
                String[] fila = new String[ss.Count + 1];
                for (int i = 0; i < fila.Length; i++)
                {
                    fila[i] = "-1";
                }

                if (estado.aceptacion)
                    fila[0] = "S" + estado.num+" - Aceptacion";
                else
                    fila[0] = "S" + estado.num;

                int contador = 1;
                foreach (String simbol in ss)
                {
                    //Buscar el movimiento con ese simbolo
                    if(estado.movimientos.ContainsKey(simbol))
                    {
                        Move mm;
                        estado.movimientos.TryGetValue(simbol, out mm);
                        if (mm.particion != null)
                            fila[contador] = "S" + mm.particion.num;
                        else
                            fila[contador] = "error";
                    }
                    contador++;
                }
                dgv.Rows.Add(fila);
                //dgv.Rows.Insert()
            }

            t.Controls.Add(dgv);
            return t;
        }

        public TabPage crearTab_afn(Regla r)
        {
            Panel panel = new Panel();
            panel.Dock = DockStyle.Fill;
            PictureBox foto = new PictureBox();
            String ruta = this.r_afn + r.ruta_relativa+".png";
            Console.WriteLine(ruta);
            if (File.Exists(ruta))
            {
                foto.Image = Image.FromFile(ruta);
                foto.Dock = DockStyle.Fill;
                panel.Controls.Add(foto);
                panel.AutoScrollMinSize = foto.Image.Size;
                foto.SizeMode = PictureBoxSizeMode.CenterImage;
            }
            panel.AutoScroll = true;

            TabPage t = new TabPage("AFN");
            t.Controls.Add(panel);
            return t;
        }

        public TabPage crearTab_afd(Regla r)
        {
            Panel panel = new Panel();
            panel.Dock = DockStyle.Fill;
            PictureBox foto = new PictureBox();
            String ruta = this.r_afd + r.ruta_relativa + ".png";
            ;
            if (File.Exists(ruta))
            {
                foto.Image = Image.FromFile(ruta);
                foto.Dock = DockStyle.Fill;
                panel.Controls.Add(foto);
                panel.AutoScrollMinSize = foto.Image.Size;
                foto.SizeMode = PictureBoxSizeMode.CenterImage;
            }
            panel.AutoScroll = true;

            TabPage t = new TabPage("AFD");
            t.Controls.Add(panel);
            return t;
        }
                
        public TabPage crearTab_resumen(Regla r)
        {
            DataGridView dgv = new DataGridView();
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToOrderColumns = true;
            dgv.ReadOnly = true;
            dgv.Dock = System.Windows.Forms.DockStyle.Fill;

            dgv.Columns.Add("cestado", "Estado");
            dgv.Columns[0].DefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Italic);
            dgv.Columns.Add("ccerradura", "Cerradura");
            dgv.Columns.Add("ccsubconjuntos", "Subconjunto");
            dgv.Columns.Add("ccmovimientos", "Movimientos");            
            dgv.Columns[3].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgv.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            foreach (Thompson item in r.resumen_subconjuntos)
            {
                dgv.Rows.Add(item.estado, item.cadena, item.subconjuntos, item.movimientos);
            }

            TabPage t = new TabPage("Operaciones");
            t.Controls.Add(dgv);
            return t;
        }

    }
}
