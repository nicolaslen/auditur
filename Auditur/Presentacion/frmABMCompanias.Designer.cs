namespace Auditur.Presentacion
{
    partial class frmABMCompanias
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
            this.grbListadoCompanias = new System.Windows.Forms.GroupBox();
            this.btnCrearCompania = new System.Windows.Forms.Button();
            this.dgvCompanias = new System.Windows.Forms.DataGridView();
            this.grbABMCompanias = new System.Windows.Forms.GroupBox();
            this.btnCancelarCompania = new System.Windows.Forms.Button();
            this.btnGuardarCompania = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNombreCompania = new System.Windows.Forms.TextBox();
            this.txtCompaniaID = new System.Windows.Forms.TextBox();
            this.txtCodigoCompania = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.grbListadoCompanias.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompanias)).BeginInit();
            this.grbABMCompanias.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbListadoCompanias
            // 
            this.grbListadoCompanias.Controls.Add(this.btnCrearCompania);
            this.grbListadoCompanias.Controls.Add(this.dgvCompanias);
            this.grbListadoCompanias.Location = new System.Drawing.Point(4, 4);
            this.grbListadoCompanias.Margin = new System.Windows.Forms.Padding(4);
            this.grbListadoCompanias.Name = "grbListadoCompanias";
            this.grbListadoCompanias.Padding = new System.Windows.Forms.Padding(4);
            this.grbListadoCompanias.Size = new System.Drawing.Size(747, 353);
            this.grbListadoCompanias.TabIndex = 0;
            this.grbListadoCompanias.TabStop = false;
            this.grbListadoCompanias.Text = "ABM de Compañías...";
            // 
            // btnCrearCompania
            // 
            this.btnCrearCompania.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCrearCompania.Location = new System.Drawing.Point(605, 33);
            this.btnCrearCompania.Name = "btnCrearCompania";
            this.btnCrearCompania.Size = new System.Drawing.Size(134, 36);
            this.btnCrearCompania.TabIndex = 0;
            this.btnCrearCompania.Text = "Nueva";
            this.btnCrearCompania.UseVisualStyleBackColor = true;
            this.btnCrearCompania.Click += new System.EventHandler(this.btnCrearCompania_Click);
            // 
            // dgvCompanias
            // 
            this.dgvCompanias.AllowUserToAddRows = false;
            this.dgvCompanias.AllowUserToDeleteRows = false;
            this.dgvCompanias.AllowUserToOrderColumns = true;
            this.dgvCompanias.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCompanias.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCompanias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCompanias.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Nombre,
            this.Codigo,
            this.Edit});
            this.dgvCompanias.Location = new System.Drawing.Point(8, 74);
            this.dgvCompanias.Margin = new System.Windows.Forms.Padding(4);
            this.dgvCompanias.Name = "dgvCompanias";
            this.dgvCompanias.ReadOnly = true;
            this.dgvCompanias.RowHeadersVisible = false;
            this.dgvCompanias.Size = new System.Drawing.Size(731, 271);
            this.dgvCompanias.TabIndex = 1;
            this.dgvCompanias.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCompanias_CellContentClick);
            // 
            // grbABMCompanias
            // 
            this.grbABMCompanias.Controls.Add(this.label1);
            this.grbABMCompanias.Controls.Add(this.txtCodigoCompania);
            this.grbABMCompanias.Controls.Add(this.btnCancelarCompania);
            this.grbABMCompanias.Controls.Add(this.btnGuardarCompania);
            this.grbABMCompanias.Controls.Add(this.label3);
            this.grbABMCompanias.Controls.Add(this.label2);
            this.grbABMCompanias.Controls.Add(this.txtNombreCompania);
            this.grbABMCompanias.Controls.Add(this.txtCompaniaID);
            this.grbABMCompanias.Enabled = false;
            this.grbABMCompanias.Location = new System.Drawing.Point(4, 364);
            this.grbABMCompanias.Name = "grbABMCompanias";
            this.grbABMCompanias.Size = new System.Drawing.Size(747, 224);
            this.grbABMCompanias.TabIndex = 1;
            this.grbABMCompanias.TabStop = false;
            // 
            // btnCancelarCompania
            // 
            this.btnCancelarCompania.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancelarCompania.Location = new System.Drawing.Point(376, 178);
            this.btnCancelarCompania.Name = "btnCancelarCompania";
            this.btnCancelarCompania.Size = new System.Drawing.Size(134, 36);
            this.btnCancelarCompania.TabIndex = 7;
            this.btnCancelarCompania.Text = "Cancelar";
            this.btnCancelarCompania.UseVisualStyleBackColor = true;
            this.btnCancelarCompania.Click += new System.EventHandler(this.btnCancelarCompania_Click);
            // 
            // btnGuardarCompania
            // 
            this.btnGuardarCompania.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnGuardarCompania.Location = new System.Drawing.Point(236, 178);
            this.btnGuardarCompania.Name = "btnGuardarCompania";
            this.btnGuardarCompania.Size = new System.Drawing.Size(134, 36);
            this.btnGuardarCompania.TabIndex = 6;
            this.btnGuardarCompania.Text = "Guardar";
            this.btnGuardarCompania.UseVisualStyleBackColor = true;
            this.btnGuardarCompania.Click += new System.EventHandler(this.btnGuardarCompania_Click);
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
            // txtNombreCompania
            // 
            this.txtNombreCompania.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNombreCompania.Location = new System.Drawing.Point(130, 85);
            this.txtNombreCompania.Name = "txtNombreCompania";
            this.txtNombreCompania.Size = new System.Drawing.Size(609, 31);
            this.txtNombreCompania.TabIndex = 3;
            // 
            // txtCompaniaID
            // 
            this.txtCompaniaID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCompaniaID.Location = new System.Drawing.Point(130, 48);
            this.txtCompaniaID.Name = "txtCompaniaID";
            this.txtCompaniaID.Size = new System.Drawing.Size(609, 31);
            this.txtCompaniaID.TabIndex = 1;
            // 
            // txtCodigoCompania
            // 
            this.txtCodigoCompania.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCodigoCompania.Location = new System.Drawing.Point(130, 122);
            this.txtCodigoCompania.Name = "txtCodigoCompania";
            this.txtCodigoCompania.Size = new System.Drawing.Size(609, 31);
            this.txtCodigoCompania.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "Código:";
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.FillWeight = 25.82105F;
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            // 
            // Nombre
            // 
            this.Nombre.DataPropertyName = "Nombre";
            this.Nombre.FillWeight = 102.9763F;
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.Name = "Nombre";
            this.Nombre.ReadOnly = true;
            // 
            // Codigo
            // 
            this.Codigo.DataPropertyName = "Codigo";
            this.Codigo.FillWeight = 18.90332F;
            this.Codigo.HeaderText = "Código";
            this.Codigo.Name = "Codigo";
            this.Codigo.ReadOnly = true;
            // 
            // Edit
            // 
            this.Edit.FillWeight = 21.85755F;
            this.Edit.HeaderText = "";
            this.Edit.Image = global::Auditur.Presentacion.Properties.Resources.Modificar;
            this.Edit.Name = "Edit";
            this.Edit.ReadOnly = true;
            this.Edit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // frmABMCompanias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.Controls.Add(this.grbABMCompanias);
            this.Controls.Add(this.grbListadoCompanias);
            this.Font = new System.Drawing.Font("Calibri", 14.25F);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmABMCompanias";
            this.Size = new System.Drawing.Size(755, 618);
            this.Load += new System.EventHandler(this.frmABMCompanias_Load);
            this.grbListadoCompanias.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompanias)).EndInit();
            this.grbABMCompanias.ResumeLayout(false);
            this.grbABMCompanias.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbListadoCompanias;
        private System.Windows.Forms.DataGridView dgvCompanias;
        private System.Windows.Forms.Button btnCrearCompania;
        private System.Windows.Forms.GroupBox grbABMCompanias;
        private System.Windows.Forms.Button btnCancelarCompania;
        private System.Windows.Forms.Button btnGuardarCompania;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNombreCompania;
        private System.Windows.Forms.TextBox txtCompaniaID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Codigo;
        private System.Windows.Forms.DataGridViewImageColumn Edit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCodigoCompania;
    }
}
