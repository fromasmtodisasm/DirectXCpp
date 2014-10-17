using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Windows;
using SharpDX.Direct3D9;
using SharpDX.D3DCompiler;
using System.Runtime.InteropServices;

//Resolve name conflicts
using Device = SharpDX.Direct3D9.Device;
using SharpDX.Direct3D;
using System.Windows.Forms;

namespace Ch01_01EmptyProject
{
    public struct CUSTOMVERTEX
    {
        float X, Y, Z, RHW; Color COLOR;

        public CUSTOMVERTEX(float X, float Y, float Z, float RHW, Color COLOR)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;

            this.RHW = RHW;
            this.COLOR = COLOR;
        }
    };

    public class D3D9 
    {
        //Direct3D and Device should be probably only two things which should be in constructor
        private Direct3D d3d;// the pointer to our Direct3D device interface
        private Device d3ddev; //device context //d3ddev / the pointer to the device class
        private VertexBuffer v_buffer;    // the pointer to the vertex buffer

        //public Device Device
        //{
        //    get { return device; }
        //    set { device = value; }
        //}

        //public Direct3D DeviceContext
        //{
        //    get { return d3d; }
        //    set { d3d = value; }
        //}


        private void InitGraphic()
        {
            // create the vertices using the CUSTOMVERTEX struct
            var v1 = new CUSTOMVERTEX(400.0f, 62.5f, 0.5f, 1.0f, new Color(0, 0, 255));
            var v2 = new CUSTOMVERTEX(650.0f, 500.0f, 0.5f, 1.0f, new Color(0, 255, 0));
            var v3 = new CUSTOMVERTEX(150.0f, 500.0f, 0.5f, 1.0f, new Color(255, 0, 0));

            CUSTOMVERTEX[] vertices = new CUSTOMVERTEX[] { v1, v2, v3 };

            v_buffer = new VertexBuffer(d3ddev, 3 * Utilities.SizeOf<CUSTOMVERTEX>(), Usage.None, VertexFormat.Diffuse, Pool.Managed);

            DataStream v = v_buffer.Lock(0, 0, LockFlags.None);

            Utilities.CopyMemory(v.PositionPointer, v_buffer.NativePointer, vertices.Length);

            v_buffer.Unlock();
        }

        public D3D9(WindowConfiguration windowConfig)
        {
            try
            {
                d3d = new Direct3D();

                PresentParameters pp = new PresentParameters();
                pp.SwapEffect = SwapEffect.Discard;
                pp.DeviceWindowHandle = windowConfig.FormWindowHandle;
                pp.BackBufferFormat = Format.X8B8G8R8;
                pp.BackBufferWidth = windowConfig.Width;
                pp.BackBufferHeight = windowConfig.Height;

                PresentParameters[] presentParameters = new PresentParameters[] { pp };

                d3ddev = new Device(d3d, 0, DeviceType.Hardware, windowConfig.FormWindowHandle, CreateFlags.None, presentParameters);

                InitGraphic();
            }
            catch (Exception ex)
            {
                throw new Exception("D3D11 could not initialize: " + ex);
            }
        }

        //RenderScene
        public void DrawScene()
        {
            try
            {
                d3ddev.Clear(ClearFlags.Target, Color.Blue, 1.0f, 0);
                d3ddev.BeginScene();
                //d3ddev.SetVertexShaderConstant()
                d3ddev.SetStreamSource(0, v_buffer, 0, Utilities.SizeOf<CUSTOMVERTEX>());
                d3ddev.DrawPrimitives(PrimitiveType.TriangleList, 0, 1);
                d3ddev.EndScene();
            }
            catch (Exception ex)
            {
                throw new Exception("D3D11 scene failed to draw: " + ex);
            }
        }

        //re/check rastertek example cause of naming
        public void PresentRenderedScene()
        {
            try
            {
                d3ddev.Present();
            }
            catch (SharpDX.SharpDXException ex)
            {
                throw new Exception("D3D11 Could not present scene: " + ex.Message);
            }
        }

        public void Dispose()
        {
            v_buffer.Dispose();
            d3d.Dispose();
            d3ddev.Dispose();
        }
    }
}

