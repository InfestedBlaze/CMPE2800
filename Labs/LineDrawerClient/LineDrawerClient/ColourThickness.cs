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
    public delegate void delVoidColor(Color x);
    public delegate void delVoidInt(int x);

    public partial class ColourThickness : Form
    {
        public delVoidColor ChangeColour = null;
        public delVoidInt ChangeThickness = null;

        public ColourThickness()
        {
            InitializeComponent();
        }

        private void UI_trackBar_Thickness_Scroll(object sender, EventArgs e)
        {
            //Update the dialog UI to the value
            UI_label_ThickNumber.Text = UI_trackBar_Thickness.Value.ToString();
            //Invoke the callback
            Invoke(ChangeThickness, UI_trackBar_Thickness.Value);
        }

        private void UI_button_ColourSelect_Click(object sender, EventArgs e)
        {
            if(UI_colorDialog.ShowDialog() == DialogResult.OK)
            {
                //Invoke the callback
                Invoke(ChangeColour, UI_colorDialog.Color);
            }
        }
    }
}
