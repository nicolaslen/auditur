using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Auditur.Negocio;
using System.IO;
using Helpers;
using System.Globalization;

namespace Auditur.Presentacion
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            CultureInfo culture = new CultureInfo("es-AR");

            culture.NumberFormat.NumberDecimalSeparator = ",";
            culture.NumberFormat.NumberGroupSeparator = ".";
            culture.NumberFormat.CurrencyDecimalSeparator = ",";
            culture.NumberFormat.CurrencyGroupSeparator = ".";

            Application.CurrentCulture = culture;
            InitializeComponent();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            ucMenu1.Div = spcDiv.Panel2;
            CargarFirst();
            //spcDiv.BackColor = System.Drawing.Color.Cornsilk;
        }

        private void spcDiv_Panel2_ControlRemoved(object sender, ControlEventArgs e)
        {
            /*if (UserControls.MostrarPrincipal)
            {
                CargarFirst();
                UserControls.MostrarPrincipal = false;
            }*/
        }

        private void CargarFirst()
        {
            frmHome frmHome1 = new frmHome();
            frmHome1.Height = spcDiv.Height;
            frmHome1.Width = spcDiv.Width;
            frmHome1.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            spcDiv.Panel2.Controls.Add(frmHome1);
        }

        
    }
}
