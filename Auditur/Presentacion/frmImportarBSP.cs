using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Auditur.Negocio;
using Auditur.Presentacion.Classes;
using Helpers;
using System.ComponentModel;
using System.Globalization;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;

namespace Auditur.Presentacion
{
    public partial class frmImportarBSP : UserControl
    {
        public frmImportarBSP()
        {
            InitializeComponent();
            BSPActions = new BSPActions(Application.CurrentCulture);
        }

        private BSPActions BSPActions { get; set; }
        private Semana semanaToImport { get; set; }
        private List<ACM> listACM { get; set; }
        private List<ADM> listADM { get; set; }

        private void btnExaminar_BSP_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Archivos PDF|*.pdf",
                Title = "Por favor, selecciona el archivo BSP."
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtFilePath_BSP.Text = dialog.FileName;
                txtFilePath_ACM.Text = "";
                txtFilePath_ADM.Text = "";

                BSP_ReadPdfFile_Cabecera(txtFilePath_BSP.Text);
                if (semanaToImport != null && semanaToImport.Agencia != null)
                {
                    txtAgencia.Text = semanaToImport.Agencia.Nombre;
                    txtPeriodoS.Text = semanaToImport.Periodo.Day.ToString();
                    txtPeriodoM.Text = semanaToImport.Periodo.Month.ToString();
                    txtPeriodoA.Text = semanaToImport.Periodo.Year.ToString();

                    dtpFechaDesde.Value = semanaToImport.FechaDesde;
                    dtpFechaHasta.Value = semanaToImport.FechaHasta;

                    btnExaminar_ACM.Enabled = true;
                    btnExaminar_ADM.Enabled = true;

                    btnReadFile.Enabled = true;
                }
                else
                {
                    txtFilePath_BSP.Text = "";
                    txtAgencia.Text = "";
                    txtPeriodoS.Text = "";
                    txtPeriodoM.Text = "";
                    txtPeriodoA.Text = "";

                    dtpFechaDesde.Value = DateTime.Now;
                    dtpFechaHasta.Value = DateTime.Now;

                    btnExaminar_ACM.Enabled = false;
                    btnExaminar_ADM.Enabled = false;

                    btnReadFile.Enabled = false;
                }
            }
        }

        #region BSP
        public void BSP_ReadPdfFile_Cabecera(string fileName)
        {
            Semana oSemanaAux = null;
            try
            {
                semanaToImport = null;
                if (File.Exists(fileName))
                {
                    PdfReader pdfReader = new PdfReader(fileName);
                    PdfDocument pdfDoc = new PdfDocument(pdfReader);
                    var pdfPage = pdfDoc.GetPage(1);

                    Agencias agencias = new Agencias();

                    var pageChunks = pdfPage.ExtractChunks();
                    var pageLines = pageChunks.GroupBy(x => x.Y).OrderByDescending(y => y.Key).ToList();

                    var agencyLine = pageLines.ElementAt(0);
                    var datesLine = pageLines.ElementAt(1);

                    string strAgencia = agencyLine.ElementAt(2).Text;
                    var splitAgencia = strAgencia.Split(' ');

                    string dates = datesLine.ElementAt(1).Text;
                    string periodo = dates.Substring(0, 6);
                    string fechaDesde = dates.Substring(7, 11);
                    string fechaHasta = dates.Substring(22, 11);


                    if (int.TryParse(splitAgencia[0].Replace("-", "") + splitAgencia[1], out var IDAgencia))
                    {
                        var oAgencia = agencias.GetByID(IDAgencia);
                        if (oAgencia == null)
                        {
                            oAgencia = new Agencia { ID = IDAgencia, Nombre = String.Join(" ", splitAgencia.Skip(3)) };
                            agencias.Insertar(oAgencia);
                        }
                        oSemanaAux = new Semana
                        {
                            Periodo = new DateTime(int.Parse(DateTime.Now.Year.ToString().Substring(0, 2) + periodo.Substring(0, 2)), int.Parse(periodo.Substring(2, 2)), int.Parse(periodo.Substring(4, 2))),
                            FechaDesde = Convert.ToDateTime(fechaDesde),
                            FechaHasta = Convert.ToDateTime(fechaHasta),
                            Agencia = oAgencia
                        };

                        if (Publics.Semana != null && Publics.Semana.Agencia.ID == oSemanaAux.Agencia.ID && Publics.Semana.Periodo == oSemanaAux.Periodo)
                        {
                            oSemanaAux.ID = Publics.Semana.ID;
                            oSemanaAux.BSPCargado = Publics.Semana.BSPCargado;
                            oSemanaAux.BOCargado = Publics.Semana.BOCargado;
                            oSemanaAux.TicketsBO = Publics.Semana.TicketsBO;
                        }
                        else
                        {
                            Semanas semanas = new Semanas();
                            Semana oSemanaVerif = semanas.GetByAgenciaPeriodo(oSemanaAux.Agencia.ID, oSemanaAux.Periodo);
                            semanas.CloseConnection();

                            if (oSemanaVerif != null)
                            {
                                oSemanaAux.ID = oSemanaVerif.ID;
                                oSemanaAux.BSPCargado = oSemanaVerif.BSPCargado;
                                if (oSemanaVerif.BOCargado)
                                {
                                    oSemanaAux.BOCargado = true;
                                    BO_Tickets BO_Tickets = new BO_Tickets();
                                    oSemanaAux.TicketsBO = BO_Tickets.ObtenerPorSemana(oSemanaAux.ID);
                                    BO_Tickets.CloseConnection();
                                }
                            }
                        }
                    }
                    agencias.CloseConnection();
                }
            }
            catch (Exception Exception1)
            {
                TextToFile.Errores(TextToFile.Error(Exception1));
                MessageBox.Show("Error: " + Exception1.Message + "\nfileName: " + fileName + "\n", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Semana actual: Semana del archivo seleccionado.
            semanaToImport = oSemanaAux;
        }

        public void BSP_ReadPdfFile(string fileName)
        {
            int page = 0, index = 0;

            BSP_Ticket oBSP_Ticket = null;
            //BSP_Ticket_Detalle oBSP_Ticket_Detalle = null;

            Companias companias = new Companias();
            List<Compania> lstCompanias = companias.GetAll();
            List<Compania> lstNuevasCompanias = new List<Compania>();

            Conceptos conceptos = new Conceptos();
            List<Concepto> lstConceptos = conceptos.GetAll();
            string strFinConcepto = "";
            List<Concepto> lstNuevosConceptos = new List<Concepto>();
            Concepto concepto = null;

            BSP_Rg? rg = null;
            Moneda? moneda = null;

            try
            {
                if (File.Exists(fileName))
                {
                    semanaToImport.TicketsBSP = new List<BSP_Ticket>();

                    PdfReader pdfReader = new PdfReader(fileName);
                    PdfDocument pdfDoc = new PdfDocument(pdfReader);

                    for (page = 1; page <= pdfDoc.GetNumberOfPages(); page++)
                    {
                        var pdfPage = pdfDoc.GetPage(page);

                        var pageChunks = pdfPage.ExtractChunks();
                        var pageLines = pageChunks.GroupBy(x => x.Y).OrderByDescending(y => y.Key).ToList();

                        var lineOfRg = pageLines.FirstOrDefault(x => x.Any(y => y.Text == "SCOPE"));
                        if (lineOfRg != null)
                        {
                            rg = lineOfRg.ElementAt(1).Text == "INTERNATIONAL"
                                ? BSP_Rg.Internacional
                                : BSP_Rg.Doméstico;
                        }

                        var lineOfMoneda = pageLines.FirstOrDefault(x => x.Any(y => y.Text == "ARS" || y.Text == "USD"));
                        if (lineOfMoneda != null)
                        {
                            moneda = lineOfMoneda.ElementAt(1).Text == "ARS"
                                ? Moneda.Peso
                                : Moneda.Dolar;
                        }

                        if (!pageChunks.Any() || pageChunks.All(x => x.Text != "CIA"))
                            continue;

                        float alturaCIA = pageChunks.Where(y => y.Text == "CIA").Select(y => y.Y).FirstOrDefault();
                        float alturaUltimoElemento = pageChunks.Min(x => x.Y);
                        bool saltearDireccion = false;

                        var filteredPageLines = pageLines.Where(x =>
                            x.Any() &&
                            alturaCIA > x.Key &&
                            x.Key >
                            alturaUltimoElemento); //Filtro desde el encabezado para arriba, y la última línea con la fecha

                        Compania compania = null;

                        foreach (var line in filteredPageLines)
                        {
                            var orderedLine = line.OrderBy(x => x.StartX).ToList();

                            if (compania == null)
                            {
                                string posibleCompania = orderedLine.First().Text;
                                if (posibleCompania.Length != 3 ||
                                    !int.TryParse(posibleCompania, out int codCompania))
                                    break; //Si la primer línea que le sigue al encabezado, no empieza con un número de compañía, salteo la página

                                compania = lstCompanias.Find(x => x.ID == codCompania);
                                if (compania == null)
                                {
                                    compania = new Compania { ID = codCompania, Nombre = orderedLine.ElementAt(1).Text };
                                    companias.Insertar(compania);
                                    lstCompanias.Add(compania);
                                    lstNuevasCompanias.Add(compania);
                                }

                                saltearDireccion = true;

                                continue;
                            }

                            if (saltearDireccion)
                            {
                                saltearDireccion = false;
                                continue;
                            }

                            if (concepto == null)
                            {
                                string posibleLlave = orderedLine.First().Text;
                                if (posibleLlave.Length < 3 || posibleLlave.Substring(0, 3) != "***")
                                    continue;

                                string llave = posibleLlave.Substring(4);
                                concepto = lstConceptos.Find(x => x.Nombre == llave);
                                if (concepto == null)
                                {
                                    concepto = new Concepto { Nombre = llave };
                                    conceptos.Insertar(concepto);
                                    lstConceptos.Add(concepto);
                                    lstNuevosConceptos.Add(concepto);
                                }

                                strFinConcepto = llave + " TOTAL";
                                continue;
                            }

                            if (orderedLine.First().Text == strFinConcepto)
                            {
                                concepto = null;
                                continue;
                            }

                            var posibleTicket = orderedLine.GetChunkTextBetween(22, 34);
                            if (posibleTicket == null || !int.TryParse(posibleTicket, out int ticketCodCompania))
                            {
                                if (oBSP_Ticket == null) continue;
                            }
                            else
                            {
                                if (compania.ID != ticketCodCompania)
                                    throw new Exception("Se llegó a otra compañía sin darnos cuenta");

                                if (oBSP_Ticket != null)
                                    semanaToImport.TicketsBSP.Add(oBSP_Ticket);

                                oBSP_Ticket = orderedLine.ObtenerBSP_Ticket(compania, concepto, moneda.Value, rg.Value, Application.CurrentCulture);
                                continue;
                            }

                            if (oBSP_Ticket != null && orderedLine.First().Text.Length >= 4 &&
                                orderedLine.First().Text.Substring(0, 4) == "TOUR")
                            {
                                oBSP_Ticket.Tour = orderedLine.First().Text.Substring(5).Trim();
                                continue;
                            }

                            if (oBSP_Ticket != null && orderedLine.First().Text.Length >= 4 &&
                                orderedLine.First().Text.Substring(0, 4) == "ESAC")
                            {
                                oBSP_Ticket.Esac = orderedLine.First().Text.Substring(5).Trim();
                                continue;
                            }

                            var detalle = orderedLine.ObtenerBSP_Ticket_Detalle();

                            //ACM y ADM
                            if (listACM != null && concepto.Nombre == "CREDIT MEMOS")
                            {
                                ACM oACM = listACM.Find(x => x.Billete == oBSP_Ticket.NroDocumento);
                                if (oACM != null)
                                    detalle.Observaciones += (string.IsNullOrEmpty(detalle.Observaciones) ? "" : "|") + oACM.Observaciones;
                            }
                            if (listADM != null && concepto.Nombre == "DEBIT MEMOS")
                            {
                                ADM oADM = listADM.Find(x => x.Billete == oBSP_Ticket.NroDocumento);
                                if (oADM != null)
                                    detalle.Observaciones += (string.IsNullOrEmpty(detalle.Observaciones) ? "" : "|") + oADM.Observaciones;
                            }

                            oBSP_Ticket.Detalle.Add(detalle);
                        }
                    }

                    if (oBSP_Ticket != null)
                    {
                        semanaToImport.TicketsBSP.Add(oBSP_Ticket);
                    }

                    semanaToImport.TicketsBSP = semanaToImport.TicketsBSP.OrderBy(x => x.Compania.Codigo).ThenBy(x => x.NroDocumento).ToList();
                    semanaToImport.BSPCargado = true;

                    pdfDoc.Close();
                    pdfReader.Close();

                    if (lstNuevasCompanias.Any())
                    {
                        string mensaje = "Se han encontrado Compañías Aéreas que no estaban registradas en el sistema:\n\n";
                        lstNuevasCompanias.ForEach(x => mensaje += "ID: " + x.ID + " | Nombre: " + x.Nombre + "\n");
                        mensaje += "\nÉstas han sido guardadas automáticamente, pero el código alfanumérico deberá ser ingresado manualmente.\n";
                        mensaje += "Por favor, ingrese al \"ABM de Compañías\" y complete la información solicitada.\n";
                        mensaje += "Muchas gracias.";
                        MessageBox.Show(mensaje, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception Exception1)
            {
                TextToFile.Errores(TextToFile.Error(Exception1));
                MessageBox.Show("Error: " + Exception1.Message + "\nfileName: " + fileName + "\npage: " + page + "\nline: " + index, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                companias.CloseConnection();
                conceptos.CloseConnection();
            }
        }

        public void ACM_ReadPdfFile(string fileName)
        {
            string Linea;
            string Observaciones;
            ACM oACM = null;
            try
            {
                if (!File.Exists(fileName))
                    return;

                listACM = new List<ACM>();

                StreamReader fileReader = new StreamReader(fileName);
                fileReader.ReadLine();
                while ((Linea = fileReader.ReadLine()) != null)
                {
                    string[] Columnas = Linea.Split(new char[] { '|' });
                    long BilleteACM = 0;
                    if (long.TryParse(Columnas[3], out BilleteACM))
                    {
                        Observaciones = Linea;
                        while (Observaciones.IndexOf("||") > -1)
                            Observaciones = Observaciones.Replace("||", "|");
                        if (Observaciones.Substring(Observaciones.Length - 1) == "|") Observaciones = Observaciones.Substring(0, Observaciones.Length - 1);
                        oACM = new ACM { Billete = BilleteACM, Observaciones = Observaciones };
                        listACM.Add(oACM);
                    }
                }

                fileReader.Close();
            }
            catch (Exception Exception1)
            {
                MessageBox.Show("Error: " + Exception1.Message + "\nfileName: " + fileName, "Error");
            }
        }

        public void ADM_ReadPdfFile(string fileName)
        {
            string Linea;
            string Observaciones;
            ADM oADM = null;
            try
            {
                if (!File.Exists(fileName))
                    return;

                listADM = new List<ADM>();

                StreamReader fileReader = new StreamReader(fileName);
                fileReader.ReadLine();
                while ((Linea = fileReader.ReadLine()) != null)
                {
                    string[] Columnas = Linea.Split(new char[] { '|' });
                    long BilleteADM = 0;
                    if (long.TryParse(Columnas[3], out BilleteADM))
                    {
                        Observaciones = Linea;
                        while (Observaciones.IndexOf("||") > -1)
                            Observaciones = Observaciones.Replace("||", "|");
                        if (Observaciones.Substring(Observaciones.Length - 1) == "|") Observaciones = Observaciones.Substring(0, Observaciones.Length - 1);
                        oADM = new ADM { Billete = BilleteADM, Observaciones = Observaciones };
                        listADM.Add(oADM);
                    }
                }

                fileReader.Close();
            }
            catch (Exception Exception1)
            {
                MessageBox.Show("Error: " + Exception1.Message + "\nfileName: " + fileName, "Error");
            }
        }
        #endregion
        private void btnReadFile_Click(object sender, EventArgs e)
        {
            if (semanaToImport != null && semanaToImport.Agencia != null && (!semanaToImport.BSPCargado || MessageBox.Show("Ya ha cargado este archivo previamente. ¿Desea volver a cargarlo?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {
                EnableDisableControls(false);
                backgroundWorker1.RunWorkerAsync(true);
            }
        }

        private void EnableDisableControls(bool Habilitar)
        {
            grbFileImport_BSP.Enabled = Habilitar;
            grbACM.Enabled = Habilitar;
            grbADM.Enabled = Habilitar;
            this.Parent.Parent.Controls[0].Enabled = Habilitar;

            if (!Habilitar)
            {
                btnReadFile.Enabled = false;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = AuditurHelpers.DefaultCultureInfo();
            System.Threading.Thread.CurrentThread.CurrentCulture = AuditurHelpers.DefaultCultureInfo();

            bool blnResult = false;
            BackgroundWorker bw = sender as BackgroundWorker;

            listACM = null;
            listADM = null;
            if (!string.IsNullOrEmpty(txtFilePath_ACM.Text))
                ACM_ReadPdfFile(txtFilePath_ACM.Text);
            if (!string.IsNullOrEmpty(txtFilePath_ADM.Text))
                ADM_ReadPdfFile(txtFilePath_ADM.Text);

            BSP_ReadPdfFile(txtFilePath_BSP.Text);
            if ((bool)e.Argument)
                BSPActions.Guardar(semanaToImport, backgroundWorker1);

            blnResult = true;

            e.Result = blnResult;
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

            txtFilePath_BSP.ResetText();
            txtAgencia.ResetText();
            txtPeriodoA.ResetText();
            txtPeriodoM.ResetText();
            txtPeriodoS.ResetText();
            dtpFechaDesde.ResetText();
            dtpFechaHasta.ResetText();

            EnableDisableControls(true);
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            lblProgressStatus.Text = (string)e.UserState;
        }

        private void btnExaminar_ACM_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Archivos TXT|*.txt",
                Title = "Por favor, selecciona el archivo ACM."
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtFilePath_ACM.Text = dialog.FileName;
            }
        }

        private void btnExaminar_ADM_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Archivos TXT|*.txt",
                Title = "Por favor, selecciona el archivo ACM."
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtFilePath_ADM.Text = dialog.FileName;
            }
        }

        /*
        public void TESTING()
        {
            //ELEGIR PATH
            string path = "";
            foreach (string txtName in Directory.EnumerateFiles(path, "*.pdf"))
            {
                BSP_ReadPdfFileTESTING(path + "\\" + txtName);
            }
        }

        public void BSP_ReadPdfFileTESTING(string fileName)
        {
            int page = 0, index = 0;
            string currentText = "";

            BSP_Rg Tipo = BSP_Rg.Ambas;
            int CompaniaID = 0;

            string Linea = "";
            try
            {
                //ELEGIR PATH
                string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                StringBuilder sb = new StringBuilder();

                // Enumerate the files in the My Documents path, filtering for text files only.
                foreach (string txtName in Directory.EnumerateFiles(mydocpath, "*.txt"))
                {
                    // Open a stream reader and write the name of the file, a visual separator, 
                    // and the contents of the file to the stream.
                    using (StreamWriter outfile = new StreamWriter(mydocpath + @"\AllTxtFiles.txt"))
                    {
                        PdfReader pdfReader = new PdfReader(fileName);
                        for (page = pageStart; page <= pdfReader.NumberOfPages; page++)
                        {
                            currentText = "";
                            currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, new SimpleTextExtractionStrategy());
                            currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));

                            string[] arrLineas = currentText.Split(new char[] { '\n' });
                            if (BSPActions.EsAnalisisDeVenta(ref arrLineas))
                            {
                                foreach (string linea in arrLineas)
                                {
                                    outfile.WriteLine(linea);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception Exception1)
            {
                TextToFile.Errores(TextToFile.Error(Exception1));
                MessageBox.Show("Error: " + Exception1.Message + "\nfileName: " + fileName + "\npage: " + page + "\nline: " + index, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }*/
    }
}
