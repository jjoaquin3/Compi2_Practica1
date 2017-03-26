using Practica1_0781.ParteC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practica1_0781
{
    public partial class Reportes : Form
    {
        List<TokenD> lista_tokens;
        public Reportes(List<TokenD> lista)
        {
            InitializeComponent();

            this.lista_tokens = lista;

            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToOrderColumns = true;
            dgv.ReadOnly = true;

            dgv.Columns.Add(new DataGridViewColumn());
            dgv.Columns.Add(new DataGridViewColumn());
            dgv.Columns.Add(new DataGridViewColumn());
            dgv.Columns.Add(new DataGridViewColumn());
            dgv.Columns.Add(new DataGridViewColumn());

            dgv.Columns[0].Name = "Token";
            dgv.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[0].DefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Italic);
            dgv.Columns[1].Name = "Valor (Lexema)";            
            dgv.Columns[2].Name = "Tipo";
            dgv.Columns[3].Name = "Linea";
            dgv.Columns[4].Name = "Columna";

            dgv.Columns[0].CellTemplate = new DataGridViewTextBoxCell();
            dgv.Columns[1].CellTemplate = new DataGridViewTextBoxCell();
            dgv.Columns[2].CellTemplate = new DataGridViewTextBoxCell();
            dgv.Columns[3].CellTemplate = new DataGridViewTextBoxCell();
            dgv.Columns[4].CellTemplate = new DataGridViewTextBoxCell();

            dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            
            dgv.BackgroundColor = Color.Honeydew;

            foreach (TokenD item in lista)
            {
                this.dgv.Rows.Add(item.nombre_token, item.valor, item.tipo, item.linea, item.columna);
            }

        }

        //desktop  = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)+"\\Files\\";
          //  r_afn = desktop + "\\Files\\AFND\\";
//            r_afd = desktop + "\\Files\\AFD\\";
        private void exportarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder xml = new StringBuilder();

            xml.Append("<tokens>\n");

            foreach (TokenD item in this.lista_tokens)
            {
                xml.Append("    <token>\n");
                xml.Append("        <nombre>\n");
                xml.Append("            ");
                xml.Append(item.nombre_token+"\n");
                xml.Append("        </nombre>\n\n");

                xml.Append("        <tipo>\n");
                xml.Append("         ");
                xml.Append(item.tipo + "\n");
                xml.Append("        </tipo>\n\n");

                xml.Append("        <valor>\n");
                xml.Append("            ");
                xml.Append(item.valor + "\n");
                xml.Append("        </valor>\n\n");

                xml.Append("        <yyline>\n");
                xml.Append("            ");
                xml.Append(item.linea + "\n");
                xml.Append("        </yyline>\n\n");

                xml.Append("        <yyrow>\n");
                xml.Append("            ");
                xml.Append(item.columna + "\n");
                xml.Append("        </yyrow>\n\n");
                xml.Append("    </token>\n");
            }
            xml.Append("</tokens>");

            SaveFileDialog dg = new SaveFileDialog();
            dg.Title = "Guardar Archivo de Tokens XML";
            dg.Filter = "Archivo DLex  (*.xml)|*.xml|Todos los archivos (*.*)|*.*";
            dg.CheckFileExists = false;
            dg.OverwritePrompt = true;
            dg.ShowDialog();

            if (dg.FileName.Length != 0)
            {
                System.IO.File.WriteAllText(dg.FileName, xml.ToString());
            }
        }
    }
}
