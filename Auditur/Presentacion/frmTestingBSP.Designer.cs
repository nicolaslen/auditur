namespace Auditur.Presentacion
{
    partial class frmTestingBSP
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
            this.btnExaminar_BSP = new System.Windows.Forms.Button();
            this.txtFilePath_BSP = new System.Windows.Forms.TextBox();
            this.grbFileImport_BSP = new System.Windows.Forms.GroupBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblProgressStatus = new System.Windows.Forms.Label();
            this.grbProceso = new System.Windows.Forms.GroupBox();
            this.btnReadFile = new System.Windows.Forms.Button();
            this.grbFileImport_BSP.SuspendLayout();
            this.grbProceso.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExaminar_BSP
            // 
            this.btnExaminar_BSP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExaminar_BSP.Location = new System.Drawing.Point(610, 36);
            this.btnExaminar_BSP.Margin = new System.Windows.Forms.Padding(5);
            this.btnExaminar_BSP.Name = "btnExaminar_BSP";
            this.btnExaminar_BSP.Size = new System.Drawing.Size(125, 41);
            this.btnExaminar_BSP.TabIndex = 0;
            this.btnExaminar_BSP.Text = "Examinar...";
            this.btnExaminar_BSP.UseVisualStyleBackColor = true;
            this.btnExaminar_BSP.Click += new System.EventHandler(this.btnExaminar_BSP_Click);
            // 
            // txtFilePath_BSP
            // 
            this.txtFilePath_BSP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilePath_BSP.Location = new System.Drawing.Point(10, 42);
            this.txtFilePath_BSP.Margin = new System.Windows.Forms.Padding(5);
            this.txtFilePath_BSP.Name = "txtFilePath_BSP";
            this.txtFilePath_BSP.ReadOnly = true;
            this.txtFilePath_BSP.Size = new System.Drawing.Size(590, 31);
            this.txtFilePath_BSP.TabIndex = 1;
            // 
            // grbFileImport_BSP
            // 
            this.grbFileImport_BSP.Controls.Add(this.txtFilePath_BSP);
            this.grbFileImport_BSP.Controls.Add(this.btnExaminar_BSP);
            this.grbFileImport_BSP.Location = new System.Drawing.Point(5, 0);
            this.grbFileImport_BSP.Margin = new System.Windows.Forms.Padding(5);
            this.grbFileImport_BSP.Name = "grbFileImport_BSP";
            this.grbFileImport_BSP.Padding = new System.Windows.Forms.Padding(5);
            this.grbFileImport_BSP.Size = new System.Drawing.Size(745, 83);
            this.grbFileImport_BSP.TabIndex = 2;
            this.grbFileImport_BSP.TabStop = false;
            this.grbFileImport_BSP.Text = "Seleccione un archivo BSP...";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(8, 58);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(729, 29);
            this.progressBar1.TabIndex = 9;
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
            // grbProceso
            // 
            this.grbProceso.Controls.Add(this.lblProgressStatus);
            this.grbProceso.Controls.Add(this.progressBar1);
            this.grbProceso.Location = new System.Drawing.Point(4, 92);
            this.grbProceso.Margin = new System.Windows.Forms.Padding(4);
            this.grbProceso.Name = "grbProceso";
            this.grbProceso.Padding = new System.Windows.Forms.Padding(4);
            this.grbProceso.Size = new System.Drawing.Size(745, 95);
            this.grbProceso.TabIndex = 11;
            this.grbProceso.TabStop = false;
            this.grbProceso.Text = "Proceso...";
            // 
            // btnReadFile
            // 
            this.btnReadFile.Enabled = false;
            this.btnReadFile.Location = new System.Drawing.Point(261, 520);
            this.btnReadFile.Margin = new System.Windows.Forms.Padding(4);
            this.btnReadFile.Name = "btnReadFile";
            this.btnReadFile.Size = new System.Drawing.Size(237, 41);
            this.btnReadFile.TabIndex = 12;
            this.btnReadFile.Text = "Leer archivo";
            this.btnReadFile.UseVisualStyleBackColor = true;
            this.btnReadFile.Click += new System.EventHandler(this.btnReadFile_Click);
            // 
            // frmTestingBSP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.Controls.Add(this.btnReadFile);
            this.Controls.Add(this.grbProceso);
            this.Controls.Add(this.grbFileImport_BSP);
            this.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmTestingBSP";
            this.Size = new System.Drawing.Size(755, 718);
            this.grbFileImport_BSP.ResumeLayout(false);
            this.grbFileImport_BSP.PerformLayout();
            this.grbProceso.ResumeLayout(false);
            this.grbProceso.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExaminar_BSP;
        private System.Windows.Forms.TextBox txtFilePath_BSP;
        private System.Windows.Forms.GroupBox grbFileImport_BSP;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblProgressStatus;
        private System.Windows.Forms.GroupBox grbProceso;
        private System.Windows.Forms.Button btnReadFile;
    }
}
