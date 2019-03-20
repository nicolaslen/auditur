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
using System.Reflection;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Data;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
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

                                continue;
                            }

                            if (!encontreLlave)
                            {
                                string posibleLlave = orderedLine.First().Text;
                                if (posibleLlave.Substring(0, 3) != "***")
                                    continue;

                                encontreLlave = true;
                                llave = posibleLlave.Substring(4);
                                continue;
                            }

                            if (orderedLine.First().Text == llave + " TOTAL")
                            {
                                llave = "";
                                encontreLlave = false;
                                continue;
                            }

                            var posibleTicket = orderedLine.GetChunkTextBetween(22, 24, 31, 34);
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

                                bspTicket = new BSP_Ticket();

                                bspTicket.Llave = llave;
                                bspTicket.TRNC = orderedLine.GetChunkTextBetween(37, 44, 50, 58);
                                bspTicket.Billete = Convert.ToInt64(orderedLine.GetChunkTextBetween(65, 67, 95, 96));
                                bspTicket.FechaVenta = DateTime.TryParse(orderedLine.GetChunkTextBetween(113, 115, 137, 138),
                                    out var fechaVenta) ? (DateTime?)fechaVenta : null;
                                bspTicket.CPN = orderedLine.GetChunkTextBetween(149, 151, 163, 165);
                                bspTicket.Stat = orderedLine.GetChunkTextBetween(203, 205, 205, 206);
                                bspTicket.Fop = orderedLine.GetChunkTextBetween(221, 223, 229, 230);
                                bspTicket.ValorTransaccion =
                                    Convert.ToDecimal(orderedLine.GetChunkTextBetween(260, 264, 287, 289));
                                bspTicket.ValorTarifa =
                                    Convert.ToDecimal(orderedLine.GetChunkTextBetween(315, 333, 339, 343));
                                bspTicket.ImpuestoValor =
                                    Convert.ToDecimal(orderedLine.GetChunkTextBetween(1, 1000, 386, 388));
                                bspTicket.ImpuestoCodigo = orderedLine.GetChunkTextBetween(389, 390, 396, 399);
                                bspTicket.ImpuestoTyCValor =
                                    Convert.ToDecimal(orderedLine.GetChunkTextBetween(1, 1000, 439, 442));
                                bspTicket.ImpuestoTyCCodigo = orderedLine.GetChunkTextBetween(443, 445, 450, 452);
                                bspTicket.ImpuestoPenValor =
                                    Convert.ToDecimal(orderedLine.GetChunkTextBetween(1, 1000, 490, 496));
                                bspTicket.ImpuestoPenCodigo =
                                    orderedLine.GetChunkTextBetween(497, 499, 504, 506);
                                bspTicket.ImpuestoCobl =
                                    Convert.ToDecimal(orderedLine.GetChunkTextBetween(1, 1000, 557, 559));
                                bspTicket.ComisionStdPorcentaje =
                                    Convert.ToDecimal(orderedLine.GetChunkTextBetween(570, 572, 581, 583));
                                bspTicket.ComisionStdValor =
                                    Convert.ToDecimal(orderedLine.GetChunkTextBetween(1, 1000, 635, 637));
                                bspTicket.ComisionSuppPorcentaje =
                                    Convert.ToDecimal(orderedLine.GetChunkTextBetween(640, 644, 653, 655));
                                bspTicket.ComisionSuppValor =
                                    Convert.ToDecimal(orderedLine.GetChunkTextBetween(1, 1000, 707, 709));
                                bspTicket.ImpuestoSinComision =
                                    Convert.ToDecimal(orderedLine.GetChunkTextBetween(743, 754, 759, 764));
                                bspTicket.NetoAPagar =
                                    Convert.ToDecimal(orderedLine.GetChunkTextBetween(789, 806, 809, 818));
                                bspTicket.Compania = compania;
                                
                                continue;
                            }

                            if (orderedLine.First().Text.Substring(0, 4) == "TOUR")
                                continue;

                            var detalle = new BSP_Ticket_Detalle();
                            detalle.TRNC = orderedLine.GetChunkTextBetween(37, 44, 50, 58);
                            detalle.Billete = Convert.ToInt64(orderedLine.GetChunkTextBetween(65, 67, 95, 96));
                            detalle.FechaVenta = DateTime.TryParse(orderedLine.GetChunkTextBetween(113, 115, 137, 138),
                                out var fechaVentaDetalle) ? (DateTime?)fechaVentaDetalle : null;
                            detalle.CPN = orderedLine.GetChunkTextBetween(149, 151, 163, 165);
                            detalle.Stat = orderedLine.GetChunkTextBetween(203, 205, 205, 206);
                            detalle.Fop = orderedLine.GetChunkTextBetween(221, 223, 229, 230);
                            detalle.ValorTransaccion = Convert.ToDecimal(orderedLine.GetChunkTextBetween(260, 264, 287, 289));
                            detalle.ValorTarifa = Convert.ToDecimal(orderedLine.GetChunkTextBetween(316, 318, 341, 343));
                            detalle.ImpuestoValor = Convert.ToDecimal(orderedLine.GetChunkTextBetween(1, 1000, 386, 388));
                            detalle.ImpuestoCodigo = orderedLine.GetChunkTextBetween(389, 390, 396, 399);
                            detalle.ImpuestoTyCValor = Convert.ToDecimal(orderedLine.GetChunkTextBetween(1, 1000, 439, 442));
                            detalle.ImpuestoTyCCodigo = orderedLine.GetChunkTextBetween(443, 445, 450, 452);
                            detalle.ImpuestoPenValor = Convert.ToDecimal(orderedLine.GetChunkTextBetween(1, 1000, 490, 496));
                            detalle.ImpuestoPenCodigo = orderedLine.GetChunkTextBetween(497, 499, 504, 506);
                            detalle.ImpuestoCobl = Convert.ToDecimal(orderedLine.GetChunkTextBetween(1, 1000, 557, 559));
                            detalle.ComisionStdPorcentaje = Convert.ToDecimal(orderedLine.GetChunkTextBetween(570, 572, 581, 583));
                            detalle.ComisionStdValor = Convert.ToDecimal(orderedLine.GetChunkTextBetween(1, 1000, 635, 637));
                            detalle.ComisionSuppPorcentaje = Convert.ToDecimal(orderedLine.GetChunkTextBetween(640, 644, 653, 655));
                            detalle.ComisionSuppValor = Convert.ToDecimal(orderedLine.GetChunkTextBetween(1, 1000, 707, 709));
                            detalle.ImpuestoSinComision = Convert.ToDecimal(orderedLine.GetChunkTextBetween(743, 754, 759, 764));
                            detalle.NetoAPagar = Convert.ToDecimal(orderedLine.GetChunkTextBetween(789, 806, 809, 818));

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

        #endregion
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

    public static class ReaderExtensions
    {
        public static List<PageChunks> ExtractChunks(this PdfPage page)
        {
            var textEventListener = new LocationTextExtractionStrategy();
            PdfTextExtractor.GetTextFromPage(page, textEventListener);

            IList<TextChunk> locationalResult = (IList<TextChunk>)locationalResultField.GetValue(textEventListener);
            List<PageChunks> pageChunks = new List<PageChunks>();

            foreach (TextChunk chunk in locationalResult)
            {
                ITextChunkLocation location = chunk.GetLocation();
                Vector start = location.GetStartLocation();
                Vector end = location.GetEndLocation();

                PageChunks ro = new PageChunks()
                {
                    Text = chunk.GetText().Trim(),
                    StartX = start.Get(Vector.I1),
                    Y = start.Get(Vector.I2),
                    EndX = end.Get(Vector.I1),
                    EndY = end.Get(Vector.I2)
                };
                pageChunks.Add(ro);
            }

            pageChunks = pageChunks.OrderByDescending(x => x.Y).ThenBy(x => x.StartX).ToList();
            if (pageChunks.Any())
            {
                var chunkToCompare = pageChunks.First();
                foreach (var chunk in pageChunks)
                {
                    if (Math.Abs(chunk.Y - chunkToCompare.Y) <= 1)
                    {
                        chunk.Y = chunkToCompare.Y;
                    }
                    else
                    {
                        chunkToCompare = chunk;
                    }
                }
                pageChunks = pageChunks.OrderByDescending(x => x.Y).ThenBy(x => x.StartX).ToList();
            }

            return pageChunks;
        }

        public static string GetChunkTextBetween(this List<PageChunks> pageChunks, int startA, int startB, int endA, int endB)
        {
            return pageChunks.Where(x => startA <= x.StartX && x.StartX <= startB && endA <= x.EndX && x.EndX <= endB).Select(x => x.Text).FirstOrDefault();
        }

        static FieldInfo locationalResultField = typeof(LocationTextExtractionStrategy).GetField("locationalResult", BindingFlags.NonPublic | BindingFlags.Instance);
    }
}


