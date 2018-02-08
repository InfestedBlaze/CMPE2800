/*
 * Nicholas Wasylyshyn
 * February 7, 2018
 * This program builds our knowledge of how the GDI
 * is used and how to implement it.
 * We create graphics paths to create random and fixed
 * models. Have them rotate and translate across our form.
 * Left and right clicking will create a model. Shift clicking
 * will create 1000 objects in random places within the 
 * area of our form.
 */

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
using System.Diagnostics;

namespace ICA4_NicW
{
    //This struct is used to store collisions between shapes.
    //They have a region which we draw on the screen and a
    //timestamp that tells us when they were made.
    struct timeStampRegion
    {
        public Region region;
        public long timestamp;

        public timeStampRegion(Region inRegion, long time)
        {
            region = inRegion;
            timestamp = time;
        }
    }

    public partial class Form1 : Form
    {
        //This is our list of shapes
        List<BaseShape> shapeList = new List<BaseShape>();
        //Our collisions between shapes
        LinkedList<timeStampRegion> collisions = new LinkedList<timeStampRegion>();
        //Random numbers and a stopwatch
        Random randNum = new Random();
        Stopwatch sw = new Stopwatch();

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
                    shapeList.ForEach(shape =>
                    {
                        //Triangles are orange, asteroids are yellow
                        if (shape is Triangle)
                            shape.Render(Color.Orange, bg.Graphics);
                        else
                            shape.Render(Color.Yellow, bg.Graphics);
                    });

                    //Put our intersections on the screen
                    foreach(timeStampRegion r in collisions)
                    {
                        bg.Graphics.FillRegion(new SolidBrush(Color.DarkBlue), r.region);
                    }

                    //Remove collisions that are more than 2.5 seconds old
                    foreach(timeStampRegion r in collisions.ToList())
                    {
                        if(r.timestamp + 2500 < sw.ElapsedMilliseconds)
                        {
                            collisions.Remove(r);
                        }
                    }

                    //Find intersections
                    foreach(BaseShape currentShape in shapeList)
                    {
                        //Our region that we want to keep for every check against the inner list
                        Region outerRegion = new Region(currentShape.GetPath());

                        foreach(BaseShape checkShape in shapeList)
                        {
                            //Must be within a good distance to check, and not itself
                            if (GetDistance(currentShape, checkShape) < BaseShape.TILESIZE * 2 &&
                                !ReferenceEquals(currentShape, checkShape))
                            {
                                //Get our intersection region
                                Region innerRegion = new Region(checkShape.GetPath());
                                innerRegion.Intersect(outerRegion);

                                //Check if we intersected
                                if (innerRegion.GetRegionScans(new Matrix()).Length > 0)
                                {
                                    //Delete the intersecting shapes
                                    currentShape.IsMarkedForDeath = true;
                                    checkShape.IsMarkedForDeath = true;
                                    //Add it to our collision history
                                    collisions.AddLast(new timeStampRegion(innerRegion, sw.ElapsedMilliseconds));
                                }
                            }
                        }
                    }

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

        private void Form1_Load(object sender, EventArgs e)
        {
            //Make sure our screen is big enough for the shapes
            MinimumSize = new Size(BaseShape.TILESIZE * 3, BaseShape.TILESIZE * 3);
            //Get the stop watch started
            sw.Restart();
        }
    }
}
