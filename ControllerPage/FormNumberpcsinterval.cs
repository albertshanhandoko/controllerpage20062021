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
    public partial class FormNumberpcsinterval : Form
    {
        public FormNumberpcsinterval()
        {
            InitializeComponent();

            Combobox_NumPerPCS.Items.Clear();
            foreach (int i in Enum.GetValues(typeof(number_grain)))
            {
                Combobox_NumPerPCS.Items.Add(i.ToString());
            }

        }
        public string Pcsselection;
        public static string combobox_selectedItem_number_PerPCS;

        private void Numberpcsinterval_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            combobox_selectedItem_number_PerPCS = Combobox_NumPerPCS.SelectedItem.ToString();


            /*
            string[] val = { "0", "0", "0" };
            val[0] = numericUpDown2.Value.ToString();
            val[1] = numericUpDown1.Value.ToString();
            val[2] = numericUpDown3.Value.ToString();
            this.Pcsselection = string.Join("",val);
            */
            this.DialogResult = DialogResult.OK;
            
            this.Close();

        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
