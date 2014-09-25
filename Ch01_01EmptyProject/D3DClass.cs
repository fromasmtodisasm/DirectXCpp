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

        public void InitializeWithForm(Form1 form)
        {
            //var highestFL = Device.GetSupportedFeatureLevel();
            //if (highestFL <  FeatureLevel.Level_11_0)
            //{ 
            //    throw new NotSupportedException();
            //}

            ModeDescription modeDescription = CreateModeDescription(form);
            var swapChainSetup = SetUpSwapChain(form, ref modeDescription);
            // Used for debugging dispose object references
            // Configuration.EnableObjectTracking = true;

            // Disable throws on shader compilation errors - hopefully
            // we won't have any of these! :)
            //Configuration.ThrowOnShaderCompileError = false;

            CreateSwapChain(swapChainSetup);

            var context = device.ImmediateContext;

            CreateResourceViewFromBackBuffer();

            RenderToBackBuffer();
        }

        public void StartRender(Form1 form)
        {
            RenderLoop.Run(form, () =>
            {
                BindBuffersToOutputStageOfPipeline();
                swapChain.Present(0, PresentFlags.None);

            });
        }

        private void BindBuffersToOutputStageOfPipeline()
        {
            Viewport v = new Viewport(0, 0, 40, 40);
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

        private static SwapChainDescription SetUpSwapChain(Form1 form, ref ModeDescription modeDescription)
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
