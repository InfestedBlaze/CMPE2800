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
        Connection connSock;

        ColourThickness ct = new ColourThickness();
        Point lastPoint;
        Color lineColour = Color.Red;
        int lineThickness = 10;
        byte lineAlpha = 255;

        bool clicked = false;

        public Form1()
        {
            InitializeComponent();
            ct.Show();
            ct.ChangeColour = new delVoidColor(cbColourChange);
            ct.ChangeThickness = new delVoidInt(cbThicknessChange);
            ct.ChangeAlpha = new delVoidByte(cbAlphaChange);
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Get rid of our modeless dialog
            ct.Dispose();
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
        private void cbAlphaChange(byte inAlpha)
        {
            //Update our alpha
            lineAlpha = inAlpha;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if(connSock == null || !clicked)
            {
                return;
            }

            //Make line segments, send them to the server
            LineSegment temp = new LineSegment();
            temp.Alpha = lineAlpha;
            temp.Colour = lineColour;
            temp.Thickness = (ushort)lineThickness;
            temp.Start = lastPoint;
            temp.End = e.Location;
            
            connSock.SendData(temp);

            lastPoint = e.Location;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            clicked = true;
            lastPoint = e.Location;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            clicked = false;
        }

        private void UI_toolStripSplitLabel_Connect_Click(object sender, EventArgs e)
        {
            if(connSock != null)
            {
                connSock.SoftDisconnect();
            }
            AddressSelect addSel = new AddressSelect();

            if (addSel.ShowDialog().Equals(DialogResult.OK))
            {
                connSock = new Connection(addSel.Address, addSel.Port, new Connection.delVoidString(Disconnect), new Connection.delVoidString(Error), new Connection.delVoidLineSegment(ReceiveLines));
            }
        }

        private void Disconnect(string message)
        {
            CreateGraphics().Clear(Color.LightGray);
            MessageBox.Show(message + "\nPlease reconnect", "Disconnected");
        }

        private void Error(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ReceiveLines(LineSegment ls)
        {
            //Draw the lines on the screen
            ls.Render(CreateGraphics());

            //Receive the connection data from the socket class
            Dictionary<string, uint> data = connSock.ConnectionData();

            if (data.ContainsKey("Frames"))
            {
                Invoke(new Action(() => UI_StatusStrip.Items[3].Text = $"Frames Recieved: {data["Frames"]}"));
            }
            if (data.ContainsKey("Fragments"))
            {
                Invoke(new Action(() => UI_StatusStrip.Items[4].Text = $"Fragments: {data["Fragments"]}"));
            }
            if (data.ContainsKey("Fragments") && data.ContainsKey("Receives"))
            {
                Invoke(new Action(() => UI_StatusStrip.Items[5].Text = $"Destack Average: {(data["Frames"] / (float)data["Receives"]).ToString("F2")}"));
            }
            if (data.ContainsKey("Bytes"))
            {
                float temp = data["Bytes"];

                int magnitude = 0;
                while(temp >= 1000)
                {
                    temp /= 1000;
                    magnitude++;
                }
                string mag = "";
                switch (magnitude)
                {
                    case 1:
                        mag = "K";
                        break;
                    case 2:
                        mag = "M";
                        break;
                    case 3:
                        mag = "G";
                        break;
                    case 4:
                        mag = "T";
                        break;
                    case 5:
                        mag = "P";
                        break;
                    default:
                        mag = "?";
                        break;
                }
                Invoke(new Action(() => UI_StatusStrip.Items[6].Text = $"Bytes Recieved: {temp.ToString("F2")}{mag}B"));
            }
        }
    }
}
