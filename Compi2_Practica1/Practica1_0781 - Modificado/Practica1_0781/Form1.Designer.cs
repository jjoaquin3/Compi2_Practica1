namespace Practica1_0781
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.msMenu = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buscarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarArchivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analizadorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cargaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tablaTokensToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.erroresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbprincipal = new System.Windows.Forms.TabControl();
            this.tpacercade = new System.Windows.Forms.TabPage();
            this.msMenu.SuspendLayout();
            this.tbprincipal.SuspendLayout();
            this.SuspendLayout();
            // 
            // msMenu
            // 
            this.msMenu.BackColor = System.Drawing.Color.RoyalBlue;
            this.msMenu.GripMargin = new System.Windows.Forms.Padding(2);
            this.msMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.analizadorToolStripMenuItem,
            this.reportesToolStripMenuItem});
            this.msMenu.Location = new System.Drawing.Point(0, 0);
            this.msMenu.Name = "msMenu";
            this.msMenu.Size = new System.Drawing.Size(734, 24);
            this.msMenu.TabIndex = 0;
            this.msMenu.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoToolStripMenuItem,
            this.abrirToolStripMenuItem,
            this.guardarToolStripMenuItem,
            this.buscarToolStripMenuItem,
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
            this.nuevoToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.nuevoToolStripMenuItem.Text = "Nuevo";
            this.nuevoToolStripMenuItem.Click += new System.EventHandler(this.nuevoToolStripMenuItem_Click);
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("abrirToolStripMenuItem.Image")));
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.abrirToolStripMenuItem.Text = "Abrir";
            this.abrirToolStripMenuItem.Click += new System.EventHandler(this.abrirToolStripMenuItem_Click);
            // 
            // guardarToolStripMenuItem
            // 
            this.guardarToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("guardarToolStripMenuItem.Image")));
            this.guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
            this.guardarToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.guardarToolStripMenuItem.Text = "Guardar";
            this.guardarToolStripMenuItem.Click += new System.EventHandler(this.guardarToolStripMenuItem_Click);
            // 
            // buscarToolStripMenuItem
            // 
            this.buscarToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("buscarToolStripMenuItem.Image")));
            this.buscarToolStripMenuItem.Name = "buscarToolStripMenuItem";
            this.buscarToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.buscarToolStripMenuItem.Text = "Buscar";
            this.buscarToolStripMenuItem.Click += new System.EventHandler(this.buscarToolStripMenuItem_Click);
            // 
            // cerrarArchivoToolStripMenuItem
            // 
            this.cerrarArchivoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cerrarArchivoToolStripMenuItem.Image")));
            this.cerrarArchivoToolStripMenuItem.Name = "cerrarArchivoToolStripMenuItem";
            this.cerrarArchivoToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.cerrarArchivoToolStripMenuItem.Text = "Cerrar Archivo";
            this.cerrarArchivoToolStripMenuItem.Click += new System.EventHandler(this.cerrarArchivoToolStripMenuItem_Click);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("salirToolStripMenuItem.Image")));
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // analizadorToolStripMenuItem
            // 
            this.analizadorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cargaToolStripMenuItem,
            this.analizarToolStripMenuItem});
            this.analizadorToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.analizadorToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("analizadorToolStripMenuItem.Image")));
            this.analizadorToolStripMenuItem.Name = "analizadorToolStripMenuItem";
            this.analizadorToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.analizadorToolStripMenuItem.Text = "Analizar";
            // 
            // cargaToolStripMenuItem
            // 
            this.cargaToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cargaToolStripMenuItem.Image")));
            this.cargaToolStripMenuItem.Name = "cargaToolStripMenuItem";
            this.cargaToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.cargaToolStripMenuItem.Text = "Cargar DLex";
            this.cargaToolStripMenuItem.Click += new System.EventHandler(this.cargaToolStripMenuItem_Click);
            // 
            // analizarToolStripMenuItem
            // 
            this.analizarToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("analizarToolStripMenuItem.Image")));
            this.analizarToolStripMenuItem.Name = "analizarToolStripMenuItem";
            this.analizarToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.analizarToolStripMenuItem.Text = "Editor de Pruebas";
            this.analizarToolStripMenuItem.Click += new System.EventHandler(this.analizarToolStripMenuItem_Click);
            // 
            // reportesToolStripMenuItem
            // 
            this.reportesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tablaTokensToolStripMenuItem,
            this.erroresToolStripMenuItem});
            this.reportesToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.reportesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("reportesToolStripMenuItem.Image")));
            this.reportesToolStripMenuItem.Name = "reportesToolStripMenuItem";
            this.reportesToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.reportesToolStripMenuItem.Text = "Reportes";
            // 
            // tablaTokensToolStripMenuItem
            // 
            this.tablaTokensToolStripMenuItem.Enabled = false;
            this.tablaTokensToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("tablaTokensToolStripMenuItem.Image")));
            this.tablaTokensToolStripMenuItem.Name = "tablaTokensToolStripMenuItem";
            this.tablaTokensToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.tablaTokensToolStripMenuItem.Text = "Tabla Tokens";
            this.tablaTokensToolStripMenuItem.Click += new System.EventHandler(this.tablaTokensToolStripMenuItem_Click);
            // 
            // erroresToolStripMenuItem
            // 
            this.erroresToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("erroresToolStripMenuItem.Image")));
            this.erroresToolStripMenuItem.Name = "erroresToolStripMenuItem";
            this.erroresToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.erroresToolStripMenuItem.Text = "Errores";
            this.erroresToolStripMenuItem.Click += new System.EventHandler(this.erroresToolStripMenuItem_Click);
            // 
            // tbprincipal
            // 
            this.tbprincipal.Controls.Add(this.tpacercade);
            this.tbprincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbprincipal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbprincipal.Location = new System.Drawing.Point(0, 24);
            this.tbprincipal.Name = "tbprincipal";
            this.tbprincipal.SelectedIndex = 0;
            this.tbprincipal.Size = new System.Drawing.Size(734, 437);
            this.tbprincipal.TabIndex = 1;
            // 
            // tpacercade
            // 
            this.tpacercade.Location = new System.Drawing.Point(4, 22);
            this.tpacercade.Name = "tpacercade";
            this.tpacercade.Padding = new System.Windows.Forms.Padding(3);
            this.tpacercade.Size = new System.Drawing.Size(726, 411);
            this.tpacercade.TabIndex = 0;
            this.tpacercade.Text = "Acerca de";
            this.tpacercade.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 461);
            this.Controls.Add(this.tbprincipal);
            this.Controls.Add(this.msMenu);
            this.MainMenuStrip = this.msMenu;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DLex";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.msMenu.ResumeLayout(false);
            this.msMenu.PerformLayout();
            this.tbprincipal.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip msMenu;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buscarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem analizadorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cargaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem analizarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tablaTokensToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem erroresToolStripMenuItem;
        private System.Windows.Forms.TabControl tbprincipal;
        private System.Windows.Forms.TabPage tpacercade;
        private System.Windows.Forms.ToolStripMenuItem cerrarArchivoToolStripMenuItem;
    }
}

