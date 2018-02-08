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
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;

namespace ICA4_NicW
{
    abstract class BaseShape
    {
        //Members
        protected float rotation;       //Current angle
        protected float rotIncrement;   //Rotation speed
        protected float XSpeed;         //Horizontal speed
        protected float YSpeed;         //Vertical speed
        public const int TILESIZE = 50; //Scale

        //Static members
        static protected Random randNum; //Random number generator
        
        //Properties
        public bool IsMarkedForDeath { get; set; }      //Whether we should kill the model
        public PointF Position { get; protected set; }  //Current position on the form

        //Methods

        //Get the graphics path of the shape
        abstract public GraphicsPath GetPath();

        //Draw the shape on the inputted canvas
        public void Render(Color FillColor, Graphics canvas)
        {
            //Draw the model, fully filled in, with the input colour
            canvas.FillPath(new SolidBrush(FillColor), GetPath());
        }

        //Create a shape with a certain number of points, radius, and random change in radius.
        static protected GraphicsPath GenerateShape(int points, int radius, float variance)
        {
            //Our outputted graphics path
            PointF[] outputPoints = new PointF[points];
            //Our rotation to the next point
            double theta = 0;

            //Make every point
            for (int i = 0; i < points; i++)
            {
                //Get the rotation from 0
                theta = (Math.PI * 2 / points) * i;
                //Find our point
                PointF temp = new PointF();
                temp.X = (float)(Math.Cos(theta) * (radius - randNum.NextDouble() * radius * variance)); //Radius is decreased by a random amount determined by the variance
                temp.Y = (float)(Math.Sin(theta) * (radius - randNum.NextDouble() * radius * variance));
                //Put our point into our array
                outputPoints[i] = temp;
            }

            //Make a graphics path and construct our path from our points
            GraphicsPath output = new GraphicsPath();
            output.AddPolygon(outputPoints);

            return output;
        }

        //Cause this shape to rotate and move by its incremental values
        public void Tick(Size canvSize)
        {
            //Rotate our object
            rotation += rotIncrement;

            //We would go outside the horizontal window
            if (Position.X + XSpeed < 0)
            {
                XSpeed *= -1;
                Position = new PointF(0, Position.Y);
            }
            else if (Position.X + XSpeed > canvSize.Width)
            {
                XSpeed *= -1;
                Position = new PointF(canvSize.Width, Position.Y);
            }
            //We would go outside the vertical window
            if (Position.Y + YSpeed < 0)
            {
                YSpeed *= -1;
                Position = new PointF(Position.X, 0);
            }
            else if (Position.Y + YSpeed > canvSize.Height)
            {
                YSpeed *= -1;
                Position = new PointF(Position.X, canvSize.Height);
            }
            //Translate our object
            float x = Position.X + XSpeed;
            float y = Position.Y + YSpeed;
            Position = new PointF(x, y);
        }

        //Constructors
        public BaseShape(PointF inPoint)
        {
            Position = inPoint;

            rotation = 0;
            rotIncrement = (float)(randNum.NextDouble() * 6 - 3);

            XSpeed = (float)(randNum.NextDouble() * 5 - 2.5);
            YSpeed = (float)(randNum.NextDouble() * 5 - 2.5);
        }

        static BaseShape()
        {
            randNum = new Random();
        }
    }

    class Triangle : BaseShape
    {
        //Our unchangeable model
        static readonly GraphicsPath model;

        //How to get a copy our unchangeable model with appropriate transforms
        public override GraphicsPath GetPath()
        {
            //Make the transform
            Matrix mat = new Matrix();
            mat.Rotate(this.rotation);
            mat.Translate(this.Position.X, this.Position.Y, MatrixOrder.Append);
            //Clone the model and apply the transform
            GraphicsPath temp = (model.Clone() as GraphicsPath);
            temp.Transform(mat);
            //Return the transformed model
            return temp;
        }

        public Triangle(PointF inPoint) : base (inPoint)
        {}

        static Triangle()
        {
            //Make the triangle model
            model = GenerateShape(3, TILESIZE, 0);
        }
    }

    class Asteroid : BaseShape
    {
        //Our unchangeable model
        readonly GraphicsPath model;

        //How to get a copy our unchangeable model with appropriate transforms
        public override GraphicsPath GetPath()
        {
            //Make the transform
            Matrix mat = new Matrix();
            mat.Rotate(this.rotation);
            mat.Translate(this.Position.X, this.Position.Y, MatrixOrder.Append);
            //Clone the model and apply the transform
            GraphicsPath temp = (model.Clone() as GraphicsPath);
            temp.Transform(mat);
            //Return the transformed model
            return temp;
        }
        //Constructor
        public Asteroid(PointF inPoint) : base(inPoint)
        {
            this.model = GenerateShape(randNum.Next(4,12), TILESIZE, 0.7f);
        }
    }
}
