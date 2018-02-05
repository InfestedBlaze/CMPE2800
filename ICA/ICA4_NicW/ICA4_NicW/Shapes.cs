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
        static Random randNum;

        //Fields
        public bool IsMarkedForDeath { get; set; }
        public PointF Position { get; set; }

        //Methods
        abstract protected GraphicsPath GetPath();

        public void Render()
        {

        }

        protected GraphicsPath ShapeBase(int points, bool circle)
        {
            return new GraphicsPath();
        }

        public void Tick()
        {

        }

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
            model = ShapeBase(3, true);
        }
    }
}
