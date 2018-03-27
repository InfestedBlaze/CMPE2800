using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class SocketSelect : Form
    {
        public int socketPort = 0;
        public string socketIP = "localhost";

        public SocketSelect()
        {
            InitializeComponent();
        }

        private void UI_button_Connect_Click(object sender, EventArgs e)
        {
            this.socketPort = (int)UI_numericUpDown_Port.Value;
            this.socketIP = UI_textBox_IPAddress.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
