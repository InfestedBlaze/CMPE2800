using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LineDrawerClient
{
    public partial class AddressSelect : Form
    {
        public string Address;
        public int Port;

        public AddressSelect()
        {
            InitializeComponent();
        }

        private void UI_button_Connect_Click(object sender, EventArgs e)
        {
            Address = UI_textBox_Address.Text;
            Port = (int)UI_numericUpDown_Port.Value;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
