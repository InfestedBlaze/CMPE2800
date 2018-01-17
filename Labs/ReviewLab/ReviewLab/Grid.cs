/*
 * Nicholas Wasylyshyn
 * January 16, 2018
 * This program will create a visual display of a grid of blocks.
 * Left clicking will add a block that can fall.
 * Right clicking will destroy a block.
 * Shift-Right clicking will add a non-falling block
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using GDIDrawer;

namespace ReviewLab
{
    class Grid
    {
        private Block[,] grid;  //Container for our blocks
        //Used to avoid querying the grid for its lengths
        private int XLength;    //X length of grid.
        private int YLength;    //Y length of grid.

        public Grid(int X, int Y)
        {
            grid = new Block[X, Y];
            XLength = X;
            YLength = Y;
        }

        //Adds a new Free falling block to the point in the grid. Ignores occupancy
        private void AddFreeBlock(Point inPoint)
        {
            grid[inPoint.X, inPoint.Y] = new FreeBlock();
        }

        //Adds a new solid block to the point in the grid. Ignores occupancy
        private void AddSolidBlock(Point inPoint)
        {
            grid[inPoint.X, inPoint.Y] = new SolidBlock();
        }

        //Nulls whatever is at the point.
        private void KillBlock(int X, int Y)
        {
            grid[X, Y] = null;
        }

        //This will add a block to the grid at the designated point
        public void AddBlock(Point inPoint, bool Solid)
        {
            //Make sure that the point is empty, can only place in an empty cell or non reserved cell
            if (grid[inPoint.X, inPoint.Y] == null && !(grid[inPoint.X, inPoint.Y] is RetainerBlock))
            {
                //Check if shift was pressed. If true, Add a solid block. Else, falling block.
                if (Solid)
                {
                    AddSolidBlock(inPoint);
                }
                else
                {
                    AddFreeBlock(inPoint);
                }
            }
        }

        //This will remove a block at the specified location
        public void RemoveBlock(Point inPoint)
        {
            //Can't remove a null or reserved block
            if (grid[inPoint.X, inPoint.Y] != null && !(grid[inPoint.X, inPoint.Y] is RetainerBlock))
            {
                //Can't remove a block that is currently falling
                if(!(grid[inPoint.X, inPoint.Y] is FreeBlock) || (grid[inPoint.X, inPoint.Y] as FreeBlock).fall != Fall.Falling)
                {
                    //Flag for death
                    grid[inPoint.X, inPoint.Y].life = Life.Dying;
                }
                
            }
        }

        //Give the point of the block you want to move
        private void RetainBlockBelow(int X, int Y)
        {
            //Move down one, for the block to move into
            Y += 1;
            //Hold that spot for the falling block, not outside of our grid
            if(Y < YLength)
                grid[X, Y] = new RetainerBlock();
        }

        //Will do one operation of the grid.
        //Move a block done or continue to die
        public bool Tick()
        {
            //Tells us whether any action took place.
            bool Continue = false;

            for(int x = 0; x < XLength; x++)
            {
                for(int y = 0; y < YLength; y++)
                {
                    //Loop through every block, column by column

                    //Falling-------------------------------------------
                    if(grid[x,y] is FreeBlock)
                    {
                        //The block is ready to move to the next spot
                        if((grid[x, y] as FreeBlock).fall == Fall.Falling && grid[x, y].AnimationState == 9)
                        {
                            //Move the block down
                            grid[x, y + 1] = grid[x, y];
                            //Remove old location
                            KillBlock(x, y);
                            //The block is no longer falling
                            (grid[x, y+1] as FreeBlock).fall = Fall.Still;
                            //It resets its animations
                            grid[x, y+1].AnimationState = 0;
                            //We have done an action
                            Continue = true;
                        }
                        //The block is still falling
                        else if ((grid[x, y] as FreeBlock).fall == Fall.Falling && grid[x, y].AnimationState < 9)
                        {
                            grid[x, y].AnimationState++;
                            Continue = true;
                        }
                        //The block can fall
                        else if ((grid[x, y] as FreeBlock).fall == Fall.Still &&  //The block is currently not falling
                            y < YLength -1 &&                                     //It isn't on the bottom row of the grid
                            grid[x, y+1] == null &&                               //The block below it is empty
                            grid[x,y].life != Life.Dying)                         //The block isn't dying
                        {
                            //Retain the block for us to fall into
                            RetainBlockBelow(x, y);
                            //Say that we are falling
                            (grid[x, y] as FreeBlock).fall = Fall.Falling;
                            //We have done an action
                            Continue = true;                                      
                        }
                    }

                    //Death------------------------------------------------
                    if (grid[x, y] != null && grid[x, y].life == Life.Dying && grid[x, y].AnimationState == 9) //Animations are over, dead
                    {
                        KillBlock(x, y);
                        //We have done an action
                        Continue = true;
                    }
                    else if (grid[x,y] != null && grid[x, y].life == Life.Dying && grid[x, y].AnimationState < 9) //Still dying
                    {
                        //Increment animation counter
                        grid[x, y].AnimationState++;
                        //We have done an action
                        Continue = true;
                    }
                    
                    //Leftover Retainer block--------------------------------
                    if(y > 0 && (grid[x,y] is RetainerBlock)) //We must be below the top row and be a retainer
                    {
                        //If we have a retainer block, and no free block above
                        if(!(grid[x,y-1] is FreeBlock))
                        {
                            //Kill the retainer block
                            KillBlock(x, y);
                            //We have done an action
                            Continue = true;
                        }
                    }
                }
            }

            //True, something happened and we should run Tick() again.
            //False, nothing happened. No need to run again.
            return Continue;
        }

        //Take in a canvas, draw all of the blocks
        public void Render(CDrawer canvas, int Size)
        {
            canvas.Clear();

            for (int x = 0; x < XLength; x++)
            {
                for (int y = 0; y < YLength; y++)
                {
                    if (grid[x, y] != null && !(grid[x,y] is RetainerBlock))
                    {
                        //Centered point
                        Point point = new Point(x * Size + (Size/2), y * Size + (Size / 2));
                        //If free block, offset the point by Animation state
                        if (grid[x, y] is FreeBlock && grid[x, y].life != Life.Dying)
                            point.Y += (Size/10) * grid[x,y].AnimationState; //Move down 10% of the size
                        
                        //Get our side length, shrunk by death
                        int sideLength = Size;
                        if (grid[x, y].life == Life.Dying)
                        {
                            sideLength = (-(Size/10)) * grid[x,y].AnimationState + Size; //Shrink by 10% of size for every AnimationState
                        }

                        //Add every block that isn't null
                        canvas.AddCenteredRectangle(point.X, point.Y, sideLength, sideLength, grid[x, y].Colour, 1, Color.Black);
                    }
                }
            }

            canvas.Render();
        }

        //Used for console output. Test code
        public void Write()
        {
            string line;
            for (int y = 0; y < YLength; y++)
            {
                line = "";
                for (int x = 0; x < XLength; x++)
                {
                    if (grid[x, y] == null)
                    {
                        line += "X ";
                    }
                    if ((grid[x, y] is RetainerBlock))
                    {
                        line += "R ";
                    }
                    if (grid[x,y] is SolidBlock)
                    {
                        line += "S ";
                    }
                    if (grid[x, y] is FreeBlock)
                    {
                        line += "F ";
                    }
                }
                Console.WriteLine(line);
            }
            Console.WriteLine();
        }
    }
}
