using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Windows;
using SharpDX.RawInput;
using SharpDX.DirectInput;
using System.Diagnostics;

namespace Ch01_01EmptyProject.Inputs
{
    //mouse applies basically a state pattern..
    //it have not moving state and when user move with mouse
    //its state its changed to moving state in which is object rotating
    
    public class Input : IDisposable
    {
        private WindowConfiguration wc;
        private int screenWidth;
        private int screenHeight;
        private int mouseX;
        private int mouseY;
        private Mouse mouse;
        private DirectInput directInput;
        private MouseState mouseState;
        private SharpDX.ResultDescriptor getCurrentStateErrorResult;
        private int mouseXlastFrame;
        private int mouseYlastFrame;

        public Input(WindowConfiguration wc)
        {
            this.wc = wc;
        }

        public void Initialize()
        {
            try
            {
                screenWidth = wc.Width;
                screenHeight = wc.Height;

                mouseX = 0;
                mouseY = 0;

                directInput = new DirectInput();

                mouse = new Mouse(directInput);
                mouse.Properties.AxisMode = DeviceAxisMode.Relative;
          
                // Set the cooperative level of the mouse to share with other programs.
                //it seems buggy though - RAC
                //  mouse.SetCooperativeLevel(wc.FormWindowHandle, CooperativeLevel.Foreground | CooperativeLevel.NonExclusive);
                //mouse.SetCooperativeLevel(wc.FormWindowHandle, CooperativeLevel.Exclusive);

                mouse.Acquire();
            }
            catch (Exception ex)
            {
                throw new Exception("Input init: " + ex);
            }

            try
            {
                mouse.Acquire();
            }
            catch (Exception ex)
            {
                throw new Exception("Mouse acquaring " + ex);
            }

        }

        private void ReadMouse()
        {
            mouseState = new MouseState();
            
            try
            {
                mouse.GetCurrentState(ref mouseState);

            }
            //using sharpdx exception
            catch (SharpDX.SharpDXException ex)
            {
                getCurrentStateErrorResult = ex.Descriptor;
            }

            try
            {
                // If the mouse lost focus or was not acquired then try to get control back.
                if (getCurrentStateErrorResult == ResultCode.InputLost || getCurrentStateErrorResult == ResultCode.NotAcquired)
                    mouse.Acquire();
            }
            catch (SharpDX.SharpDXException ex)
            {
                //if (ex.Descriptor == ResultCode.ReadOnly)
                //    return true;
                //resultCode = ex.Descriptor;
            }
        }

        private void ProcessInput()
        {
            if (mouseState != null)
            {
                mouseX += mouseState.X;
                mouseY += mouseState.Y;
            }

            if (mouseX < 0) mouseX = 0;
            if (mouseY < 0) mouseY = 0;

            if (mouseX > screenWidth) mouseX = screenWidth;
            if (mouseY > screenHeight) mouseY = screenHeight;
        }

        //instead of out, return Vector2?
        public void GetMouseLocation(out int outMouseX, out int outMmouseY)
        {
            outMouseX = mouseX;
            outMmouseY = mouseY;
        }

        //its extremly ugly, but theres build in way how to do this, check for tutorial
        public bool IsMouseMoving()
        {
            if (mouseXlastFrame != mouseX || mouseYlastFrame != mouseY)
            {
#if DEBUG
                //Debug.WriteLine("Graphic.Frame :: mouse coord x = " + mouseX + "and y = " + mouseY);
#endif
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Frame()
        {
            ReadMouse();
            ProcessInput();
        }

        public void Dispose()
        {
            mouse.Unacquire();
            mouse.Dispose();

            directInput.Dispose();
        }
    }
}
