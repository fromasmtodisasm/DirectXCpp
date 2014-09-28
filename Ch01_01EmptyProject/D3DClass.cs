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
using System.Windows.Forms;

namespace Ch01_01EmptyProject
{
   public class WindowConfiguration
    {
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
    }

    public class D3DClass : ID3DApp
    {
        #region BaseMethodsIncludedInExamples
        Device device;
        SwapChain swapChain;
        private Texture2D backBuffer;
        private RenderTargetView renderTargetView;

        private bool IsStopped = false;

        WindowConfiguration windowConfig;
        private RenderForm MainForm { get; set; }

        private TimerTick tt;

        public void Initialize(IntPtr formWindowHandle, WindowConfiguration windowConfig)
        {
            this.windowConfig = windowConfig;

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


            ModeDescription modeDescription = CreateModeDescription();
            SwapChainDescription swapChainDescription = SwapChainDescription(formWindowHandle, ref modeDescription);

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

            CreateRenderTargetViewForTarget();

            var depthBuffer = new Texture2DDescription();
            depthBuffer.Format = Format.D24_UNorm_S8_UInt;
            // number of textures in array
            depthBuffer.ArraySize = 1;
            depthBuffer.MipLevels = 1;
            depthBuffer.Width = windowConfig.Width;
            depthBuffer.Height = windowConfig.Height;

            // no MSAA
            depthBuffer.SampleDescription.Count = 1;
            depthBuffer.SampleDescription.Quality = 0;

            depthBuffer.Usage = ResourceUsage.Default;
            depthBuffer.BindFlags = BindFlags.DepthStencil;
            depthBuffer.CpuAccessFlags = 0;
            depthBuffer.OptionFlags = 0;

            DeviceContext context = device.ImmediateContext;

            var depthStencilBuffer = new Texture2D(device, depthBuffer);
            DepthStencilView depthStencilView = new DepthStencilView(device, depthStencilBuffer);

            BindBuffersToOutputStageOfPipeline(context, depthStencilView);

            Viewport vp = new Viewport();
            vp.X = 0;
            vp.Y = 0;
            vp.Width = windowConfig.Width;
            vp.Height = windowConfig.Height;
            vp.MinDepth = 0;
            vp.MaxDepth = 1;


            context.Rasterizer.SetViewport(vp);
        }

        public void StartRender(Form1 form)
        {
            RenderLoop.Run(form, () =>
            {
                //BindBuffersToOutputStageOfPipeline();

                //CalculateFrameStats();
                //UpdateScene(tt.);
                //  DrawScene();

                swapChain.Present(0, PresentFlags.None);

            });
        }
        public void CleanD3D()
        {
            device.Dispose();
            swapChain.Dispose();
            renderTargetView.Dispose();
            backBuffer.Dispose();
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
        #endregion

        #region HelperMethodsNotIncludedInExampleResources
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

        private void CreateRenderTargetViewForTarget()
        {
            renderTargetView = new RenderTargetView(device, backBuffer);
        }

        private void BindBuffersToOutputStageOfPipeline(DeviceContext context, DepthStencilView depthStencilView)
        {
            context.OutputMerger.SetTargets(depthStencilView, renderTargetView);
        }

        private void CreateResourceViewFromBackBuffer()
        {
            backBuffer = Texture2D.FromSwapChain<Texture2D>(swapChain, 0);
        }

        private ModeDescription CreateModeDescription()
        {
            ModeDescription modeDescription = new ModeDescription();

            modeDescription.Width = windowConfig.Width;
            modeDescription.Height = windowConfig.Height;
            modeDescription.RefreshRate = new Rational(60, 1);
            modeDescription.Format = Format.R8G8B8A8_UNorm;
            return modeDescription;
        }

        private static SwapChainDescription SwapChainDescription(IntPtr formWindowHandle, ref ModeDescription modeDescription)
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
            swapChainSetup.OutputHandle = formWindowHandle;
            swapChainSetup.SampleDescription = new SampleDescription(1, 0);
            swapChainSetup.SwapEffect = SwapEffect.Discard;
            swapChainSetup.Usage = Usage.RenderTargetOutput;
            return swapChainSetup;
        }
        
        #endregion
    
        #region Id3DApp Interface methods

        public GameTimer Timer
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        Device ID3DApp.device
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public DeviceContext DeviceContext
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Run()
        {
            Application.Run(MainForm);
        }

        public bool Init()
        {
            throw new NotImplementedException();
        }

        public void OnResize()
        {
            throw new NotImplementedException();
        }

        public void UpdateScene(float deltaT)
        {
            throw new NotImplementedException();
        }

        public void DrawScene()
        {
            throw new NotImplementedException();
        }

        public void OnMouseDown()
        {
            throw new NotImplementedException();
        }

        public void OnMouseUp()
        {
            throw new NotImplementedException();
        }

        public void OnMouseMove()
        {
            throw new NotImplementedException();
        }

        public bool InitMainWindow()
        {
            throw new NotImplementedException();
        }

        public bool InitDirect3D()
        {
            throw new NotImplementedException();
        }

        public void CalculateFrameStats()
        {
            throw new NotImplementedException();
        }

        public object MessageProcessing()
        {
            throw new NotImplementedException();
        }

        public bool IsApplicationPaused
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int IsApplicationMinimized
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool IsApplicationMaximized
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int IsApplicationResized
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        } 
        #endregion
    }
}

