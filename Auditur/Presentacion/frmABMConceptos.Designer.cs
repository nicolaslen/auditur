namespace Auditur.Presentacion
{
    partial class frmABMConceptos
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
            this.grbABMConceptos = new System.Windows.Forms.GroupBox();
            this.btnCancelarConcepto = new System.Windows.Forms.Button();
            this.btnGuardarConcepto = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNombreConcepto = new System.Windows.Forms.TextBox();
            this.txtConceptoID = new System.Windows.Forms.TextBox();
            this.grbListadoConceptos = new System.Windows.Forms.GroupBox();
            this.btnCrearConcepto = new System.Windows.Forms.Button();
            this.dgvConceptos = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.cboTipo = new System.Windows.Forms.ComboBox();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.grbABMConceptos.SuspendLayout();
            this.grbListadoConceptos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConceptos)).BeginInit();
            this.SuspendLayout();
            // 
            // grbABMConceptos
            // 
            this.grbABMConceptos.Controls.Add(this.cboTipo);
            this.grbABMConceptos.Controls.Add(this.label1);
            this.grbABMConceptos.Controls.Add(this.btnCancelarConcepto);
            this.grbABMConceptos.Controls.Add(this.btnGuardarConcepto);
            this.grbABMConceptos.Controls.Add(this.label3);
            this.grbABMConceptos.Controls.Add(this.label2);
            this.grbABMConceptos.Controls.Add(this.txtNombreConcepto);
            this.grbABMConceptos.Controls.Add(this.txtConceptoID);
            this.grbABMConceptos.Enabled = false;
            this.grbABMConceptos.Location = new System.Drawing.Point(4, 362);
            this.grbABMConceptos.Name = "grbABMConceptos";
            this.grbABMConceptos.Size = new System.Drawing.Size(747, 226);
            this.grbABMConceptos.TabIndex = 1;
            this.grbABMConceptos.TabStop = false;
            // 
            // btnCancelarConcepto
            // 
            this.btnCancelarConcepto.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancelarConcepto.Location = new System.Drawing.Point(376, 180);
            this.btnCancelarConcepto.Name = "btnCancelarConcepto";
            this.btnCancelarConcepto.Size = new System.Drawing.Size(134, 36);
            this.btnCancelarConcepto.TabIndex = 7;
            this.btnCancelarConcepto.Text = "Cancelar";
            this.btnCancelarConcepto.UseVisualStyleBackColor = true;
            this.btnCancelarConcepto.Click += new System.EventHandler(this.btnCancelarConcepto_Click);
            // 
            // btnGuardarConcepto
            // 
            this.btnGuardarConcepto.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnGuardarConcepto.Location = new System.Drawing.Point(236, 180);
            this.btnGuardarConcepto.Name = "btnGuardarConcepto";
            this.btnGuardarConcepto.Size = new System.Drawing.Size(134, 36);
            this.btnGuardarConcepto.TabIndex = 6;
            this.btnGuardarConcepto.Text = "Guardar";
            this.btnGuardarConcepto.UseVisualStyleBackColor = true;
            this.btnGuardarConcepto.Click += new System.EventHandler(this.btnGuardarConcepto_Click);
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
            this.label2.Location = new System.Drawing.Point(10, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "ID:";
            // 
            // txtNombreConcepto
            // 
            this.txtNombreConcepto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNombreConcepto.Location = new System.Drawing.Point(130, 85);
            this.txtNombreConcepto.Name = "txtNombreConcepto";
            this.txtNombreConcepto.Size = new System.Drawing.Size(609, 31);
            this.txtNombreConcepto.TabIndex = 3;
            // 
            // txtConceptoID
            // 
            this.txtConceptoID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConceptoID.Location = new System.Drawing.Point(130, 48);
            this.txtConceptoID.Name = "txtConceptoID";
            this.txtConceptoID.ReadOnly = true;
            this.txtConceptoID.Size = new System.Drawing.Size(609, 31);
            this.txtConceptoID.TabIndex = 1;
            // 
            // grbListadoConceptos
            // 
            this.grbListadoConceptos.Controls.Add(this.btnCrearConcepto);
            this.grbListadoConceptos.Controls.Add(this.dgvConceptos);
            this.grbListadoConceptos.Location = new System.Drawing.Point(4, 4);
            this.grbListadoConceptos.Margin = new System.Windows.Forms.Padding(4);
            this.grbListadoConceptos.Name = "grbListadoConceptos";
            this.grbListadoConceptos.Padding = new System.Windows.Forms.Padding(4);
            this.grbListadoConceptos.Size = new System.Drawing.Size(747, 351);
            this.grbListadoConceptos.TabIndex = 0;
            this.grbListadoConceptos.TabStop = false;
            this.grbListadoConceptos.Text = "ABM de Conceptos...";
            // 
            // btnCrearConcepto
            // 
            this.btnCrearConcepto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCrearConcepto.Location = new System.Drawing.Point(605, 33);
            this.btnCrearConcepto.Name = "btnCrearConcepto";
            this.btnCrearConcepto.Size = new System.Drawing.Size(134, 36);
            this.btnCrearConcepto.TabIndex = 0;
            this.btnCrearConcepto.Text = "Nuevo";
            this.btnCrearConcepto.UseVisualStyleBackColor = true;
            this.btnCrearConcepto.Click += new System.EventHandler(this.btnCrearConcepto_Click);
            // 
            // dgvConceptos
            // 
            this.dgvConceptos.AllowUserToAddRows = false;
            this.dgvConceptos.AllowUserToDeleteRows = false;
            this.dgvConceptos.AllowUserToOrderColumns = true;
            this.dgvConceptos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvConceptos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvConceptos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConceptos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Nombre,
            this.Tipo,
            this.Edit});
            this.dgvConceptos.Location = new System.Drawing.Point(8, 74);
            this.dgvConceptos.Margin = new System.Windows.Forms.Padding(4);
            this.dgvConceptos.Name = "dgvConceptos";
            this.dgvConceptos.ReadOnly = true;
            this.dgvConceptos.RowHeadersVisible = false;
            this.dgvConceptos.Size = new System.Drawing.Size(731, 269);
            this.dgvConceptos.TabIndex = 1;
            this.dgvConceptos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvConceptos_CellContentClick);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.FillWeight = 59.46922F;
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            // 
            // Nombre
            // 
            this.Nombre.DataPropertyName = "Nombre";
            this.Nombre.FillWeight = 144.2263F;
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.Name = "Nombre";
            this.Nombre.ReadOnly = true;
            // 
            // Tipo
            // 
            this.Tipo.DataPropertyName = "Tipo";
            this.Tipo.FillWeight = 26.47556F;
            this.Tipo.HeaderText = "Tipo";
            this.Tipo.Name = "Tipo";
            this.Tipo.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "Tipo:";
            // 
            // cboTipo
            // 
            this.cboTipo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipo.FormattingEnabled = true;
            this.cboTipo.Items.AddRange(new object[] {
            "Billete",
            "Reembolso",
            "Crédito",
            "Débito",
            "Surface"});
            this.cboTipo.Location = new System.Drawing.Point(130, 123);
            this.cboTipo.Name = "cboTipo";
            this.cboTipo.Size = new System.Drawing.Size(609, 31);
            this.cboTipo.TabIndex = 5;
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.FillWeight = 21.00698F;
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.Image = global::Auditur.Presentacion.Properties.Resources.Modificar;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewImageColumn1.Width = 95;
            // 
            // Edit
            // 
            this.Edit.FillWeight = 30.61319F;
            this.Edit.HeaderText = "";
            this.Edit.Image = global::Auditur.Presentacion.Properties.Resources.Modificar;
            this.Edit.Name = "Edit";
            this.Edit.ReadOnly = true;
            this.Edit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // frmABMConceptos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.Controls.Add(this.grbABMConceptos);
            this.Controls.Add(this.grbListadoConceptos);
            this.Font = new System.Drawing.Font("Calibri", 14.25F);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmABMConceptos";
            this.Size = new System.Drawing.Size(755, 618);
            this.Load += new System.EventHandler(this.frmABMConceptos_Load);
            this.grbABMConceptos.ResumeLayout(false);
            this.grbABMConceptos.PerformLayout();
            this.grbListadoConceptos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvConceptos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.GroupBox grbABMConceptos;
        private System.Windows.Forms.Button btnCancelarConcepto;
        private System.Windows.Forms.Button btnGuardarConcepto;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNombreConcepto;
        private System.Windows.Forms.TextBox txtConceptoID;
        private System.Windows.Forms.GroupBox grbListadoConceptos;
        private System.Windows.Forms.Button btnCrearConcepto;
        private System.Windows.Forms.DataGridView dgvConceptos;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tipo;
        private System.Windows.Forms.DataGridViewImageColumn Edit;
        private System.Windows.Forms.ComboBox cboTipo;
        private System.Windows.Forms.Label label1;

    }
}
