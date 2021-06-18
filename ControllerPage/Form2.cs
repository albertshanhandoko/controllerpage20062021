using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControllerPage
{
   
    public partial class Form2 : Form
    {
        public class RoundButton : Button
        {
            protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
            {
                GraphicsPath grPath = new GraphicsPath();
                grPath.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height);
                this.Region = new System.Drawing.Region(grPath);
                base.OnPaint(e);
            }

            private void InitializeComponent()
            {
                this.progressBar1 = new System.Windows.Forms.ProgressBar();
                this.SuspendLayout();
                // 
                // progressBar1
                // 
                this.progressBar1.Location = new System.Drawing.Point(0, 0);
                this.progressBar1.Name = "progressBar1";
                this.progressBar1.Size = new System.Drawing.Size(100, 23);
                this.progressBar1.TabIndex = 0;
                this.ResumeLayout(false);

            }

            private void progressBar1_Click(object sender, EventArgs e)
            {

            }

            private ProgressBar progressBar1;
        }

        public Form2()
        {

            InitializeComponent();
            GraphicsPath p = new GraphicsPath();
            p.AddEllipse(1, 1, button_minus_1.Width - 4, button_minus_1.Height - 4);
            button_minus_1.Region = new Region(p);

            GraphicsPath p2 = new GraphicsPath();
            p2.AddEllipse(1, 1, button_plus_1.Width - 4, button_plus_1.Height - 4);
            //button_minus_1.Region = new Region(p);

            button_plus_1.Region = new Region(p2);


        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
