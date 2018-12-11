using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Auditur.Negocio;

namespace Auditur.Presentacion
{
    public partial class frmABMConceptos : UserControl
    {
        public frmABMConceptos()
        {
            InitializeComponent();
        }

        private void frmABMConceptos_Load(object sender, EventArgs e)
        {
            dgvConceptos_Load();
        }

        private void dgvConceptos_Load()
        {
            dgvConceptos.DataSource = null;
            Conceptos Conceptos = new Conceptos();
            dgvConceptos.AutoGenerateColumns = false;
            dgvConceptos.DataSource = Conceptos.GetAll();
            Conceptos.CloseConnection();
        }

        private void dgvConceptos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn && e.RowIndex >= 0)
            {
                DataGridViewTextBoxCell Celda = (DataGridViewTextBoxCell)senderGrid.Rows[e.RowIndex].Cells["ID"];
                long ConceptoID = Convert.ToInt64(Celda.Value);


                switch (senderGrid.Columns[e.ColumnIndex].Name)
                {
                    case "Edit":
                        EditarConcepto(ConceptoID);
                        break;
                }
            }
        }

        private void EditarConcepto(long ConceptoID)
        {
            grbListadoConceptos.Enabled = false;
            grbABMConceptos.Enabled = true;

            if (ConceptoID > 0)
            {
                Conceptos Conceptos = new Conceptos();
                Concepto oConcepto = Conceptos.GetByID(ConceptoID);
                Conceptos.CloseConnection();

                txtConceptoID.Text = oConcepto.ID.ToString();
                txtNombreConcepto.Text = oConcepto.Nombre;
                cboTipo.SelectedIndex = TipoToCombo(oConcepto.Tipo);
                btnGuardarConcepto.Text = "Guardar cambios";
                grbABMConceptos.Text = "Modificar concepto";
            }
            else
            {
                txtConceptoID.Text = "";
                txtNombreConcepto.Text = "";
                cboTipo.SelectedIndex = 0;
                btnGuardarConcepto.Text = "Crear";
                grbABMConceptos.Text = "Crear nueva concepto";
            }
            txtNombreConcepto.Focus();
        }

        private void AgregarConcepto(Concepto oConcepto, bool Nuevo)
        {
            Conceptos Conceptos = new Conceptos();
            if (Nuevo)
                Conceptos.Insertar(oConcepto);
            else
                Conceptos.Modificar(oConcepto);
            Conceptos.CloseConnection();

            grbABMConceptos.Text = "";
            grbListadoConceptos.Enabled = true;
            grbABMConceptos.Enabled = false;
            txtConceptoID.Text = "";
            txtNombreConcepto.Text = "";
            cboTipo.SelectedIndex = 0;
            dgvConceptos_Load();
        }

        private char ComboToTipo(int index)
        {
            switch (index)
            {
                case 0:
                    return 'B';
                case 1:
                    return 'R';
                case 2:
                    return 'C';
                case 3:
                    return 'D';
                case 4:
                    return 'S';
                default:
                    return 'B';
            }
        }

        private int TipoToCombo(char Tipo)
        {
            switch (Tipo)
            {
                case 'B':
                    return 0;
                case 'R':
                    return 1;
                case 'C':
                    return 2;
                case 'D':
                    return 3;
                case 'S':
                    return 4;
                default:
                    return 0;
            }
        }

        private void btnGuardarConcepto_Click(object sender, EventArgs e)
        {
            long ConceptoID = 0;
            string Nombre = txtNombreConcepto.Text.ToUpper();
            char Tipo = ComboToTipo(cboTipo.SelectedIndex);

            if ((txtConceptoID.Text == "" || long.TryParse(txtConceptoID.Text, out ConceptoID)) && Nombre != "")
            {
                Concepto oConcepto = new Concepto { ID = ConceptoID, Nombre = Nombre, Tipo = Tipo };
                AgregarConcepto(oConcepto, txtConceptoID.Text == "");
            }
            else
            {
                MessageBox.Show("Los datos que ha ingresado son incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCrearConcepto_Click(object sender, EventArgs e)
        {
            EditarConcepto(0);
        }

        private void btnCancelarConcepto_Click(object sender, EventArgs e)
        {
            txtConceptoID.Text = "";
            txtNombreConcepto.Text = "";
            cboTipo.SelectedIndex = 0;
            grbListadoConceptos.Enabled = true;
            grbABMConceptos.Enabled = false;
        }
    }
}
