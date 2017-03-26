using Practica1_0781.Generales;
using Practica1_0781.ParteB;
using Practica1_0781.ParteC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practica1_0781
{
    public partial class Debugger : Form
    {
        private int contador = 0;
        private List<Archivo2> listapestanias;
        public Archivo arch;

        public Debugger(Archivo a)
        {
            InitializeComponent();
            listapestanias = new List<Archivo2>();
            this.arch = a;
            nuevo();
            compilado = 1; segundos = 0; tiempo = 0;
            string_simbolo = "0";
        }

        private Archivo2 capturarArchivo()
        {
            int indice = tbprincipal.SelectedIndex;
            if (indice == 0)
                return null;

            foreach (Archivo2 item in this.listapestanias)
            {
                if (item.indice == indice)
                    return item;
            }
            return null;
        }

        private void nuevo()
        {
            Archivo2 a = new Archivo2();
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
            abrir.Title = "Abrir archivos de Texto";
            abrir.Filter = "Archivo DLex  (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            abrir.ShowDialog();

            if (abrir.FileName.Length == 0)
            {
                return;
            }

            System.IO.StreamReader sr = new System.IO.StreamReader(abrir.FileName, System.Text.Encoding.Default);
            String contenido = sr.ReadToEnd();
            sr.Close();

            Archivo2 a = new Archivo2();
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
            Archivo2 a = this.capturarArchivo();
            if (a == null)
                return;

            a.Guardar();
            tbprincipal.TabPages[a.indice].Text = a.nombre;
        }

        private void cerrarArchivo()
        {
            Archivo2 a = this.capturarArchivo();
            if (a == null)
                return;

            a.Guardar();

            int indice = tbprincipal.SelectedIndex;

            tbprincipal.TabPages.Remove(tbprincipal.SelectedTab);
            listapestanias.Remove(a);

            tbprincipal.SelectedIndex = indice - 1;

            foreach (Archivo2 item in listapestanias)
            {
                if (item.indice >= indice)
                    item.indice = item.indice - 1;
            }
        }

        private void salir()
        {
            //if (MessageBox.Show("Esta apunto de salir, continuar?", "DLex", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
            //Application.Exit();
            this.Close();
        }

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

        private void cerrarArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.cerrarArchivo();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.salir();
        }

        private void analizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Archivo2 a = capturarArchivo();
            if (a == null)
            {
                return;
            }
            this.a_activo = a;
            a.Reiniciar();
            this.pasarLexico(a.textbox);
        }

        private void debuggerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Archivo2 a = capturarArchivo();
            if (a == null)
            {
                return;
            }
            this.a_activo = a;
            a.Reiniciar();
        }

        private void tablaTokensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Archivo2 a = capturarArchivo();
            if (a == null)
            {
                return;
            }
            this.a_activo = a;
            Reportes re = new Reportes(a.tokens_lexicos);
            re.Visible = true;
        }

        private void erroresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.desplegarErrores();
        }

        private void desplegarErrores()
        {
            Archivo2 a = capturarArchivo();
            if (a == null)
            {
                return;
            }
            this.a_activo = a;
            StringBuilder html = new StringBuilder();
            html.Append(a.getHeader("Reporte de Errores Lexicos"));

            html.Append("<tr>\n");
            html.Append("<td style=\"text-align:center\"><span style=\"font-family:arial,helvetica,sans-serif\"><strong>Simbolo</strong></span></td>\n");
            html.Append("<td style=\"text-align:center\"><span style=\"font-family:arial,helvetica,sans-serif\"><strong>Linea</strong></span></td>\n");
            html.Append("<td style=\"text-align:center\"><span style=\"font-family:arial,helvetica,sans-serif\"><strong>Columna</strong></span></td>\n");
            html.Append("<td style=\"text-align:center\"><span style=\"font-family:arial,helvetica,sans-serif\"><strong>Tipo</strong></span></td>\n");
            html.Append("<td style=\"text-align:center\"><span style=\"font-family:arial,helvetica,sans-serif\"><strong>Descripcion</strong></span></td>\n");
            html.Append("</tr>\n");

            foreach (Terror item in a.errores_lexicos)
            {
                html.Append("<tr>\n");
                html.Append("<td>"); html.Append(item.simbolo); html.Append("</td>\n");
                html.Append("<td>"); html.Append(item.linea); html.Append("</td>\n");
                html.Append("<td>"); html.Append(item.columna); html.Append("</td>\n");
                html.Append("<td>"); html.Append(item.tipo); html.Append("</td>\n");
                html.Append("<td>"); html.Append(item.descripcion); html.Append("</td>\n");
                html.Append("</tr>\n");
            }

            html.Append(a.getFoot());

            String r = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\Files\\Reportes\\errores.html";
            System.IO.File.WriteAllText(r, html.ToString());
            autoAbrir(r);
        }

        private void todoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReporteDLex rdlex = new ReporteDLex(this.arch.reglas);
            rdlex.Visible = true;
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

        private void bnormal_Click(object sender, EventArgs e)
        {
            Archivo2 a = capturarArchivo();
            if(a==null)
            {
                return;
            }
            this.a_activo = a;
            a.Reiniciar();
            this.compilado = 1;
            this.pasarLexico(a.textbox);
            this.txtlog.Text = "";
        }

        private void bnext_Click(object sender, EventArgs e)
        {
            Archivo2 a = capturarArchivo();
            if (a == null)
                return;
            this.a_activo = a;
            a.Reiniciar();
            this.txtlog.Text = "";
        }

        public int compilado, tiempo, segundos;

        private void brun_Click(object sender, EventArgs e)
        {
            Console.Write(barra.Size);
            Archivo2 a = capturarArchivo();
            if (a == null)
                return;
            this.a_activo = a;
            a.Reiniciar();
            this.limpiarTabla();

            this.compilado = 2;
            this.segundos = barra.Value;
            this.tiempo = segundos * 100;
            MessageBox.Show("Analisis con tiempo: " + segundos);
            this.txtlog.Text = "";
            this.pasarLexico(a.textbox);               
        }
        Archivo2 a_activo;

        private void Debugger_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Esta apunto de salir, continuar?", "DLex", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                e.Cancel = true;
        }

        private void analizarLexico()
        {
            Archivo2 a = this.capturarArchivo();
            if(a==null)
            {
                MessageBox.Show("No hay pestania activa seleccionada", "DLex", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(arch.reglas.Count==0)
            {
                MessageBox.Show("No hay reglas cargadas (dx)", "DLex", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(arch.errores.Count>0)
            {
                MessageBox.Show("Existen errore, vuelva a cargar (dx)", "DLex", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


        }

        //private void pasarLexico(ScintillaNET.Scintilla textbox)
        //{
        //    Console.WriteLine("---------------------------------------------------------\nStart Lexico");

        //    MessageBox.Show("Start Lexer");

        //    #region "A. Separar por caracteres"            
        //    List<Caracter> lista = this.separarCaracteres(textbox);
        //    #endregion

        //    MessageBox.Show("Split Chars");

        //    #region "B. Moverse entre AFDs"
        //    this.moverme(lista);
        //    #endregion

        //    MessageBox.Show("End Lexer");

        //    if (this.a_activo.errores_lexicos.Count > 0)
        //        this.desplegarErrores();
        //}

        private void pasarLexico(RichTextBox textbox)
        {
            Console.WriteLine("---------------------------------------------------------\nStart Lexico");
            textbox.Text += "\n";
            textbox.Text = textbox.Text.Replace("\n", " \n");
            MessageBox.Show("Start Lexer");

            #region "A. Separar por caracteres"            
            List<Caracter> lista = this.separarCaracteres(textbox);
            #endregion

            MessageBox.Show("Split Chars");

            #region "B. Moverse entre AFDs"
            this.moverme(lista);
            #endregion

            MessageBox.Show("End Lexer");

            if (this.a_activo.errores_lexicos.Count > 0)
                this.desplegarErrores();
        }

        //private List<Caracter> separarCaracteres(ScintillaNET.Scintilla textbox)
        //{
        //    List < Caracter >  listacaracteres = new List<Caracter>();
        //    for (int i = 0; i < textbox.Lines.Count; i++)
        //    {
        //        if (textbox.Lines[i].ToString().Equals("") == false)
        //        {
        //            for (int j = 0; j < textbox.Lines[i].Length; j++)
        //            {
        //                listacaracteres.Add(new Caracter(textbox.Lines[i].Text[j].ToString(), i, j));
        //            }
        //        }
        //        else
        //            continue;
        //    }
        //    //listacaracteres.Add(new Caracter("\t", 0, 0));
        //    return listacaracteres;
        //}

        private List<Caracter> separarCaracteres(RichTextBox textbox)
        {
            List<Caracter> listacaracteres = new List<Caracter>();
            //for (int i = 0; i < textbox.Lines.Count; i++)
            for (int i = 0; i<textbox.Lines.Count();i++)
            {
                if (textbox.Lines[i].ToString().Equals("") == false)
                {
                    for (int j = 0; j < textbox.Lines[i].Length; j++)
                    {
                        //listacaracteres.Add(new Caracter(textbox.Lines[i].Text[j].ToString(), i, j));
                        listacaracteres.Add(new Caracter(textbox.Lines[i].ToCharArray()[j].ToString(), i, j));
                    }
                }
                else
                    continue;
            }
            //listacaracteres.Add(new Caracter("\t", 0, 0));
            return listacaracteres;
        }

        private void limpiarTabla()
        {
            //while (dgvtabla.RowCount > 1)
            //{

            //    dgvtabla.Rows.Remove(dgvtabla.CurrentRow);

            //}
            dgv.Rows.Clear();
            dgv.Columns.Clear();
        }

        private void llenarTabla(Regla r)
        {
            //DataGridView dgv = new DataGridView();
            //dgv = new DataGridView();
            tptabla.Text = r.nombre;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToOrderColumns = true;
            dgv.ReadOnly = true;
            dgv.Dock = System.Windows.Forms.DockStyle.Fill;

            dgv.Columns.Add(new DataGridViewColumn());
            dgv.Columns[0].Name = "Estado";
            dgv.Columns[0].CellTemplate = new DataGridViewTextBoxCell();
            dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[0].DefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Italic);

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

            foreach (EstadoD estado in r.estadosD)
            {
                String[] fila = new String[ss.Count + 1];
                for (int i = 0; i < fila.Length; i++)
                {
                    fila[i] = "-1";
                }

                if (estado.aceptacion)
                    fila[0] = "S" + estado.num + " - Aceptacion";
                else
                    fila[0] = "S" + estado.num;

                int contador = 1;
                foreach (String simbol in ss)
                {
                    //Buscar el movimiento con ese simbolo
                    if (estado.movimientos.ContainsKey(simbol))
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
        }

        private void ejecutar_otros(Regla re)
        {
            if(this.compilado==2)
            {
                this.limpiarTabla();
                this.llenarTabla(re);
            }
        }

        /// <summary>
        /// La aplicacion tienen n automatas
        /// Se evalua si hay un posible camino dentro de cada automata
        ///     si es asi se mueve dentro del automata
        /// Si no hay camino o hay un error dentro un automata recien entrado
        /// se busca el siguiente automata para ser aceptado
        /// 
        /// si se finaliza todos los automata y no fue aceptado se guarda un error del tipo DLEX, xq no
        /// no se puede aceptar la cadena en ninguna regla escrita
        /// 
        /// el error es LEXICO, este se crea si la cadena se acepta en al regla de "error" escrito en el lenguaje dx
        /// </summary>
        /// <param name="listacaracteres"></param>
        private void moverme(List<Caracter> listacaracteres)
        {
            Indice iterador = new Indice(0);
            int i_anterior = iterador.i;
            TokenD lexema;
            Caracter caracter;

            for (iterador.i = 0; iterador.i < listacaracteres.Count; iterador.i  = iterador.i + 1)
            {
                lexema = new TokenD();
                i_anterior = iterador.i;

                //Buscar Camino dentro de la lista de reglas
                    //Cada regla tiene asociada un automata
                List<Regla> reglas=this.arch.reglas;

                int index_reglas = 0;
                for (index_reglas = 0; index_reglas < reglas.Count; index_reglas++)
                {
                    caracter = listacaracteres[i_anterior];
                    lexema = new TokenD(caracter.linea,caracter.columna);

                    Regla regla = reglas[index_reglas];
                    //regla.estadosD.
                    //Obtener el estado 0
                    EstadoD estado_0 = regla.estadosD[0];
                    if (this.hayCamino(estado_0, caracter,regla))
                    {
                        this.ejecutar_otros(regla);
                        //PASAR AL ANALISIS DENTRO DEL AUTOMATA
                        this.moverme2(regla, listacaracteres, lexema, iterador);
                    }
                    else
                    {
                        this.ejecutar_otros(regla);
                        if ((index_reglas + 1)==reglas.Count)
                        {
                            //Evaluar si yano queda mas automatas = error
                            //NO ENCONTRE CAMINO
                            this.guardarError_error_sin_regla(caracter);
                            break;
                        }
                        else
                        {
                            //Aun hay automatas por recorrer
                            continue;
                        }
                    }

                    //Si llego aqui, el caracter si entro dentro un automata

                    //Si lexema = aceptado, 
                    //guardo lexeman
                    //salgo del for y continuo con el sig. caracter
                    if(lexema.aceptado)
                    {
                        this.guardarLexema(regla, lexema);
                        //this.limpiarTabla();
                        break;
                    }

                    //Si lexema = error && no quedan mas automatas
                    //guardo el error, retroceo el indice hasta el anterior aceptado
                    //ENCONTRE CAMINO PERO NO FUE ACEPTADO
                    if(lexema.aceptado==false && (index_reglas+1)==reglas.Count)
                    {
                        guardarError_error_sin_regla(caracter);
                        iterador.i = i_anterior;
                        //this.limpiarTabla();
                        break;
                    }
                    //Si lexema = error && aun quedan mas automatas
                    //retrocedo el indice hasta el ultimo aceptado
                    if(lexema.aceptado==false)
                    {
                        if (i_anterior == listacaracteres.Count - 1)
                            break;
                        iterador.i = i_anterior;                        
                        //this.limpiarTabla();
                    }                        
                }

            }

        }

        //int int_estado;
        //int int_simbolo;
        String string_simbolo;
        private void pintarCelda(int yyyyy, Color c, Regla rrr)
        {   
            if (compilado != 2)
                return;
            Application.DoEvents();
            int xusar = 0;
            int xxxxxx = 0;
            foreach (String item in rrr.afn.listaSimbolos.Keys)
            {
                xxxxxx++;
                if (item.Equals(string_simbolo))
                {
                    xusar = xxxxxx-1;
                    break;
                }                
            }
            //ya tengo x y Y
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                for (int j = 0; j < dgv.Columns.Count; j++)
                {
                    this.dgv.Rows[i].Cells[j].Style.BackColor = Color.White;
                }
            }
            this.txtlog.Text += "Expresion: " + rrr.nombre + "Transicion al : S" + yyyyy + " -> " + string_simbolo+"\n";
            this.dgv.Rows[yyyyy].Cells[1 + xusar].Style.BackColor = c;
            //DGV_RepCon.Item(1, 1).Style.BackColor = Color.Green 
            //visor.Rows[fila].Cells[2].Style.BackColor = c;
            //this.dgv.Item
            //MessageBox.Show("ER:"+rrr.nombre+"\nTransicion al : [S"+yyyyy+" - Simbolo: "+string_simbolo+"]");
            Thread.Sleep(this.tiempo);
            Application.DoEvents();
        }

        private void moverme2(Regla regla, List<Caracter> listacaracteres, TokenD lexema, Indice piterador )
        {
            int i_original = piterador.i;
            int iestado = 0;
            Caracter ccaracter;
            List <EstadoD> listaestados= regla.estadosD;
            
            for (piterador.i = i_original; piterador.i<listacaracteres.Count; piterador.i=piterador.i+1)
            {
                ccaracter = listacaracteres[piterador.i];
                Console.WriteLine("Iterador: "+piterador.i.ToString() + ". Caracter: " + ccaracter.caracter + ", Estado: " + iestado.ToString());
                EstadoD estado_actual=null;

                foreach (EstadoD item in listaestados)
                {
                    if (item.num == iestado)
                    {
                        estado_actual = item;
                        break;
                    }
                }

                if (estado_actual==null)
                {
                    MessageBox.Show("Se arruino!!");
                    return;
                }

                iestado = this.estadoFuturo(estado_actual,ccaracter, regla);

                //Transicion, IMPosible, no hay camino a seguir
                if (iestado==-1)
                {
                    //Error, pero si
                        //Es estado aceptacion, acepto el lexema, retrocedo el inidice en 1, salgo de la funcion
                    if((estado_actual.aceptacion==true) || (lexema.aceptado==true))
                    {
                        lexema.aceptado = true;
                        piterador.i = piterador.i - 1;
                        this.pintarCelda(estado_actual.num, Color.LightGreen, regla);
                        return;
                    }

                    //Si llego aqui no estamos en estado de aceptacion, //ser rechaza el lexema
                    lexema.aceptado = false;
                    this.pintarCelda(estado_actual.num, Color.Red, regla);
                    return;
                }

                //Transicion, Posible, hay camino a seguir
                if(iestado>-1)
                {                    
                    lexema.valor += ccaracter.caracter;
                    if(estado_actual.aceptacion)
                    {
                        this.pintarCelda(iestado, Color.LightGreen, regla);
                        lexema.aceptado = true;
                    }
                    else
                    {
                        this.pintarCelda(iestado, Color.Yellow, regla);
                        lexema.aceptado = false;
                    }
                }
            }
        }


        private void guardarLexema(Regla rule, TokenD lexem)
        {            
            //La regla es del tipo error
            if(rule.soy_error)
            {
                //linea, columna, valor
                this.a_activo.errores_lexicos.Add(new Terror(lexem.valor, lexem.linea, lexem.columna, "Lexico", "Error grabado por el metodo error"));
                return;
            }

            //si es no_guardar, tonces se omite
            if (rule.metodo.nombre_token.Equals("no_guardar"))
            {
                return;
            }

            //Primero buscar dentro las palabras reservadas
            foreach (String nombre_reserv in rule.reservadas.Keys)
            {
                //token
                //linea
                //columna
                String valor_reserv="";
                rule.reservadas.TryGetValue(nombre_reserv, out valor_reserv);
                if(valor_reserv.Equals(lexem.valor))
                {
                    this.a_activo.tokens_lexicos.Add(new TokenD(nombre_reserv, lexem.valor, lexem.linea, lexem.columna));
                    return;
                }
            }
            //Paso, no es reservada
            if (rule.metodo.nivel == 1)
            {
                //token
                //linea
                //columna
                lexem.tipo = "token";
                lexem.valor = rule.metodo.nombre_token;
                lexem.nombre_token = rule.metodo.nombre_token;
            }
            else if (rule.metodo.nivel == 2)
            {
                //token
                //valor
                //tipo
                lexem.nombre_token = rule.metodo.nombre_token;
                //valor ya lo trae por default
                lexem.tipo = rule.metodo.stipo;
            }

            a_activo.tokens_lexicos.Add(new TokenD(lexem.nombre_token, lexem.valor, lexem.tipo, lexem.linea, lexem.columna));

        }


        private void guardarError_error_sin_regla(Caracter c)
        {
            a_activo.errores_dlex.Add(new Terror(c.caracter, c.linea, c.columna, "Lexico", "No se especifico regla que guarde este error"));
            this.a_activo.errores_lexicos.Add(new Terror(c.caracter, c.linea, c.columna, "Lexico", "NO HAY CAMINO, no hay regla que soporte el error"));
        }

        private Boolean hayCamino(EstadoD estado, Caracter carac, Regla rr)
        {
            if (this.estadoFuturo(estado, carac, rr) > -1)
                return true;
            return false;
        }

        /// <summary>
        /// Analizar los movimiento que puede hacer un estado con el caracter
        /// si movimiento.particion ==null
        ///     es decir que no hay movimiento valido :D       
        /// </summary>
        /// <param name="estado">Estado donde se encuentra el analizador</param>
        /// <param name="carac">Caracter con el cual se quiere desplazar a otro estado</param>
        /// <returns>indica el estado futuro a donde ha de llegar con el caracter</returns>
        private int estadoFuturo(EstadoD estado, Caracter carac, Regla rr)
        {
            //El movimiento move[T, a] = U
            //T, es el estado en el que me encuentro
            //a, es el simolo de transicion         
            //U, es el estado a donde voy a llegar, si U==NULL error, no hay movimiento con dicho simbolo, pero
            //habra validar que no este en [:todos:], x~y, etc.

            String c = carac.caracter;
            
            if(estado.movimientos.ContainsKey(c))
            {
                Move movimiento;
                estado.movimientos.TryGetValue(c, out movimiento);
                if(movimiento.particion!=null)
                {
                    int estadofuturo = movimiento.particion.num;
                    this.string_simbolo = c+"";
                    return estadofuturo;
                }
            }

            //Paso, no hay movimientos con el caracter
            //puede que sea un escape, o este dentro los rangos

            //Si fuera No Sensitivo el lenguaje
            //hay que pasar caracter a minuscula
            int ascii_caracter = toAscii(c);

            foreach (Move movimiento in estado.movimientos.Values)
            {
                if (movimiento.particion == null)
                    continue;

                this.string_simbolo =(movimiento.simbolo).ToString();

                //Validar que sea comilla doble "
                if (movimiento.simbolo.Equals("comilla_d"))
                {
                    if(ascii_caracter==34 || ascii_caracter==249)
                    {
                        return movimiento.particion.num;
                    }
                }
                //Validar que sea comilla simple '
                if (movimiento.simbolo.Equals("comilla_s"))
                {
                    if(ascii_caracter==39 || ascii_caracter == 96 || ascii_caracter==236)
                    {
                        return movimiento.particion.num;
                    }
                }
                //Puede estar dentro un rango el caracter
                if(movimiento.simbolo.Contains("[") && movimiento.simbolo.Contains("]"))
                {
                    String s2 = movimiento.simbolo.Replace("[", "").Replace("~", ",").Replace("]","");
                    String []t = s2.Split(',');
                    int iserie = this.toAscii(t[0]);
                    int fserie = this.toAscii(t[1]);
                    if(ascii_caracter>= iserie && ascii_caracter<=fserie)
                        return movimiento.particion.num;
                }

                //Validar salto de linea
                if(movimiento.simbolo.Equals("salto") || c=="\n")
                {
                    if (ascii_caracter == 10)
                        return movimiento.particion.num;
                }
                //Validar que sea un retorno de carro
                if(movimiento.simbolo.Equals("retorno")|| c=="\r")
                {
                    if (ascii_caracter == 13)
                        return movimiento.particion.num;
                }
                //Validar que sea una tabulacion
                if(movimiento.simbolo.Equals("tab"))
                {
                    if (ascii_caracter == 9 || c == "\t")
                        return movimiento.particion.num;
                }
                //Validar que sea un espacio en blanco
                //Este existe si se llamo a al expresion [:blanco:], en el archivo dx
                //entonces el se hace un or con todos los blancos(salto, retorno, tab, espacio)
                if(movimiento.simbolo.Equals("espacio"))
                {
                    if (ascii_caracter == 32 || c==" ")
                        return movimiento.particion.num;
                }
                //Talves sea el caso especia de que se uso la expresion [:todo:]
                //por lo que si se puede haber movimiento con cualquier simbolo que no sea 
                //retorno=13 y salto=10
                if(movimiento.simbolo.Equals("todo"))
                {
                    if (ascii_caracter != 10 && ascii_caracter != 13)
                        return movimiento.particion.num;
                }
            }
            return -1;
        }

        public int toAscii(Char c)
        {
            return Encoding.ASCII.GetBytes(c.ToString())[0];
        }

        public int toAscii(String s)
        {
            return Encoding.ASCII.GetBytes(s)[0];
        }

        public String toSCaracter(int c)
        {
            return (Convert.ToChar(c)).ToString();
        }

        
    }
}
