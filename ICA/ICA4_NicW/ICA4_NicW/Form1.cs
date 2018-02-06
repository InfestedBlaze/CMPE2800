﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICA4_NicW
{
    public partial class Form1 : Form
    {
        List<BaseShape> shapeList = new List<BaseShape>();
        LinkedList<Region> collisions = new LinkedList<Region>();
        Random randNum = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void timer_25ms_Tick(object sender, EventArgs e)
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
                    shapeList.ForEach(shape => shape.Render(Color.Yellow, bg.Graphics));

                    //Remove all the shapes that are marked for death
                    shapeList.RemoveAll(shape => shape.IsMarkedForDeath);

                    //Render our shapes
                    bg.Render();
                }
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            //Shift is pressed
            if(Control.ModifierKeys == Keys.Shift)
            {
                //Left click
                if(e.Button == MouseButtons.Left)
                {
                    //Make 1000 triangles in random locations
                    for (int i = 0; i < 1000; i++)
                    {
                        shapeList.Add(new Triangle(new PointF(randNum.Next(ClientSize.Width), randNum.Next(ClientSize.Height))));
                    }
                }
                //Right click
                else if (e.Button == MouseButtons.Right)
                {
                    //Make 1000 asteroids in random locations
                    for (int i = 0; i < 1000; i++)
                    {
                        shapeList.Add(new Asteroid(new PointF(randNum.Next(ClientSize.Width), randNum.Next(ClientSize.Height))));
                    }
                }
            }
            else //Shift is not pressed
            {
                //Left click
                if (e.Button == MouseButtons.Left)
                {
                    shapeList.Add(new Triangle(e.Location));
                }
                //Right click
                else if (e.Button == MouseButtons.Right)
                {
                    shapeList.Add(new Asteroid(e.Location));
                }
            }
        }

        private float GetDistance(BaseShape arg1, BaseShape arg2)
        {
            // _/{ (X2 - X1)^2 + (Y2 - Y1)^2 }
            return (float)Math.Sqrt( Math.Pow(arg1.Position.X - arg2.Position.X, 2) + Math.Pow(arg1.Position.Y - arg2.Position.Y, 2));
        }

        private bool Intersect(BaseShape arg1, BaseShape arg2)
        {
            Region regArg1, regArg2;

            regArg1 = new Region(arg1.GetPath());
            regArg2 = new Region(arg2.GetPath());
            
        }
    }
}
