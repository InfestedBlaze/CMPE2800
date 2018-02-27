/*
 * Author: Nic Wasylyshyn
 * Date: February 26, 2018
 * Description: This is a proof of concept for our
 * input class and our model generation and handling.
 * We have default controls to rotate, pause, and 
 * some other nifty tricks.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProofOfConcept
{
    public partial class Form1 : Form
    {
        //Only need to check here for inputs
        InputControls keyControls = new InputControls();
        //Have our shapes
        List<BaseShape> shapeList = new List<BaseShape>();

        public Form1()
        {
            InitializeComponent();
            shapeList.Add(new Triangle(new PointF(ClientSize.Width/2, ClientSize.Height/2)));
        }

        //Main form does nothing with the key codes. Sends it to our input handler
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            keyControls.KeyDown(e.KeyCode);
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            keyControls.KeyUp(e.KeyCode);
        }

        private void timer_Game_Tick(object sender, EventArgs e)
        {
            using (BufferedGraphicsContext bgc = new BufferedGraphicsContext())
            {
                using (BufferedGraphics bg = bgc.Allocate(CreateGraphics(), ClientRectangle))
                {
                    //Clear our form of shapes
                    bg.Graphics.Clear(Color.Black);

                    //Tick through all of our shapes
                    shapeList.ForEach(shape => shape.Tick(ClientSize));

                    //Put our shapes on the screen
                    shapeList.ForEach(shape =>
                    {
                        //Triangles are yellow, asteroids are red
                        if (shape is Triangle)
                            shape.Render(Color.Yellow, bg.Graphics);
                        else
                            shape.Render(Color.Red, bg.Graphics);
                    });

                    //Remove all the shapes that are marked for death
                    shapeList.RemoveAll(shape => shape.IsMarkedForDeath);

                    //Render our shapes
                    bg.Render();
                }
            }
        }
    }
}
