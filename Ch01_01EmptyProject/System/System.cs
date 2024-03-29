﻿using SharpDX.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ch01_01EmptyProject.Graphic;
using Ch01_01EmptyProject.Inputs;
using Ch01_01EmptyProject.System;
using System.Diagnostics;

namespace Ch01_01EmptyProject
{
    public class XCube : IDisposable
    {
        private static string PROGRAM_TITLE = "D3DRendering - Cube drawing";

        private static int FORM_WIDTH = 1024;
        private static int FORM_HEIGHT = 768;

        private static int FORM_WIDTH_SMALL = 800;
        private static int FORM_HEIGHT_SMALL = 600;

        private Form1 form;
        private D3DGraphic graphic;
        private Input input;
        private SystemConfiguration wc;
        private FPS fps;
        private SystemTime timer;
        private Position position;

        public XCube()
        {
            // Create the window to render to
            form = new Form1();

            form.Text = PROGRAM_TITLE;
            form.Width = FORM_WIDTH_SMALL;
            form.Height = FORM_HEIGHT_SMALL;
            form.FormBorderStyle = FormBorderStyle.FixedToolWindow;

            wc = new SystemConfiguration();
            wc.Width = FORM_WIDTH;
            wc.Height = FORM_HEIGHT;
            wc.FormWindowHandle = form.Handle;
        }

        public void Initialize()
        {
            input = new Input(wc);
            input.Initialize();
            graphic = new D3DGraphic(wc);
            fps = new FPS();
            fps.Value = 60;
            timer = new SystemTime();
            position = new Position();
        }

        public void Run()
        {
            RenderLoop.Run(form, () =>
                {
                    Frame();
                });
        }

        int mouseX, mouseY;
        int recentMouseX = 0;
        int recentMouseY = 0;

        private void Frame()
        {
            //tricky part is, some classes have render and other frame
            // pick one and make Composite out of it


            fps.Frame();
            timer.Frame();

            //input.Frame();

            //recentMouseX = mouseX;
            //recentMouseY = mouseY;

            //input.GetMouseLocation(out mouseX, out mouseY);

            //if (recentMouseY != mouseY || recentMouseX != mouseX)
            //{
            //   float dx = recentMouseX - mouseX;
            //   float dy = recentMouseY - mouseY;

            //   D3DGraphic.Rotation = dy;
            //   //Debug.Write("Graphic.Frame :: mouse coord x = " + dx.ToString() + "and y = " + dy.ToString());
            //}

            graphic.FPS = fps.Value;

            //set the frame time for calculating the updated position
            graphic.FrameTime = timer.FrameTime;

            //for simplicty, if mouse is moving, rotate
            //if (input.IsMouseMoving()) { }
            //position.TurnLeft();

            position.FrameTime = timer.FrameTime;

            graphic.Position = position;
            graphic.Frame();

            //graphic.Render();
        }

        public void Dispose()
        {
            input.Dispose();
            graphic.Dispose();
        }
    }
}

