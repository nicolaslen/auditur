using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Auditur.Negocio;
using System.IO;
using Auditur.Presentacion.Classes;
using Helpers;

namespace Auditur.Presentacion
{
    public partial class frmImportarBO : UserControl
    {
        public frmImportarBO()
        {
            InitializeComponent();
            this.BOActions = new BOActions(Application.CurrentCulture);
        }

        private BOActions BOActions { get; set; }
        private Semana semanaToImport { get; set; }

        private void btnExaminar_BO_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Archivo de valores separados por comas de Microsoft Excel|*.csv",
                Title = "Por favor, selecciona el archivo Back Office."
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtFilePath_BO.Text = dialog.FileName;
                bool HabilitarBotones = BO_Cabecera(txtFilePath_BO.Text);
                btnReadFile.Enabled = HabilitarBotones;
            }
        }

        private void CargarAgencias()
        {
            Agencias agencias = new Agencias();
            List<Agencia> lstAgencias = agencias.GetAll().OrderBy(x => x.Nombre).ToList();
            agencias.CloseConnection();

            lstAgencias.Insert(0, new Agencia { ID = 0, Nombre = "[ SELECCIONE UNA AGENCIA ]" });
            cboAgencia.DataSource = lstAgencias;
        }

        private bool BO_Cabecera(string fileName)
        {
            string Linea;
            semanaToImport = null;
            
            try
            {
                if (!File.Exists(fileName))
                    return false;

                StreamReader fileReader = new StreamReader(fileName);
                Linea = fileReader.ReadLine();
                string[] arrLinea = Linea.Split(new char[] { ',', ';' });
                if (Linea != null)
                {
                    return true;
                }
            }
            catch (Exception Exception1)
            {
                MessageBox.Show("Error: " + Exception1.Message + "\nfileName: " + fileName, "Error");
            }
            return false;
        }

        private bool Verificar()
        {
            Semana oSemanaAux = null;

            Agencia oAgencia = null;
            DateTime? Periodo = null;

            DateTime? fechaDesde = null;
            DateTime? fechaHasta = null;

            if (cboAgencia.SelectedIndex > 0)
                oAgencia = (Agencia)cboAgencia.SelectedItem;

            if (Validators.IsNumeric(txtPeriodoS.Text) && Validators.IsNumeric(txtPeriodoM.Text) && Validators.IsNumeric(txtPeriodoA.Text))
                Periodo = new DateTime(Convert.ToInt32(txtPeriodoA.Text), Convert.ToInt32(txtPeriodoM.Text), Convert.ToInt32(txtPeriodoS.Text));

            fechaDesde = dtpFechaDesde.Value;
            fechaHasta = dtpFechaHasta.Value;

            if (oAgencia == null)
            {
                MessageBox.Show("Seleccione una Agencia.", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (Periodo == null)
            {
                MessageBox.Show("Verifique el Período ingresado.", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (fechaDesde >= fechaHasta)
            {
                MessageBox.Show("La Fecha Hasta debe ser posterior a la Fecha Desde.", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            oSemanaAux = new Semana()
            {
                Agencia = oAgencia,
                Periodo = Periodo.Value,
                FechaDesde = fechaDesde.Value,
                FechaHasta = fechaHasta.Value,
            };

            if (Publics.Semana != null && Publics.Semana.Agencia.ID == oSemanaAux.Agencia.ID && Publics.Semana.Periodo == oSemanaAux.Periodo)
            {
                oSemanaAux.ID = Publics.Semana.ID;
                oSemanaAux.BSPCargado = Publics.Semana.BSPCargado;
                oSemanaAux.BOCargado = Publics.Semana.BOCargado;
                oSemanaAux.TicketsBSP = Publics.Semana.TicketsBSP;
            }
            else
            {
                Semanas semanas = new Semanas();
                Semana oSemanaVerif = semanas.GetByAgenciaPeriodo(oSemanaAux.Agencia.ID, oSemanaAux.Periodo);
                semanas.CloseConnection();

                if (oSemanaVerif != null)
                {
                    oSemanaAux.ID = oSemanaVerif.ID;
                    oSemanaAux.BOCargado = oSemanaVerif.BOCargado;
                    if (oSemanaVerif.BSPCargado)
                    {
                        oSemanaAux.BSPCargado = true;
                        BSP_Tickets BSP_Tickets = new BSP_Tickets();
                        oSemanaAux.TicketsBSP = BSP_Tickets.ObtenerPorSemana(oSemanaAux.ID);
                        BSP_Tickets.CloseConnection();
                    }
                }
            }

            semanaToImport = oSemanaAux;
            return semanaToImport != null && semanaToImport.Agencia != null && (!semanaToImport.BOCargado || MessageBox.Show("Ya ha cargado este archivo previamente. ¿Desea volver a cargarlo?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes);
        }

        private void BO_ReadFile(string fileName)
        {
            int iLinea = 0;
            string Linea;
            List<BO_Ticket> lstDetalles = new List<BO_Ticket>();
            BO_Ticket oBO_Detalle = null;
            
            try
            {
                if (!File.Exists(fileName))
                    return;

                Companias companias = new Companias();
                List<Compania> lstCompanias = companias.GetAll();
                companias.CloseConnection();
                List<Compania> lstNuevasCompanias = new List<Compania>();

                semanaToImport.TicketsBO = new List<BO_Ticket>();

                StreamReader fileReader = new StreamReader(fileName);
                fileReader.ReadLine();
                while ((Linea = fileReader.ReadLine()) != null)
                {
                    iLinea++;
                    oBO_Detalle = BOActions.GetDetalles(ref Linea, iLinea, lstCompanias);
                    if (oBO_Detalle != null)
                    {
                        if (!lstCompanias.Any(x => x.Codigo == oBO_Detalle.Compania.Codigo) && !lstNuevasCompanias.Any(x => x.Codigo == oBO_Detalle.Compania.Codigo))
                            lstNuevasCompanias.Add(oBO_Detalle.Compania);
                        lstDetalles.Add(oBO_Detalle);
                    }
                }

                fileReader.Close();

                semanaToImport.TicketsBO = lstDetalles;
                semanaToImport.BOCargado = true;

                if (lstNuevasCompanias.Count > 0)
                {
                    string mensaje = "Se han encontrado Compañías Aéreas que no estaban registradas en el sistema:\n\n";
                    lstNuevasCompanias.ForEach(x => mensaje += "Código: " + x.Codigo + "\n");
                    mensaje += "\nPor favor, ingrese al \"ABM de Compañías\" y agregue estas compañías.\n";
                    mensaje += "Muchas gracias.";
                    MessageBox.Show(mensaje, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception Exception1)
            {
                MessageBox.Show("Error: " + Exception1.Message + "\nfileName: " + fileName + "\nline: " + iLinea, "Error");
            }
        }
        private void EnableDisableControls(bool Habilitar)
        {
            grbFileImport_BO.Enabled = Habilitar;
            this.Parent.Parent.Controls[0].Enabled = Habilitar;

            if (!Habilitar)
            {
                btnReadFile.Enabled = false;
            }
        }
        private void btnReadFile_Click(object sender, EventArgs e)
        {
            if (Verificar())
            {
                EnableDisableControls(false);
                backgroundWorker1.RunWorkerAsync(true);
            }
        }

        private void frmImportarBO_Load(object sender, EventArgs e)
        {
            CargarAgencias();
            if (Publics.Semana != null)
            {
                Semana oSemana = Publics.Semana;
                int iAgencia = 0;
                while (((Agencia)cboAgencia.Items[iAgencia]).ID != oSemana.Agencia.ID) { iAgencia++; };
                cboAgencia.SelectedIndex = iAgencia;
                txtPeriodoS.Text = oSemana.Periodo.Day.ToString();
                txtPeriodoM.Text = oSemana.Periodo.Month.ToString();
                txtPeriodoA.Text = oSemana.Periodo.Year.ToString();
                dtpFechaDesde.Value = oSemana.FechaDesde;
                dtpFechaHasta.Value = oSemana.FechaHasta;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            bool blnResult = false;
            BackgroundWorker bw = sender as BackgroundWorker;

            BO_ReadFile(txtFilePath_BO.Text);
            if ((bool)e.Argument)
                BOActions.Guardar(semanaToImport, backgroundWorker1);

            blnResult = true;

            e.Result = blnResult;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            lblProgressStatus.Text = (string)e.UserState;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                string msg = String.Format("Se ha producido el siguiente error: {0}", e.Error.Message);
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("La operación ha sido completada con éxito", "Operación terminada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            lblProgressStatus.Text = "";
            progressBar1.Value = 0;

            Publics.Semana = semanaToImport;
            frmCurrents1.Reset();

            txtFilePath_BO.Text = "";
            grbFileImport_BO.Enabled = true;
            this.Parent.Parent.Controls[0].Enabled = true;
        }
    }
}
