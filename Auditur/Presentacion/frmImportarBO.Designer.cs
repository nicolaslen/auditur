namespace Auditur.Presentacion
{
    partial class frmImportarBO
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
            this.btnExaminar_BO = new System.Windows.Forms.Button();
            this.txtFilePath_BO = new System.Windows.Forms.TextBox();
            this.grbFileImport_BO = new System.Windows.Forms.GroupBox();
            this.grbDatos = new System.Windows.Forms.GroupBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btnReadFile = new System.Windows.Forms.Button();
            this.lblAgencia = new System.Windows.Forms.Label();
            this.dtpFechaHasta = new System.Windows.Forms.DateTimePicker();
            this.cboAgencia = new System.Windows.Forms.ComboBox();
            this.dtpFechaDesde = new System.Windows.Forms.DateTimePicker();
            this.lblPeriodo = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPeriodoS = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPeriodoA = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPeriodoM = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grbProceso = new System.Windows.Forms.GroupBox();
            this.lblProgressStatus = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.frmCurrents1 = new Auditur.Presentacion.frmCurrents();
            this.grbFileImport_BO.SuspendLayout();
            this.grbDatos.SuspendLayout();
            this.grbProceso.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExaminar_BO
            // 
            this.btnExaminar_BO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExaminar_BO.Location = new System.Drawing.Point(609, 32);
            this.btnExaminar_BO.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.btnExaminar_BO.Name = "btnExaminar_BO";
            this.btnExaminar_BO.Size = new System.Drawing.Size(125, 41);
            this.btnExaminar_BO.TabIndex = 1;
            this.btnExaminar_BO.Text = "Examinar...";
            this.btnExaminar_BO.UseVisualStyleBackColor = true;
            this.btnExaminar_BO.Click += new System.EventHandler(this.btnExaminar_BO_Click);
            // 
            // txtFilePath_BO
            // 
            this.txtFilePath_BO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilePath_BO.Location = new System.Drawing.Point(11, 37);
            this.txtFilePath_BO.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.txtFilePath_BO.Name = "txtFilePath_BO";
            this.txtFilePath_BO.ReadOnly = true;
            this.txtFilePath_BO.Size = new System.Drawing.Size(584, 31);
            this.txtFilePath_BO.TabIndex = 0;
            // 
            // grbFileImport_BO
            // 
            this.grbFileImport_BO.Controls.Add(this.txtFilePath_BO);
            this.grbFileImport_BO.Controls.Add(this.btnExaminar_BO);
            this.grbFileImport_BO.Location = new System.Drawing.Point(5, 5);
            this.grbFileImport_BO.Margin = new System.Windows.Forms.Padding(5);
            this.grbFileImport_BO.Name = "grbFileImport_BO";
            this.grbFileImport_BO.Padding = new System.Windows.Forms.Padding(5);
            this.grbFileImport_BO.Size = new System.Drawing.Size(745, 83);
            this.grbFileImport_BO.TabIndex = 0;
            this.grbFileImport_BO.TabStop = false;
            this.grbFileImport_BO.Text = "Seleccione un archivo Back Office...";
            // 
            // grbDatos
            // 
            this.grbDatos.Controls.Add(this.lblAgencia);
            this.grbDatos.Controls.Add(this.dtpFechaHasta);
            this.grbDatos.Controls.Add(this.cboAgencia);
            this.grbDatos.Controls.Add(this.dtpFechaDesde);
            this.grbDatos.Controls.Add(this.lblPeriodo);
            this.grbDatos.Controls.Add(this.label5);
            this.grbDatos.Controls.Add(this.txtPeriodoS);
            this.grbDatos.Controls.Add(this.label4);
            this.grbDatos.Controls.Add(this.txtPeriodoA);
            this.grbDatos.Controls.Add(this.label3);
            this.grbDatos.Controls.Add(this.txtPeriodoM);
            this.grbDatos.Controls.Add(this.label2);
            this.grbDatos.Controls.Add(this.label1);
            this.grbDatos.Location = new System.Drawing.Point(5, 97);
            this.grbDatos.Margin = new System.Windows.Forms.Padding(4);
            this.grbDatos.Name = "grbDatos";
            this.grbDatos.Padding = new System.Windows.Forms.Padding(4);
            this.grbDatos.Size = new System.Drawing.Size(745, 217);
            this.grbDatos.TabIndex = 1;
            this.grbDatos.TabStop = false;
            this.grbDatos.Text = "Complete los siguientes datos...";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // btnReadFile
            // 
            this.btnReadFile.Enabled = false;
            this.btnReadFile.Location = new System.Drawing.Point(260, 425);
            this.btnReadFile.Margin = new System.Windows.Forms.Padding(4);
            this.btnReadFile.Name = "btnReadFile";
            this.btnReadFile.Size = new System.Drawing.Size(238, 41);
            this.btnReadFile.TabIndex = 14;
            this.btnReadFile.Text = "Leer archivo";
            this.btnReadFile.UseVisualStyleBackColor = true;
            this.btnReadFile.Click += new System.EventHandler(this.btnReadFile_Click);
            // 
            // lblAgencia
            // 
            this.lblAgencia.AutoSize = true;
            this.lblAgencia.Location = new System.Drawing.Point(8, 55);
            this.lblAgencia.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAgencia.Name = "lblAgencia";
            this.lblAgencia.Size = new System.Drawing.Size(75, 23);
            this.lblAgencia.TabIndex = 13;
            this.lblAgencia.Text = "Agencia:";
            // 
            // dtpFechaHasta
            // 
            this.dtpFechaHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaHasta.Location = new System.Drawing.Point(557, 120);
            this.dtpFechaHasta.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaHasta.Name = "dtpFechaHasta";
            this.dtpFechaHasta.Size = new System.Drawing.Size(140, 31);
            this.dtpFechaHasta.TabIndex = 25;
            // 
            // cboAgencia
            // 
            this.cboAgencia.DisplayMember = "Nombre";
            this.cboAgencia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAgencia.FormattingEnabled = true;
            this.cboAgencia.Location = new System.Drawing.Point(92, 52);
            this.cboAgencia.Margin = new System.Windows.Forms.Padding(4);
            this.cboAgencia.Name = "cboAgencia";
            this.cboAgencia.Size = new System.Drawing.Size(270, 31);
            this.cboAgencia.TabIndex = 14;
            this.cboAgencia.ValueMember = "ID";
            // 
            // dtpFechaDesde
            // 
            this.dtpFechaDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaDesde.Location = new System.Drawing.Point(557, 48);
            this.dtpFechaDesde.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaDesde.Name = "dtpFechaDesde";
            this.dtpFechaDesde.Size = new System.Drawing.Size(140, 31);
            this.dtpFechaDesde.TabIndex = 23;
            // 
            // lblPeriodo
            // 
            this.lblPeriodo.AutoSize = true;
            this.lblPeriodo.Location = new System.Drawing.Point(8, 127);
            this.lblPeriodo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPeriodo.Name = "lblPeriodo";
            this.lblPeriodo.Size = new System.Drawing.Size(75, 23);
            this.lblPeriodo.TabIndex = 15;
            this.lblPeriodo.Text = "Período:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(445, 127);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 23);
            this.label5.TabIndex = 24;
            this.label5.Text = "Fecha hasta:";
            // 
            // txtPeriodoS
            // 
            this.txtPeriodoS.Location = new System.Drawing.Point(92, 156);
            this.txtPeriodoS.Margin = new System.Windows.Forms.Padding(4);
            this.txtPeriodoS.Name = "txtPeriodoS";
            this.txtPeriodoS.Size = new System.Drawing.Size(85, 31);
            this.txtPeriodoS.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(445, 55);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 23);
            this.label4.TabIndex = 22;
            this.label4.Text = "Fecha desde:";
            // 
            // txtPeriodoA
            // 
            this.txtPeriodoA.Location = new System.Drawing.Point(278, 156);
            this.txtPeriodoA.Margin = new System.Windows.Forms.Padding(4);
            this.txtPeriodoA.Name = "txtPeriodoA";
            this.txtPeriodoA.Size = new System.Drawing.Size(85, 31);
            this.txtPeriodoA.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(88, 129);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 23);
            this.label3.TabIndex = 16;
            this.label3.Text = "Semana";
            // 
            // txtPeriodoM
            // 
            this.txtPeriodoM.Location = new System.Drawing.Point(186, 156);
            this.txtPeriodoM.Margin = new System.Windows.Forms.Padding(4);
            this.txtPeriodoM.Name = "txtPeriodoM";
            this.txtPeriodoM.Size = new System.Drawing.Size(84, 31);
            this.txtPeriodoM.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(182, 129);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 23);
            this.label2.TabIndex = 18;
            this.label2.Text = "Mes";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(275, 129);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 23);
            this.label1.TabIndex = 20;
            this.label1.Text = "Año";
            // 
            // grbProceso
            // 
            this.grbProceso.Controls.Add(this.lblProgressStatus);
            this.grbProceso.Controls.Add(this.progressBar1);
            this.grbProceso.Location = new System.Drawing.Point(5, 322);
            this.grbProceso.Margin = new System.Windows.Forms.Padding(4);
            this.grbProceso.Name = "grbProceso";
            this.grbProceso.Padding = new System.Windows.Forms.Padding(4);
            this.grbProceso.Size = new System.Drawing.Size(746, 95);
            this.grbProceso.TabIndex = 16;
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
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(730, 29);
            this.progressBar1.TabIndex = 9;
            // 
            // frmCurrents1
            // 
            this.frmCurrents1.BackColor = System.Drawing.Color.Cornsilk;
            this.frmCurrents1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmCurrents1.Location = new System.Drawing.Point(5, 475);
            this.frmCurrents1.Margin = new System.Windows.Forms.Padding(5);
            this.frmCurrents1.Name = "frmCurrents1";
            this.frmCurrents1.ShowReportButton = true;
            this.frmCurrents1.Size = new System.Drawing.Size(745, 138);
            this.frmCurrents1.TabIndex = 15;
            // 
            // frmImportarBO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.Controls.Add(this.grbProceso);
            this.Controls.Add(this.frmCurrents1);
            this.Controls.Add(this.btnReadFile);
            this.Controls.Add(this.grbDatos);
            this.Controls.Add(this.grbFileImport_BO);
            this.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmImportarBO";
            this.Size = new System.Drawing.Size(755, 618);
            this.Load += new System.EventHandler(this.frmImportarBO_Load);
            this.grbFileImport_BO.ResumeLayout(false);
            this.grbFileImport_BO.PerformLayout();
            this.grbDatos.ResumeLayout(false);
            this.grbDatos.PerformLayout();
            this.grbProceso.ResumeLayout(false);
            this.grbProceso.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbFileImport_BO;
        private System.Windows.Forms.TextBox txtFilePath_BO;
        private System.Windows.Forms.Button btnExaminar_BO;
        private System.Windows.Forms.GroupBox grbDatos;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btnReadFile;
        private frmCurrents frmCurrents1;
        private System.Windows.Forms.Label lblAgencia;
        private System.Windows.Forms.DateTimePicker dtpFechaHasta;
        private System.Windows.Forms.ComboBox cboAgencia;
        private System.Windows.Forms.DateTimePicker dtpFechaDesde;
        private System.Windows.Forms.Label lblPeriodo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPeriodoS;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPeriodoA;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPeriodoM;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grbProceso;
        private System.Windows.Forms.Label lblProgressStatus;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}
