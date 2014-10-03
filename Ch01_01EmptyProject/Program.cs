using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ch01_01EmptyProject
{
    static class Program
    {
        private static System system;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                system = new System();
                system.Run();
            }
            finally
            {
                system.Dispose();
            }
        }
    }
}
