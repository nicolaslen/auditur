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
        int pageStart { get; set; }

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
            int page = 0, index = 0;
            string currentText = "";
            int paginaInicial = 0;
            int IDAgencia = 0;
            Agencia oAgencia = null;
            Semana oSemanaAux = null;
            try
            {
                pageStart = 0;
                semanaToImport = null;
                if (File.Exists(fileName))
                {
                    PdfReader pdfReader = new PdfReader(fileName);
                    Agencias agencias = new Agencias();
                    for (page = 1; page <= pdfReader.GetFileLength() && paginaInicial == 0; page++)
                    {
                        currentText = "";
                        currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, new SimpleTextExtractionStrategy());
                        currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));

                        string[] arrLineas = currentText.Split(new char[] { '\n' });
                        if (BSPActions.EsAnalisisDeVenta(ref arrLineas))
                        {
                            string Linea4 = arrLineas[4];
                            string strAgencia = Linea4.Substring(Linea4.IndexOf("LIQUIDACION DE :") + ("LIQUIDACION DE :").Length, 70).Trim();
                            if (int.TryParse(strAgencia.Substring(0, strAgencia.IndexOf(" ") + 2).Replace(" ", ""), out IDAgencia))
                            {
                                oAgencia = agencias.GetByID(IDAgencia);
                                if (oAgencia == null)
                                {
                                    oAgencia = new Agencia { ID = IDAgencia, Nombre = strAgencia.Substring(11) };
                                    agencias.Insertar(oAgencia);
                                }

                                string Linea2 = arrLineas[2];
                                string Linea3 = arrLineas[3];
                                oSemanaAux = new Semana
                                {
                                    Periodo = Convert.ToDateTime(Linea2.Substring(Linea2.LastIndexOf("PERIODO: ") + ("PERIODO: ").Length, 10).Trim()),
                                    FechaDesde = Convert.ToDateTime(Linea3.Substring(Linea3.LastIndexOf("DEL: ") + ("DEL: ").Length, 8).Trim()),
                                    FechaHasta = Convert.ToDateTime(Linea3.Substring(Linea3.LastIndexOf("AL ") + ("AL ").Length, 8).Trim()),
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

                                paginaInicial = page;
                            }
                        }
                    }
                    agencias.CloseConnection();
                }
            }
            catch (Exception Exception1)
            {
                TextToFile.Errores(TextToFile.Error(Exception1));
                MessageBox.Show("Error: " + Exception1.Message + "\nfileName: " + fileName + "\npage: " + page + "\nline: " + index, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Semana actual: Semana del archivo seleccionado.
            semanaToImport = oSemanaAux;
            pageStart = paginaInicial;
        }

        public void BSP_ReadPdfFile(string fileName)
        {
            int page = 0, index = 0;
            string currentText = "";

            BSP_Ticket oBSP_Ticket = null;
            BSP_Ticket_Detalle oBSP_Ticket_Detalle = null;

            Companias companias = new Companias();
            List<Compania> lstCompanias = companias.GetAll();
            Compania oCompaniaActual = null;
            string strFinCompania = "";
            List<Compania> lstNuevasCompanias = new List<Compania>();

            Conceptos conceptos = new Conceptos();
            List<Concepto> lstConceptos = conceptos.GetAll();
            Concepto oConceptoActual = null;
            string strFinConcepto = "";

            BSP_Rg Tipo = BSP_Rg.Ambas;
            int CompaniaID = 0;

            string Linea = "";
            try
            {
                if (File.Exists(fileName))
                {
                    semanaToImport.TicketsBSP = new List<BSP_Ticket>();

                    PdfReader pdfReader = new PdfReader(fileName);
                    for (page = pageStart; page <= pdfReader.NumberOfPages; page++)
                    {
                        currentText = "";
                        currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, new SimpleTextExtractionStrategy());
                        currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));

                        string[] arrLineas = currentText.Split(new char[] { '\n' });
                        if (BSPActions.EsAnalisisDeVenta(ref arrLineas))
                        {
                            string strTipo = arrLineas[3].Substring(21, 30).Trim();
                            Tipo = BSP_Rg.Ambas;
                            if (strTipo == "DOMESTICO")
                                Tipo = BSP_Rg.Doméstico;
                            else if (strTipo == "INTERNACIONAL")
                                Tipo = BSP_Rg.Internacional;

                            if (Tipo != BSP_Rg.Ambas) //Si no estoy en el resumen final
                            {
                                for (index = 10; index < arrLineas.Length; index++) //10: Salteo cabecera
                                {
                                    Linea = arrLineas[index];
                                    if (Linea.Trim() != "")
                                    {
                                        if (oCompaniaActual == null && index == 10 && Linea.Length >= 3 && int.TryParse(Linea.Substring(0, 3), out CompaniaID)) //Si no estoy dentro de una compañía aerea...
                                        {
                                            oCompaniaActual = lstCompanias.Find(x => x.ID == CompaniaID);
                                            if (oCompaniaActual == null)
                                            {
                                                oCompaniaActual = new Compania() { ID = CompaniaID, Nombre = Linea.Substring(4).Trim() };
                                                companias.Insertar(oCompaniaActual);
                                                lstCompanias.Add(oCompaniaActual);
                                                lstNuevasCompanias.Add(oCompaniaActual);
                                            }
                                            strFinCompania = "TOT " + Linea.Substring(4).Trim();
                                        }
                                        else if (Linea.Length >= strFinCompania.Length && Linea.Substring(0, strFinCompania.Length) == strFinCompania) //Si estoy en el cierre de la compañía aerea...
                                        {
                                            oCompaniaActual = null;
                                        }
                                        else if (oConceptoActual == null) //Si estoy en la compañía aerea y no estoy dentro de un concepto...
                                        {
                                            oConceptoActual = lstConceptos.Find(x => x.Nombre.Length <= Linea.Length && x.Nombre.ToUpper() == Linea.Substring(0, x.Nombre.Length));
                                            if (oConceptoActual != null)
                                                strFinConcepto = "TOT " + (oConceptoActual.Nombre.Length >= 21 ? oConceptoActual.Nombre.Substring(0, 21) : oConceptoActual.Nombre).Trim().ToUpper();
                                        }
                                        else if (Linea.Length >= strFinConcepto.Length && Linea.Substring(0, strFinConcepto.Length) == strFinConcepto) //Si estoy en la compañía aerea y estoy en el cierre de un concepto
                                        {
                                            oConceptoActual = null;
                                        }
                                        else if (BSPActions.EsNuevoTicket(Linea)) //Si estoy en la compañía aerea y estoy en el concepto, y si los primeros 10 caracteres son long...
                                        {
                                            if (oBSP_Ticket != null)
                                            {
                                                semanaToImport.TicketsBSP.Add(oBSP_Ticket);
                                                oBSP_Ticket = null;
                                            }
                                            oBSP_Ticket = BSPActions.GetTicket(Linea, semanaToImport.Periodo.Year);
                                            oBSP_Ticket.Concepto = oConceptoActual;
                                            oBSP_Ticket.Compania = oCompaniaActual;
                                            oBSP_Ticket.Rg = Tipo;

                                            oBSP_Ticket_Detalle = BSPActions.GetTicketDetalle(Linea);

                                            //ACM y ADM
                                            if (listACM != null && oConceptoActual.Tipo == 'C')
                                            {
                                                ACM oACM = listACM.Find(x => x.Billete == oBSP_Ticket.Billete);
                                                if (oACM != null)
                                                    oBSP_Ticket_Detalle.Observaciones += (string.IsNullOrEmpty(oBSP_Ticket_Detalle.Observaciones) ? "" : "|") + oACM.Observaciones;
                                            }
                                            if (listADM != null && oConceptoActual.Tipo == 'D')
                                            {
                                                ADM oADM = listADM.Find(x => x.Billete == oBSP_Ticket.Billete);
                                                if (oADM != null)
                                                    oBSP_Ticket_Detalle.Observaciones += (string.IsNullOrEmpty(oBSP_Ticket_Detalle.Observaciones) ? "" : "|") + oADM.Observaciones;
                                            }

                                            oBSP_Ticket.Detalle.Add(oBSP_Ticket_Detalle);
                                            oBSP_Ticket_Detalle = null;
                                        }
                                        else if (oBSP_Ticket != null)
                                        {
                                            oBSP_Ticket_Detalle = BSPActions.GetTicketDetalle(Linea);
                                            oBSP_Ticket.Detalle.Add(oBSP_Ticket_Detalle);
                                            oBSP_Ticket_Detalle = null;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Linea = arrLineas[10];
                                string strMoneda = Linea.Substring(("MONEDA: ").Length, 3);
                                Moneda Moneda = strMoneda == "ARS" ? Moneda.Peso : Moneda.Dolar;
                                foreach (BSP_Ticket bsp_ticket in semanaToImport.TicketsBSP.Where(x => x.Moneda == null))
                                    bsp_ticket.Moneda = Moneda;
                                if (oBSP_Ticket != null)
                                    oBSP_Ticket.Moneda = Moneda;
                            }
                        }
                    }
                    if (oBSP_Ticket != null)
                    {
                        semanaToImport.TicketsBSP.Add(oBSP_Ticket);
                        oBSP_Ticket = null;
                    }
                    semanaToImport.TicketsBSP = semanaToImport.TicketsBSP.OrderBy(x => x.Compania.Codigo).ThenBy(x => x.Billete).ToList();
                    semanaToImport.BSPCargado = true;
                    pdfReader.Close();
                    if (lstNuevasCompanias.Count > 0)
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
