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
using mdtypes;

namespace LineDrawerClient
{
    public partial class Form1 : Form
    {
        ColourThickness ct = new ColourThickness();
        Color lineColour = Color.Red;
        int lineThickness = 10;

        public Form1()
        {
            InitializeComponent();
            ct.Show();
            ct.ChangeColour = new delVoidColor(cbColourChange);
            ct.ChangeThickness = new delVoidInt(cbThicknessChange);
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Get rid of our modeless dialog
            ct.Dispose();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            CreateGraphics().Clear(Color.Black);
        }

        public void cbColourChange(Color inColour)
        {
            //Update the colour we use to draw
            lineColour = inColour;
            //Update the UI
            UI_StatusStrip.Items[1].ForeColor = inColour;
        }
        public void cbThicknessChange(int inThick)
        {
            //Update the line thickness we use to draw
            lineThickness = inThick;
            //Update the UI
            UI_StatusStrip.Items[2].Text = $"Thickness: {inThick}";
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //Make line segments, send them to the server
        }
    }
}
