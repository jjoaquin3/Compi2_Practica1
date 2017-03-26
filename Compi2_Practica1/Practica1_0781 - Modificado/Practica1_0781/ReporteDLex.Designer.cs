namespace Practica1_0781
{
    partial class ReporteDLex
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
            this.tc = new System.Windows.Forms.TabControl();
            this.tp1 = new System.Windows.Forms.TabPage();
            this.txtacerca = new System.Windows.Forms.RichTextBox();
            this.tc.SuspendLayout();
            this.tp1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tc
            // 
            this.tc.Controls.Add(this.tp1);
            this.tc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc.Location = new System.Drawing.Point(0, 0);
            this.tc.Name = "tc";
            this.tc.SelectedIndex = 0;
            this.tc.Size = new System.Drawing.Size(799, 518);
            this.tc.TabIndex = 0;
            // 
            // tp1
            // 
            this.tp1.Controls.Add(this.txtacerca);
            this.tp1.Location = new System.Drawing.Point(4, 22);
            this.tp1.Name = "tp1";
            this.tp1.Padding = new System.Windows.Forms.Padding(3);
            this.tp1.Size = new System.Drawing.Size(791, 492);
            this.tp1.TabIndex = 1;
            this.tp1.Text = "Acerca de";
            this.tp1.UseVisualStyleBackColor = true;
            // 
            // txtacerca
            // 
            this.txtacerca.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtacerca.Location = new System.Drawing.Point(3, 3);
            this.txtacerca.Name = "txtacerca";
            this.txtacerca.Size = new System.Drawing.Size(785, 486);
            this.txtacerca.TabIndex = 0;
            this.txtacerca.Text = "Resumen de\n* Tabla de transiciones\n* AFN\n* AFD\n* Operaciones de Subconjuntos\n\nPar" +
    "a cada regla descrita en el lenguaje dx, cargado";
            // 
            // ReporteDLex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 518);
            this.Controls.Add(this.tc);
            this.Name = "ReporteDLex";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReporteDLex";
            this.tc.ResumeLayout(false);
            this.tp1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tc;
        private System.Windows.Forms.TabPage tp1;
        private System.Windows.Forms.RichTextBox txtacerca;
    }
}