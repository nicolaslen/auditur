namespace Auditur.Presentacion
{
    partial class frmABMAgencias
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grbListadoAgencias = new System.Windows.Forms.GroupBox();
            this.btnCrearAgencia = new System.Windows.Forms.Button();
            this.dgvAgencias = new System.Windows.Forms.DataGridView();
            this.grbABMAgencias = new System.Windows.Forms.GroupBox();
            this.btnCancelarAgencia = new System.Windows.Forms.Button();
            this.btnGuardarAgencia = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNombreAgencia = new System.Windows.Forms.TextBox();
            this.txtAgenciaID = new System.Windows.Forms.TextBox();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.grbListadoAgencias.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAgencias)).BeginInit();
            this.grbABMAgencias.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbListadoAgencias
            // 
            this.grbListadoAgencias.Controls.Add(this.btnCrearAgencia);
            this.grbListadoAgencias.Controls.Add(this.dgvAgencias);
            this.grbListadoAgencias.Location = new System.Drawing.Point(4, 4);
            this.grbListadoAgencias.Margin = new System.Windows.Forms.Padding(4);
            this.grbListadoAgencias.Name = "grbListadoAgencias";
            this.grbListadoAgencias.Padding = new System.Windows.Forms.Padding(4);
            this.grbListadoAgencias.Size = new System.Drawing.Size(747, 391);
            this.grbListadoAgencias.TabIndex = 0;
            this.grbListadoAgencias.TabStop = false;
            this.grbListadoAgencias.Text = "ABM de Agencias...";
            // 
            // btnCrearAgencia
            // 
            this.btnCrearAgencia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCrearAgencia.Location = new System.Drawing.Point(605, 33);
            this.btnCrearAgencia.Name = "btnCrearAgencia";
            this.btnCrearAgencia.Size = new System.Drawing.Size(134, 36);
            this.btnCrearAgencia.TabIndex = 1;
            this.btnCrearAgencia.Text = "Nueva";
            this.btnCrearAgencia.UseVisualStyleBackColor = true;
            this.btnCrearAgencia.Click += new System.EventHandler(this.btnCrearAgencia_Click);
            // 
            // dgvAgencias
            // 
            this.dgvAgencias.AllowUserToAddRows = false;
            this.dgvAgencias.AllowUserToDeleteRows = false;
            this.dgvAgencias.AllowUserToOrderColumns = true;
            this.dgvAgencias.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAgencias.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAgencias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAgencias.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Nombre,
            this.Edit});
            this.dgvAgencias.Location = new System.Drawing.Point(8, 74);
            this.dgvAgencias.Margin = new System.Windows.Forms.Padding(4);
            this.dgvAgencias.Name = "dgvAgencias";
            this.dgvAgencias.ReadOnly = true;
            this.dgvAgencias.RowHeadersVisible = false;
            this.dgvAgencias.Size = new System.Drawing.Size(731, 309);
            this.dgvAgencias.TabIndex = 2;
            this.dgvAgencias.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAgencias_CellContentClick);
            // 
            // grbABMAgencias
            // 
            this.grbABMAgencias.Controls.Add(this.btnCancelarAgencia);
            this.grbABMAgencias.Controls.Add(this.btnGuardarAgencia);
            this.grbABMAgencias.Controls.Add(this.label3);
            this.grbABMAgencias.Controls.Add(this.label2);
            this.grbABMAgencias.Controls.Add(this.txtNombreAgencia);
            this.grbABMAgencias.Controls.Add(this.txtAgenciaID);
            this.grbABMAgencias.Enabled = false;
            this.grbABMAgencias.Location = new System.Drawing.Point(4, 402);
            this.grbABMAgencias.Name = "grbABMAgencias";
            this.grbABMAgencias.Size = new System.Drawing.Size(747, 186);
            this.grbABMAgencias.TabIndex = 1;
            this.grbABMAgencias.TabStop = false;
            // 
            // btnCancelarAgencia
            // 
            this.btnCancelarAgencia.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancelarAgencia.Location = new System.Drawing.Point(376, 140);
            this.btnCancelarAgencia.Name = "btnCancelarAgencia";
            this.btnCancelarAgencia.Size = new System.Drawing.Size(134, 36);
            this.btnCancelarAgencia.TabIndex = 5;
            this.btnCancelarAgencia.Text = "Cancelar";
            this.btnCancelarAgencia.UseVisualStyleBackColor = true;
            this.btnCancelarAgencia.Click += new System.EventHandler(this.btnCancelarAgencia_Click);
            // 
            // btnGuardarAgencia
            // 
            this.btnGuardarAgencia.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnGuardarAgencia.Location = new System.Drawing.Point(236, 140);
            this.btnGuardarAgencia.Name = "btnGuardarAgencia";
            this.btnGuardarAgencia.Size = new System.Drawing.Size(134, 36);
            this.btnGuardarAgencia.TabIndex = 4;
            this.btnGuardarAgencia.Text = "Guardar";
            this.btnGuardarAgencia.UseVisualStyleBackColor = true;
            this.btnGuardarAgencia.Click += new System.EventHandler(this.btnGuardarAgencia_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "Nombre:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "IATA Número:";
            // 
            // txtNombreAgencia
            // 
            this.txtNombreAgencia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNombreAgencia.Location = new System.Drawing.Point(130, 85);
            this.txtNombreAgencia.Name = "txtNombreAgencia";
            this.txtNombreAgencia.Size = new System.Drawing.Size(609, 31);
            this.txtNombreAgencia.TabIndex = 3;
            // 
            // txtAgenciaID
            // 
            this.txtAgenciaID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAgenciaID.Location = new System.Drawing.Point(130, 48);
            this.txtAgenciaID.Name = "txtAgenciaID";
            this.txtAgenciaID.Size = new System.Drawing.Size(609, 31);
            this.txtAgenciaID.TabIndex = 1;
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.FillWeight = 40.80819F;
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            // 
            // Nombre
            // 
            this.Nombre.DataPropertyName = "Nombre";
            this.Nombre.FillWeight = 98.96908F;
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.Name = "Nombre";
            this.Nombre.ReadOnly = true;
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.FillWeight = 21.00698F;
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.Image = global::Auditur.Presentacion.Properties.Resources.Modificar;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.ReadOnly = true;
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewImageColumn1.Width = 95;
            // 
            // Edit
            // 
            this.Edit.FillWeight = 21.00698F;
            this.Edit.HeaderText = "";
            this.Edit.Image = global::Auditur.Presentacion.Properties.Resources.Modificar;
            this.Edit.Name = "Edit";
            this.Edit.ReadOnly = true;
            this.Edit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // frmABMAgencias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.Controls.Add(this.grbABMAgencias);
            this.Controls.Add(this.grbListadoAgencias);
            this.Font = new System.Drawing.Font("Calibri", 14.25F);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmABMAgencias";
            this.Size = new System.Drawing.Size(755, 618);
            this.Load += new System.EventHandler(this.frmABMAgencias_Load);
            this.grbListadoAgencias.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAgencias)).EndInit();
            this.grbABMAgencias.ResumeLayout(false);
            this.grbABMAgencias.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbListadoAgencias;
        private System.Windows.Forms.DataGridView dgvAgencias;
        private System.Windows.Forms.Button btnCrearAgencia;
        private System.Windows.Forms.GroupBox grbABMAgencias;
        private System.Windows.Forms.Button btnCancelarAgencia;
        private System.Windows.Forms.Button btnGuardarAgencia;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNombreAgencia;
        private System.Windows.Forms.TextBox txtAgenciaID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewImageColumn Edit;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
    }
}
