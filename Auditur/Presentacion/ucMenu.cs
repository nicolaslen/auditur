using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Auditur.Negocio;

namespace Auditur.Presentacion
{
    public partial class ucMenu : UserControl
    {
        public ucMenu()
        {
            InitializeComponent();
        }

        #region DivPanel
        private SplitterPanel _splDiv;

        public SplitterPanel Div
        {
            get
            {
                return _splDiv;
            }
            set
            {
                _splDiv = value;
            }
        }
        #endregion

        private void ChequearDivs()
        {
            if (Div.Controls.Count >= 1)
                Div.Controls.Clear();
        }

        public void MostrarForm(UserControl Formulario)
        {
            ChequearDivs();
            /*Formulario.Height = Div.Height;
            Formulario.Width = Div.Width;
           /* Formulario.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;*/
            Div.Controls.Add(Formulario);
        }

        private void smiImportarBSP_Click(object sender, EventArgs e)
        {
            MostrarForm(new frmImportarBSP());
        }

        private void smiImportarBO_Click(object sender, EventArgs e)
        {
            MostrarForm(new frmImportarBO());
        }

        private void smiElegirSemana_Click(object sender, EventArgs e)
        {
            MostrarForm(new frmElegirSemana());
        }

        private void smnCargarBD_Click(object sender, EventArgs e)
        {
            BackUp oBackup = new BackUp();
            bool blnRespuesta = oBackup.Cargar();
            if (blnRespuesta == true)
            {
                MessageBox.Show("Se ha cargado la nueva base de datos", "Carga de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmHome frmHome1 = new frmHome();
                MostrarForm(frmHome1);
            }
        }

        private void smnBackUp_Click(object sender, EventArgs e)
        {
            BackUp oBackup = new BackUp();
            bool blnRespuesta = oBackup.Resguardar();
            if (blnRespuesta == true)
            {
                MessageBox.Show("La copia de seguridad se realizó con éxito", "Copia de seguridad", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmHome frmHome1 = new frmHome();
                MostrarForm(frmHome1);
            }
        }

        private void smiAgencias_Click(object sender, EventArgs e)
        {
            MostrarForm(new frmABMAgencias());
        }

        private void smiConceptos_Click(object sender, EventArgs e)
        {
            MostrarForm(new frmABMConceptos());
        }

        private void smiCompanias_Click(object sender, EventArgs e)
        {
            MostrarForm(new frmABMCompanias());
        }
    }
}
