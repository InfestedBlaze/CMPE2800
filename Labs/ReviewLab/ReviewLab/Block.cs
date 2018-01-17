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
using System.Drawing;

namespace ReviewLab
{
    //Enums to determine state
    enum Life { Alive, Dying}  //Whether the block is dying or not
    enum Fall { Still, Falling} //Whether the block is falling (Downward displacement)

    //An item in the grid
    abstract class Block
    {
        private Color colour;           //Colour that the block will be drawn as
        public Color Colour { get { return colour; } protected set { colour = value; } }
        public Life life = Life.Alive;  //Flag whether or not the block should be removed
        public byte AnimationState = 0; //The state of our current animation. Number from 0-9
    }

    //A block that is able to fall to the next area
    class FreeBlock : Block
    {
        public Fall fall = Fall.Still;  //Flag whether or not the block should fall
        public FreeBlock ()
        {
            Colour = Color.Red;
        }
    }

    //A block that does not move around the grid
    class SolidBlock : Block
    {
        public SolidBlock ()
        {
            Colour = Color.Orange;
        }
    }

    //A nonmoving block that will save a space for a falling block
    //Size is irrelevant. Only made to occupy grid
    class RetainerBlock : Block
    {
        public RetainerBlock ()
        {
            Colour = Color.Transparent;
        }
    }
}
