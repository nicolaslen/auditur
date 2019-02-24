using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Auditur.Negocio;
using Auditur.Presentacion.Classes;
using Helpers;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.ComponentModel;
using System.Globalization;

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
                pageStart = 0;
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
            string testingpath = Path.Combine(Directory.GetDirectoryRoot(fileName), "text.txt");

            if (!File.Exists(testingpath))
                File.Create(testingpath);

            try
            {
                if (File.Exists(fileName))
                {
                    PdfReader pdfReader = new PdfReader(fileName);
                    for (page = pageStart; page <= pdfReader.NumberOfPages; page++)
                    {
                        currentText = "";
                        currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, new SimpleTextExtractionStrategy());
                        currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));

                        string[] arrLineas = currentText.Split(new char[] { '\n' });

                        using (var tw = new StreamWriter(testingpath, true))
                        {
                            foreach (var linea in arrLineas)
                            {
                                tw.WriteLine(linea);
                            }
                        }
                    }
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

            lblProgressStatus.Text = "";
            progressBar1.Value = 0;

            txtFilePath_BSP.ResetText();
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            lblProgressStatus.Text = (string)e.UserState;
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
