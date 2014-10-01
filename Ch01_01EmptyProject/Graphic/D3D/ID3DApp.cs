using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX;
using SharpDX.Windows;
using SharpDX.DXGI;
using Device = SharpDX.Direct3D11.Device;
using SharpDX.Direct3D11;

namespace Ch01_01EmptyProject
{
    //trying to make interface acc. a Luna book
    //which is several separate classes in rastertek
   public interface ID3DApp
    {
        System.GameTimer Timer { get; set; }

        Device device { get; set; }
        DeviceContext DeviceContext { get; set; }

        void Run();

        bool Init();
        void OnResize();
        void UpdateScene(float deltaT);
        void DrawScene();

        void OnMouseDown();
        void OnMouseUp();
        void OnMouseMove();

        bool InitMainWindow();
        bool InitDirect3D();

        void CalculateFrameStats();

        Object MessageProcessing();

        bool IsApplicationPaused { get; set; }
        int IsApplicationMinimized { get; set; }
        bool IsApplicationMaximized { get; set; }
        int IsApplicationResized { get; set; }
    }
}
