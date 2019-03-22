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

                                bspTicket = new BSP_Ticket();

                                bspTicket.Llave = llave;
                                bspTicket.TRNC = orderedLine.GetChunkTextBetween(37, 60);
                                bspTicket.Billete = Convert.ToInt64(orderedLine.GetChunkTextBetween(63, 98));
                                bspTicket.FechaVenta = DateTime.TryParse(orderedLine.GetChunkTextBetween(110, 140), out var fechaVenta) ? (DateTime?)fechaVenta : null;
                                bspTicket.CPN = orderedLine.GetChunkTextBetween(143, 168);
                                bspTicket.NR = orderedLine.GetChunkTextBetween(169, 192);
                                bspTicket.Stat = orderedLine.GetChunkTextBetween(200, 211);
                                bspTicket.Fop = orderedLine.GetChunkTextBetween(218, 235);
                                bspTicket.ValorTransaccion = Convert.ToDecimal(orderedLine.GetChunkTextBetween(242, 292));
                                bspTicket.ValorTarifa = Convert.ToDecimal(orderedLine.GetChunkTextBetween(306, 346));
                                bspTicket.ImpuestoValor = Convert.ToDecimal(orderedLine.GetChunkTextBetween(355, 388));
                                bspTicket.ImpuestoCodigo = orderedLine.GetChunkTextBetween(388, 400);
                                bspTicket.ImpuestoTyCValor = Convert.ToDecimal(orderedLine.GetChunkTextBetween(406, 442));
                                bspTicket.ImpuestoTyCCodigo = orderedLine.GetChunkTextBetween(442, 453);
                                bspTicket.ImpuestoPenValor = Convert.ToDecimal(orderedLine.GetChunkTextBetween(461, 496));
                                bspTicket.ImpuestoPenCodigo = orderedLine.GetChunkTextBetween(496, 507);
                                bspTicket.ImpuestoCobl = Convert.ToDecimal(orderedLine.GetChunkTextBetween(520, 562));
                                bspTicket.ComisionStdPorcentaje = Convert.ToDecimal(orderedLine.GetChunkTextBetween(562, 585));
                                bspTicket.ComisionStdValor = Convert.ToDecimal(orderedLine.GetChunkTextBetween(594, 639));
                                bspTicket.ComisionSuppPorcentaje = Convert.ToDecimal(orderedLine.GetChunkTextBetween(638, 657));
                                bspTicket.ComisionSuppValor = Convert.ToDecimal(orderedLine.GetChunkTextBetween(670, 711));
                                bspTicket.ImpuestoSinComision = Convert.ToDecimal(orderedLine.GetChunkTextBetween(730, 764));
                                bspTicket.NetoAPagar = Convert.ToDecimal(orderedLine.GetChunkTextBetween(780, 818));
                                bspTicket.Compania = compania;

                                continue;
                            }

                            if (orderedLine.First().Text.Substring(0, 4) == "TOUR")
                                continue;

                            var detalle = new BSP_Ticket_Detalle();
                            detalle.TRNC = orderedLine.GetChunkTextBetween(37, 60);
                            detalle.Billete = Convert.ToInt64(orderedLine.GetChunkTextBetween(63, 98));
                            detalle.FechaVenta = DateTime.TryParse(orderedLine.GetChunkTextBetween(110, 140),
                                out var fechaVentaDetalle) ? (DateTime?)fechaVentaDetalle : null;
                            detalle.CPN = orderedLine.GetChunkTextBetween(143, 168);
                            detalle.NR = orderedLine.GetChunkTextBetween(169, 192);
                            detalle.Stat = orderedLine.GetChunkTextBetween(200, 211);
                            detalle.Fop = orderedLine.GetChunkTextBetween(218, 235);
                            detalle.ValorTransaccion = Convert.ToDecimal(orderedLine.GetChunkTextBetween(242, 292));
                            detalle.ValorTarifa = Convert.ToDecimal(orderedLine.GetChunkTextBetween(306, 346));
                            detalle.ImpuestoValor = Convert.ToDecimal(orderedLine.GetChunkTextBetween(355, 388));
                            detalle.ImpuestoCodigo = orderedLine.GetChunkTextBetween(388, 400);
                            detalle.ImpuestoTyCValor = Convert.ToDecimal(orderedLine.GetChunkTextBetween(406, 442));
                            detalle.ImpuestoTyCCodigo = orderedLine.GetChunkTextBetween(442, 453);
                            detalle.ImpuestoPenValor = Convert.ToDecimal(orderedLine.GetChunkTextBetween(461, 496));
                            detalle.ImpuestoPenCodigo = orderedLine.GetChunkTextBetween(496, 507);
                            detalle.ImpuestoCobl = Convert.ToDecimal(orderedLine.GetChunkTextBetween(520, 562));
                            detalle.ComisionStdPorcentaje = Convert.ToDecimal(orderedLine.GetChunkTextBetween(562, 585));
                            detalle.ComisionStdValor = Convert.ToDecimal(orderedLine.GetChunkTextBetween(594, 639));
                            detalle.ComisionSuppPorcentaje = Convert.ToDecimal(orderedLine.GetChunkTextBetween(638, 657));
                            detalle.ComisionSuppValor = Convert.ToDecimal(orderedLine.GetChunkTextBetween(670, 711));
                            detalle.ImpuestoSinComision = Convert.ToDecimal(orderedLine.GetChunkTextBetween(730, 764));
                            detalle.NetoAPagar = Convert.ToDecimal(orderedLine.GetChunkTextBetween(780, 818));

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

        public static string GetChunkTextBetween(this List<PageChunks> pageChunks, int start, int end)
        {
            return pageChunks.Where(x => start <= x.StartX && x.EndX <= end).Select(x => x.Text).FirstOrDefault();
        }

        static FieldInfo locationalResultField = typeof(LocationTextExtractionStrategy).GetField("locationalResult", BindingFlags.NonPublic | BindingFlags.Instance);
    }
}


