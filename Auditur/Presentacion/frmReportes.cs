using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Auditur.Presentacion.Classes;
using Auditur.Negocio.Reportes;
using Helpers;
using System.IO;
using Auditur.Negocio;
using System.Diagnostics;

namespace Auditur.Presentacion
{
    public partial class frmReportes : UserControl
    {
        public frmReportes()
        {
            InitializeComponent();
            semanaToReport = Publics.Semana;
        }

        private Semana semanaToReport { get; set; }

        private void OpenFolder()
        {
            Process.Start(txtFilePath.Text);
        }

        private string GetPeriodo()
        {
            string Dia = "";
            string Mes = "";
            string Ano = semanaToReport.Periodo.Year.ToString().Substring(2);
            switch (semanaToReport.Periodo.Day)
            {
                case 1:
                    Dia = "1RA";
                    break;
                case 2:
                    Dia = "2DA";
                    break;
                case 3:
                    Dia = "3RA";
                    break;
                case 4:
                    Dia = "4TA";
                    break;
                case 5:
                    Dia = "5TA";
                    break;
            }
            switch (semanaToReport.Periodo.Month)
            {
                case 1:
                    Mes = "ENE";
                    break;
                case 2:
                    Mes = "FEB";
                    break;
                case 3:
                    Mes = "MAR";
                    break;
                case 4:
                    Mes = "ABR";
                    break;
                case 5:
                    Mes = "MAY";
                    break;
                case 6:
                    Mes = "JUN";
                    break;
                case 7:
                    Mes = "JUL";
                    break;
                case 8:
                    Mes = "AGO";
                    break;
                case 9:
                    Mes = "SEP";
                    break;
                case 10:
                    Mes = "OCT";
                    break;
                case 11:
                    Mes = "NOV";
                    break;
                case 12:
                    Mes = "DIC";
                    break;
            }
            return Dia + " " + Mes + "-" + Ano;
        }

        private string GetAgencia()
        {
            return semanaToReport.Agencia.Nombre;
        }

        private void EnableControls(bool Enabled)
        {
            grbFileImport.Enabled = Enabled;
            grbReportes.Enabled = Enabled;
        }

        private void StartWorking(int Opcion)
        {
            EnableControls(false);
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 30;
            lblProgressStatus.Text = "Generando reportes. Por favor espere...";
            backgroundWorker1.RunWorkerAsync(Opcion);
        }

        private void btnGenerarTodos_Click(object sender, EventArgs e)
        {
            StartWorking(0);
        }

        private void btnEmisiones_Click(object sender, EventArgs e)
        {
            StartWorking(1);
        }

        private void btnControlIVAs_Click(object sender, EventArgs e)
        {
            StartWorking(2);
        }

        private void btnCreditos_Click(object sender, EventArgs e)
        {
            StartWorking(3);
        }

        private void btnDebitos_Click(object sender, EventArgs e)
        {
            StartWorking(4);
        }
        private void btnDiferencias_Click(object sender, EventArgs e)
        {
            StartWorking(5);
        }
        private void btnOvers_Click(object sender, EventArgs e)
        {
            StartWorking(6);
        }
        private void btnReembolsos_Click(object sender, EventArgs e)
        {
            StartWorking(7);
        }
        private void btnSituacionBOs_Click(object sender, EventArgs e)
        {
            StartWorking(8);
        }
        private void btnSituacionBSPs_Click(object sender, EventArgs e)
        {
            StartWorking(9);
        }
        private void btnFacturacion_Click(object sender, EventArgs e)
        {
            StartWorking(10);
        }

        private string GetFileName(string Reporte)
        {
            return txtFilePath.Text + "\\" + Reporte + " " + GetAgencia() + " " + GetPeriodo() + ".xlsx";
        }

        private bool CheckFile(string FileName, string Reporte)
        {
            bool blnReturn = true;
            try
            {
                if (File.Exists(FileName))
                {
                    if (MessageBox.Show("Ya existe el reporte " + Reporte + " en la carpeta seleccionada. ¿Desea eliminarlo y volverlo a generar?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        File.Delete(FileName);
                    }
                    else
                    {
                        blnReturn = false;
                    }
                }
            }
            catch (Exception Exception1)
            {
                blnReturn = false;
                MessageBox.Show(Exception1.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return blnReturn;
        }

        private bool GenerarEmisiones()
        {
            string Reporte = "Emisiones";
            string FileName = GetFileName(Reporte);

            if (CheckFile(FileName, Reporte))
            {
                Emisiones emisiones = new Emisiones();
                List<Emision> lstReporte = emisiones.Generar(semanaToReport);
                List<string> header = GetHeader(semanaToReport, "Emisiones");
                string footer = lstReporte.Count + " registros";
                CreateExcelFile.CreateExcelDocument(lstReporte, Reporte, FileName, header.ToArray(), footer);
                return true;
            }
            return false;
        }

        private bool GenerarDiferenciasIVA()
        {
            string Reporte = "DiferenciasIVAyComs";
            string FileName = GetFileName(Reporte);

            if (CheckFile(FileName, Reporte))
            {
                DiferenciasIVAs ControlIVAs = new DiferenciasIVAs();
                List<DiferenciasIVA> lstReporte = ControlIVAs.Generar(semanaToReport);
                List<string> header = GetHeader(semanaToReport, "Diferencias IVA y Comisiones");
                string footer = lstReporte.Count(x => x.Origen == "BSP") + " registros";
                CreateExcelFile.CreateExcelDocument(lstReporte, Reporte, FileName, header.ToArray(), footer);
                return true;
            }
            return false;
        }

        private bool GenerarCreditos()
        {
            string Reporte = "Creditos";
            string FileName = GetFileName(Reporte);

            if (CheckFile(FileName, Reporte))
            {
                Creditos Creditos = new Creditos();
                List<Credito> lstReporte = Creditos.Generar(semanaToReport);
                List<string> header = GetHeader(semanaToReport, "Notas de Crédito en BSP");
                string footer = lstReporte.Count(x => x.Cia != "TOTAL") + " registros";
                CreateExcelFile.CreateExcelDocument(lstReporte, Reporte, FileName, header.ToArray(), footer);
                return true;
            }
            return false;
        }

        private bool GenerarDebitos()
        {
            string Reporte = "Debitos";
            string FileName = GetFileName(Reporte);

            if (CheckFile(FileName, Reporte))
            {
                Debitos Debitos = new Debitos();
                List<Debito> lstReporte = Debitos.Generar(semanaToReport);
                List<string> header = GetHeader(semanaToReport, "Notas de Débito en BSP");
                string footer = lstReporte.Count(x => x.Cia != "TOTAL").ToString() + " registros";
                CreateExcelFile.CreateExcelDocument(lstReporte, Reporte, FileName, header.ToArray(), footer);
                return true;
            }
            return false;
        }
        private bool GenerarDiferenciasEmisiones()
        {
            string Reporte = "DiferenciasEmisiones";
            string FileName = GetFileName(Reporte);

            if (CheckFile(FileName, Reporte))
            {
                Diferencias Diferencias = new Diferencias();
                List<Diferencia> lstReporte = Diferencias.Generar(semanaToReport);
                List<string> header = GetHeader(semanaToReport, "Diferencias Emisiones");
                string footer = lstReporte.Count(x => x.Origen == "BSP") + " registros";
                CreateExcelFile.CreateExcelDocument(lstReporte, Reporte, FileName, header.ToArray(), footer);
                return true;
            }
            return false;
        }
        private bool GenerarOvers()
        {
            string Reporte = "Over";
            string FileName = GetFileName(Reporte);

            if (CheckFile(FileName, Reporte))
            {
                Overs Overs = new Overs();
                List<Over> lstReporte = Overs.Generar(semanaToReport);
                List<string> header = GetHeader(semanaToReport, "Análisis de Over");
                string footer = lstReporte.Count(x => x.Cia != "TOTAL") + " registros";
                CreateExcelFile.CreateExcelDocument(lstReporte, Reporte, FileName, header.ToArray(), footer);
                return true;
            }
            return false;
        }
        private bool GenerarReembolsos()
        {
            string Reporte = "Reembolsos";
            string FileName = GetFileName(Reporte);

            if (CheckFile(FileName, Reporte))
            {
                Reembolsos Reembolsos = new Reembolsos();
                List<Reembolso> lstReporte = Reembolsos.Generar(semanaToReport);
                List<string> header = GetHeader(semanaToReport, "Reembolsos en BSP");
                string footer = lstReporte.Count(x => x.Cia != "TOTAL") + " registros";
                CreateExcelFile.CreateExcelDocument(lstReporte, Reporte, FileName, header.ToArray(), footer);
                return true;
            }
            return false;
        }
        private bool GenerarSituacionBOs()
        {
            string Reporte = "SituacionBO";
            string FileName = GetFileName(Reporte);

            if (CheckFile(FileName, Reporte))
            {
                SituacionBOs SituacionBOs = new SituacionBOs();
                List<SituacionBO> lstReporte = SituacionBOs.Generar(semanaToReport);
                List<string> header = GetHeader(semanaToReport, "Situación Back Office (Movimientos registrados en su sistema y no en BSP)");
                string footer = lstReporte.Count + " registros";
                CreateExcelFile.CreateExcelDocument(lstReporte, Reporte, FileName, header.ToArray(), footer);
                return true;
            }
            return false;
        }
        private bool GenerarSituacionBSPs()
        {
            string Reporte = "SituacionBSP";
            string FileName = GetFileName(Reporte);

            if (CheckFile(FileName, Reporte))
            {
                SituacionBSPs SituacionBSPs = new SituacionBSPs();
                List<SituacionBSP> lstReporte = SituacionBSPs.Generar(semanaToReport);
                List<string> header = GetHeader(semanaToReport, "Situación BSP (Movimientos registrados en BSP y no en su sistema)");
                string footer = lstReporte.Count.ToString() + " registros";
                CreateExcelFile.CreateExcelDocument(lstReporte, Reporte, FileName, header.ToArray(), footer);
                return true;
            }
            return false;
        }

        private bool GenerarFacturacion()
        {
            string Reporte = "ListadoParaFacturación";
            string FileName = GetFileName(Reporte);

            if (CheckFile(FileName, Reporte))
            {
                Facturaciones Facturaciones = new Facturaciones();
                List<Facturacion> lstReporte = Facturaciones.Generar(semanaToReport);
                List<string> header = GetHeader(semanaToReport, "Listado para facturación");
                string footer = lstReporte.Count.ToString() + " registros";
                CreateExcelFile.CreateExcelDocument(lstReporte, Reporte, FileName, header.ToArray(), footer);
                return true;
            }
            return false;
        }

        private List<string> GetHeader(Semana oSemana, string Titulo)
        {
            List<string> header = new List<string>();
            header.Add("AUDITUR");
            header.Add("Informe de Auditoría: " + Titulo);
            header.Add("Empresa " + oSemana.Agencia.Nombre + " - IATA " + oSemana.Agencia.ID);
            header.Add("Período de Liquidación del " + oSemana.FechaDesde.ToShortDateString() + " hasta " + oSemana.FechaHasta.ToShortDateString() + " - Semana " + oSemana.Periodo.ToShortDateString());
            return header;
        }

        private void frmReportes_Load(object sender, EventArgs e)
        {
            btnExaminar.Enabled = semanaToReport != null;
        }

        private void btnExaminar_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog
            {
                Description = "Por favor, selecciona la carpeta donde se guardarán los reportes.",
                ShowNewFolderButton = true
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = dialog.SelectedPath;
                grbReportes.Enabled = true;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = AuditurHelpers.DefaultCultureInfo();
            System.Threading.Thread.CurrentThread.CurrentCulture = AuditurHelpers.DefaultCultureInfo();

            BackgroundWorker bw = sender as BackgroundWorker;

            int Opcion = ((int)e.Argument);

            int ReportesGenerados = 0;
            if ((Opcion == 1 || Opcion == 0) && GenerarEmisiones()) ReportesGenerados++;
            if ((Opcion == 2 || Opcion == 0) && GenerarDiferenciasIVA()) ReportesGenerados++;
            if ((Opcion == 3 || Opcion == 0) && GenerarCreditos()) ReportesGenerados++;
            if ((Opcion == 4 || Opcion == 0) && GenerarDebitos()) ReportesGenerados++;
            if ((Opcion == 5 || Opcion == 0) && GenerarDiferenciasEmisiones()) ReportesGenerados++;
            if ((Opcion == 6 || Opcion == 0) && GenerarOvers()) ReportesGenerados++;
            if ((Opcion == 7 || Opcion == 0) && GenerarReembolsos()) ReportesGenerados++;
            if ((Opcion == 8 || Opcion == 0) && GenerarSituacionBOs()) ReportesGenerados++;
            if ((Opcion == 9 || Opcion == 0) && GenerarSituacionBSPs()) ReportesGenerados++;
            if (Opcion == 10 && GenerarFacturacion()) ReportesGenerados++;

            e.Result = new Result { ReportesGenerados = ReportesGenerados, Opcion = Opcion };
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.MarqueeAnimationSpeed = 0;
            lblProgressStatus.Text = "";

            Result Result = (Result)e.Result;
            if (Result.ReportesGenerados == 0)
            {
                MessageBox.Show("No se han podido crear los reportes", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                OpenFolder();
                if (Result.Opcion == 0)
                {
                    if (Result.ReportesGenerados == 9) //Hay 10 reportes para generar
                        MessageBox.Show("Se han generado todos los reportes correctamente", "Operación satisfactoria.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Se han generado los reportes, pero uno o más han fallado.", "Operación con errores.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (Result.ReportesGenerados == 1)
                {
                    MessageBox.Show("Reporte generado correctamente", "Operación satisfactoria.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            EnableControls(true);
        }
    }
}
