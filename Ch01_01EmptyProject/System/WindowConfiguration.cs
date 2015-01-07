using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Windows;
using SharpDX.DXGI;
using SharpDX.Direct3D11;
using Device = SharpDX.Direct3D11.Device;
using SharpDX.Direct3D;
using System.Windows.Forms;

namespace Ch01_01EmptyProject
{
    public class SystemConfiguration
    {
        public SystemConfiguration()
        {
            DataFilePath = @"..\..\Data\";
            ModelFilePath = @"..\..\Models\";
        }
        
        IntPtr formWindowHandle;

        public IntPtr FormWindowHandle
        {
            get { return formWindowHandle; }
            set { formWindowHandle = value; }
        }

        int width;
    
        public int Width
        {
            get { return width; }
            set { width = value; }
        }
        int height;
    
        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        public static string DataFilePath { get; set; }

        public static string ModelFilePath { get; set; }
    }
}
