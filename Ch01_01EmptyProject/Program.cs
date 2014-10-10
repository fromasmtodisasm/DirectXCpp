using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ch01_01EmptyProject
{
    static class Program
    {
        private static GreenHornEngine system;
        private static bool USER_ERROR = true;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (USER_ERROR == true)
            {
                try
                {
                    system = new GreenHornEngine();
                    system.Run();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    system.Dispose();
                }
            }
            else
            {
                try
                {
                    system = new GreenHornEngine();
                    system.Run();
                }
                finally
                {
                    system.Dispose();
                }
            }
        }

        private static void SystemRun()
        {
            //D3DX11Test test = new D3DX11Test();
            //test.Draw();

         
        }
    }
}
