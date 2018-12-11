namespace Auditur.Presentacion
{
    partial class frmImportarBSP
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
            this.grbDatos = new System.Windows.Forms.GroupBox();
            this.txtAgencia = new System.Windows.Forms.TextBox();
            this.lblAgencia = new System.Windows.Forms.Label();
            this.dtpFechaHasta = new System.Windows.Forms.DateTimePicker();
            this.lblPeriodo = new System.Windows.Forms.Label();
            this.dtpFechaDesde = new System.Windows.Forms.DateTimePicker();
            this.txtPeriodoS = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPeriodoA = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPeriodoM = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblProgressStatus = new System.Windows.Forms.Label();
            this.grbProceso = new System.Windows.Forms.GroupBox();
            this.btnReadFile = new System.Windows.Forms.Button();
            this.frmCurrents1 = new Auditur.Presentacion.frmCurrents();
            this.grbACM = new System.Windows.Forms.GroupBox();
            this.txtFilePath_ACM = new System.Windows.Forms.TextBox();
            this.btnExaminar_ACM = new System.Windows.Forms.Button();
            this.grbADM = new System.Windows.Forms.GroupBox();
            this.txtFilePath_ADM = new System.Windows.Forms.TextBox();
            this.btnExaminar_ADM = new System.Windows.Forms.Button();
            this.grbFileImport_BSP.SuspendLayout();
            this.grbDatos.SuspendLayout();
            this.grbProceso.SuspendLayout();
            this.grbACM.SuspendLayout();
            this.grbADM.SuspendLayout();
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
            // grbDatos
            // 
            this.grbDatos.Controls.Add(this.txtAgencia);
            this.grbDatos.Controls.Add(this.lblAgencia);
            this.grbDatos.Controls.Add(this.dtpFechaHasta);
            this.grbDatos.Controls.Add(this.lblPeriodo);
            this.grbDatos.Controls.Add(this.dtpFechaDesde);
            this.grbDatos.Controls.Add(this.txtPeriodoS);
            this.grbDatos.Controls.Add(this.label5);
            this.grbDatos.Controls.Add(this.txtPeriodoA);
            this.grbDatos.Controls.Add(this.label4);
            this.grbDatos.Controls.Add(this.txtPeriodoM);
            this.grbDatos.Controls.Add(this.label3);
            this.grbDatos.Controls.Add(this.label1);
            this.grbDatos.Controls.Add(this.label2);
            this.grbDatos.Location = new System.Drawing.Point(5, 92);
            this.grbDatos.Margin = new System.Windows.Forms.Padding(4);
            this.grbDatos.Name = "grbDatos";
            this.grbDatos.Padding = new System.Windows.Forms.Padding(4);
            this.grbDatos.Size = new System.Drawing.Size(745, 217);
            this.grbDatos.TabIndex = 4;
            this.grbDatos.TabStop = false;
            this.grbDatos.Text = "Complete los siguientes datos...";
            // 
            // txtAgencia
            // 
            this.txtAgencia.Enabled = false;
            this.txtAgencia.Location = new System.Drawing.Point(92, 48);
            this.txtAgencia.Margin = new System.Windows.Forms.Padding(4);
            this.txtAgencia.Name = "txtAgencia";
            this.txtAgencia.Size = new System.Drawing.Size(270, 31);
            this.txtAgencia.TabIndex = 26;
            // 
            // lblAgencia
            // 
            this.lblAgencia.AutoSize = true;
            this.lblAgencia.Location = new System.Drawing.Point(8, 51);
            this.lblAgencia.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAgencia.Name = "lblAgencia";
            this.lblAgencia.Size = new System.Drawing.Size(75, 23);
            this.lblAgencia.TabIndex = 14;
            this.lblAgencia.Text = "Agencia:";
            // 
            // dtpFechaHasta
            // 
            this.dtpFechaHasta.Enabled = false;
            this.dtpFechaHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaHasta.Location = new System.Drawing.Point(569, 117);
            this.dtpFechaHasta.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaHasta.Name = "dtpFechaHasta";
            this.dtpFechaHasta.Size = new System.Drawing.Size(140, 31);
            this.dtpFechaHasta.TabIndex = 25;
            // 
            // lblPeriodo
            // 
            this.lblPeriodo.AutoSize = true;
            this.lblPeriodo.Location = new System.Drawing.Point(8, 123);
            this.lblPeriodo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPeriodo.Name = "lblPeriodo";
            this.lblPeriodo.Size = new System.Drawing.Size(75, 23);
            this.lblPeriodo.TabIndex = 15;
            this.lblPeriodo.Text = "Período:";
            // 
            // dtpFechaDesde
            // 
            this.dtpFechaDesde.Enabled = false;
            this.dtpFechaDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaDesde.Location = new System.Drawing.Point(569, 44);
            this.dtpFechaDesde.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaDesde.Name = "dtpFechaDesde";
            this.dtpFechaDesde.Size = new System.Drawing.Size(140, 31);
            this.dtpFechaDesde.TabIndex = 24;
            // 
            // txtPeriodoS
            // 
            this.txtPeriodoS.Enabled = false;
            this.txtPeriodoS.Location = new System.Drawing.Point(92, 152);
            this.txtPeriodoS.Margin = new System.Windows.Forms.Padding(4);
            this.txtPeriodoS.Name = "txtPeriodoS";
            this.txtPeriodoS.Size = new System.Drawing.Size(85, 31);
            this.txtPeriodoS.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(449, 123);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 23);
            this.label5.TabIndex = 23;
            this.label5.Text = "Fecha hasta:";
            // 
            // txtPeriodoA
            // 
            this.txtPeriodoA.Enabled = false;
            this.txtPeriodoA.Location = new System.Drawing.Point(278, 152);
            this.txtPeriodoA.Margin = new System.Windows.Forms.Padding(4);
            this.txtPeriodoA.Name = "txtPeriodoA";
            this.txtPeriodoA.Size = new System.Drawing.Size(85, 31);
            this.txtPeriodoA.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(449, 51);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 23);
            this.label4.TabIndex = 22;
            this.label4.Text = "Fecha desde:";
            // 
            // txtPeriodoM
            // 
            this.txtPeriodoM.Enabled = false;
            this.txtPeriodoM.Location = new System.Drawing.Point(185, 152);
            this.txtPeriodoM.Margin = new System.Windows.Forms.Padding(4);
            this.txtPeriodoM.Name = "txtPeriodoM";
            this.txtPeriodoM.Size = new System.Drawing.Size(84, 31);
            this.txtPeriodoM.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(88, 125);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 23);
            this.label3.TabIndex = 21;
            this.label3.Text = "Semana";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(274, 125);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 23);
            this.label1.TabIndex = 19;
            this.label1.Text = "Año";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(182, 125);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 23);
            this.label2.TabIndex = 20;
            this.label2.Text = "Mes";
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
            this.grbProceso.Location = new System.Drawing.Point(5, 417);
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
            // frmCurrents1
            // 
            this.frmCurrents1.BackColor = System.Drawing.Color.Cornsilk;
            this.frmCurrents1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmCurrents1.Location = new System.Drawing.Point(5, 570);
            this.frmCurrents1.Margin = new System.Windows.Forms.Padding(5);
            this.frmCurrents1.Name = "frmCurrents1";
            this.frmCurrents1.ShowReportButton = true;
            this.frmCurrents1.Size = new System.Drawing.Size(745, 143);
            this.frmCurrents1.TabIndex = 13;
            // 
            // grbACM
            // 
            this.grbACM.Controls.Add(this.txtFilePath_ACM);
            this.grbACM.Controls.Add(this.btnExaminar_ACM);
            this.grbACM.Location = new System.Drawing.Point(5, 318);
            this.grbACM.Margin = new System.Windows.Forms.Padding(5);
            this.grbACM.Name = "grbACM";
            this.grbACM.Padding = new System.Windows.Forms.Padding(5);
            this.grbACM.Size = new System.Drawing.Size(384, 83);
            this.grbACM.TabIndex = 14;
            this.grbACM.TabStop = false;
            this.grbACM.Text = "Seleccione un archivo ACM...";
            // 
            // txtFilePath_ACM
            // 
            this.txtFilePath_ACM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilePath_ACM.Location = new System.Drawing.Point(10, 42);
            this.txtFilePath_ACM.Margin = new System.Windows.Forms.Padding(5);
            this.txtFilePath_ACM.Name = "txtFilePath_ACM";
            this.txtFilePath_ACM.ReadOnly = true;
            this.txtFilePath_ACM.Size = new System.Drawing.Size(229, 31);
            this.txtFilePath_ACM.TabIndex = 1;
            // 
            // btnExaminar_ACM
            // 
            this.btnExaminar_ACM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExaminar_ACM.Enabled = false;
            this.btnExaminar_ACM.Location = new System.Drawing.Point(249, 36);
            this.btnExaminar_ACM.Margin = new System.Windows.Forms.Padding(5);
            this.btnExaminar_ACM.Name = "btnExaminar_ACM";
            this.btnExaminar_ACM.Size = new System.Drawing.Size(125, 41);
            this.btnExaminar_ACM.TabIndex = 0;
            this.btnExaminar_ACM.Text = "Examinar...";
            this.btnExaminar_ACM.UseVisualStyleBackColor = true;
            this.btnExaminar_ACM.Click += new System.EventHandler(this.btnExaminar_ACM_Click);
            // 
            // grbADM
            // 
            this.grbADM.Controls.Add(this.txtFilePath_ADM);
            this.grbADM.Controls.Add(this.btnExaminar_ADM);
            this.grbADM.Location = new System.Drawing.Point(399, 318);
            this.grbADM.Margin = new System.Windows.Forms.Padding(5);
            this.grbADM.Name = "grbADM";
            this.grbADM.Padding = new System.Windows.Forms.Padding(5);
            this.grbADM.Size = new System.Drawing.Size(351, 83);
            this.grbADM.TabIndex = 15;
            this.grbADM.TabStop = false;
            this.grbADM.Text = "Seleccione un archivo ADM...";
            // 
            // txtFilePath_ADM
            // 
            this.txtFilePath_ADM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilePath_ADM.Location = new System.Drawing.Point(10, 42);
            this.txtFilePath_ADM.Margin = new System.Windows.Forms.Padding(5);
            this.txtFilePath_ADM.Name = "txtFilePath_ADM";
            this.txtFilePath_ADM.ReadOnly = true;
            this.txtFilePath_ADM.Size = new System.Drawing.Size(196, 31);
            this.txtFilePath_ADM.TabIndex = 1;
            // 
            // btnExaminar_ADM
            // 
            this.btnExaminar_ADM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExaminar_ADM.Enabled = false;
            this.btnExaminar_ADM.Location = new System.Drawing.Point(216, 36);
            this.btnExaminar_ADM.Margin = new System.Windows.Forms.Padding(5);
            this.btnExaminar_ADM.Name = "btnExaminar_ADM";
            this.btnExaminar_ADM.Size = new System.Drawing.Size(125, 41);
            this.btnExaminar_ADM.TabIndex = 0;
            this.btnExaminar_ADM.Text = "Examinar...";
            this.btnExaminar_ADM.UseVisualStyleBackColor = true;
            this.btnExaminar_ADM.Click += new System.EventHandler(this.btnExaminar_ADM_Click);
            // 
            // frmImportarBSP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.Controls.Add(this.grbADM);
            this.Controls.Add(this.grbACM);
            this.Controls.Add(this.frmCurrents1);
            this.Controls.Add(this.btnReadFile);
            this.Controls.Add(this.grbProceso);
            this.Controls.Add(this.grbDatos);
            this.Controls.Add(this.grbFileImport_BSP);
            this.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmImportarBSP";
            this.Size = new System.Drawing.Size(755, 718);
            this.grbFileImport_BSP.ResumeLayout(false);
            this.grbFileImport_BSP.PerformLayout();
            this.grbDatos.ResumeLayout(false);
            this.grbDatos.PerformLayout();
            this.grbProceso.ResumeLayout(false);
            this.grbProceso.PerformLayout();
            this.grbACM.ResumeLayout(false);
            this.grbACM.PerformLayout();
            this.grbADM.ResumeLayout(false);
            this.grbADM.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExaminar_BSP;
        private System.Windows.Forms.TextBox txtFilePath_BSP;
        private System.Windows.Forms.GroupBox grbFileImport_BSP;
        private System.Windows.Forms.GroupBox grbDatos;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblProgressStatus;
        private System.Windows.Forms.GroupBox grbProceso;
        private System.Windows.Forms.Button btnReadFile;
        private frmCurrents frmCurrents1;
        private System.Windows.Forms.TextBox txtAgencia;
        private System.Windows.Forms.Label lblAgencia;
        private System.Windows.Forms.DateTimePicker dtpFechaHasta;
        private System.Windows.Forms.Label lblPeriodo;
        private System.Windows.Forms.DateTimePicker dtpFechaDesde;
        private System.Windows.Forms.TextBox txtPeriodoS;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPeriodoA;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPeriodoM;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grbACM;
        private System.Windows.Forms.TextBox txtFilePath_ACM;
        private System.Windows.Forms.Button btnExaminar_ACM;
        private System.Windows.Forms.GroupBox grbADM;
        private System.Windows.Forms.TextBox txtFilePath_ADM;
        private System.Windows.Forms.Button btnExaminar_ADM;
    }
}
