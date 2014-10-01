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

namespace Ch01_01EmptyProject.Graphic
{
    public class D3D : IDisposable
    {
        private Device device;

        public Device Device
        {
            get { return device; }
            set { device = value; }
        }
        private DeviceContext deviceContext;

        public DeviceContext DeviceContext
        {
            get { return deviceContext; }
            set { deviceContext = value; }
        }
        private SwapChain swapChain;
        private Texture2D backBuffer;
        private RenderTargetView renderTargetView;
        private DepthStencilView depthStencilView;
        private Texture2DDescription depthBuffer;
        private Texture2D depthStencilBuffer;

        public D3D(WindowConfiguration windowConfig)
        {
            var deviceFlags = DeviceCreationFlags.None;

            var initializeHelper = new D3DInitializeHelper(windowConfig);

            bool enforceSoftwareRendering = false;

            DriverType driverType = initializeHelper.GetDriverTypeForRenderingWhichSupportsDx11(enforceSoftwareRendering);
#if DEBUG
            deviceFlags = DeviceCreationFlags.Debug;
#endif
            device = initializeHelper.CreateDevice(deviceFlags, driverType);

            deviceContext = device.ImmediateContext;
            //CHECK AntiAliasing quality suport code coud be here

            ModeDescription modeDescription = initializeHelper.CreateModeDescription();
            SwapChainDescription swapChainDescription = initializeHelper.SwapChainDescription(ref modeDescription);

            swapChain = initializeHelper.CreateSwapChain(device, swapChainDescription);

            backBuffer = initializeHelper.CreateResourceViewFromBackBuffer(swapChain);

            renderTargetView = initializeHelper.CreateRenderTargetViewForTarget(device, backBuffer);

            depthBuffer = initializeHelper.CreateDepthBuffer();

            depthStencilBuffer = new Texture2D(device, depthBuffer);
            depthStencilView = new DepthStencilView(device, depthStencilBuffer);

            initializeHelper.BindBuffersToOutputStageOfPipeline(renderTargetView, depthStencilView, deviceContext);

            initializeHelper.CreateViewPort(deviceContext);
        }

        public void BeginScene()
        {
            //I shloud check if context and swapchain are available
            //assert(md3dImmediateContext); 
            //assert(mSwapChain);
            deviceContext.ClearRenderTargetView(renderTargetView, Color.Blue);

            deviceContext.ClearDepthStencilView(depthStencilView, DepthStencilClearFlags.Depth, 1, 0);
        }

        public void UpdateScene(float deltaT)
        {
            //Called every frame - should be used to update app over time, eq. processing animations...
            throw new NotImplementedException();
        }

        public void PresentRenderedScene()
        {
            try
            {
                swapChain.Present(0, PresentFlags.None);
            }
            catch (SharpDX.SharpDXException e)
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
    }
}

