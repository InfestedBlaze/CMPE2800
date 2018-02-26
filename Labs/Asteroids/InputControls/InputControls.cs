using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InputControls
{
    public class InputControls
    {
        private bool controllerAttached = false;

        static private Dictionary<Keys, bool> keys;
        static public Dictionary<Keys, bool> Inputs { get { return keys; } }
        
        static InputControls()
        {
            //Fills the dictionary with all of the key values
            foreach(Keys allKeys in Enum.GetValues(typeof(Keys)))
            {
                keys[allKeys] = false;
            }
        }

        /// <summary>
        /// This will change the key inputted to true
        /// </summary>
        /// <param name="inKey">The key that is pressed down</param>
        public void KeyDown(Keys inKey)
        {
            if(!controllerAttached) //No keyboard if the XBOX controller is plugged in
                keys[inKey] = true;
        }

        /// <summary>
        /// This will change the key inputted to false
        /// </summary>
        /// <param name="inKey">The key that is released</param>
        public void KeyUp(Keys inKey)
        {
            if(!controllerAttached) //No keyboard if the XBOX controller is plugged in
                keys[inKey] = false;
        }
    }
}
