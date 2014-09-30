using SharpDX.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ch01_01EmptyProject.System
{
    public class System : IDisposable
    {
        private static string PROGRAM_TITLE = "D3DRendering - Empty Project";
        private static int FORM_WIDTH = 800;
        private static int FORM_HEIGHT = 600;
        private Form1 form;
        private Graphic.Graphic graphic;

        public System()
        {
            // Create the window to render to
            form = new Form1();

            form.Text = PROGRAM_TITLE;
            form.Width = FORM_WIDTH;
            form.Height = FORM_HEIGHT;

            WindowConfiguration wc = new WindowConfiguration();
            wc.Width = FORM_WIDTH;
            wc.Height = FORM_HEIGHT;
            wc.FormWindowHandle = form.Handle;

            graphic = new Graphic.Graphic(wc);
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

