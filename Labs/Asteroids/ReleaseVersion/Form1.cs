﻿using System;
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
    enum gameState { Start, Playing, GameOver}

    public partial class Form1 : Form
    {
        //Only need to check here for inputs
        InputControls keyControls = new InputControls();

        //Have our shapes
        Ship ship;
        List<Asteroid> asteroidList = new List<Asteroid>();
        List<Bullet> bulletList = new List<Bullet>();

        //Lives
        int shipLives = 3;
        
        //Score
        int score = 0;
        int newLife = 10000; //This is how much score you need to get a new life

        //Random numbers
        static Random randNum = new Random();

        //timing things
        Stopwatch timer = new Stopwatch();
        long lastShotTime = 0;

        //Variables to check if we are paused
        bool Paused = false;        //Pauses the game when true
        bool startPaused = false;   //Determines the previous state of the pause button

        gameState gameState = gameState.Start;

        public Form1()
        {
            InitializeComponent();
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
            
        }

        //Everything our game does is in here
        private void timer_Game_Tick(object sender, EventArgs e)
        {
            if (gameState == gameState.Start)
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

                //Reset everything before going on
                shipLives = 3;
                score = 0;
                newLife = 10000;
                asteroidList.Clear();
                bulletList.Clear();
                timer.Restart();
                lastShotTime = 0;
                timer_Spawn.Interval = 2000;

                if (keyControls.Inputs[Keys.Space])
                {
                    gameState = gameState.Playing;
                }
            }
            else if (gameState == gameState.Playing)
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
                            ship.setRotationIncrement(BaseShape.degreesToRadians(-5));
                        }
                        else if (keyControls.Inputs[Keys.D])
                        {
                            //Rotate right
                            ship.setRotationIncrement(BaseShape.degreesToRadians(5));
                        }
                        else
                        {
                            ship.setRotationIncrement(0);
                        }

                        //Special inputs
                        //Drive forward
                        //if (keyControls.Inputs[Keys.W])
                        //{
                        //    if (!Paused)
                        //    {
                        //    }
                        //}

                        //Fire single shot
                        if (keyControls.Inputs[Keys.Space])
                        {
                            //Less than 8 bullets, 300ms delay between bullets
                            if (!Paused && bulletList.Count < 8 && lastShotTime < timer.ElapsedMilliseconds-300)
                            {
                                bulletList.Add(new Bullet(ship.GetPath().PathPoints[0], ship.getRotation()));
                                lastShotTime = timer.ElapsedMilliseconds;
                            }

                        }

                        //Spread shot
                        if (keyControls.Inputs[Keys.S])
                        {
                            if (!Paused && bulletList.Count < 5 && lastShotTime < timer.ElapsedMilliseconds - 300)
                            {
                                //how much to change the angle
                                float delta = BaseShape.degreesToRadians(10);
                                //get angle of the ship
                                float angle = ship.getRotation() - delta;

                                for (int i = 0; i < 3; i++)
                                {
                                    //Make a bullet
                                    bulletList.Add(new Bullet(ship.GetPath().PathPoints[0], angle));
                                    //increase angle
                                    angle += delta;
                                }
                            
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
                            foreach (BaseShape asteroid in asteroidList.ToList())
                            {
                                //Get the asteroids region
                                Region asteroidRegion = new Region(asteroid.GetPath());

                                //If we are within hit range for ship, check collision
                                if (GetDistance(asteroid, ship) < Math.Pow(ship.Size,2) + Math.Pow(asteroid.Size,2))
                                {
                                    //Get the ships region
                                    Region shipRegion = new Region(ship.GetPath());
                                    //Intersect the regions
                                    shipRegion.Intersect(asteroidRegion);
                                    //Check if there is any collision
                                    if(shipRegion.GetRegionScans(new System.Drawing.Drawing2D.Matrix()).Length > 0)
                                    {
                                        //Lose a life
                                        if(shipLives > 0)
                                            shipLives--;

                                        //Kill the asteroid
                                        asteroid.IsMarkedForDeath = true;
                                    }
                                }

                                //If we are within hit range for bullet, check collision
                                foreach (BaseShape bullet in bulletList)
                                {
                                    if (GetDistance(asteroid, bullet) < Math.Pow(bullet.Size,2) + Math.Pow(asteroid.Size,5))
                                    {
                                        //Get the bullets region
                                        Region bulletRegion = new Region(bullet.GetPath());
                                        //Intersect the regions
                                        bulletRegion.Intersect(asteroidRegion);
                                        //Check if there is any collision
                                        if (bulletRegion.GetRegionScans(new System.Drawing.Drawing2D.Matrix()).Length > 0)
                                        {
                                            //Check if any asteroids can be broken up.
                                            //Add score based on size. Large worth less than small
                                            if (asteroid.Size == Asteroid.MAXSIZE)
                                            {
                                                //Large asteroid can break apart into 2 new, smaller asteroids
                                                asteroidList.Add(new Asteroid(asteroid.Position, asteroid.Size - (Asteroid.MAXSIZE / 3)));
                                                asteroidList.Add(new Asteroid(asteroid.Position, asteroid.Size - (Asteroid.MAXSIZE / 3)));

                                                //Score based on asteroid size
                                                score += 100;
                                            }
                                            else if (asteroid.Size >= Asteroid.MAXSIZE / 2)
                                            {
                                                //Medium asteroid can break apart into 3 new, smaller asteroids
                                                asteroidList.Add(new Asteroid(asteroid.Position, asteroid.Size - (Asteroid.MAXSIZE / 3)));
                                                asteroidList.Add(new Asteroid(asteroid.Position, asteroid.Size - (Asteroid.MAXSIZE / 3)));
                                                asteroidList.Add(new Asteroid(asteroid.Position, asteroid.Size - (Asteroid.MAXSIZE / 3)));

                                                //Score based on asteroid size
                                                score += 200;
                                            }
                                            else
                                            {
                                                //Score based on asteroid size
                                                score += 300;
                                            }
                                        
                                            //Kill bullet and asteroid
                                            asteroid.IsMarkedForDeath = true;
                                            bullet.IsMarkedForDeath = true;
                                        }
                                    }
                                }
                            }
                        
                            //Check if we have more than/equal to newLife points
                            if (score - newLife >= 0)
                            {
                                shipLives++;
                                newLife += 10000; //This is assuming 10,000 is your starting value
                            }
                            //Check if we are out of lives
                            if (shipLives <= 0)
                            {
                                //Game over
                                gameState = gameState.GameOver;
                            }

                            //Remove all the shapes that are marked for death
                            asteroidList.RemoveAll(shape => shape.IsMarkedForDeath);
                            bulletList.RemoveAll(shape => shape.IsMarkedForDeath);
                        }


                        //Put our shapes on the screen
                        ship.Render(Color.Yellow, bg.Graphics);
                        asteroidList.ForEach(shape => shape.Render(Color.Red, bg.Graphics));
                        bulletList.ForEach(shape => shape.Render(Color.Yellow, bg.Graphics));
                        bg.Graphics.DrawString($"Score: {score}\nLives: {shipLives}", new Font(FontFamily.GenericMonospace, 20), new SolidBrush(Color.Green), ClientRectangle);

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
            else if (gameState == gameState.GameOver)
            {
                //Write on the screen
                using (BufferedGraphicsContext bgc = new BufferedGraphicsContext())
                {
                    using (BufferedGraphics bg = bgc.Allocate(CreateGraphics(), ClientRectangle))
                    {
                        bg.Graphics.Clear(Color.Black);

                        //Give instructions
                        int startX = ClientSize.Width / 4;
                        bg.Graphics.DrawString("Game Over", new Font(FontFamily.GenericMonospace, 20), new SolidBrush(Color.White), startX, 300);
                        bg.Graphics.DrawString($"Score: {score}", new Font(FontFamily.GenericMonospace, 20), new SolidBrush(Color.White), startX, 350);

                        bg.Render();
                    }
                }

                if (keyControls.Inputs[Keys.Space])
                {
                    gameState = gameState.Start;
                }
            }
        }

        private void timer_Spawn_Tick(object sender, EventArgs e)
        {
            if (timer_Spawn.Interval > 1000) timer_Spawn.Interval--;
            asteroidList.Add(new Asteroid(new PointF(randNum.Next(ClientSize.Width), randNum.Next(ClientSize.Height)), Asteroid.MAXSIZE));
        }

        private float GetDistance(BaseShape arg1, BaseShape arg2)
        {
            // _/{ (X2 - X1)^2 + (Y2 - Y1)^2 }
            return (float)Math.Sqrt(Math.Pow(arg2.Position.X - arg1.Position.X, 2) + Math.Pow(arg2.Position.Y - arg1.Position.Y, 2));
        }
    }
}
