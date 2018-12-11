namespace Auditur.Presentacion
{
    partial class frmReportes
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
            this.grbReportes = new System.Windows.Forms.GroupBox();
            this.btnFacturacion = new System.Windows.Forms.Button();
            this.btnGenerarTodos = new System.Windows.Forms.Button();
            this.btnSituacionBSPs = new System.Windows.Forms.Button();
            this.btnSituacionBOs = new System.Windows.Forms.Button();
            this.btnOvers = new System.Windows.Forms.Button();
            this.btnDiferencias = new System.Windows.Forms.Button();
            this.btnDebitos = new System.Windows.Forms.Button();
            this.btnReembolsos = new System.Windows.Forms.Button();
            this.btnCreditos = new System.Windows.Forms.Button();
            this.btnControlIVAs = new System.Windows.Forms.Button();
            this.btnBSPNroOPs = new System.Windows.Forms.Button();
            this.grbFileImport = new System.Windows.Forms.GroupBox();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnExaminar = new System.Windows.Forms.Button();
            this.frmCurrents1 = new Auditur.Presentacion.frmCurrents();
            this.btnExaminar_BSP = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.grbProceso = new System.Windows.Forms.GroupBox();
            this.lblProgressStatus = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.grbReportes.SuspendLayout();
            this.grbFileImport.SuspendLayout();
            this.grbProceso.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbReportes
            // 
            this.grbReportes.Controls.Add(this.btnFacturacion);
            this.grbReportes.Controls.Add(this.btnGenerarTodos);
            this.grbReportes.Controls.Add(this.btnSituacionBSPs);
            this.grbReportes.Controls.Add(this.btnSituacionBOs);
            this.grbReportes.Controls.Add(this.btnOvers);
            this.grbReportes.Controls.Add(this.btnDiferencias);
            this.grbReportes.Controls.Add(this.btnDebitos);
            this.grbReportes.Controls.Add(this.btnReembolsos);
            this.grbReportes.Controls.Add(this.btnCreditos);
            this.grbReportes.Controls.Add(this.btnControlIVAs);
            this.grbReportes.Controls.Add(this.btnBSPNroOPs);
            this.grbReportes.Enabled = false;
            this.grbReportes.Location = new System.Drawing.Point(4, 230);
            this.grbReportes.Margin = new System.Windows.Forms.Padding(4);
            this.grbReportes.Name = "grbReportes";
            this.grbReportes.Padding = new System.Windows.Forms.Padding(4);
            this.grbReportes.Size = new System.Drawing.Size(747, 260);
            this.grbReportes.TabIndex = 6;
            this.grbReportes.TabStop = false;
            this.grbReportes.Text = "Elija que reporte desea generar...";
            // 
            // btnFacturacion
            // 
            this.btnFacturacion.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnFacturacion.Font = new System.Drawing.Font("Calibri", 14.25F);
            this.btnFacturacion.Location = new System.Drawing.Point(570, 152);
            this.btnFacturacion.Margin = new System.Windows.Forms.Padding(4);
            this.btnFacturacion.Name = "btnFacturacion";
            this.btnFacturacion.Size = new System.Drawing.Size(140, 33);
            this.btnFacturacion.TabIndex = 14;
            this.btnFacturacion.Text = "Facturación";
            this.btnFacturacion.UseVisualStyleBackColor = true;
            this.btnFacturacion.Click += new System.EventHandler(this.btnFacturacion_Click);
            // 
            // btnGenerarTodos
            // 
            this.btnGenerarTodos.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnGenerarTodos.Font = new System.Drawing.Font("Calibri", 14.25F);
            this.btnGenerarTodos.Location = new System.Drawing.Point(230, 207);
            this.btnGenerarTodos.Margin = new System.Windows.Forms.Padding(4);
            this.btnGenerarTodos.Name = "btnGenerarTodos";
            this.btnGenerarTodos.Size = new System.Drawing.Size(302, 42);
            this.btnGenerarTodos.TabIndex = 13;
            this.btnGenerarTodos.Text = "Generar todos";
            this.btnGenerarTodos.UseVisualStyleBackColor = true;
            this.btnGenerarTodos.Click += new System.EventHandler(this.btnGenerarTodos_Click);
            // 
            // btnSituacionBSPs
            // 
            this.btnSituacionBSPs.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSituacionBSPs.Font = new System.Drawing.Font("Calibri", 14.25F);
            this.btnSituacionBSPs.Location = new System.Drawing.Point(39, 152);
            this.btnSituacionBSPs.Margin = new System.Windows.Forms.Padding(4);
            this.btnSituacionBSPs.Name = "btnSituacionBSPs";
            this.btnSituacionBSPs.Size = new System.Drawing.Size(140, 33);
            this.btnSituacionBSPs.TabIndex = 12;
            this.btnSituacionBSPs.Text = "Sit. BSPs";
            this.btnSituacionBSPs.UseVisualStyleBackColor = true;
            this.btnSituacionBSPs.Click += new System.EventHandler(this.btnSituacionBSPs_Click);
            // 
            // btnSituacionBOs
            // 
            this.btnSituacionBOs.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSituacionBOs.Font = new System.Drawing.Font("Calibri", 14.25F);
            this.btnSituacionBOs.Location = new System.Drawing.Point(570, 98);
            this.btnSituacionBOs.Margin = new System.Windows.Forms.Padding(4);
            this.btnSituacionBOs.Name = "btnSituacionBOs";
            this.btnSituacionBOs.Size = new System.Drawing.Size(140, 33);
            this.btnSituacionBOs.TabIndex = 11;
            this.btnSituacionBOs.Text = "Sit. BOs";
            this.btnSituacionBOs.UseVisualStyleBackColor = true;
            this.btnSituacionBOs.Click += new System.EventHandler(this.btnSituacionBOs_Click);
            // 
            // btnOvers
            // 
            this.btnOvers.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnOvers.Font = new System.Drawing.Font("Calibri", 14.25F);
            this.btnOvers.Location = new System.Drawing.Point(392, 98);
            this.btnOvers.Margin = new System.Windows.Forms.Padding(4);
            this.btnOvers.Name = "btnOvers";
            this.btnOvers.Size = new System.Drawing.Size(140, 33);
            this.btnOvers.TabIndex = 10;
            this.btnOvers.Text = "Overs";
            this.btnOvers.UseVisualStyleBackColor = true;
            this.btnOvers.Click += new System.EventHandler(this.btnOvers_Click);
            // 
            // btnDiferencias
            // 
            this.btnDiferencias.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnDiferencias.Font = new System.Drawing.Font("Calibri", 14.25F);
            this.btnDiferencias.Location = new System.Drawing.Point(212, 98);
            this.btnDiferencias.Margin = new System.Windows.Forms.Padding(4);
            this.btnDiferencias.Name = "btnDiferencias";
            this.btnDiferencias.Size = new System.Drawing.Size(140, 33);
            this.btnDiferencias.TabIndex = 9;
            this.btnDiferencias.Text = "Diferencias";
            this.btnDiferencias.UseVisualStyleBackColor = true;
            this.btnDiferencias.Click += new System.EventHandler(this.btnDiferencias_Click);
            // 
            // btnDebitos
            // 
            this.btnDebitos.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnDebitos.Font = new System.Drawing.Font("Calibri", 14.25F);
            this.btnDebitos.Location = new System.Drawing.Point(570, 45);
            this.btnDebitos.Margin = new System.Windows.Forms.Padding(4);
            this.btnDebitos.Name = "btnDebitos";
            this.btnDebitos.Size = new System.Drawing.Size(140, 33);
            this.btnDebitos.TabIndex = 8;
            this.btnDebitos.Text = "Débitos";
            this.btnDebitos.UseVisualStyleBackColor = true;
            this.btnDebitos.Click += new System.EventHandler(this.btnDebitos_Click);
            // 
            // btnReembolsos
            // 
            this.btnReembolsos.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnReembolsos.Font = new System.Drawing.Font("Calibri", 14.25F);
            this.btnReembolsos.Location = new System.Drawing.Point(39, 98);
            this.btnReembolsos.Margin = new System.Windows.Forms.Padding(4);
            this.btnReembolsos.Name = "btnReembolsos";
            this.btnReembolsos.Size = new System.Drawing.Size(140, 33);
            this.btnReembolsos.TabIndex = 7;
            this.btnReembolsos.Text = "Reembolsos";
            this.btnReembolsos.UseVisualStyleBackColor = true;
            this.btnReembolsos.Click += new System.EventHandler(this.btnReembolsos_Click);
            // 
            // btnCreditos
            // 
            this.btnCreditos.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnCreditos.Font = new System.Drawing.Font("Calibri", 14.25F);
            this.btnCreditos.Location = new System.Drawing.Point(392, 45);
            this.btnCreditos.Margin = new System.Windows.Forms.Padding(4);
            this.btnCreditos.Name = "btnCreditos";
            this.btnCreditos.Size = new System.Drawing.Size(140, 33);
            this.btnCreditos.TabIndex = 6;
            this.btnCreditos.Text = "Créditos";
            this.btnCreditos.UseVisualStyleBackColor = true;
            this.btnCreditos.Click += new System.EventHandler(this.btnCreditos_Click);
            // 
            // btnControlIVAs
            // 
            this.btnControlIVAs.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnControlIVAs.Font = new System.Drawing.Font("Calibri", 14.25F);
            this.btnControlIVAs.Location = new System.Drawing.Point(212, 45);
            this.btnControlIVAs.Margin = new System.Windows.Forms.Padding(4);
            this.btnControlIVAs.Name = "btnControlIVAs";
            this.btnControlIVAs.Size = new System.Drawing.Size(140, 33);
            this.btnControlIVAs.TabIndex = 5;
            this.btnControlIVAs.Text = "Control IVAs";
            this.btnControlIVAs.UseVisualStyleBackColor = true;
            this.btnControlIVAs.Click += new System.EventHandler(this.btnControlIVAs_Click);
            // 
            // btnBSPNroOPs
            // 
            this.btnBSPNroOPs.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnBSPNroOPs.Font = new System.Drawing.Font("Calibri", 14.25F);
            this.btnBSPNroOPs.Location = new System.Drawing.Point(39, 45);
            this.btnBSPNroOPs.Margin = new System.Windows.Forms.Padding(4);
            this.btnBSPNroOPs.Name = "btnBSPNroOPs";
            this.btnBSPNroOPs.Size = new System.Drawing.Size(140, 33);
            this.btnBSPNroOPs.TabIndex = 4;
            this.btnBSPNroOPs.Text = "BSP + Nro OPs";
            this.btnBSPNroOPs.UseVisualStyleBackColor = true;
            this.btnBSPNroOPs.Click += new System.EventHandler(this.btnBSPNroOPs_Click);
            // 
            // grbFileImport
            // 
            this.grbFileImport.Controls.Add(this.txtFilePath);
            this.grbFileImport.Controls.Add(this.btnExaminar);
            this.grbFileImport.Location = new System.Drawing.Point(4, 138);
            this.grbFileImport.Margin = new System.Windows.Forms.Padding(5);
            this.grbFileImport.Name = "grbFileImport";
            this.grbFileImport.Padding = new System.Windows.Forms.Padding(5);
            this.grbFileImport.Size = new System.Drawing.Size(745, 83);
            this.grbFileImport.TabIndex = 7;
            this.grbFileImport.TabStop = false;
            this.grbFileImport.Text = "Seleccione una carpeta para guardar los reportes...";
            // 
            // txtFilePath
            // 
            this.txtFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilePath.Location = new System.Drawing.Point(10, 42);
            this.txtFilePath.Margin = new System.Windows.Forms.Padding(5);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(590, 31);
            this.txtFilePath.TabIndex = 1;
            // 
            // btnExaminar
            // 
            this.btnExaminar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExaminar.Location = new System.Drawing.Point(610, 36);
            this.btnExaminar.Margin = new System.Windows.Forms.Padding(5);
            this.btnExaminar.Name = "btnExaminar";
            this.btnExaminar.Size = new System.Drawing.Size(125, 41);
            this.btnExaminar.TabIndex = 0;
            this.btnExaminar.Text = "Examinar...";
            this.btnExaminar.UseVisualStyleBackColor = true;
            this.btnExaminar.Click += new System.EventHandler(this.btnExaminar_Click);
            // 
            // frmCurrents1
            // 
            this.frmCurrents1.BackColor = System.Drawing.Color.Cornsilk;
            this.frmCurrents1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmCurrents1.Location = new System.Drawing.Point(5, 5);
            this.frmCurrents1.Margin = new System.Windows.Forms.Padding(5);
            this.frmCurrents1.Name = "frmCurrents1";
            this.frmCurrents1.ShowReportButton = false;
            this.frmCurrents1.Size = new System.Drawing.Size(745, 123);
            this.frmCurrents1.TabIndex = 13;
            // 
            // btnExaminar_BSP
            // 
            this.btnExaminar_BSP.Location = new System.Drawing.Point(0, 0);
            this.btnExaminar_BSP.Name = "btnExaminar_BSP";
            this.btnExaminar_BSP.Size = new System.Drawing.Size(75, 23);
            this.btnExaminar_BSP.TabIndex = 0;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // grbProceso
            // 
            this.grbProceso.Controls.Add(this.lblProgressStatus);
            this.grbProceso.Controls.Add(this.progressBar1);
            this.grbProceso.Location = new System.Drawing.Point(4, 498);
            this.grbProceso.Margin = new System.Windows.Forms.Padding(4);
            this.grbProceso.Name = "grbProceso";
            this.grbProceso.Padding = new System.Windows.Forms.Padding(4);
            this.grbProceso.Size = new System.Drawing.Size(745, 95);
            this.grbProceso.TabIndex = 14;
            this.grbProceso.TabStop = false;
            this.grbProceso.Text = "Proceso...";
            // 
            // lblProgressStatus
            // 
            this.lblProgressStatus.AutoSize = true;
            this.lblProgressStatus.Location = new System.Drawing.Point(9, 31);
            this.lblProgressStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProgressStatus.Name = "lblProgressStatus";
            this.lblProgressStatus.Size = new System.Drawing.Size(0, 23);
            this.lblProgressStatus.TabIndex = 10;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(8, 58);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar1.MarqueeAnimationSpeed = 0;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(729, 29);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 9;
            // 
            // frmReportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.Controls.Add(this.grbProceso);
            this.Controls.Add(this.frmCurrents1);
            this.Controls.Add(this.grbReportes);
            this.Controls.Add(this.grbFileImport);
            this.Font = new System.Drawing.Font("Calibri", 14.25F);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmReportes";
            this.Size = new System.Drawing.Size(755, 604);
            this.Load += new System.EventHandler(this.frmReportes_Load);
            this.grbReportes.ResumeLayout(false);
            this.grbFileImport.ResumeLayout(false);
            this.grbFileImport.PerformLayout();
            this.grbProceso.ResumeLayout(false);
            this.grbProceso.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private frmCurrents frmCurrents1;
        private System.Windows.Forms.GroupBox grbReportes;
        private System.Windows.Forms.Button btnBSPNroOPs;
        private System.Windows.Forms.Button btnSituacionBOs;
        private System.Windows.Forms.Button btnOvers;
        private System.Windows.Forms.Button btnDiferencias;
        private System.Windows.Forms.Button btnDebitos;
        private System.Windows.Forms.Button btnReembolsos;
        private System.Windows.Forms.Button btnCreditos;
        private System.Windows.Forms.Button btnControlIVAs;
        private System.Windows.Forms.Button btnGenerarTodos;
        private System.Windows.Forms.Button btnSituacionBSPs;
        private System.Windows.Forms.GroupBox grbFileImport;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnExaminar;
        private System.Windows.Forms.Button btnExaminar_BSP;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox grbProceso;
        private System.Windows.Forms.Label lblProgressStatus;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnFacturacion;
    }
}
