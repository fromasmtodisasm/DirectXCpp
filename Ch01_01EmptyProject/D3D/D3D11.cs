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

using SharpDX.Direct3D;
using System.Windows.Forms;

namespace Ch01_01EmptyProject
{
    public class D3D11 : ID3D
    {
        private Device device;
        private DeviceContext deviceContext;

        private SwapChain swapChain;
        private Texture2D backBuffer;
        private RenderTargetView renderTargetView;
        private DepthStencilView depthStencilView;
        private Texture2DDescription depthBuffer;
        private Texture2D depthStencilBuffer;

        public Device Device
        {
            get { return device; }
            set { device = value; }
        }

        public DeviceContext DeviceContext
        {
            get { return deviceContext; }
            set { deviceContext = value; }
        }

        public D3D11(WindowConfiguration windowConfig)
        {
            try
            {
                var deviceFlags = DeviceCreationFlags.Debug;

                var initializeHelper = new D3DInitializeHelper(windowConfig);

                DriverType driverType = DriverType.Warp; //initializeHelper.GetDriverTypeForRenderingWhichSupportsDx11(false);
#if DEBUG
                //deviceFlags = DeviceCreationFlags.Debug;
#endif
                device = initializeHelper.CreateDevice(deviceFlags, driverType);

                deviceContext = device.ImmediateContext;
                //CHECK AntiAliasing quality suport code could be here

                ModeDescription modeDescription = initializeHelper.CreateModeDescription();
                SwapChainDescription swapChainDescription = initializeHelper.SwapChainDescription(ref modeDescription);

                swapChain = initializeHelper.CreateSwapChain(device, swapChainDescription);

                backBuffer = initializeHelper.CreateResourceViewFromBackBuffer(swapChain);

                renderTargetView = initializeHelper.CreateRenderTargetViewForTarget(device, backBuffer);

                depthBuffer = initializeHelper.CreateDepthBuffer();

                depthStencilBuffer = initializeHelper.CreateDepthStencilBuffer(device, depthBuffer);

                depthStencilView = new DepthStencilView(device, depthStencilBuffer);

                initializeHelper.BindBuffersToOutputStageOfPipeline(renderTargetView, depthStencilView, deviceContext);

                initializeHelper.CreateViewPort(deviceContext);
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
                deviceContext.ClearRenderTargetView(renderTargetView, Color.Blue);
                deviceContext.ClearDepthStencilView(depthStencilView, DepthStencilClearFlags.Depth, 1, 0);
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
                swapChain.Present(0, PresentFlags.None);
            }
            catch (SharpDX.SharpDXException ex)
            {
                throw new Exception("D3D11 Could not present scene: " + ex.Message);
            }
        }

        public void Dispose()
        {
            depthStencilView.Dispose();
            depthStencilBuffer.Dispose();
            backBuffer.Dispose();
            deviceContext.ClearState();
            deviceContext.Flush();
            deviceContext.Dispose();
            device.Dispose();
            swapChain.Dispose();
            renderTargetView.Dispose();
        }
    }
}

