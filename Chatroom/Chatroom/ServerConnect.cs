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
    public partial class ServerConnect : Form
    {
        public int Port = 1666;

        public ServerConnect()
        {
            InitializeComponent();
        }

        private void UI_numericUpDown_Port_ValueChanged(object sender, EventArgs e)
        {
            Port = (int)UI_numericUpDown_Port.Value;
        }
    }
}
