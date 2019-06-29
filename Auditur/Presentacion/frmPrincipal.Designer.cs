namespace Auditur.Presentacion
{
    partial class frmPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrincipal));
            this.spcDiv = new System.Windows.Forms.SplitContainer();
            this.ucMenu1 = new Auditur.Presentacion.ucMenu();
            ((System.ComponentModel.ISupportInitialize)(this.spcDiv)).BeginInit();
            this.spcDiv.Panel1.SuspendLayout();
            this.spcDiv.SuspendLayout();
            this.SuspendLayout();
            // 
            // spcDiv
            // 
            this.spcDiv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spcDiv.BackColor = System.Drawing.Color.AliceBlue;
            this.spcDiv.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.spcDiv.IsSplitterFixed = true;
            this.spcDiv.Location = new System.Drawing.Point(0, 0);
            this.spcDiv.Margin = new System.Windows.Forms.Padding(5);
            this.spcDiv.Name = "spcDiv";
            this.spcDiv.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spcDiv.Panel1
            // 
            this.spcDiv.Panel1.Controls.Add(this.ucMenu1);
            // 
            // spcDiv.Panel2
            // 
            this.spcDiv.Panel2.AutoScroll = true;
            this.spcDiv.Panel2.BackColor = System.Drawing.Color.AliceBlue;
            this.spcDiv.Panel2MinSize = 41;
            this.spcDiv.Size = new System.Drawing.Size(762, 640);
            this.spcDiv.SplitterDistance = 37;
            this.spcDiv.SplitterWidth = 8;
            this.spcDiv.TabIndex = 0;
            // 
            // ucMenu1
            // 
            this.ucMenu1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucMenu1.BackColor = System.Drawing.Color.Transparent;
            this.ucMenu1.Div = null;
            this.ucMenu1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucMenu1.Location = new System.Drawing.Point(0, 0);
            this.ucMenu1.Margin = new System.Windows.Forms.Padding(5);
            this.ucMenu1.Name = "ucMenu1";
            this.ucMenu1.Size = new System.Drawing.Size(762, 44);
            this.ucMenu1.TabIndex = 0;
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(762, 640);
            this.Controls.Add(this.spcDiv);
            this.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AUDITUR 3.0 - Auditoría del BSP";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPrincipal_Load);
            this.spcDiv.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcDiv)).EndInit();
            this.spcDiv.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer spcDiv;
        private ucMenu ucMenu1;

    }
}