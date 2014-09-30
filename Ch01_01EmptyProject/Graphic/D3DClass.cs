using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Windows;
using SharpDX.DXGI;
using SharpDX.Direct3D11;
using SharpDX.D3DCompiler;

//Resolve name conflicts
using Device = SharpDX.Direct3D11.Device;
using Ch01_01EmptyProject.System;

using SharpDX.Direct3D;
using System.Windows.Forms;

namespace Ch01_01EmptyProject
{
    public class D3D : IDisposable
    {
        #region BaseMethodsIncludedInExamples
        Device device;
        SwapChain swapChain;
        private Texture2D backBuffer;
        private RenderTargetView renderTargetView;
        private DepthStencilView depthStencilView;

        DeviceContext context;
        WindowConfiguration windowConfig;
        private Texture2DDescription depthBuffer;
        private Texture2D depthStencilBuffer;
        private RenderForm MainForm { get; set; }

        public void Initialize(WindowConfiguration windowConfig)
        {
            this.windowConfig = windowConfig;

            var deviceFlags = DeviceCreationFlags.None;

            bool enforceSoftwareRendering = false;
            DriverType driverType = GetDriverTypeForRenderingWhichSupportsDx11(enforceSoftwareRendering);

#if DEBUG
            deviceFlags = DeviceCreationFlags.Debug;
#endif
            CreateDevice(deviceFlags, driverType);

            //CHECK AntiAliasing quality suport code coud be here

            ModeDescription modeDescription = CreateModeDescription();
            SwapChainDescription swapChainDescription = SwapChainDescription(windowConfig.FormWindowHandle, ref modeDescription);

            swapChainDescription = CreateSwapChain(swapChainDescription);

            backBuffer = Texture2D.FromSwapChain<Texture2D>(swapChain, 0);

            CreateRenderTargetViewForTarget();

            depthBuffer = CreateDepthBuffer(windowConfig);

            context = device.ImmediateContext;

            depthStencilBuffer = new Texture2D(device, depthBuffer);
            depthStencilView = new DepthStencilView(device, depthStencilBuffer);

            BindBuffersToOutputStageOfPipeline(context, depthStencilView);

            CreateViewPort(windowConfig);
        }

        private SharpDX.DXGI.SwapChainDescription CreateSwapChain(SwapChainDescription swapChainDescription)
        {
            try
            {
                using (var dxgi = device.QueryInterface<SharpDX.DXGI.Device>())
                using (var adapter = dxgi.Adapter)
                using (var factory = adapter.GetParent<Factory>())
                {
                    swapChain = new SwapChain(factory, device, swapChainDescription);
                }
                return swapChainDescription;
            }
            catch (Exception e)
            {

                throw new Exception("", e);
            }
        }

        private void CreateDevice(DeviceCreationFlags deviceFlags, DriverType driverType)
        {
            try
            {
                device = new Device(driverType, deviceFlags);
            }
            catch (Exception e)
            {
                throw new NotSupportedException("", e);
            }
        }

        private void CreateViewPort(WindowConfiguration windowConfig)
        {
            Viewport vp = new Viewport();
            vp.X = 0;
            vp.Y = 0;
            vp.Width = windowConfig.Width;
            vp.Height = windowConfig.Height;
            vp.MinDepth = 0;
            vp.MaxDepth = 1;

            context.Rasterizer.SetViewport(vp);
        }

        private static Texture2DDescription CreateDepthBuffer(WindowConfiguration windowConfig)
        {
            var depthBuffer = new Texture2DDescription();
            try
            {
                depthBuffer.Format = Format.D24_UNorm_S8_UInt;
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
                return depthBuffer;
            }
            catch (Exception e)
            {
                throw new Exception("", e);
            }
        }

        public void Dispose()
        {
            depthStencilView.Dispose();
            depthStencilBuffer.Dispose();

            device.Dispose();
            swapChain.Dispose();
            renderTargetView.Dispose();
            backBuffer.Dispose();
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
            try
            {
                renderTargetView = new RenderTargetView(device, backBuffer);
            }
            catch (Exception e)
            {

                throw new Exception("", e);
            }
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
            try
            {
                ModeDescription modeDescription = new ModeDescription();

                modeDescription.Width = windowConfig.Width;
                modeDescription.Height = windowConfig.Height;
                modeDescription.RefreshRate = new Rational(60, 1);
                modeDescription.Format = Format.R8G8B8A8_UNorm;
                return modeDescription;
            }
            catch (Exception e)
            {
                
                throw new Exception("", e);
            }
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

        public void UpdateScene(float deltaT)
        {
            //Called every frame - should be used to update app over time, eq. processing animations...
            throw new NotImplementedException();
        }

        public void BeginScene()
        {
            //I shloud check if context and swapchain are available
            //assert(md3dImmediateContext); 
            //assert(mSwapChain);
            context.ClearRenderTargetView(renderTargetView, Color.Blue);

            context.ClearDepthStencilView(depthStencilView, DepthStencilClearFlags.Depth, 1, 0);
        }

        public void PresentRenderedScene()
        {
            swapChain.Present(0, PresentFlags.None);
        }

        #endregion
    }
}

