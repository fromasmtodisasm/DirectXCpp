using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ch01_01EmptyProject
{
    static class Program
    {
        private static string PROGRAM_TITLE = "D3DRendering - Empty Project";
        private static int FORM_WIDTH = 800;
        private static int FORM_HEIGHT = 600;


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Create the window to render to
            Form1 form = new Form1();
            form.Text = PROGRAM_TITLE;
            form.Width = FORM_WIDTH;
            form.Height = FORM_HEIGHT;

          WindowConfiguration wc = new WindowConfiguration();
            wc.Width = FORM_WIDTH;
            wc.Height = FORM_HEIGHT;

            var d3d = new D3DClass();
            d3d.Initialize(form.Handle, wc);
            d3d.StartRender(form);
            d3d.CleanD3D();

        }
    }
}
