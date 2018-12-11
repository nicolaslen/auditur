namespace Auditur.Presentacion
{
    partial class frmElegirSemana
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
            this.grbDatos = new System.Windows.Forms.GroupBox();
            this.btnCargar = new System.Windows.Forms.Button();
            this.cboAno = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboAgencia = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvSemanas = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Periodo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaDesde = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaHasta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BSPLoaded = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.BOLoaded = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnReport = new System.Windows.Forms.DataGridViewButtonColumn();
            this.grbSemanasCargadas = new System.Windows.Forms.GroupBox();
            this.lblAño = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblAgencia = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grbDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSemanas)).BeginInit();
            this.grbSemanasCargadas.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbDatos
            // 
            this.grbDatos.Controls.Add(this.btnCargar);
            this.grbDatos.Controls.Add(this.cboAno);
            this.grbDatos.Controls.Add(this.label5);
            this.grbDatos.Controls.Add(this.cboAgencia);
            this.grbDatos.Controls.Add(this.label2);
            this.grbDatos.Location = new System.Drawing.Point(4, 4);
            this.grbDatos.Margin = new System.Windows.Forms.Padding(4);
            this.grbDatos.Name = "grbDatos";
            this.grbDatos.Padding = new System.Windows.Forms.Padding(4);
            this.grbDatos.Size = new System.Drawing.Size(748, 93);
            this.grbDatos.TabIndex = 0;
            this.grbDatos.TabStop = false;
            this.grbDatos.Text = "Elija agencia y año para ver los datos ya cargados...";
            // 
            // btnCargar
            // 
            this.btnCargar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCargar.Location = new System.Drawing.Point(619, 36);
            this.btnCargar.Margin = new System.Windows.Forms.Padding(4);
            this.btnCargar.Name = "btnCargar";
            this.btnCargar.Size = new System.Drawing.Size(111, 42);
            this.btnCargar.TabIndex = 3;
            this.btnCargar.Text = "Cargar";
            this.btnCargar.UseVisualStyleBackColor = true;
            this.btnCargar.Click += new System.EventHandler(this.btnCargar_Click);
            // 
            // cboAno
            // 
            this.cboAno.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboAno.DisplayMember = "Nombre";
            this.cboAno.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAno.FormattingEnabled = true;
            this.cboAno.Location = new System.Drawing.Point(426, 41);
            this.cboAno.Margin = new System.Windows.Forms.Padding(4);
            this.cboAno.Name = "cboAno";
            this.cboAno.Size = new System.Drawing.Size(146, 31);
            this.cboAno.TabIndex = 2;
            this.cboAno.ValueMember = "ID";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(372, 45);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 23);
            this.label5.TabIndex = 2;
            this.label5.Text = "Año:";
            // 
            // cboAgencia
            // 
            this.cboAgencia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboAgencia.DisplayMember = "Nombre";
            this.cboAgencia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAgencia.FormattingEnabled = true;
            this.cboAgencia.Location = new System.Drawing.Point(91, 41);
            this.cboAgencia.Margin = new System.Windows.Forms.Padding(4);
            this.cboAgencia.Name = "cboAgencia";
            this.cboAgencia.Size = new System.Drawing.Size(273, 31);
            this.cboAgencia.TabIndex = 1;
            this.cboAgencia.ValueMember = "ID";
            this.cboAgencia.SelectedIndexChanged += new System.EventHandler(this.cboAgencia_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 45);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Agencia:";
            // 
            // dgvSemanas
            // 
            this.dgvSemanas.AllowUserToAddRows = false;
            this.dgvSemanas.AllowUserToDeleteRows = false;
            this.dgvSemanas.AllowUserToOrderColumns = true;
            this.dgvSemanas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSemanas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSemanas.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dgvSemanas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSemanas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Periodo,
            this.FechaDesde,
            this.FechaHasta,
            this.BSPLoaded,
            this.BOLoaded,
            this.btnReport});
            this.dgvSemanas.Location = new System.Drawing.Point(8, 88);
            this.dgvSemanas.Margin = new System.Windows.Forms.Padding(4);
            this.dgvSemanas.Name = "dgvSemanas";
            this.dgvSemanas.ReadOnly = true;
            this.dgvSemanas.RowHeadersVisible = false;
            this.dgvSemanas.Size = new System.Drawing.Size(732, 381);
            this.dgvSemanas.TabIndex = 0;
            this.dgvSemanas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSemanas_CellContentClick);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // Periodo
            // 
            this.Periodo.DataPropertyName = "Periodo";
            this.Periodo.HeaderText = "Período";
            this.Periodo.Name = "Periodo";
            this.Periodo.ReadOnly = true;
            // 
            // FechaDesde
            // 
            this.FechaDesde.DataPropertyName = "FechaDesde";
            this.FechaDesde.HeaderText = "Fecha desde";
            this.FechaDesde.Name = "FechaDesde";
            this.FechaDesde.ReadOnly = true;
            // 
            // FechaHasta
            // 
            this.FechaHasta.DataPropertyName = "FechaHasta";
            this.FechaHasta.HeaderText = "Fecha hasta";
            this.FechaHasta.Name = "FechaHasta";
            this.FechaHasta.ReadOnly = true;
            // 
            // BSPLoaded
            // 
            this.BSPLoaded.DataPropertyName = "BSPCargado";
            this.BSPLoaded.FalseValue = "False";
            this.BSPLoaded.HeaderText = "BSP Cargado";
            this.BSPLoaded.Name = "BSPLoaded";
            this.BSPLoaded.ReadOnly = true;
            this.BSPLoaded.TrueValue = "True";
            // 
            // BOLoaded
            // 
            this.BOLoaded.DataPropertyName = "BOCargado";
            this.BOLoaded.HeaderText = "BO Cargado";
            this.BOLoaded.Name = "BOLoaded";
            this.BOLoaded.ReadOnly = true;
            // 
            // btnReport
            // 
            this.btnReport.HeaderText = "";
            this.btnReport.Name = "btnReport";
            this.btnReport.ReadOnly = true;
            this.btnReport.Text = "Reportar";
            this.btnReport.UseColumnTextForButtonValue = true;
            // 
            // grbSemanasCargadas
            // 
            this.grbSemanasCargadas.Controls.Add(this.lblAño);
            this.grbSemanasCargadas.Controls.Add(this.label3);
            this.grbSemanasCargadas.Controls.Add(this.lblAgencia);
            this.grbSemanasCargadas.Controls.Add(this.label1);
            this.grbSemanasCargadas.Controls.Add(this.dgvSemanas);
            this.grbSemanasCargadas.Location = new System.Drawing.Point(4, 105);
            this.grbSemanasCargadas.Margin = new System.Windows.Forms.Padding(4);
            this.grbSemanasCargadas.Name = "grbSemanasCargadas";
            this.grbSemanasCargadas.Padding = new System.Windows.Forms.Padding(4);
            this.grbSemanasCargadas.Size = new System.Drawing.Size(748, 477);
            this.grbSemanasCargadas.TabIndex = 1;
            this.grbSemanasCargadas.TabStop = false;
            this.grbSemanasCargadas.Text = "Semanas cargadas...";
            // 
            // lblAño
            // 
            this.lblAño.AutoSize = true;
            this.lblAño.Location = new System.Drawing.Point(449, 46);
            this.lblAño.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAño.Name = "lblAño";
            this.lblAño.Size = new System.Drawing.Size(0, 23);
            this.lblAño.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(365, 46);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 23);
            this.label3.TabIndex = 9;
            this.label3.Text = "Año:";
            // 
            // lblAgencia
            // 
            this.lblAgencia.AutoSize = true;
            this.lblAgencia.Location = new System.Drawing.Point(91, 46);
            this.lblAgencia.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAgencia.Name = "lblAgencia";
            this.lblAgencia.Size = new System.Drawing.Size(0, 23);
            this.lblAgencia.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 46);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "Agencia:";
            // 
            // frmElegirSemana
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.Controls.Add(this.grbSemanasCargadas);
            this.Controls.Add(this.grbDatos);
            this.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmElegirSemana";
            this.Size = new System.Drawing.Size(755, 589);
            this.Load += new System.EventHandler(this.frmElegirBSP_Load);
            this.grbDatos.ResumeLayout(false);
            this.grbDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSemanas)).EndInit();
            this.grbSemanasCargadas.ResumeLayout(false);
            this.grbSemanasCargadas.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbDatos;
        private System.Windows.Forms.ComboBox cboAgencia;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvSemanas;
        private System.Windows.Forms.Button btnCargar;
        private System.Windows.Forms.ComboBox cboAno;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox grbSemanasCargadas;
        private System.Windows.Forms.Label lblAño;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblAgencia;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Periodo;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaDesde;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaHasta;
        private System.Windows.Forms.DataGridViewCheckBoxColumn BSPLoaded;
        private System.Windows.Forms.DataGridViewCheckBoxColumn BOLoaded;
        private System.Windows.Forms.DataGridViewButtonColumn btnReport;

    }
}
