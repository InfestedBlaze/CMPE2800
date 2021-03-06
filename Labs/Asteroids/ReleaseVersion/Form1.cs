﻿/* Author:      Nicholas Wasylyshyn
 * Date:        March 12, 2018
 * Description: This is an asteroids game.
 * You pilot a ship using the keyboard or an
 * XBOX controller. Your goal is to shoot as
 * many asteroids and get as large of a score
 * as possible before dying. Asteroids will
 * spawn in on a timer and will spawn faster
 * based on how long you have played.
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
using System.Diagnostics;

namespace ReleaseVersion
{
    //Different game states will determine what screen we are on
    enum gameState { Start, Playing, GameOver}

    //Background for our play screen
    class Star
    {
        //Where we are on our screen
        public PointF Position { get; private set; }
        //What colour we are
        public Color Colour { get; private set; }

        public Star(PointF inPosition, Color inColor)
        {
            Position = inPosition;
            Colour = inColor;
        }

        public void changePosition(float X, float Y, Size inSize)
        {
            //Get new position
            X = Position.X + X;
            Y = Position.Y + Y;

            //Outside size boundary, warp to other side
            if(X < 0)
            {
                X = inSize.Width;
            }
            else if(X > inSize.Width)
            {
                X = 0;
            }
            //Outside size boundary, warp to other side
            if (Y < 0)
            {
                Y = inSize.Height;
            }
            else if (Y > inSize.Height)
            {
                Y = 0;
            }

            //Set new position
            Position = new PointF(X, Y);
        }
    }

    public partial class Form1 : Form
    {
        //Only need to check here for inputs
        InputControls keyControls = new InputControls();

        //Have our shapes
        Ship ship;
        List<Asteroid> asteroidList = new List<Asteroid>();
        List<Bullet> bulletList = new List<Bullet>();
        List<Star> starList = new List<Star>();

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

        //Variables for thruster
        float ThrusterX = 0;        //The X speed to add to asteroids/stars
        float ThrusterY = 0;        //The Y speed to add to asteroids/stars

        //Open the game on the starting screen
        gameState gameState = gameState.Start;

        public Form1()
        {
            InitializeComponent();

            //Make our ship
            ship = new Ship(new PointF(ClientSize.Width / 2, ClientSize.Height / 2));

            //Make all of our stars
            while(starList.Count < 300)
            {
                int x = randNum.Next(ClientSize.Width);
                int y = randNum.Next(ClientSize.Height);

                starList.Add(new Star(new PointF(x, y), Color.FromArgb(randNum.Next(int.Parse("FF000000", System.Globalization.NumberStyles.HexNumber), int.Parse("FFFFFFFF", System.Globalization.NumberStyles.HexNumber)))));
            }
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
            if (gameState == gameState.Start) //On the start screen. Give instructions, reset everything
            {
                //Write to the screen
                using (BufferedGraphicsContext bgc = new BufferedGraphicsContext())
                {
                    using (BufferedGraphics bg = bgc.Allocate(CreateGraphics(), ClientRectangle))
                    {
                        bg.Graphics.Clear(Color.Black);

                        //Give instructions
                        StringFormat sf = new StringFormat();
                        sf.Alignment = StringAlignment.Center;
                        bg.Graphics.DrawString("W to boost\nA/D to rotate\nP to pause\nSpace/S to shoot\nSpace to start", new Font(FontFamily.GenericMonospace, 20), new SolidBrush(Color.White), ClientSize.Width/2, ClientSize.Height/2 - 186, sf);
                        bg.Graphics.DrawString("B to boost\nLeft Stick to rotate\nStart to pause\nA/X to shoot\nA to start", new Font(FontFamily.GenericMonospace, 20), new SolidBrush(Color.Green), ClientSize.Width / 2, ClientSize.Height / 2, sf);

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

                //Wait for the space button
                if (keyControls.Inputs[Keys.Space])
                {
                    gameState = gameState.Playing;
                    timer_Spawn.Enabled = true;
                }
            }
            else if (gameState == gameState.Playing)
            {

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
                        //Stop rotating
                        ship.setRotationIncrement(0);
                    }

                    //Special inputs
                    //Drive forward
                    if (keyControls.Inputs[Keys.W])
                    {
                        //Get the forward direction of the ship
                        ThrusterX = (float)-Math.Cos(ship.getRotation() + Math.PI  / 2);
                        ThrusterY = (float)-Math.Sin(ship.getRotation() + Math.PI / 2);
                        //Add it to each asteroid
                        asteroidList.ForEach(ast => ast.ChangeSpeed(ThrusterX, ThrusterY));
                        //Move stars
                        starList.ForEach(star => star.changePosition(ThrusterX, ThrusterY, ClientSize));
                    }

                    //Fire single shot
                    if (keyControls.Inputs[Keys.Space])
                    {
                        //Less than 8 bullets, 300ms delay between bullets
                        if (bulletList.Count < 8 && lastShotTime < timer.ElapsedMilliseconds - 300)
                        {
                            //Make a bullet
                            bulletList.Add(new Bullet(ship.GetPath().PathPoints[0], ship.getRotation()));
                            //Change when we last shot
                            lastShotTime = timer.ElapsedMilliseconds;
                        }
                    }

                    //Spread shot
                    if (keyControls.Inputs[Keys.S])
                    {
                        //must have less than 5 bullet (max 8), and 3x as much time before our last shot compared to single shot
                        if (bulletList.Count < 5 && lastShotTime < timer.ElapsedMilliseconds - 900)
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
                            //Change when we last shot
                            lastShotTime = timer.ElapsedMilliseconds;
                        }
                    }

                    //Tick through all of our shapes
                    asteroidList.ForEach(shape => shape.Tick(ClientSize));
                    bulletList.ForEach(shape => shape.Tick(ClientSize));
                    ship.Tick(ClientSize);

                    //Do collision calculations
                    foreach (Asteroid asteroid in asteroidList.ToList())
                    {
                        //Get the asteroids region
                        Region asteroidRegion = new Region(asteroid.GetPath());

                        //If we are within hit range for ship, check collision
                        if (GetDistance(asteroid, ship) < Math.Pow(ship.Size, 2) + Math.Pow(asteroid.Size, 2))
                        {
                            //Get the ships region
                            Region shipRegion = new Region(ship.GetPath());
                            //Intersect the regions
                            shipRegion.Intersect(asteroidRegion);
                            //Check if there is any collision
                            if (shipRegion.GetRegionScans(new System.Drawing.Drawing2D.Matrix()).Length > 0)
                            {
                                //Lose a life
                                if (shipLives > 0)
                                    shipLives--;

                                //Kill the asteroid
                                asteroid.IsMarkedForDeath = true;
                            }
                        }

                        //If we are within hit range for bullet, check collision
                        foreach (Bullet bullet in bulletList)
                        {
                            //Check if we are in range to hit
                            if (GetDistance(asteroid, bullet) < Math.Pow(bullet.Size, 2) + Math.Pow(asteroid.Size, 5))
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
                    if (score - newLife > 0)
                    {
                        shipLives++;
                        newLife += 10000; //Get our new point for a new life
                    }
                    //Check if we are out of lives
                    if (shipLives <= 0)
                    {
                        //Game over
                        gameState = gameState.GameOver;
                        //Stop spawning asteroids.
                        timer_Spawn.Enabled = false;
                    }

                    //Remove all the shapes that are marked for death
                    asteroidList.RemoveAll(shape => shape.IsMarkedForDeath);
                    bulletList.RemoveAll(shape => shape.IsMarkedForDeath);
                }

                //Draw everything
                using (BufferedGraphicsContext bgc = new BufferedGraphicsContext())
                {
                    using (BufferedGraphics bg = bgc.Allocate(CreateGraphics(), ClientRectangle))
                    {
                        //Clear our form of shapes
                        bg.Graphics.Clear(Color.Black);

                        //Put our shapes on the screen
                        starList.ForEach(star => bg.Graphics.DrawRectangle(new Pen(new SolidBrush(star.Colour)), star.Position.X, star.Position.Y, 1, 1));
                        ship.Render(Color.Yellow, bg.Graphics);
                        asteroidList.ForEach(shape => shape.Render(Color.Red, bg.Graphics));
                        bulletList.ForEach(shape => shape.Render(Color.Yellow, bg.Graphics));
                        bg.Graphics.DrawString($"Score: {score}\nLives: {shipLives}", new Font(FontFamily.GenericMonospace, 20), new SolidBrush(Color.Green), ClientRectangle);

                        //Game is paused, draw pause over top screen
                        if (Paused)
                        {
                            //Draw a black fade over the screen
                            bg.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(200, Color.Black)), ClientRectangle);
                            //Centered, red text
                            StringFormat sf = new StringFormat();
                            sf.Alignment = StringAlignment.Center;
                            bg.Graphics.DrawString("Paused", new Font(FontFamily.GenericMonospace, 20), new SolidBrush(Color.Red), ClientSize.Width / 2, ClientSize.Height / 2 - 61, sf);
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
                        StringFormat sf = new StringFormat();
                        sf.Alignment = StringAlignment.Center;
                        bg.Graphics.DrawString($"Game Over\nScore: {score}\nPause to restart", new Font(FontFamily.GenericMonospace, 20), new SolidBrush(Color.White), ClientSize.Width / 2, ClientSize.Height / 2 - 61, sf);

                        bg.Render();
                    }
                }

                if (keyControls.Inputs[Keys.P])
                {
                    gameState = gameState.Start;
                }
            }
        }

        private void timer_Spawn_Tick(object sender, EventArgs e)
        {
            //Increase the spawn speed as the game goes on.
            if (timer_Spawn.Interval > 1000) timer_Spawn.Interval--;

            //Where we will spawn our asteroid
            float x = 0;
            float y = 0;

            //Don't allow asteroids to start near the ship.
            do
            {
                x = randNum.Next(-100, ClientSize.Width + 100);
            } while (x > 0 && x < ClientSize.Width);
            do
            {
                y = randNum.Next(-100, ClientSize.Height + 100);
            } while (y > 0 && y < ClientSize.Height);

            asteroidList.Add(new Asteroid(new PointF(x, y), Asteroid.MAXSIZE));
        }

        private float GetDistance(BaseShape arg1, BaseShape arg2)
        {
            //Used to get the distance from our objects.
            //Determines whether we do the collision calculations.
            // _/{ (X2 - X1)^2 + (Y2 - Y1)^2 }
            return (float)Math.Sqrt(Math.Pow(arg2.Position.X - arg1.Position.X, 2) + Math.Pow(arg2.Position.Y - arg1.Position.Y, 2));
        }
    }
}
