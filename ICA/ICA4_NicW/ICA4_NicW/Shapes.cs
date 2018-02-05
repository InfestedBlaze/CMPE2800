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
        protected float rotation;
        protected float rotIncrement;
        protected float XSpeed;
        protected float YSpeed;
        public const int TILESIZE = 50;

        //Static members
        static protected Random randNum;

        //Fields
        public bool IsMarkedForDeath { get; set; }
        public PointF Position { get; set; }

        //Methods
        abstract protected GraphicsPath GetPath();

        public void Render(Color FillColor, Graphics canvas)
        {
            //Draw the model, fully filled in, with the input colour
            canvas.DrawPath(new Pen(new SolidBrush(FillColor)), GetPath());
        }

        static protected GraphicsPath ShapeBase(int points, int radius, float variance)
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
                temp.X = (float)(Math.Cos(theta) * radius * (variance + randNum.NextDouble() * 0.2)); //Variance can have up to 0.2 added to it.
                temp.Y = (float)(Math.Sin(theta) * radius * (variance + randNum.NextDouble() * 0.2));
                //Put our point into our array
                outputPoints[i] = temp;
            }

            //Make a graphics path and construct our path from our points
            GraphicsPath output = new GraphicsPath();
            output.AddPolygon(outputPoints);

            return output;
        }

        public void Tick(Size canvSize)
        {
            //Rotate our object
            rotation += rotIncrement;

            //We would go outside the horizontal window
            if (Position.X + XSpeed + TILESIZE < 0 || Position.X + XSpeed + TILESIZE > canvSize.Width)
            {
                XSpeed *= -1;
            }
            //We would go outside the vertical window
            if (Position.Y + YSpeed + TILESIZE < 0 || Position.Y + YSpeed + TILESIZE > canvSize.Height)
            {
                YSpeed *= -1;
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
            rotIncrement = randNum.Next(-3, 3);

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
        static readonly GraphicsPath model;

        protected override GraphicsPath GetPath()
        {
            //Make the transform
            Matrix mat = new Matrix();
            mat.Rotate(this.rotation);
            mat.Translate(this.XSpeed, this.YSpeed, MatrixOrder.Append);
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
            model = ShapeBase(3, TILESIZE, 1.0f);
        }
    }

    class Asteroid : BaseShape
    {
        //Our unchangeable model
        readonly GraphicsPath model;

        //How to get a copy our unchangeable model with appropriate transforms
        protected override GraphicsPath GetPath()
        {
            //Make the transform
            Matrix mat = new Matrix();
            mat.Rotate(this.rotation);
            mat.Translate(this.XSpeed, this.YSpeed, MatrixOrder.Append);
            //Clone the model and apply the transform
            GraphicsPath temp = (model.Clone() as GraphicsPath);
            temp.Transform(mat);
            //Return the transformed model
            return temp;
        }
        //Constructor
        public Asteroid(PointF inPoint) : base(inPoint)
        {
            this.model = ShapeBase(randNum.Next(4,12), TILESIZE, 0.8f);
        }
    }
}
