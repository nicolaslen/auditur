namespace Auditur.Presentacion
{
    partial class ucMenu
    {
        /// <summary> 
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar 
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.mnuGeneral = new System.Windows.Forms.MenuStrip();
            this.importarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smiImportarBSP = new System.Windows.Forms.ToolStripMenuItem();
            this.smiImportarBO = new System.Windows.Forms.ToolStripMenuItem();
            this.aBMsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smiAgencias = new System.Windows.Forms.ToolStripMenuItem();
            this.smiConceptos = new System.Windows.Forms.ToolStripMenuItem();
            this.smiCompanias = new System.Windows.Forms.ToolStripMenuItem();
            this.reportesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smiElegirSemana = new System.Windows.Forms.ToolStripMenuItem();
            this.ayudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smnCargarBD = new System.Windows.Forms.ToolStripMenuItem();
            this.smnBackUp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGeneral.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuGeneral
            // 
            this.mnuGeneral.BackColor = System.Drawing.Color.Cornsilk;
            this.mnuGeneral.Font = new System.Drawing.Font("Calibri", 14.25F);
            this.mnuGeneral.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importarToolStripMenuItem,
            this.aBMsToolStripMenuItem,
            this.reportesToolStripMenuItem,
            this.ayudaToolStripMenuItem});
            this.mnuGeneral.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuGeneral.Location = new System.Drawing.Point(0, 0);
            this.mnuGeneral.Name = "mnuGeneral";
            this.mnuGeneral.Padding = new System.Windows.Forms.Padding(10, 4, 0, 4);
            this.mnuGeneral.Size = new System.Drawing.Size(1575, 35);
            this.mnuGeneral.TabIndex = 0;
            this.mnuGeneral.Text = "menuStrip1";
            // 
            // importarToolStripMenuItem
            // 
            this.importarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smiImportarBSP,
            this.smiImportarBO});
            this.importarToolStripMenuItem.Name = "importarToolStripMenuItem";
            this.importarToolStripMenuItem.Size = new System.Drawing.Size(92, 27);
            this.importarToolStripMenuItem.Text = "Importar";
            // 
            // smiImportarBSP
            // 
            this.smiImportarBSP.Name = "smiImportarBSP";
            this.smiImportarBSP.Size = new System.Drawing.Size(239, 28);
            this.smiImportarBSP.Text = "Importar BSP";
            this.smiImportarBSP.Click += new System.EventHandler(this.smiImportarBSP_Click);
            // 
            // smiImportarBO
            // 
            this.smiImportarBO.Name = "smiImportarBO";
            this.smiImportarBO.Size = new System.Drawing.Size(239, 28);
            this.smiImportarBO.Text = "Importar Back Office";
            this.smiImportarBO.Click += new System.EventHandler(this.smiImportarBO_Click);
            // 
            // aBMsToolStripMenuItem
            // 
            this.aBMsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smiAgencias,
            this.smiConceptos,
            this.smiCompanias});
            this.aBMsToolStripMenuItem.Name = "aBMsToolStripMenuItem";
            this.aBMsToolStripMenuItem.Size = new System.Drawing.Size(59, 27);
            this.aBMsToolStripMenuItem.Text = "ABM";
            // 
            // smiAgencias
            // 
            this.smiAgencias.Name = "smiAgencias";
            this.smiAgencias.Size = new System.Drawing.Size(229, 28);
            this.smiAgencias.Text = "ABM de Agencias";
            this.smiAgencias.Click += new System.EventHandler(this.smiAgencias_Click);
            // 
            // smiConceptos
            // 
            this.smiConceptos.Name = "smiConceptos";
            this.smiConceptos.Size = new System.Drawing.Size(229, 28);
            this.smiConceptos.Text = "ABM de Conceptos";
            this.smiConceptos.Click += new System.EventHandler(this.smiConceptos_Click);
            // 
            // smiCompanias
            // 
            this.smiCompanias.Name = "smiCompanias";
            this.smiCompanias.Size = new System.Drawing.Size(229, 28);
            this.smiCompanias.Text = "ABM de Compañías";
            this.smiCompanias.Click += new System.EventHandler(this.smiCompanias_Click);
            // 
            // reportesToolStripMenuItem
            // 
            this.reportesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smiElegirSemana});
            this.reportesToolStripMenuItem.Name = "reportesToolStripMenuItem";
            this.reportesToolStripMenuItem.Size = new System.Drawing.Size(141, 27);
            this.reportesToolStripMenuItem.Text = "Datos cargados";
            // 
            // smiElegirSemana
            // 
            this.smiElegirSemana.Name = "smiElegirSemana";
            this.smiElegirSemana.Size = new System.Drawing.Size(186, 28);
            this.smiElegirSemana.Text = "Elegir semana";
            this.smiElegirSemana.Click += new System.EventHandler(this.smiElegirSemana_Click);
            // 
            // ayudaToolStripMenuItem
            // 
            this.ayudaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smnCargarBD,
            this.smnBackUp});
            this.ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            this.ayudaToolStripMenuItem.Size = new System.Drawing.Size(71, 27);
            this.ayudaToolStripMenuItem.Text = "Ayuda";
            // 
            // smnCargarBD
            // 
            this.smnCargarBD.Name = "smnCargarBD";
            this.smnCargarBD.Size = new System.Drawing.Size(260, 28);
            this.smnCargarBD.Text = "Exportar base de datos";
            this.smnCargarBD.Click += new System.EventHandler(this.smnBackUp_Click);
            // 
            // smnBackUp
            // 
            this.smnBackUp.Name = "smnBackUp";
            this.smnBackUp.Size = new System.Drawing.Size(260, 28);
            this.smnBackUp.Text = "Importar base de datos";
            this.smnBackUp.Click += new System.EventHandler(this.smnCargarBD_Click);
            // 
            // ucMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.Controls.Add(this.mnuGeneral);
            this.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "ucMenu";
            this.Size = new System.Drawing.Size(1575, 39);
            this.mnuGeneral.ResumeLayout(false);
            this.mnuGeneral.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuGeneral;
        private System.Windows.Forms.ToolStripMenuItem importarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smiImportarBSP;
        private System.Windows.Forms.ToolStripMenuItem smiImportarBO;
        private System.Windows.Forms.ToolStripMenuItem reportesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smiElegirSemana;
        private System.Windows.Forms.ToolStripMenuItem ayudaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smnCargarBD;
        private System.Windows.Forms.ToolStripMenuItem smnBackUp;
        private System.Windows.Forms.ToolStripMenuItem aBMsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smiAgencias;
        private System.Windows.Forms.ToolStripMenuItem smiConceptos;
        private System.Windows.Forms.ToolStripMenuItem smiCompanias;
    }
}
