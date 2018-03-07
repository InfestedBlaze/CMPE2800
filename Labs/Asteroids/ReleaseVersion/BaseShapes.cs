/*
 * Author: Nic Wasylyshyn
 * Date: February 26, 2018
 * Description: This hierarchy is used to create shapes
 * for our game. They have sizes and movements unique to
 * themselves. They have a way to render themselves onto 
 * a form.
 */
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

abstract class BaseShape
{
    //Members
    protected float rotation;       //Current angle
    protected float rotIncrement;   //Rotation speed
    protected float XSpeed;         //Horizontal speed
    protected float YSpeed;         //Vertical speed
    protected int tilesize;         //Scale

    public int Size { get { return tilesize; } }

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
        //Draw the model, outlined, with the input colour
        canvas.DrawPath(new Pen(FillColor), GetPath());
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
    public virtual void Tick(Size canvSize)
    {
        //Rotate our object
        rotation += rotIncrement;
        
        //Translate our object
        float x = Position.X + XSpeed;
        float y = Position.Y + YSpeed;
        Position = new PointF(x, y);
    }

    //Constructors
    public BaseShape(PointF inPoint, int size = 5)
    {
        Position = inPoint;

        tilesize = size;

        rotation = (float)(randNum.NextDouble() * Math.PI * 2);
        rotIncrement = degreesToRadians((float)(randNum.NextDouble() * 6 - 3));

        XSpeed = (float)(randNum.NextDouble() * 5 - 2.5);
        YSpeed = (float)(randNum.NextDouble() * 5 - 2.5);
    }

    public BaseShape(PointF inPoint, int size = 5,  float inRotation = 0, float inRotInc = 0, float inXSpeed = 0, float inYSpeed = 0)
    {
        Position = inPoint;

        tilesize = size;

        rotation = inRotation;
        rotIncrement = inRotInc;

        XSpeed = inXSpeed;
        YSpeed = inYSpeed;
    }

    static BaseShape()
    {
        randNum = new Random();
    }
    
    static public float degreesToRadians(float degrees)
    {
        return degrees * (float)((Math.PI * 2) / 360);
    }
    static public float radianToDegrees(float radians)
    {
        return radians * (float)(360 / (Math.PI * 2));
    }
}

class Ship : BaseShape
{
    //Our unchangeable model
    static readonly GraphicsPath model;

    //How to get a copy our unchangeable model with appropriate transforms
    public override GraphicsPath GetPath()
    {
        //Make the transform
        Matrix mat = new Matrix();
        mat.Rotate(radianToDegrees(this.rotation));
        mat.Scale(tilesize, tilesize, MatrixOrder.Append);
        mat.Translate(this.Position.X, this.Position.Y, MatrixOrder.Append);
        //Clone the model and apply the transform
        GraphicsPath temp = (model.Clone() as GraphicsPath);
        temp.Transform(mat);
        //Return the transformed model
        return temp;
    }
    
    public Ship(PointF inPoint) : base(inPoint, 5, (float)Math.PI, 0, 0, 0)
    {
    }

    static Ship()
    {
        //Make a triangle but with some of the bottom cut out
        PointF[] outputTriangle = new PointF[4];
        outputTriangle[0] = new PointF(0, 3);
        outputTriangle[1] = new PointF(3, -3);
        outputTriangle[2] = new PointF(0, -2);
        outputTriangle[3] = new PointF(-3, -3);
        GraphicsPath gp = new GraphicsPath();
        gp.AddPolygon(outputTriangle);
        model = gp;
    }

    //vvvvvvvv Mods for the asteroid game vvvvvvvvvvvvvvvvvv
    
    //Get the current rotation of the triangle
    public float getRotation()
    {
        return rotation;
    }

    //Set the rotation of a triangle
    public void setRotationIncrement(float inIncrement)
    {
        rotIncrement = inIncrement;
    }
    //Set the speed of a triangle
    public void setSpeed(float inXSpeed, float inYSpeed)
    {
        XSpeed = inXSpeed;
        YSpeed = inYSpeed;
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
        mat.Rotate(radianToDegrees(this.rotation));
        mat.Scale(tilesize, tilesize, MatrixOrder.Append);
        mat.Translate(this.Position.X, this.Position.Y, MatrixOrder.Append);
        //Clone the model and apply the transform
        GraphicsPath temp = (model.Clone() as GraphicsPath);
        temp.Transform(mat);
        //Return the transformed model
        return temp;
    }
    //Constructor
    public Asteroid(PointF inPoint, int inSize) : base(inPoint, inSize)
    {
        this.model = GenerateShape(randNum.Next(5, 12), tilesize, 0.7f);
    }

    //vvvvvvvv Mods for the asteroid game vvvvvvvvvvvvvvvvvv

    public override void Tick(Size canvSize)
    {
        //The asteroids have a 100 pixel buffer around the screen.
        //This helps give a safe spawn area for them, and the
        //illusion of a spherical game world.

        //We would go outside the horizontal window, wrap to other edge
        if (Position.X + XSpeed < -100)
        {
            Position = new PointF(canvSize.Width + 100, Position.Y);
        }
        else if (Position.X + XSpeed > canvSize.Width + 100)
        {
            Position = new PointF(-100, Position.Y);
        }
        //We would go outside the vertical window, wrap to other edge
        if (Position.Y + YSpeed < -100)
        {
            Position = new PointF(Position.X, canvSize.Height + 100);
        }
        else if (Position.Y + YSpeed > canvSize.Height + 100)
        {
            Position = new PointF(Position.X, -100);
        }

        base.Tick(canvSize);
    }

    //Maximum size of the asteroid
    public const int MAXSIZE = 10;
}

class Bullet : BaseShape
{
    //Our unchangeable model
    static readonly GraphicsPath model;

    //How to get a copy our unchangeable model with appropriate transforms
    public override GraphicsPath GetPath()
    {
        //Make the transform
        Matrix mat = new Matrix();
        mat.Rotate(radianToDegrees(this.rotation));
        mat.Scale(tilesize, tilesize, MatrixOrder.Append);
        mat.Translate(this.Position.X, this.Position.Y, MatrixOrder.Append);
        //Clone the model and apply the transform
        GraphicsPath temp = (model.Clone() as GraphicsPath);
        temp.Transform(mat);
        //Return the transformed model
        return temp;
    }

    public Bullet(PointF inPoint, float direction) : base(inPoint, 2, 0, 0, 0, 0)
    {
        this.XSpeed = (float)(Math.Cos(direction + Math.PI/2) * 5);
        this.YSpeed = (float)(Math.Sin(direction + Math.PI / 2) * 5);
    }

    static Bullet()
    {
        model = GenerateShape(360, 2, 0);
    }

    public override void Tick(Size canvSize)
    {
        //If we are outside the bounds of the canvas, we are dead
        //Has the same game size as the asteroids.
        if(Position.X > canvSize.Width + 100 || Position.X < -100 || Position.Y > canvSize.Height + 100 || Position.Y < -100)
        {
            IsMarkedForDeath = true;
        }
        
        base.Tick(canvSize);
    }
}
