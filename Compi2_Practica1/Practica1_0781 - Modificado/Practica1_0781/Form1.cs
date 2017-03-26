using Practica1_0781.Generales;
using Practica1_0781.ParteA;
using System;
using System.Collections.Generic;
//using System.IO;
using System.Windows.Forms;

namespace Practica1_0781
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            listapestanias = new List<Archivo>();
            generarRutas();
            nuevo();
        }
        
        #region "Archivo"

        #region "Manejo de Archivos"

        private int contador = 0;
        private List<Archivo> listapestanias;

        private void generarRutas()
        {
            String desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            List<String> rutas = new List<String>();
            rutas.Add(desktop + "\\Files");
            rutas.Add(desktop + "\\Files\\Arbol");
            rutas.Add(desktop + "\\Files\\Reportes");
            rutas.Add(desktop + "\\Files\\AFND");
            rutas.Add(desktop + "\\Files\\AFD");
            foreach (String item in rutas)
            {
                if (!System.IO.Directory.Exists(item))
                {
                    System.IO.DirectoryInfo dir = System.IO.Directory.CreateDirectory(item);
                }
            }
        }

        private Archivo capturarArchivo()
        {
            int indice = tbprincipal.SelectedIndex;
            if (indice == 0)
                return null;

            foreach (Archivo item in this.listapestanias)
            {
                if (item.indice == indice)
                    return item;
            }
            return null;
        }

        private void nuevo()
        {
            Archivo a = new Archivo();
            TabPage nueva_pestania = new TabPage("New " + contador);
            tbprincipal.TabPages.Add(nueva_pestania);
            nueva_pestania.Controls.Add(a.textbox);
            a.indice = tbprincipal.TabCount - 1;
            tbprincipal.SelectedIndex = a.indice;
            listapestanias.Add(a);
            contador++;
        }

        private void abrir()
        {        
            OpenFileDialog abrir = new OpenFileDialog();
            abrir.Title = "Abrir archivod DLex";
            abrir.Filter = "Archivo DLex  (*.dx)|*.dx|Todos los archivos (*.*)|*.*";
            abrir.ShowDialog();

            if (abrir.FileName.Length == 0)
            {
                return;
            }

            System.IO.StreamReader sr = new System.IO.StreamReader(abrir.FileName, System.Text.Encoding.Default);
            String contenido = sr.ReadToEnd();
            sr.Close();

            Archivo a = new Archivo();
            TabPage tab_abrir = new TabPage(System.IO.Path.GetFileName(abrir.FileName));
            a.ruta = abrir.FileName;
            a.nombre = System.IO.Path.GetFileName(abrir.FileName);
            a.textbox.Text = contenido;
            a.es_nuevo = false;

            tab_abrir.Controls.Add(a.textbox);
            tab_abrir.Text = a.nombre;
            tbprincipal.TabPages.Add(tab_abrir);

            a.indice = tbprincipal.TabCount - 1;            
            tbprincipal.SelectedIndex = a.indice;

            listapestanias.Add(a);            
        }

        private void guardar()
        {
            Archivo a = this.capturarArchivo();
            if (a == null)
                return;

            a.Guardar();
            tbprincipal.TabPages[a.indice].Text = a.nombre;
        }

        private void cerrarArchivo()
        {            
            Archivo a = this.capturarArchivo();
            if (a == null)
                return;
            
            a.Guardar();

            int indice = tbprincipal.SelectedIndex;

            tbprincipal.TabPages.Remove(tbprincipal.SelectedTab);
            listapestanias.Remove(a);

            tbprincipal.SelectedIndex = indice - 1;            
            
            foreach (Archivo item in listapestanias)
            {
                if (item.indice >= indice)
                    item.indice = item.indice - 1;
            }            
        }

        private void buscar()
        {

        }

        private void salir()
        {
            //if (MessageBox.Show("Esta apunto de salir, continuar?", "DLex", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
                Application.Exit();
        }

        #endregion


        #region "Eventos Manejo de Archivos"

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.nuevo();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.abrir();
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.guardar();
        }

        private void buscarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.buscar();
        }

        private void cerrarArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.cerrarArchivo();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.salir();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Esta apunto de salir, continuar?", "DLex", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                e.Cancel = true;
        }

        #endregion

        #endregion


        #region "Analizador"

        private void cargar()
        {
            Archivo a = this.capturarArchivo();
            if (a == null)
                return;

            AnalizadorP1 parser = new AnalizadorP1();
            a.Reiniciar();
            parser.compilar(a.textbox.Text, a);
            if(a.errores.Count==0)
                MessageBox.Show("Carga Completa");
        }

        #endregion


        private void cargaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.cargar();
        }

        private void analizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Archivo a = this.capturarArchivo();
            if(a==null)
            {
                MessageBox.Show("No hay pestaña seleccionada", "DLex", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(a.reglas.Count==0)
            {
                MessageBox.Show("No hay reglas cargadas", "DLex", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Debugger d = new Debugger(a);
            //d.Show(this);
            d.Visible = true;
        }

        private void debuggerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Archivo a = this.capturarArchivo();
            if (a == null)
            {
                MessageBox.Show("No hay pestaña seleccionada", "DLex", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (a.reglas.Count == 0)
            {
                MessageBox.Show("No hay reglas cargadas", "DLex", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Debugger d = new Debugger(a);
            //d.Show(this);
            d.Visible = true;
        }

        private void tablaTokensToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void erroresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Archivo a = this.capturarArchivo();
            if (a == null)
            {
                MessageBox.Show("No hay pestaña seleccionada", "DLex", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            a.desplegarErrores();
        }
    }
}

