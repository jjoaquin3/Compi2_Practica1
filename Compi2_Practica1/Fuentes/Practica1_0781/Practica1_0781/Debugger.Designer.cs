namespace Practica1_0781
{
    partial class Debugger
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Debugger));
            this.tbprincipal = new System.Windows.Forms.TabControl();
            this.tpacercade = new System.Windows.Forms.TabPage();
            this.txtacerca = new System.Windows.Forms.RichTextBox();
            this.msMenu = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarArchivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tablaTokensToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.erroresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.todoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tptabla = new System.Windows.Forms.TabPage();
            this.txtlog = new System.Windows.Forms.RichTextBox();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.tabdebugger = new System.Windows.Forms.TabControl();
            this.barra = new System.Windows.Forms.TrackBar();
            this.bnext = new System.Windows.Forms.Button();
            this.brun = new System.Windows.Forms.Button();
            this.bnormal = new System.Windows.Forms.Button();
            this.tbprincipal.SuspendLayout();
            this.tpacercade.SuspendLayout();
            this.msMenu.SuspendLayout();
            this.tptabla.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.tabdebugger.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barra)).BeginInit();
            this.SuspendLayout();
            // 
            // tbprincipal
            // 
            this.tbprincipal.Controls.Add(this.tpacercade);
            this.tbprincipal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbprincipal.Location = new System.Drawing.Point(12, 28);
            this.tbprincipal.Name = "tbprincipal";
            this.tbprincipal.SelectedIndex = 0;
            this.tbprincipal.Size = new System.Drawing.Size(411, 477);
            this.tbprincipal.TabIndex = 2;
            // 
            // tpacercade
            // 
            this.tpacercade.Controls.Add(this.txtacerca);
            this.tpacercade.Location = new System.Drawing.Point(4, 22);
            this.tpacercade.Name = "tpacercade";
            this.tpacercade.Padding = new System.Windows.Forms.Padding(3);
            this.tpacercade.Size = new System.Drawing.Size(403, 451);
            this.tpacercade.TabIndex = 0;
            this.tpacercade.Text = "Acerca de";
            this.tpacercade.UseVisualStyleBackColor = true;
            // 
            // txtacerca
            // 
            this.txtacerca.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtacerca.Enabled = false;
            this.txtacerca.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtacerca.Location = new System.Drawing.Point(3, 3);
            this.txtacerca.Name = "txtacerca";
            this.txtacerca.Size = new System.Drawing.Size(397, 445);
            this.txtacerca.TabIndex = 0;
            this.txtacerca.Text = resources.GetString("txtacerca.Text");
            // 
            // msMenu
            // 
            this.msMenu.BackColor = System.Drawing.Color.RoyalBlue;
            this.msMenu.GripMargin = new System.Windows.Forms.Padding(2);
            this.msMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.reportesToolStripMenuItem});
            this.msMenu.Location = new System.Drawing.Point(0, 0);
            this.msMenu.Name = "msMenu";
            this.msMenu.Size = new System.Drawing.Size(897, 24);
            this.msMenu.TabIndex = 3;
            this.msMenu.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoToolStripMenuItem,
            this.abrirToolStripMenuItem,
            this.guardarToolStripMenuItem,
            this.cerrarArchivoToolStripMenuItem,
            this.salirToolStripMenuItem});
            this.archivoToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.archivoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("archivoToolStripMenuItem.Image")));
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // nuevoToolStripMenuItem
            // 
            this.nuevoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("nuevoToolStripMenuItem.Image")));
            this.nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
            this.nuevoToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.nuevoToolStripMenuItem.Text = "Nuevo";
            this.nuevoToolStripMenuItem.Click += new System.EventHandler(this.nuevoToolStripMenuItem_Click);
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("abrirToolStripMenuItem.Image")));
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.abrirToolStripMenuItem.Text = "Abrir";
            this.abrirToolStripMenuItem.Click += new System.EventHandler(this.abrirToolStripMenuItem_Click);
            // 
            // guardarToolStripMenuItem
            // 
            this.guardarToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("guardarToolStripMenuItem.Image")));
            this.guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
            this.guardarToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.guardarToolStripMenuItem.Text = "Guardar";
            this.guardarToolStripMenuItem.Click += new System.EventHandler(this.guardarToolStripMenuItem_Click);
            // 
            // cerrarArchivoToolStripMenuItem
            // 
            this.cerrarArchivoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cerrarArchivoToolStripMenuItem.Image")));
            this.cerrarArchivoToolStripMenuItem.Name = "cerrarArchivoToolStripMenuItem";
            this.cerrarArchivoToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cerrarArchivoToolStripMenuItem.Text = "Cerrar Archivo";
            this.cerrarArchivoToolStripMenuItem.Click += new System.EventHandler(this.cerrarArchivoToolStripMenuItem_Click);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("salirToolStripMenuItem.Image")));
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // reportesToolStripMenuItem
            // 
            this.reportesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tablaTokensToolStripMenuItem,
            this.erroresToolStripMenuItem,
            this.todoToolStripMenuItem});
            this.reportesToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.reportesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("reportesToolStripMenuItem.Image")));
            this.reportesToolStripMenuItem.Name = "reportesToolStripMenuItem";
            this.reportesToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.reportesToolStripMenuItem.Text = "Reportes";
            // 
            // tablaTokensToolStripMenuItem
            // 
            this.tablaTokensToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("tablaTokensToolStripMenuItem.Image")));
            this.tablaTokensToolStripMenuItem.Name = "tablaTokensToolStripMenuItem";
            this.tablaTokensToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.tablaTokensToolStripMenuItem.Text = "Tabla Tokens";
            this.tablaTokensToolStripMenuItem.Click += new System.EventHandler(this.tablaTokensToolStripMenuItem_Click);
            // 
            // erroresToolStripMenuItem
            // 
            this.erroresToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("erroresToolStripMenuItem.Image")));
            this.erroresToolStripMenuItem.Name = "erroresToolStripMenuItem";
            this.erroresToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.erroresToolStripMenuItem.Text = "Errores";
            this.erroresToolStripMenuItem.Click += new System.EventHandler(this.erroresToolStripMenuItem_Click);
            // 
            // todoToolStripMenuItem
            // 
            this.todoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("todoToolStripMenuItem.Image")));
            this.todoToolStripMenuItem.Name = "todoToolStripMenuItem";
            this.todoToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.todoToolStripMenuItem.Text = "Todo";
            this.todoToolStripMenuItem.Click += new System.EventHandler(this.todoToolStripMenuItem_Click);
            // 
            // tptabla
            // 
            this.tptabla.Controls.Add(this.txtlog);
            this.tptabla.Controls.Add(this.dgv);
            this.tptabla.Location = new System.Drawing.Point(4, 22);
            this.tptabla.Name = "tptabla";
            this.tptabla.Padding = new System.Windows.Forms.Padding(3);
            this.tptabla.Size = new System.Drawing.Size(448, 513);
            this.tptabla.TabIndex = 3;
            this.tptabla.Text = "Tabla";
            this.tptabla.UseVisualStyleBackColor = true;
            // 
            // txtlog
            // 
            this.txtlog.Location = new System.Drawing.Point(6, 323);
            this.txtlog.Name = "txtlog";
            this.txtlog.Size = new System.Drawing.Size(436, 184);
            this.txtlog.TabIndex = 2;
            this.txtlog.Text = "";
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(3, 3);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.Size = new System.Drawing.Size(442, 314);
            this.dgv.TabIndex = 1;
            // 
            // tabdebugger
            // 
            this.tabdebugger.Controls.Add(this.tptabla);
            this.tabdebugger.Location = new System.Drawing.Point(429, 28);
            this.tabdebugger.Name = "tabdebugger";
            this.tabdebugger.SelectedIndex = 0;
            this.tabdebugger.Size = new System.Drawing.Size(456, 539);
            this.tabdebugger.TabIndex = 2;
            // 
            // barra
            // 
            this.barra.BackColor = System.Drawing.Color.White;
            this.barra.Location = new System.Drawing.Point(100, 518);
            this.barra.Name = "barra";
            this.barra.Size = new System.Drawing.Size(242, 45);
            this.barra.TabIndex = 0;
            this.barra.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.barra.Value = 7;
            // 
            // bnext
            // 
            this.bnext.Location = new System.Drawing.Point(348, 540);
            this.bnext.Name = "bnext";
            this.bnext.Size = new System.Drawing.Size(75, 23);
            this.bnext.TabIndex = 1;
            this.bnext.Text = "Next";
            this.bnext.UseVisualStyleBackColor = true;
            this.bnext.Click += new System.EventHandler(this.bnext_Click);
            // 
            // brun
            // 
            this.brun.Location = new System.Drawing.Point(348, 511);
            this.brun.Name = "brun";
            this.brun.Size = new System.Drawing.Size(75, 23);
            this.brun.TabIndex = 5;
            this.brun.Text = "Run";
            this.brun.UseVisualStyleBackColor = true;
            this.brun.Click += new System.EventHandler(this.brun_Click);
            // 
            // bnormal
            // 
            this.bnormal.Location = new System.Drawing.Point(19, 534);
            this.bnormal.Name = "bnormal";
            this.bnormal.Size = new System.Drawing.Size(75, 23);
            this.bnormal.TabIndex = 6;
            this.bnormal.Text = "Normal";
            this.bnormal.UseVisualStyleBackColor = true;
            this.bnormal.Click += new System.EventHandler(this.bnormal_Click);
            // 
            // Debugger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(897, 579);
            this.Controls.Add(this.bnormal);
            this.Controls.Add(this.bnext);
            this.Controls.Add(this.brun);
            this.Controls.Add(this.tabdebugger);
            this.Controls.Add(this.msMenu);
            this.Controls.Add(this.barra);
            this.Controls.Add(this.tbprincipal);
            this.Name = "Debugger";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Debugger";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Debugger_FormClosing);
            this.tbprincipal.ResumeLayout(false);
            this.tpacercade.ResumeLayout(false);
            this.msMenu.ResumeLayout(false);
            this.msMenu.PerformLayout();
            this.tptabla.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.tabdebugger.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barra)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tbprincipal;
        private System.Windows.Forms.TabPage tpacercade;
        private System.Windows.Forms.MenuStrip msMenu;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cerrarArchivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tablaTokensToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem erroresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem todoToolStripMenuItem;
        private System.Windows.Forms.RichTextBox txtacerca;
        private System.Windows.Forms.TabPage tptabla;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.TabControl tabdebugger;
        private System.Windows.Forms.TrackBar barra;
        private System.Windows.Forms.Button bnext;
        private System.Windows.Forms.Button brun;
        private System.Windows.Forms.Button bnormal;
        private System.Windows.Forms.RichTextBox txtlog;
    }
}