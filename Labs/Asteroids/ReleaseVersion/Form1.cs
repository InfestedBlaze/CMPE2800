using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReleaseVersion
{
    public partial class Form1 : Form
    {
        //Only need to check here for inputs
        InputControls keyControls = new InputControls();
        //Have our shapes
        Triangle ship;
        List<BaseShape> asteroidList = new List<BaseShape>();
        List<Bullet> bulletList = new List<Bullet>();
        //Random numbers
        static Random randNum = new Random();

        //Variables to check if we are paused
        bool Paused = false;        //Pauses the game when true
        bool startPaused = false;   //Determines the previous state of the pause button

        public Form1()
        {
            InitializeComponent();
            //Triangle is only used for our ship, it has been slightly modded to allow for this
            ship = new Triangle(new PointF(ClientSize.Width / 2, ClientSize.Height / 2));
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


        //Everything our game does is in here
        private void timer_Game_Tick(object sender, EventArgs e)
        {
            using (BufferedGraphicsContext bgc = new BufferedGraphicsContext())
            {
                using (BufferedGraphics bg = bgc.Allocate(CreateGraphics(), ClientRectangle))
                {
                    //Clear our form of shapes
                    bg.Graphics.Clear(Color.Black);

                    //Check the inputs for our triangle

                    //Rotation
                    if (keyControls.Inputs[Keys.A])
                    {
                        //Rotate left
                        ship.setRotationIncrement(-5);
                    }
                    else if (keyControls.Inputs[Keys.D])
                    {
                        //Rotate right
                        ship.setRotationIncrement(5);
                    }
                    else
                    {
                        ship.setRotationIncrement(0);
                    }


                    //Special inputs
                    if (keyControls.Inputs[Keys.Space])
                    {
                        //Fire
                        if (!Paused && bulletList.Count < 8)
                        {
                            bulletList.Add(new Bullet(ship.GetPath().PathPoints[0], ship.getRotation()));
                        }

                    }
                    //Check for a new button state, for pausing
                    if (keyControls.Inputs[Keys.P] != startPaused)
                    {
                        //Got a new button state
                        startPaused = keyControls.Inputs[Keys.P];
                        //Check if new button state is pressed down
                        if (startPaused)
                        {
                            //Toggle the game's pause
                            Paused = !Paused;
                            //Stop the spawn timer if we are paused
                            timer_Spawn.Enabled = !Paused;
                        }
                    }

                    //Game is not paused, do all things
                    if (!Paused)
                    {
                        //Tick through all of our shapes
                        asteroidList.ForEach(shape => shape.Tick(ClientSize));
                        bulletList.ForEach(shape => shape.Tick(ClientSize));
                        ship.Tick(ClientSize);

                        //Do collision calculations

                        //Remove all the shapes that are marked for death
                        asteroidList.RemoveAll(shape => shape.IsMarkedForDeath);
                        bulletList.RemoveAll(shape => shape.IsMarkedForDeath);
                    }


                    //Put our shapes on the screen
                    ship.Render(Color.Yellow, bg.Graphics);
                    asteroidList.ForEach(shape => shape.Render(Color.Red, bg.Graphics));
                    bulletList.ForEach(shape => shape.Render(Color.Yellow, bg.Graphics));

                    //Game is paused, draw pause over top screen
                    if (Paused)
                    {
                        //Draw a black fade over the screen
                        bg.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(200, Color.Black)), ClientRectangle);
                        bg.Graphics.DrawString("Paused", new Font(FontFamily.GenericMonospace, 20), new SolidBrush(Color.Red), ClientRectangle);
                    }

                    //Render our shapes
                    bg.Render();
                }
            }
        }

        private void timer_Spawn_Tick(object sender, EventArgs e)
        {
            timer_Spawn.Interval--;
            asteroidList.Add(new Asteroid(new PointF(randNum.Next(ClientSize.Width), randNum.Next(ClientSize.Height))));
        }
    }
}
