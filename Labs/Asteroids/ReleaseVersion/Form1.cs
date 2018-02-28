using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace ReleaseVersion
{
    public partial class Form1 : Form
    {
        //Only need to check here for inputs
        InputControls keyControls = new InputControls();
        //Have our shapes
        Ship ship;
        List<Asteroid> asteroidList = new List<Asteroid>();
        List<Bullet> bulletList = new List<Bullet>();

        //Random numbers
        static Random randNum = new Random();
        //timing things
        Stopwatch timer = new Stopwatch();
        long lastShotTime = 0;

        //Variables to check if we are paused
        bool Paused = false;        //Pauses the game when true
        bool startPaused = false;   //Determines the previous state of the pause button

        public Form1()
        {
            InitializeComponent();
            //Triangle is only used for our ship, it has been slightly modded to allow for this
            ship = new Ship(new PointF(ClientSize.Width / 2, ClientSize.Height / 2));
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            openingScreen();
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

        //Opening screen
        private void openingScreen()
        {
            while (!keyControls.Inputs[Keys.Space])
            {
                using (BufferedGraphicsContext bgc = new BufferedGraphicsContext())
                {
                    using (BufferedGraphics bg = bgc.Allocate(CreateGraphics(), ClientRectangle))
                    {
                        bg.Graphics.Clear(Color.Black);

                        //Give instructions
                        int startX = ClientSize.Width / 4;
                        bg.Graphics.DrawString("A/D to rotate", new Font(FontFamily.GenericMonospace, 20), new SolidBrush(Color.White), startX, 300);
                        bg.Graphics.DrawString("P to pause", new Font(FontFamily.GenericMonospace, 20), new SolidBrush(Color.White), startX, 350);
                        bg.Graphics.DrawString("Space to shoot", new Font(FontFamily.GenericMonospace, 20), new SolidBrush(Color.White), startX, 400);
                        bg.Graphics.DrawString("Space to start", new Font(FontFamily.GenericMonospace, 20), new SolidBrush(Color.White), startX, 450);

                        bg.Render();
                    }
                }
            }
            //Enable the timers and stopwatch
            timer_Game.Enabled = true;
            timer_Spawn.Enabled = true;
            timer.Restart();
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
                        //Less than 8 bullets, 300ms delay between bullets
                        if (!Paused && bulletList.Count < 8 && lastShotTime < timer.ElapsedMilliseconds-300)
                        {
                            bulletList.Add(new Bullet(ship.GetPath().PathPoints[0], ship.getRotation()));
                            lastShotTime = timer.ElapsedMilliseconds;
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
                        foreach (BaseShape asteroid in asteroidList)
                        {
                            //If we are within the size of the ship, check collision
                            if(GetDistance(asteroid, ship) < ship.Size)
                            {

                            }
                            //If we are within the size of any bullet, check collision
                            foreach (BaseShape bullet in bulletList)
                            {
                                if (GetDistance(asteroid, bullet) < bullet.Size)
                                {

                                }
                            }
                        }

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
            if (timer_Spawn.Interval > 0) timer_Spawn.Interval--;
            asteroidList.Add(new Asteroid(new PointF(randNum.Next(ClientSize.Width), randNum.Next(ClientSize.Height))));
        }

        private float GetDistance(BaseShape arg1, BaseShape arg2)
        {
            // _/{ (X2 - X1)^2 + (Y2 - Y1)^2 }
            return (float)Math.Sqrt(Math.Pow(arg1.Position.X - arg2.Position.X, 2) + Math.Pow(arg1.Position.Y - arg2.Position.Y, 2));
        }

        
    }
}
