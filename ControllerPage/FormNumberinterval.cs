using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ControllerPage.Constant;
using ControllerPage.Helper;

namespace ControllerPage
{
    public partial class FormNumberinterval : Form
    {
        public static bool password_value;
        public static string combobox_selectedItem_number_Interval;

        public FormNumberinterval()
        {
            InitializeComponent();
            Combobox_NumInterval.Items.Clear();
            for (int i = 1; i <= 50; i++)
            {
                Combobox_NumInterval.Items.Add(i.ToString());
            }

        }

        public decimal Intervalselection { get; set; }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.Intervalselection = numericUpDown1.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();

            combobox_selectedItem_number_Interval = Combobox_NumInterval.SelectedItem.ToString();


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
