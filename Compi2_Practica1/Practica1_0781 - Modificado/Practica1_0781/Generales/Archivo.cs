using Practica1_0781.ParteA;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Practica1_0781.Generales
{
    [Serializable]
    public class Archivo
    {
        public int indice;
        public Boolean es_nuevo;
        public String ruta, nombre;
        //public ScintillaNET.Scintilla textbox;
        public RichTextBox textbox;
        public List<Token> tokens;
        public List<Terror> errores;
        //public List<Terror> errores_dlex;
        public Dictionary<String, Conjunto> conjuntos;
        public List<Regla> reglas;        
        public Boolean expandir;
        public Regla regla_error;

        #region "Basicos"

        //public Archivo()
        //{
        //    this.indice = 0;
        //    this.es_nuevo = true;
        //    this.textbox = new ScintillaNET.Scintilla();
        //    this.textbox.Dock = System.Windows.Forms.DockStyle.Fill;
        //    this.textbox.Lexing.Lexer = ScintillaNET.Lexer.Cpp;
        //    this.textbox.Margins[0].Width = 25;
        //    this.textbox.Styles[this.textbox.Lexing.StyleNameMap["IDENTIFIER"]].ForeColor = System.Drawing.Color.Black;
        //    this.textbox.Lexing.Keywords[0] = "CONJ RESERV retorno error yytext yyline yyrow int float string char bool";
        //    this.textbox.Styles[this.textbox.Lexing.StyleNameMap["WORD"]].ForeColor = System.Drawing.Color.Blue;
        //    this.textbox.Styles[this.textbox.Lexing.StyleNameMap["STRING"]].ForeColor = System.Drawing.Color.Orange;
        //    this.textbox.Styles[this.textbox.Lexing.StyleNameMap["CHARACTER"]].ForeColor = System.Drawing.Color.DarkOrange;
        //    this.textbox.Styles[this.textbox.Lexing.StyleNameMap["NUMBER"]].ForeColor = System.Drawing.Color.DarkRed;
        //    this.textbox.Styles[this.textbox.Lexing.StyleNameMap["OPERATOR"]].ForeColor = System.Drawing.Color.Purple;
        //    this.textbox.Styles[this.textbox.Lexing.StyleNameMap["DOCUMENT_DEFAULT"]].ForeColor = System.Drawing.Color.Black;
        //    this.Reiniciar();
        //}

        public Archivo()
        {
            this.indice = 0;
            this.es_nuevo = true;
            this.textbox = new RichTextBox();
            this.textbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Reiniciar();
        }

        public void Reiniciar()
        {
            tokens = new List<Token>();
            errores = new List<Terror>();
            //errores_dlex = new List<Terror>();
            conjuntos = new Dictionary<String, Conjunto>();
            reglas = new List<Regla>();
            expandir = false;
            regla_error = null;
        }

        public void Guardar()
        {
            String s1 = "Guardar Cambios";
            if(MessageBox.Show(s1, "DLex", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                return;

            if (es_nuevo==false)//Si ya tiene nombre y ruta
            {
                MessageBox.Show("Guardando Archivo: " + this.nombre);
                System.IO.File.WriteAllText(this.ruta, textbox.Text);
                return;
            }

            SaveFileDialog dg = new SaveFileDialog();
            dg.Title = "Guardar Archivo DLex";
            dg.Filter = "Archivo DLex  (*.dx)|*.dx|Todos los archivos (*.*)|*.*";
            dg.CheckFileExists = false;
            dg.OverwritePrompt = true;
            dg.ShowDialog();

            if(dg.FileName.Length!=0)
            {
                System.IO.File.WriteAllText(dg.FileName, textbox.Text);
                this.ruta = dg.FileName;
                this.nombre = System.IO.Path.GetFileName(this.ruta);
                this.es_nuevo = false;
            }
        }

        #endregion


        public void saveToken()
        {

        }


        #region "Manejo de Errores ~ Reporte"

        public void guardarError(String token, int l, int c, String t, String d)
        {
            this.errores.Add(new Terror(token, l, c, t, d));
        }

        public void desplegarErrores()
        {
            if (this.errores.Count == 0)
                return;

            StringBuilder html = new StringBuilder();
            html.Append(this.getHeader("Reporte de Errores"));

            html.Append("<tr>\n");
            html.Append("<td style=\"text-align:center\"><span style=\"font-family:arial,helvetica,sans-serif\"><strong>Simbolo</strong></span></td>\n");
            html.Append("<td style=\"text-align:center\"><span style=\"font-family:arial,helvetica,sans-serif\"><strong>Linea</strong></span></td>\n");
            html.Append("<td style=\"text-align:center\"><span style=\"font-family:arial,helvetica,sans-serif\"><strong>Columna</strong></span></td>\n");
            html.Append("<td style=\"text-align:center\"><span style=\"font-family:arial,helvetica,sans-serif\"><strong>Tipo</strong></span></td>\n");
            html.Append("<td style=\"text-align:center\"><span style=\"font-family:arial,helvetica,sans-serif\"><strong>Descripcion</strong></span></td>\n");
            html.Append("</tr>\n");

            foreach (Terror item in this.errores)
            {
                html.Append("<tr>\n");
                html.Append("<td>"); html.Append(item.simbolo); html.Append("</td>\n");
                html.Append("<td>"); html.Append(item.linea); html.Append("</td>\n");
                html.Append("<td>"); html.Append(item.columna); html.Append("</td>\n");
                html.Append("<td>"); html.Append(item.tipo); html.Append("</td>\n");
                html.Append("<td>"); html.Append(item.descripcion); html.Append("</td>\n");
                html.Append("</tr>\n");
            }

            html.Append(this.getFoot());

            String r = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)+"\\Files\\Reportes\\errores.html";
            System.IO.File.WriteAllText(r, html.ToString());
            autoAbrir(r);
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
            html.Append("<big><strong>\n");html.Append(title);html.Append("</strong></big>\n");
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

        private void autoAbrir(String ruta)
        {
            try
            {
                System.Diagnostics.Process.Start(ruta);
            }
            catch (Exception ex)
            {
            }
        }

        #endregion


        #region "Manejo de Conjuntos"

        public Conjunto getConjunto(String name)
        {
            if(this.conjuntos.ContainsKey(name))
            {
                Conjunto reto;
                this.conjuntos.TryGetValue(name, out reto);
                return reto;
            }
            return null;
        }

        #endregion


        #region "Manejo de Reglas"

        public Boolean existeRegla(String name)
        {
            foreach (Regla item in this.reglas)
            {
                if(item.nombre.Equals(name))
                {
                    return true;
                }
            }
            return false;
        }

        public Regla getRegla(String name)
        {
            foreach (Regla item in this.reglas)
            {
                if (item.nombre.Equals(name))
                    return item;
            }
            return null;
        }

        #endregion

    }
}
