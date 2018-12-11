using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Auditur.Negocio;
using Auditur.Presentacion.Classes;
using System.Data.SqlServerCe;
using Helpers;

namespace Auditur.Presentacion
{
    public partial class frmElegirSemana : UserControl
    {
        public frmElegirSemana()
        {
            InitializeComponent();
        }

        private void frmElegirBSP_Load(object sender, EventArgs e)
        {
            Agencias agencias = new Agencias(); 
            List<Agencia> lstAgencias = agencias.GetAll().OrderBy(x => x.Nombre).ToList();
            agencias.CloseConnection();

            lstAgencias.Insert(0, new Agencia { ID = 0, Nombre = "[ SELECCIONE UNA AGENCIA ]" });
            cboAgencia.DataSource = lstAgencias;
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            Agencia oAgencia = (Agencia)cboAgencia.SelectedItem;

            Semanas semanas = new Semanas();
            List<Semana> lstSemanas = semanas.GetByAgenciaAno(oAgencia, Convert.ToInt32(cboAno.SelectedValue));
            semanas.CloseConnection();

            dgvSemanas.AutoGenerateColumns = false;
            dgvSemanas.DataSource = lstSemanas;
            lblAgencia.Text = oAgencia.Nombre;
            lblAño.Text = cboAno.SelectedValue.ToString();
        }

        private void dgvSemanas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if ((senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn || senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn) && e.RowIndex >= 0)
            {
                Semanas Semanas = new Semanas();
                BO_Tickets BO_Tickets = null;
                BSP_Tickets BSP_Tickets = null;

                Publics.Semana = null;
                DataGridViewTextBoxCell Celda = (DataGridViewTextBoxCell)senderGrid.Rows[e.RowIndex].Cells["ID"];
                long SemanaID = Convert.ToInt64(Celda.Value);
                Semana oSemana = Semanas.GetByID(SemanaID);

                ucMenu ucMenu1 = (ucMenu)this.Parent.Parent.Controls[0].Controls[0];
                if (oSemana != null)
                {
                    Agencias Agencias = new Agencias();
                    oSemana.Agencia = Agencias.GetByID(oSemana.Agencia.ID);
                    Agencias.CloseConnection();

                    Semanas.VerificarTicketsCargados(oSemana);
                    Publics.Semana = oSemana;
                    switch (senderGrid.Columns[e.ColumnIndex].Name)
                    {
                        case "BSPLoaded":
                            DataGridViewCheckBoxCell BSPCheckBox = (DataGridViewCheckBoxCell)senderGrid.Rows[e.RowIndex].Cells["BSPLoaded"];
                            if (!(bool)BSPCheckBox.Value)
                            {
                                if (MessageBox.Show("Entrar al formulario para importar BSP?", "Pregunta", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                                {
                                    ucMenu1.MostrarForm(new frmImportarBSP());
                                }
                            }
                            else
                            {
                                if (MessageBox.Show("¿Está seguro que desea eliminar los tickets BSP guardados?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                                {
                                    BSP_Tickets = new BSP_Tickets();
                                    BSP_Tickets.EliminarPorSemana(SemanaID);
                                    BSP_Tickets.CloseConnection();
                                    MessageBox.Show("Registros eliminados correctamente.", "Aviso");
                                    btnCargar_Click(null, null);
                                }
                            }
                            break;
                        case "BOLoaded":
                             DataGridViewCheckBoxCell BOCheckBox = (DataGridViewCheckBoxCell)senderGrid.Rows[e.RowIndex].Cells["BOLoaded"];
                             if (!(bool)BOCheckBox.Value)
                            {
                                if (MessageBox.Show("Entrar al formulario para importar BO?", "Pregunta", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                                {
                                    ucMenu1.MostrarForm(new frmImportarBO());
                                }
                            }
                            else
                            {
                                if (MessageBox.Show("¿Está seguro que desea eliminar los tickets BO guardados?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                                {
                                    BO_Tickets = new BO_Tickets();
                                    BO_Tickets.EliminarPorSemana(SemanaID);
                                    BO_Tickets.CloseConnection();

                                    MessageBox.Show("Registros eliminados correctamente.", "Aviso");
                                    btnCargar_Click(null, null);
                                }
                            }
                            break;
                        case "btnReport":
                            BO_Tickets = new BO_Tickets();
                            Publics.Semana.TicketsBO = BO_Tickets.ObtenerPorSemana(Publics.Semana.ID);
                            BO_Tickets.CloseConnection();

                            BSP_Tickets = new BSP_Tickets();
                            Publics.Semana.TicketsBSP = BSP_Tickets.ObtenerPorSemana(Publics.Semana.ID);
                            BSP_Tickets.CloseConnection();

                            ucMenu1.MostrarForm(new frmReportes());
                            break;
                        default:
                            break;
                    }
                }
                Semanas.CloseConnection();
            }
        }

        private void cboAgencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboAgencia.SelectedIndex > 0)
            {
                Semanas Semanas = new Semanas();
                cboAno.DataSource = Semanas.GetAñosByAgencia(Convert.ToInt32(cboAgencia.SelectedValue));
                Semanas.CloseConnection();

                if (cboAno.Items.Count > 0)
                {
                    btnCargar.Enabled = true;
                    cboAno.SelectedIndex = 0;
                }
                else
                {
                    btnCargar.Enabled = false;
                    MessageBox.Show("Aún no se han importado datos para esta agencia.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                btnCargar.Enabled = false;
                cboAno.DataSource = null;
            }
        }
    }
}
