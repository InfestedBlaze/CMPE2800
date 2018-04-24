using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chatroom
{
    public partial class ClientConnect : Form
    {
        public string IPAddress = "localhost";
        public int Port = 1666;

        public ClientConnect()
        {
            InitializeComponent();
        }

        private void UI_textBox_IPAddress_TextChanged(object sender, EventArgs e)
        {
            IPAddress = UI_textBox_IPAddress.Text;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Port = (int)numericUpDown1.Value;
        }
    }
}
