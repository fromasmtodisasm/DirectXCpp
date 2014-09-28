using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX;
using SharpDX.Windows;
using SharpDX.DXGI;
using SharpDX.Direct3D11;

//Resolve name conflicts
using Device = SharpDX.Direct3D11.Device;
using SharpDX.Direct3D;

namespace Ch01_01EmptyProject
{
    public class D3DClass
    {
        Device device;
        SwapChain swapChain;
        private Texture2D backBuffer;
        private RenderTargetView renderTargetView;

        private TimerTick tt;

        public void InitializeWithForm(Form1 form)
        {

            var deviceFlags = DeviceCreationFlags.None;

            bool enforceSoftwareRendering = false;
            DriverType driverType = GetDriverTypeForRenderingWhichSupportsDx11(enforceSoftwareRendering);

#if DEBUG
            deviceFlags = DeviceCreationFlags.Debug;
#endif
            try
            {
                device = new Device(driverType, deviceFlags);
            }
            catch (Exception)
            {
                //MessageBox(0, L"Direct3D Feature Level 11 unsupported.", 0, 0);
                throw;
            }
            //CHECK AntiAliasing quality suport code coud be here


            ModeDescription modeDescription = CreateModeDescription(form);
            SwapChainDescription swapChainDescription = SwapChainDescription(form, ref modeDescription);

            //CreateSwapChain(swapChainDescription);

            using (var dxgi = device.QueryInterface<SharpDX.DXGI.Device>())
            using (var adapter = dxgi.Adapter)
            using (var factory = adapter.GetParent<Factory>())
            {
                swapChain = new SwapChain(factory, device, swapChainDescription);
            }

            //CreateResourceViewFromBackBuffer();
            //RenderToBackBuffer();

            backBuffer = Texture2D.FromSwapChain<Texture2D>(swapChain, 0);
            // Create an RTV for the rendertarget.
            renderTargetView = (new RenderTargetView(device, backBuffer));

            var depthBuffer = new Texture2DDescription();
            depthBuffer.Format = Format.D24_UNorm_S8_UInt;
            // number of textures in array
            depthBuffer.ArraySize = 1;
            depthBuffer.MipLevels = 1;
            depthBuffer.Width = form.ClientSize.Width;
            depthBuffer.Height = form.ClientSize.Height;

            // no MSAA
            depthBuffer.SampleDescription.Count = 1;
            depthBuffer.SampleDescription.Quality = 0;

            depthBuffer.Usage = ResourceUsage.Default;
            depthBuffer.BindFlags = BindFlags.DepthStencil;
            depthBuffer.CpuAccessFlags = 0;
            depthBuffer.OptionFlags = 0;

            var context = device.ImmediateContext;

            var depthStencilBuffer = new Texture2D(device, depthBuffer);
            DepthStencilView depthStencilView = new DepthStencilView(device, depthStencilBuffer);

            context.OutputMerger.SetTargets(depthStencilView, renderTargetView);

            Viewport vp = new Viewport();
            vp.X = 0;
            vp.Y = 0;
            vp.Width = form.ClientSize.Width;
            vp.Height = form.ClientSize.Height;
            vp.MinDepth = 0;
            vp.MaxDepth = 1;


            context.Rasterizer.SetViewport(vp);
        }

        private static DriverType GetDriverTypeForRenderingWhichSupportsDx11(bool enforceSoftwareRendering)
        {
            FeatureLevel highestDirectXVersionSupported = Device.GetSupportedFeatureLevel();
            if (highestDirectXVersionSupported < FeatureLevel.Level_11_0 && enforceSoftwareRendering)
            {
                return DriverType.Software;
            }
            else
            {
                return DriverType.Hardware;
            }
        }

        public void Run()
        {
            tt = new TimerTick();
            tt.Reset();

            wh`le (true) //msq.message != WM_QUIT
            {
                tt.Tick();
                if (!appPaused)
                {
                    CalculateFrameStats();
                    UpdateScene(tt.ElapsedTime());
                    DrawScene();
                }
                else
	{
                //Sleep(100)        
	}
            }
            // return msg.wParam
        }

        public void StartRender(Form1 form)
        {
            RenderLoop.Run(form, () =>
            {
                //BindBuffersToOutputStageOfPipeline();
                
                  CalculateFrameStats();
                  UpdateScene(tt.);
                    DrawScene();
                
                swapChain.Present(0, PresentFlags.None);

            });
        }

        private void BindBuffersToOutputStageOfPipeline()
        {
            device.ImmediateContext.ClearRenderTargetView(renderTargetView, Color.Blue);
        }

        public void CleanD3D()
        {
            device.Dispose();
            swapChain.Dispose();
            renderTargetView.Dispose();
            backBuffer.Dispose();
        }

        private void RenderToBackBuffer()
        {
            renderTargetView = new RenderTargetView(device, backBuffer);
        }

        private void CreateResourceViewFromBackBuffer()
        {
            backBuffer = Texture2D.FromSwapChain<Texture2D>(swapChain, 0);
        }

        private static ModeDescription CreateModeDescription(Form1 form)
        {
            ModeDescription modeDescription = new ModeDescription();

            modeDescription.Width = form.ClientSize.Width;
            modeDescription.Height = form.ClientSize.Height;
            modeDescription.RefreshRate = new Rational(60, 1);
            modeDescription.Format = Format.R8G8B8A8_UNorm;
            return modeDescription;
        }

        private static SwapChainDescription SwapChainDescription(Form1 form, ref ModeDescription modeDescription)
        {
            // SwapChain description: set up our double buffer
            var swapChainSetup = new SwapChainDescription();
            // We need one spare buffer here; if we needed more,
            // we could set a larger value
            swapChainSetup.BufferCount = 1;
            // Various properties of how the OS/Graphics card
            // will handle the window
            swapChainSetup.ModeDescription = modeDescription;
            swapChainSetup.IsWindowed = true;
            swapChainSetup.OutputHandle = form.Handle;
            swapChainSetup.SampleDescription = new SampleDescription(1, 0);
            swapChainSetup.SwapEffect = SwapEffect.Discard;
            swapChainSetup.Usage = Usage.RenderTargetOutput;
            return swapChainSetup;
        }

        private void CreateSwapChain(SwapChainDescription swapChainSetup)
        {
            try
            {
                Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.None, swapChainSetup, out device, out swapChain);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

