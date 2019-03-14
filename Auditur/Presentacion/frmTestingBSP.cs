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
            bool encontreCIA = false;
            bool encontreCompania = false;
            int codCompania;
            string nombreCompania;
            bool encontreLlave = false;
            string llave = "";

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
                        var pageLines = pageChunks.GroupBy(x => x.Y).OrderByDescending(x => x.Key).ToList();

                        if (!pageChunks.Any())
                            continue;

                        float alturaCIA = 0;

                        foreach (var line in pageLines)
                        {
                            if (!encontreCIA)
                            { 
                                var chunk = line.First();
                                if (chunk.Text != "CIA")
                                    continue;

                                encontreCIA = true;
                                alturaCIA = chunk.Y - 20;
                            }

                            if (line.Key <= alturaCIA)
                                continue;

                            if (!line.Any())
                                continue;

                            if (!encontreCompania)
                            {
                                string posibleCompania = line.First().Text;
                                if (posibleCompania.Length != 3 ||
                                    !int.TryParse(posibleCompania, out codCompania))
                                    continue;
                                
                                encontreCompania = true;
                                nombreCompania = line.ElementAt(2).Text;
                                continue;
                            }

                            if (!encontreLlave)
                            {
                                string posibleLlave = line.First().Text;
                                if (posibleLlave.Substring(0, 3) != "***")
                                    continue;

                                encontreLlave = true;
                                llave = posibleLlave.Substring(5);
                                continue;
                            }

                            if (line.First().Text == llave + " TOTAL")
                            {
                                encontreLlave = false;
                                continue;
                            }
                            

                            

                            

                            foreach (var chunk in line)
                            {
                                
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

        //public static string[] ExtractText(this PdfPage page, StreamWriter tw, params Rectangle[] rects)
        //{
        //    var textEventListener = new LocationTextExtractionStrategy();
        //    var prueba = PdfTextExtractor.GetTextFromPage(page, textEventListener);
        //    string[] result = new string[rects.Length];
        //    for (int i = 0; i < result.Length; i++)
        //    {
        //        result[i] = textEventListener.GetResultantText(tw, rects[i]);
        //    }
        //    return result;
        //}

        //public static String lala(this float lele)
        //{
        //    return lele.ToString("0000.0000");
        //}

        //public static String GetResultantText(this LocationTextExtractionStrategy strategy, StreamWriter tw, Rectangle rect)
        //{
        //    IList<TextChunk> locationalResult = (IList<TextChunk>)locationalResultField.GetValue(strategy);
        //    List<TextChunk> nonMatching = new List<TextChunk>();
        //    List<PageChunks> readerObjects = new List<PageChunks>();
        //    foreach (TextChunk chunk in locationalResult)
        //    {
        //        ITextChunkLocation location = chunk.GetLocation();
        //        Vector start = location.GetStartLocation();
        //        Vector end = location.GetEndLocation();

        //        tw.WriteLine(start.Get(Vector.I1).lala() + "|" + start.Get(Vector.I2).lala() + "|" +
        //                     end.Get(Vector.I1).lala() + "|" +
        //                     end.Get(Vector.I2).lala() + "|" + chunk.GetText());

        //        PageChunks ro = new PageChunks()
        //        {
        //            Text = chunk.GetText(),
        //            StartX = start.Get(Vector.I1),
        //            Y = start.Get(Vector.I2),
        //            EndX = end.Get(Vector.I1),
        //            EndY = end.Get(Vector.I2)
        //        };
        //        readerObjects.Add(ro);

        //        if (!rect.IntersectsLine(start.Get(Vector.I1), start.Get(Vector.I2), end.Get(Vector.I1), end.Get(Vector.I2)))
        //        {
        //            nonMatching.Add(chunk);
        //        }
        //    }
        //    nonMatching.ForEach(c => locationalResult.Remove(c));
        //    try
        //    {
        //        return strategy.GetResultantText();
        //    }
        //    finally
        //    {
        //        nonMatching.ForEach(c => locationalResult.Add(c));
        //    }
        //}

        static FieldInfo locationalResultField = typeof(LocationTextExtractionStrategy).GetField("locationalResult", BindingFlags.NonPublic | BindingFlags.Instance);
    }
}


