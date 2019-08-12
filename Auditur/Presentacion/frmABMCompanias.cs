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
using Auditur.Presentacion.Classes;
using Helpers;

namespace Auditur.Presentacion
{
    public partial class frmABMCompanias : UserControl
    {
        public frmABMCompanias()
        {
            InitializeComponent();
        }

        private void frmABMCompanias_Load(object sender, EventArgs e)
        {
            dgvCompanias_Load();
        }

        private void dgvCompanias_Load()
        {
            dgvCompanias.DataSource = null;
            Companias Companias = new Companias();
            dgvCompanias.AutoGenerateColumns = false;
            dgvCompanias.DataSource = Companias.GetAll();
            Companias.CloseConnection();
        }

        private void dgvCompanias_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn && e.RowIndex >= 0)
            {
                DataGridViewTextBoxCell Celda = (DataGridViewTextBoxCell)senderGrid.Rows[e.RowIndex].Cells["ID"];
                long CompaniaID = Convert.ToInt64(Celda.Value);


                switch (senderGrid.Columns[e.ColumnIndex].Name)
                {
                    case "Edit":
                        EditarCompania(CompaniaID);
                        break;
                    case "Del":
                        EliminarCompania(CompaniaID);
                        break;
                }
            }
        }

        private void EditarCompania(long CompaniaID)
        {
            grbListadoCompanias.Enabled = false;
            grbABMCompanias.Enabled = true;

            if (CompaniaID > 0)
            {
                Companias Companias = new Companias();
                Compania oCompania = Companias.GetByID(CompaniaID);
                Companias.CloseConnection();

                txtCompaniaID.Text = oCompania.ID.ToString();
                txtCompaniaID.ReadOnly = true;
                txtNombreCompania.Text = oCompania.Nombre;
                txtCodigoCompania.Text = oCompania.Codigo;
                btnGuardarCompania.Text = "Guardar cambios";
                grbABMCompanias.Text = "Modificar compañía";
                txtNombreCompania.Focus();
            }
            else
            {
                txtCompaniaID.Text = "";
                txtCompaniaID.ReadOnly = false;
                txtNombreCompania.Text = "";
                txtCodigoCompania.Text = "";
                btnGuardarCompania.Text = "Crear";
                grbABMCompanias.Text = "Crear nueva compañía";
                txtCompaniaID.Focus();
            }
        }

        private void EliminarCompania(long CompaniaID)
        {
            if (MessageBox.Show("¿Está seguro que desea eliminar la compañía?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                using (SqlCeConnection conn = AccesoDatos.OpenConn())
                {
                    BSP_Tickets BSPTickets = new BSP_Tickets();
                    Companias companias = new Companias();

                    BSPTickets.EliminarPorCompania(CompaniaID);
                    companias.Eliminar(CompaniaID);
                }
                MessageBox.Show("Compañía eliminada correctamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvCompanias_Load();
            }
}

        private void AgregarCompania(Compania oCompania, bool Nuevo)
        {
            Companias Companias = new Companias();
            if (Nuevo)
                Companias.Insertar(oCompania);
            else
            {
                Companias.Modificar(oCompania);
                ComprobarActuales(oCompania);
            }
            Companias.CloseConnection();

            grbABMCompanias.Text = "";
            grbListadoCompanias.Enabled = true;
            grbABMCompanias.Enabled = false;
            txtCompaniaID.Text = "";
            txtNombreCompania.Text = "";
            txtCodigoCompania.Text = "";
            dgvCompanias_Load();
        }

        private void ComprobarActuales(Compania oCompania)
        {
            if (Publics.Semana != null)
            {
                if (Publics.Semana.TicketsBSP != null)
                {
                    Publics.Semana.TicketsBSP.ForEach(x =>
                    {
                        if (x.Compania.ID == oCompania.ID)
                            x.Compania = oCompania;
                    });
                }
                if (Publics.Semana.TicketsBO != null)
                {
                    Publics.Semana.TicketsBO.ForEach(x =>
                    {
                        if (x.Compania.ID == oCompania.ID)
                            x.Compania = oCompania;
                    });
                }
            }
        }

        private void btnGuardarCompania_Click(object sender, EventArgs e)
        {
            long CompaniaID = 0;
            string Nombre = txtNombreCompania.Text.ToUpper();
            string Codigo = txtCodigoCompania.Text.ToUpper();

            if (long.TryParse(txtCompaniaID.Text, out CompaniaID) && Nombre != "" && Codigo.Length == 2)
            {
                Compania oCompania = new Compania { ID = CompaniaID, Nombre = Nombre, Codigo = Codigo };
                AgregarCompania(oCompania, !txtCompaniaID.ReadOnly);
            }
            else
            {
                MessageBox.Show("Los datos que ha ingresado son incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCrearCompania_Click(object sender, EventArgs e)
        {
            EditarCompania(0);
        }

        private void btnCancelarCompania_Click(object sender, EventArgs e)
        {
            txtCompaniaID.Text = "";
            txtNombreCompania.Text = "";
            txtCodigoCompania.Text = "";
            grbListadoCompanias.Enabled = true;
            grbABMCompanias.Enabled = false;
        }
    }
}
