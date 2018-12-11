using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Auditur.Negocio;
using System.Data.SqlServerCe;

namespace Auditur.Presentacion
{
    public partial class frmABMAgencias : UserControl
    {
        public frmABMAgencias()
        {
            InitializeComponent();
        }

        private void frmABMAgencias_Load(object sender, EventArgs e)
        {
            dgvAgencias_Load();
        }

        private void dgvAgencias_Load()
        {
            dgvAgencias.DataSource = null;
            Agencias Agencias = new Agencias();
            dgvAgencias.AutoGenerateColumns = false;
            dgvAgencias.DataSource = Agencias.GetAll().OrderBy(x => x.Nombre).ToList();
            Agencias.CloseConnection();
        }

        private void dgvAgencias_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn && e.RowIndex >= 0)
            {
                DataGridViewTextBoxCell Celda = (DataGridViewTextBoxCell)senderGrid.Rows[e.RowIndex].Cells["ID"];
                long AgenciaID = Convert.ToInt64(Celda.Value);


                switch (senderGrid.Columns[e.ColumnIndex].Name)
                {
                    case "Edit":
                        EditarAgencia(AgenciaID);
                        break;
                }
            }
        }

        private void EditarAgencia(long AgenciaID)
        {
            grbListadoAgencias.Enabled = false;
            grbABMAgencias.Enabled = true;

            if (AgenciaID > 0)
            {
                Agencias Agencias = new Agencias();
                Agencia oAgencia = Agencias.GetByID(AgenciaID);
                Agencias.CloseConnection();

                txtAgenciaID.Text = oAgencia.ID.ToString();
                txtAgenciaID.ReadOnly = true;
                txtNombreAgencia.Text = oAgencia.Nombre;
                btnGuardarAgencia.Text = "Guardar cambios";
                grbABMAgencias.Text = "Modificar agencia";
                txtNombreAgencia.Focus();
            }
            else
            {
                txtAgenciaID.Text = "";
                txtAgenciaID.ReadOnly = false;
                txtNombreAgencia.Text = "";
                btnGuardarAgencia.Text = "Crear";
                grbABMAgencias.Text = "Crear nueva agencia";
                txtAgenciaID.Focus();
            }
        }

        private void AgregarAgencia(Agencia oAgencia, bool Nuevo)
        {
            Agencias Agencias = new Agencias();
            if (Nuevo)
                Agencias.Insertar(oAgencia);
            else
                Agencias.Modificar(oAgencia);
            Agencias.CloseConnection();

            grbABMAgencias.Text = "";
            grbListadoAgencias.Enabled = true;
            grbABMAgencias.Enabled = false;
            txtAgenciaID.Text = "";
            txtNombreAgencia.Text = "";
            dgvAgencias_Load();
        }

        private void btnGuardarAgencia_Click(object sender, EventArgs e)
        {
            long AgenciaID = 0;
            string Nombre = txtNombreAgencia.Text.ToUpper();

            if (long.TryParse(txtAgenciaID.Text, out AgenciaID) && Nombre != "")
            {
                Agencia oAgencia = new Agencia { ID = AgenciaID, Nombre = Nombre };
                AgregarAgencia(oAgencia, !txtAgenciaID.ReadOnly);
            }
            else
            {
                MessageBox.Show("Los datos que ha ingresado son incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCrearAgencia_Click(object sender, EventArgs e)
        {
            EditarAgencia(0);
        }

        private void btnCancelarAgencia_Click(object sender, EventArgs e)
        {
            txtAgenciaID.Text = "";
            txtNombreAgencia.Text = "";
            grbListadoAgencias.Enabled = true;
            grbABMAgencias.Enabled = false;
        }
    }
}
