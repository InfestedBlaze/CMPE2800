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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GDIDrawer;

namespace ReviewLab
{
    public partial class Form1 : Form
    {

        //Window and block constansts
        private const int WindowXSize = 500;
        private const int WindowYSize = 500;
        private const int BlockSize   = 50; //Assumed to be greater than 10

        //Canvas to display our grid
        CDrawer canvas = new CDrawer(WindowXSize, WindowYSize, false, true);
        //Grid to add our blocks to
        Grid grid = new Grid(WindowXSize/BlockSize, WindowYSize/BlockSize);
        
        public Form1()
        {
            InitializeComponent();

            canvas.MouseLeftClickScaled += Canvas_MouseLeftClickScaled;
            canvas.MouseRightClickScaled += Canvas_MouseRightClickScaled;
        }

        private void Canvas_MouseLeftClickScaled(Point pos, CDrawer dr)
        {
            //Scale point to our grid
            pos.X /= BlockSize;
            pos.Y /= BlockSize;

            //Add a falling block
            grid.AddBlock(pos, false);
            //Something changed, enable the timer
            timer.Enabled = true;
        }

        private void Canvas_MouseRightClickScaled(Point pos, CDrawer dr)
        {
            //Scale point to our grid
            pos.X /= BlockSize;
            pos.Y /= BlockSize;

            if (Control.ModifierKeys == Keys.Shift)
            {
                //Add a solid block
                grid.AddBlock(pos, true);
            }
            else
            {
                grid.RemoveBlock(pos);
            }
            //Something changed, enable the timer
            timer.Enabled = true;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //Do a tick of grid action. It will determine whether we continue
            timer.Enabled = grid.Tick();
            //Show the change
            grid.Render(canvas, BlockSize);
            //On the console as well
            //grid.Write();
        }
    }
}
