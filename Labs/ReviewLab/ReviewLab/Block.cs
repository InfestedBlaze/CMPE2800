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
        private Color colour;
        public Color Colour { get { return colour; } protected set { colour = value; } }
        public Life life = Life.Alive;
        public byte AnimationState = 0;
    }

    //A block that is able to fall to the next area
    class FreeBlock : Block
    {
        public Fall fall = Fall.Still;
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
