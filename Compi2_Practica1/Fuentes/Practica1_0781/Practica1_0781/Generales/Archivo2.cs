using Practica1_0781.ParteC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practica1_0781.Generales
{
    [Serializable]
    public class Archivo2
    {
        public int indice;
        public Boolean es_nuevo;
        public String ruta, nombre;
        //public ScintillaNET.Scintilla textbox;
        public RichTextBox textbox;
        public List<TokenD> tokens_lexicos;
        public List<Terror> errores_dlex;
        public List<Terror> errores_lexicos;

        //public Archivo2()
        //{
        //    this.indice = 0;
        //    this.es_nuevo = true;
        //    this.textbox = new ScintillaNET.Scintilla();
        //    this.textbox.Dock = System.Windows.Forms.DockStyle.Fill;
        //    this.textbox.Lexing.Lexer = ScintillaNET.Lexer.Cpp;
        //    this.textbox.Margins[0].Width = 25;
        //    this.Reiniciar();
        //}

        public Archivo2()
        {
            this.indice = 0;
            this.es_nuevo = true;
            this.textbox = new RichTextBox();
            this.textbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Reiniciar();
        }

        public void Reiniciar()
        {
            this.tokens_lexicos = new List<TokenD>();
            this.errores_dlex = new List<Terror>();
            this.errores_lexicos = new List<Terror>();            
        }

        public void Guardar()
        {
            String s1 = "Guardar Cambios";
            if (MessageBox.Show(s1, "DLex", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                return;

            if (es_nuevo == false)//Si ya tiene nombre y ruta
            {
                MessageBox.Show("Guardando Archivo: " + this.nombre);
                System.IO.File.WriteAllText(this.ruta, textbox.Text);
                return;
            }

            SaveFileDialog dg = new SaveFileDialog();
            dg.Title = "Guardar Archivo de Texto";
            dg.Filter = "Archivo DLex  (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            dg.CheckFileExists = false;
            dg.OverwritePrompt = true;
            dg.ShowDialog();

            if (dg.FileName.Length != 0)
            {
                System.IO.File.WriteAllText(dg.FileName, textbox.Text);
                this.ruta = dg.FileName;
                this.nombre = System.IO.Path.GetFileName(this.ruta);
                this.es_nuevo = false;
            }
        }

        public String getHeader(String title)
        {
            StringBuilder html = new StringBuilder();
            //<body style="background:#80BFFF">
            html.Append("<html><body style=\"background: #fef5e7 \">\n");
            html.Append("<table align=\"center\" border=\"1\" cellpadding=\"1\" cellspacing=\"1\" style=\"width:520px\">");
            html.Append("<caption>");
            html.Append("<br>\n<hr />");
            html.Append("<div style=\"background:#eee;border:1px solid #ccc;padding:5px 10px;\" > \n");
            html.Append("<span style=\"color:#c0392b\">\n");
            html.Append("<span style=\"font - family:lucida sans unicode, lucida grande, sans - serif\">\n");
            html.Append("<big><strong>\n"); html.Append(title); html.Append("</strong></big>\n");
            html.Append("</span>\n</span>\n</div>\n");
            html.Append("</caption>");
            html.Append("<tbody>");

            return html.ToString();
        }

        public String getFoot()
        {
            StringBuilder html = new StringBuilder();
            html.Append("</tbody>\n</table>\n");
            html.Append("<br>\n<hr />");
            //html.Append("<p style=\"text-align:Center\"><strong><span style=\"font-size:14px\">");
            //html.Append("<small>ByMarco Antonio Xocop Roquel | 201122934</small>\n");
            //html.Append("</span>\n</strong>\n</p>\n");
            html.Append("</body>\n</html>");

            return html.ToString();
        }


    }
}
