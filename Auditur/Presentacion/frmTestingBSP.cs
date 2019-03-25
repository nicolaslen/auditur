using Auditur.Negocio;
using Auditur.Presentacion.Classes;
using Helpers;
using iText.Kernel.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Path = System.IO.Path;

namespace Auditur.Presentacion
{
    public partial class frmTestingBSP : UserControl
    {
        public frmTestingBSP()
        {
            InitializeComponent();
            BSPActions = new BSPActions(Application.CurrentCulture);
        }

        private BSPActions BSPActions { get; set; }
        private int pageStart { get; set; }

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
                BSP_ReadPdfFile_Cabecera(txtFilePath_BSP.Text);
                btnReadFile.Enabled = true;
            }
        }

        #region BSP

        public void BSP_ReadPdfFile_Cabecera(string fileName)
        {
            int page = 0, index = 0;
            int paginaInicial = 0;
            try
            {
                paginaInicial = 1;
            }
            catch (Exception Exception1)
            {
                TextToFile.Errores(TextToFile.Error(Exception1));
                MessageBox.Show("Error: " + Exception1.Message + "\nfileName: " + fileName + "\npage: " + page + "\nline: " + index, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Semana actual: Semana del archivo seleccionado.
            pageStart = paginaInicial;
        }

        public void BSP_ReadPdfFile(string fileName)
        {
            int page = 0, index = 0;
            string currentText = "";
            string testingpath = Path.Combine(Path.GetDirectoryName(fileName), "text.txt");

            List<BSP_Ticket> tickets = new List<BSP_Ticket>();
            Compania compania = null;
            bool encontreLlave = false;
            string llave = "";
            BSP_Ticket bspTicket = null;

            if (!File.Exists(testingpath))
                File.Create(testingpath);
            File.WriteAllText(testingpath, string.Empty);

            try
            {
                if (File.Exists(fileName))
                {
                    PdfReader pdfReader = new PdfReader(fileName);
                    PdfDocument pdfDoc = new PdfDocument(pdfReader);

                    for (page = 1; page <= pdfDoc.GetNumberOfPages(); page++)
                    {
                        var pdfPage = pdfDoc.GetPage(page);

                        var pageChunks = pdfPage.ExtractChunks();
                        var pageLines = pageChunks.GroupBy(x => x.Y).OrderByDescending(y => y.Key).ToList();

                        if (!pageChunks.Any() || pageChunks.All(x => x.Text != "CIA"))
                            continue;

                        float alturaCIA = pageChunks.Where(y => y.Text == "CIA").Select(y => y.Y).FirstOrDefault();
                        float alturaUltimoElemento = pageChunks.Min(x => x.Y);
                        //float alturaTickets = alturaCIA - 43;
                        bool saltearDireccion = false;

                        var filteredPageLines = pageLines.Where(x =>
                            x.Any() &&
                            alturaCIA > x.Key && x.Key > alturaUltimoElemento); //Filtro desde el encabezado para arriba, y la última línea con la fecha

                        compania = null;

                        foreach (var line in filteredPageLines)
                        {
                            var orderedLine = line.OrderBy(x => x.StartX).ToList();

                            if (compania == null)
                            {
                                string posibleCompania = orderedLine.First().Text;
                                if (posibleCompania.Length != 3 ||
                                    !int.TryParse(posibleCompania, out int codCompania))
                                    break; //Si la primer línea que le sigue al encabezado, no empieza con un número de compañía, salteo la página

                                compania = new Compania
                                {
                                    Codigo = codCompania.ToString(), //TODO: SACAR EL TOSTRING
                                    Nombre = orderedLine.ElementAt(1).Text
                                };
                                saltearDireccion = true;

                                continue;
                            }

                            if (saltearDireccion)
                            {
                                saltearDireccion = false;
                                continue;
                            }

                            /*if (line.Key > alturaTickets)
                                continue;*/

                            if (!encontreLlave)
                            {
                                string posibleLlave = orderedLine.First().Text;
                                if (posibleLlave.Length < 3 || posibleLlave.Substring(0, 3) != "***")
                                    continue;

                                encontreLlave = true;
                                llave = posibleLlave.Substring(4);
                                continue;
                            }

                            if (encontreLlave && orderedLine.First().Text == llave + " TOTAL")
                            {
                                llave = "";
                                encontreLlave = false;
                                continue;
                            }

                            var posibleTicket = orderedLine.GetChunkTextBetween(22, 34);
                            if (posibleTicket == null || !int.TryParse(posibleTicket, out int ticketCodCompania))
                            {
                                if (bspTicket == null) continue;
                            }
                            else
                            {
                                if (compania.Codigo != ticketCodCompania.ToString())//TODO: SACAR EL TOSTRING
                                    throw new Exception("Se llegó a otra compañía sin darnos cuenta");

                                if (bspTicket != null)
                                    tickets.Add(bspTicket);

                                bspTicket = orderedLine.ObtenerBSP_Ticket(compania, null);

                                continue;
                            }

                            if (orderedLine.First().Text.Length >= 4 && new[] { "TOUR", "ESAC" }.Contains(orderedLine.First().Text.Substring(0, 4)))
                                continue;

                            var detalle = orderedLine.ObtenerBSP_Ticket_Detalle();

                            bspTicket.Detalle.Add(detalle);
                        }
                    }
                    if (bspTicket != null)
                    {
                        tickets.Add(bspTicket);
                    }

                    pdfDoc.Close();
                    pdfReader.Close();
                }
            }
            catch (Exception Exception1)
            {
                TextToFile.Errores(TextToFile.Error(Exception1));
                MessageBox.Show("Error: " + Exception1.Message + "\nfileName: " + fileName + "\npage: " + page + "\nline: " + index, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion BSP

        private void btnReadFile_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync(true);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            bool blnResult = false;
            BackgroundWorker bw = sender as BackgroundWorker;

            BSP_ReadPdfFile(txtFilePath_BSP.Text);

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

            progressBar1.Value = 0;
            /*lblProgressStatus.Text = "";
            progressBar1.Value = 0;

            txtFilePath_BSP.ResetText();*/
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            lblProgressStatus.Text = (string)e.UserState;
        }
    }

    public class PageChunks
    {
        public string Text { get; set; }
        public float StartX { get; set; }
        public float Y { get; set; }
        public float RelativeY => 595.5f - Y;
        public float EndX { get; set; }
        public float EndY { get; set; }
    }
}