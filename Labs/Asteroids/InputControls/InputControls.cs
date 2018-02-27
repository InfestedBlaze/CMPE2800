/*
 * Author: Nic Wasylyshyn
 * Date: February 26, 2018
 * Description: This class is an abstraction layer for
 * keyboard and controller inputs. Keyboard events need
 * to be hooked up to KeyDown and KeyUp, and XBOX controls
 * are automatic. The user will check the Inputs property
 * to determine the current state of the desired keys.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class InputControls
{
    //Determines if the XBOX controller plugged in
    private bool controllerAttached = false;

    //Our keys to determine what is pressed
    private Dictionary<System.Windows.Forms.Keys, bool> keys;
    public Dictionary<System.Windows.Forms.Keys, bool> Inputs { get { return keys; } }
        
    //Constructors
    public InputControls()
    {
        Thread xboxPolling = new Thread(XboxPollingThread);
        xboxPolling.IsBackground = true;
        xboxPolling.Start();

        keys = new Dictionary<System.Windows.Forms.Keys, bool>();
        //Fills the dictionary with all of the key values
        foreach(System.Windows.Forms.Keys allKeys in Enum.GetValues(typeof(System.Windows.Forms.Keys)))
        {
            keys[allKeys] = false;
        }
    }

    //Functions that change our input states
    /// <summary>
    /// This will change the key inputted to true, doesn't work if XBOX is plugged in
    /// </summary>
    /// <param name="inKey">The key that is pressed down</param>
    public void KeyDown(System.Windows.Forms.Keys inKey)
    {
        lock (keys)
        {
            if (!controllerAttached) //No keyboard if the XBOX controller is plugged in
                keys[inKey] = true;
        }
    }

    /// <summary>
    /// This will change the key inputted to false, doesn't work if XBOX is plugged in
    /// </summary>
    /// <param name="inKey">The key that is released</param>
    public void KeyUp(System.Windows.Forms.Keys inKey)
    {
        lock (keys)
        {
            if (!controllerAttached) //No keyboard if the XBOX controller is plugged in
                keys[inKey] = false;
        }
    }

    private void XboxPollingThread()
    {
        while (true)
        {
            //Get the state of player one
            GamePadState gps = GamePad.GetState(PlayerIndex.One);

            //Check if the controller is attached
            controllerAttached = gps.IsConnected;
            //If the controller is attached then we only look at it for inputs
            if (controllerAttached)
            {
                //Lock out our key inputs
                lock (keys)
                {
                    //Left thumbstick is mapped to WASD
                    keys[System.Windows.Forms.Keys.W] = gps.ThumbSticks.Left.Y > 0;
                    keys[System.Windows.Forms.Keys.S] = gps.ThumbSticks.Left.Y < 0;
                    keys[System.Windows.Forms.Keys.A] = gps.ThumbSticks.Left.X < 0;
                    keys[System.Windows.Forms.Keys.D] = gps.ThumbSticks.Left.X > 0;

                    //The A button is mapped to Spacebar
                    keys[System.Windows.Forms.Keys.Space] = gps.IsButtonDown(Buttons.A);

                    //The start button is mapped to P
                    keys[System.Windows.Forms.Keys.P] = gps.IsButtonDown(Buttons.Start);
                }
            }

            //Wait for more inputs
            Thread.Sleep(25);
        }
    }
}

