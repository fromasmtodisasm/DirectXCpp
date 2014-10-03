using SharpDX.Direct3D;
using SharpDX.DXGI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Resolve name conflicts
using Device = SharpDX.Direct3D11.Device;
using Ch01_01EmptyProject;
using SharpDX.Direct3D11;
using SharpDX;


namespace Ch01_01EmptyProject
{
   public class D3DInitializeHelper
    {
       private WindowConfiguration windowConfig;
       public D3DInitializeHelper(WindowConfiguration windowConfig)
       {
           this.windowConfig = windowConfig;
       } 
       
       public DriverType GetDriverTypeForRenderingWhichSupportsDx11(bool enforceSoftwareRendering)
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
       public Device CreateDevice(DeviceCreationFlags deviceFlags, DriverType driverType)
       {
           try
           {
               return new Device(driverType, deviceFlags);
           }
           catch (Exception ex)
           {
               throw new NotSupportedException(ex.Message);
           }
       }

       public SwapChain CreateSwapChain(Device device, SwapChainDescription swapChainDescription)
       {
           try
           {
               using (var dxgi = device.QueryInterface<SharpDX.DXGI.Device>())
               using (var adapter = dxgi.Adapter)
               using (var factory = adapter.GetParent<Factory>())
               {
                   return new SwapChain(factory, device, swapChainDescription);
               }
           }
           catch (Exception ex)
           {

               throw new Exception("D3D Could not create Swap Chain: " + ex);
           }
       }

        public RenderTargetView CreateRenderTargetViewForTarget(Device device, Texture2D backBuffer)
        {
            try
            {
                 return new RenderTargetView(device, backBuffer);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public void BindBuffersToOutputStageOfPipeline(RenderTargetView renderTargetView, DepthStencilView depthStencilView, DeviceContext context)
        {
            context.OutputMerger.SetTargets(depthStencilView, renderTargetView);
        }

        public Texture2D CreateResourceViewFromBackBuffer(SwapChain swapChain)
        {
            try
            {
                return Texture2D.FromSwapChain<Texture2D>(swapChain, 0);
            }
            catch (Exception ex)
            {
                
                throw new Exception("D3D couldnt create resource: " + ex);
            }
        }

        public ModeDescription CreateModeDescription()
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
            catch (Exception ex)
            {

                throw new Exception("D3D could not create ModeDecription for swapChain: ", ex);
            }
        }

        public SwapChainDescription SwapChainDescription(ref ModeDescription modeDescription)
        {
            try
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
                swapChainSetup.OutputHandle = windowConfig.FormWindowHandle;
                swapChainSetup.SampleDescription = new SampleDescription(1, 0);
                swapChainSetup.SwapEffect = SwapEffect.Discard;
                swapChainSetup.Usage = Usage.RenderTargetOutput;
                return swapChainSetup;
            }
            catch (Exception ex)
            {

                throw new Exception("" + ex);
            }
        }
        public void CreateViewPort(DeviceContext context)
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

        public Texture2DDescription CreateDepthBuffer()
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
            catch (Exception ex)
            {
                throw new Exception("D3D Could not create DepthBuffer: " + ex);
            }
        }
   }
}
