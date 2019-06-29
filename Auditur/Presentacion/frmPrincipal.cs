using Helpers;
using System;
using System.Windows.Forms;

namespace Auditur.Presentacion
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            Application.CurrentCulture = AuditurHelpers.DefaultCultureInfo();
            InitializeComponent();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            ucMenu1.Div = spcDiv.Panel2;
            CargarFirst();
            //spcDiv.BackColor = System.Drawing.Color.AliceBlue;
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