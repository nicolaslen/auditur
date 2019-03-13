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

            if (!File.Exists(testingpath))
                File.Create(testingpath);

            try
            {
                if (File.Exists(fileName))
                {
                    PdfReader pdfReader = new PdfReader(fileName);
                    PdfDocument pdfDoc = new PdfDocument(pdfReader);

                    for (page = 2; page <= pdfDoc.GetNumberOfPages(); page++)
                    {
                        var pdfPage = pdfDoc.GetPage(page);

                        var probando = pdfPage.ExtractText(new Rectangle(int.Parse(txtX.Text), int.Parse(txtY.Text), int.Parse(txtWidth.Text), int.Parse(txtHeight.Text)));

                        using (var tw = new StreamWriter(testingpath, true))
                        {
                            tw.WriteLine(probando[0]);
                        }
                        break;

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

    public static class ReaderExtensions
    {
        public static string[] ExtractText(this PdfPage page, params Rectangle[] rects)
        {
            var textEventListener = new LocationTextExtractionStrategy();
            PdfTextExtractor.GetTextFromPage(page, textEventListener);
            string[] result = new string[rects.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = textEventListener.GetResultantText(rects[i]);
            }
            return result;
        }

        public static String GetResultantText(this LocationTextExtractionStrategy strategy, Rectangle rect)
        {
            IList<TextChunk> locationalResult = (IList<TextChunk>)locationalResultField.GetValue(strategy);
            List<TextChunk> nonMatching = new List<TextChunk>();
            foreach (TextChunk chunk in locationalResult)
            {
                ITextChunkLocation location = chunk.GetLocation();
                Vector start = location.GetStartLocation();
                Vector end = location.GetEndLocation();
                if (!rect.IntersectsLine(start.Get(Vector.I1), start.Get(Vector.I2), end.Get(Vector.I1), end.Get(Vector.I2)))
                {
                    nonMatching.Add(chunk);
                }
            }
            nonMatching.ForEach(c => locationalResult.Remove(c));
            try
            {
                return strategy.GetResultantText();
            }
            finally
            {
                nonMatching.ForEach(c => locationalResult.Add(c));
            }
        }

        static FieldInfo locationalResultField = typeof(LocationTextExtractionStrategy).GetField("locationalResult", BindingFlags.NonPublic | BindingFlags.Instance);
    }
}


