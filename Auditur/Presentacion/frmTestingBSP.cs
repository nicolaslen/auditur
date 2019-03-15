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

            Compania compania = null;
            int ticketCodCompania = 0;
            string nombreCompania;
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
                    List<PdfChunks> pdfChunks = new List<PdfChunks>();

                    for (page = 1; page <= pdfDoc.GetNumberOfPages(); page++)
                    {
                        var pdfPage = pdfDoc.GetPage(page);

                        var pageChunks = pdfPage.ExtractChunks();
                        var pageLines = pageChunks.GroupBy(x => x.Y).OrderByDescending(y => y.Key).ToList();

                        if (pageChunks.Any() || pageChunks.All(x => x.Text != "CIA"))
                            continue;

                        //float alturaCIA = 0;

                        var filteredPageLines = pageLines.Where(x =>
                            x.Any() &&
                            x.Where(y => y.Text == "CIA").Select(y => y.Y).FirstOrDefault() > x.Key &&
                            x.Key > x.Select(y => y.Y).Min()); //Filtro desde el encabezado para arriba, y la última línea con la fecha

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
                                    Nombre = orderedLine.ElementAt(2).Text
                                };

                                continue;
                            }

                            if (!encontreLlave)
                            {
                                string posibleLlave = orderedLine.First().Text;
                                if (posibleLlave.Substring(0, 3) != "***")
                                    continue;

                                encontreLlave = true;
                                llave = posibleLlave.Substring(5);
                                continue;
                            }

                            if (orderedLine.First().Text == llave + " TOTAL")
                            {
                                encontreLlave = false;
                                continue;
                            }

                            var posibleTicket = orderedLine.GetChunkBetween(22, 24, 31, 34);
                            if (posibleTicket == null || !int.TryParse(posibleTicket.Text, out ticketCodCompania))
                                continue;

                            if (compania.Codigo != ticketCodCompania.ToString())//TODO: SACAR EL TOSTRING
                                throw new Exception("Se llegó a otra compañía sin darnos cuenta");

                            if (bspTicket == null)
                            {
                                bspTicket = new BSP_Ticket();
                                bspTicket.Billete = Convert.ToInt64(orderedLine.GetChunkBetween(65, 67, 95, 96).Text);
                                bspTicket.FechaVenta =
                                    Convert.ToDateTime(orderedLine.GetChunkBetween(113, 115, 137, 138)
                                        .Text); //TODO: Ver si interpreta la fecha
                                bspTicket.CPN = orderedLine.GetChunkBetween(149, 151, 163, 165).Text;
                                bspTicket.Stat = orderedLine.GetChunkBetween(203, 205, 205, 206).Text;
                                bspTicket.Fop = orderedLine.GetChunkBetween(221, 223, 229, 230).Text;

                                bspTicket.ValorTransaccion = orderedLine.GetChunkBetween(113, 115, 137, 138).Text;
                                bspTicket.ValorTarifa = orderedLine.GetChunkBetween(113, 115, 137, 138).Text;
                                bspTicket.ImpuestoValor = orderedLine.GetChunkBetween(113, 115, 137, 138).Text;
                                bspTicket.ImpuestoCodigo = orderedLine.GetChunkBetween(113, 115, 137, 138).Text;
                                bspTicket.ImpuestoTyC = orderedLine.GetChunkBetween(113, 115, 137, 138).Text;
                                bspTicket.ImpuestoPen = orderedLine.GetChunkBetween(113, 115, 137, 138).Text;
                                bspTicket.ImpuestoCobl = orderedLine.GetChunkBetween(113, 115, 137, 138).Text;
                                bspTicket.ComisionStdPorcentaje = orderedLine.GetChunkBetween(113, 115, 137, 138).Text;
                                bspTicket.ComisionStdValor = orderedLine.GetChunkBetween(113, 115, 137, 138).Text;
                                bspTicket.ComisionSuppPorcentaje = orderedLine.GetChunkBetween(113, 115, 137, 138).Text;
                                bspTicket.ComisionSuppValor = orderedLine.GetChunkBetween(113, 115, 137, 138).Text;
                                bspTicket.ImpuestoSinComision = orderedLine.GetChunkBetween(113, 115, 137, 138).Text;
                                bspTicket.NetoAPagar = orderedLine.GetChunkBetween(113, 115, 137, 138).Text;
                            }





                        }

                        //using (var tw = new StreamWriter(testingpath, true))
                        //{
                        //    foreach (var chunk in chunks)
                        //    {
                        //        tw.WriteLine(chunk.StartX.ToString("0000.0000") + "|" + chunk.Y.ToString("0000.0000") + "|" + chunk.EndX.ToString("0000.0000") + "|" + chunk.EndY.ToString("0000.0000") + "|" + chunk.Text);
                        //    }
                        //}

                        pdfChunks.Add(new PdfChunks { Chunks = pageChunks, Page = page });

                        /*currentText = "";
                        currentText = PdfTextExtractor.GetTextFromPage(pdfPage, new SimpleTextExtractionStrategy());
                        currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));

                        string[] arrLineas = currentText.Split(new char[] { '\n' });

                        using (var tw = new StreamWriter(testingpath, true))
                        {
                            foreach (var linea in arrLineas)
                            {
                                tw.WriteLine(linea);
                            }
                        }*/
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

    public class PdfChunks
    {
        public List<PageChunks> Chunks { get; set; }
        public int Page { get; set; }
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

        public static PageChunks GetChunkBetween(this List<PageChunks> pageChunks, int startA, int startB, int endA, int endB)
        {
            return pageChunks.FirstOrDefault(x => startA <= x.StartX && x.StartX <= startB && endA <= x.EndX && x.EndX <= endB);
        }

        static FieldInfo locationalResultField = typeof(LocationTextExtractionStrategy).GetField("locationalResult", BindingFlags.NonPublic | BindingFlags.Instance);
    }
}


