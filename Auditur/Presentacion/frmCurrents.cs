using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Auditur.Negocio;
using Auditur.Presentacion.Classes;
using System.ComponentModel;

namespace Auditur.Presentacion
{
    public partial class frmCurrents : UserControl
    {
        public frmCurrents()
        {
            InitializeComponent();
        }

        [Description("Mostrar botón de reporte"), Category("Reportes")]
        public bool ShowReportButton
        {
            get
            {
                return btnReportar.Visible;
            }
            set
            {
                btnReportar.Visible = value;
            } 
        }

        private void frmCurrents_Load(object sender, EventArgs e)
        {
            Reset();
        }

        public void Reset()
        {
            Semana oSemana = Publics.Semana;
            if (oSemana != null)
            {
                lblAgencia.Text = oSemana.Agencia != null ? oSemana.Agencia.Nombre : "(Ninguna)";
                lblPeriodo.Text = oSemana.Periodo.ToShortDateString();
                lblFechaDesde.Text = oSemana.FechaDesde.ToShortDateString();
                lblFechaHasta.Text = oSemana.FechaHasta.ToShortDateString();
                lblBSPCargado.Text = oSemana.BSPCargado ? "Si" : "No";
                lblBOCargado.Text = oSemana.BOCargado ? "Si" : "No";
                btnReportar.Enabled = oSemana.BSPCargado || oSemana.BOCargado;

            }
            else
            {
                lblAgencia.Text = "(vacío)";
                lblPeriodo.Text = "(vacío)";
                lblFechaDesde.Text = "(vacío)";
                lblFechaHasta.Text = "(vacío)";
                lblBSPCargado.Text = "(vacío)";
                lblBOCargado.Text = "(vacío)";
                btnReportar.Enabled = false;
            }
        }

        private void btnReportar_Click(object sender, EventArgs e)
        {
            ucMenu ucMenu1 = (ucMenu)this.Parent.Parent.Parent.Controls[0].Controls[0];
            ucMenu1.MostrarForm(new frmReportes());
        }
    }
}
