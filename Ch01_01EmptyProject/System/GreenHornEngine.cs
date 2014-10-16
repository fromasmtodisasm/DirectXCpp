using SharpDX.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ch01_01EmptyProject.Graphic;

namespace Ch01_01EmptyProject
{
    public class GreenHornEngine : IDisposable
    {
        private static string PROGRAM_TITLE = "D3DRendering - Cube drawing";
        private static int FORM_WIDTH = 1024;
        private static int FORM_HEIGHT = 768;
        private Form1 form;
        private D3DGraphic graphic;

        public GreenHornEngine()
        {
            // Create the window to render to
            form = new Form1();

            form.Text = PROGRAM_TITLE;
            form.Width = FORM_WIDTH;
            form.Height = FORM_HEIGHT;
            form.FormBorderStyle = FormBorderStyle.FixedToolWindow;

            WindowConfiguration wc = new WindowConfiguration();
            wc.Width = FORM_WIDTH;
            wc.Height = FORM_HEIGHT;
            wc.FormWindowHandle = form.Handle;

            graphic = new D3DGraphic(wc);
        }

        public void Run()
        {
            RenderLoop.Run(form, () =>
                {
                    Frame();
                });
        }

        private void Frame()
        {
            graphic.Render();
        }

        public void Dispose()
        {
            graphic.Dispose();
        }
    }
}

