﻿/*
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
        Triangle ship;
        List<BaseShape> shapeList = new List<BaseShape>();
        //Random numbers
        static Random randNum = new Random();

        //Variables to check if we are paused
        bool Paused = false;        //Pauses the game when true
        bool startPaused = false;   //Determines the previous state of the pause button

        public Form1()
        {
            InitializeComponent();
            ship = new Triangle(new PointF(ClientSize.Width / 2, ClientSize.Height / 2));
            shapeList.Add(new Asteroid(new PointF(randNum.Next(ClientSize.Width), randNum.Next(ClientSize.Height))));
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
                        shapeList.Add(new Asteroid(ship.GetPath().PathPoints[0]));
                    }
                    //Check for a new button state
                    if (keyControls.Inputs[Keys.P] != startPaused)
                    {
                        //Got a new button state
                        startPaused = keyControls.Inputs[Keys.P];
                        //Check if new button state is pressed down
                        if (startPaused)
                        {
                            //Toggle the game's pause
                            Paused = !Paused;
                        }
                    }

                    //Game is not paused, do all things
                    if (!Paused)
                    {
                        //Tick through all of our shapes
                        shapeList.ForEach(shape => shape.Tick(ClientSize));
                        ship.Tick(ClientSize);
                        //Remove all the shapes that are marked for death
                        shapeList.RemoveAll(shape => shape.IsMarkedForDeath);
                    }


                    //Put our shapes on the screen
                    ship.Render(Color.Yellow, bg.Graphics);
                    shapeList.ForEach(shape => shape.Render(Color.Red, bg.Graphics));
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
    }
}
