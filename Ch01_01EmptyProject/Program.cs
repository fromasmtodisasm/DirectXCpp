using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ch01_01EmptyProject
{
    static class Program
    {
        //[STAThread]
        //private static void Main()
        //{
        //    // # of graphics card adapter
        //    const int numAdapter = 0;

        //    // # of output device (i.e. monitor)
        //    const int numOutput = 0;

        //    const string outputFileName = "ScreenCapture.bmp";

        //    // Create DXGI Factory1
        //    var factory = new Factory1();
        //    var adapter = factory.GetAdapter1(numAdapter);

        //    // Create device from Adapter
        //    var device = new Device(adapter);

        //    // Get DXGI.Output
        //    var output = adapter.GetOutput(numOutput);
        //    var output1 = output.QueryInterface<Output1>();

        //    // Width/Height of desktop to capture
        //    int width = output.Description.DesktopBounds.Width;
        //    int height = output.Description.DesktopBounds.Height;

        //    // Create Staging texture CPU-accessible
        //    var textureDesc = new Texture2DDescription
        //    {
        //        CpuAccessFlags = CpuAccessFlags.Read,
        //        BindFlags = BindFlags.None,
        //        Format = Format.B8G8R8A8_UNorm,
        //        Width = width / 2,
        //        Height = height / 2,
        //        OptionFlags = ResourceOptionFlags.None,
        //        MipLevels = 1,
        //        ArraySize = 1,
        //        SampleDescription = { Count = 1, Quality = 0 },
        //        Usage = ResourceUsage.Staging
        //    };
        //    var stagingTexture = new Texture2D(device, textureDesc);

        //    // Create Staging texture CPU-accessible
        //    var smallerTextureDesc = new Texture2DDescription
        //    {
        //        CpuAccessFlags = CpuAccessFlags.None,
        //        BindFlags = BindFlags.RenderTarget | BindFlags.ShaderResource,
        //        Format = Format.B8G8R8A8_UNorm,
        //        Width = width,
        //        Height = height,
        //        OptionFlags = ResourceOptionFlags.GenerateMipMaps,
        //        MipLevels = 4,
        //        ArraySize = 1,
        //        SampleDescription = { Count = 1, Quality = 0 },
        //        Usage = ResourceUsage.Default
        //    };
        //    var smallerTexture = new Texture2D(device, smallerTextureDesc);
        //    var smallerTextureView = new ShaderResourceView(device, smallerTexture);

        //    // Duplicate the output
        //    var duplicatedOutput = output1.DuplicateOutput(device);

        //    bool captureDone = false;
        //    for (int i = 0; !captureDone; i++)
        //    {
        //        try
        //        {
        //            SharpDX.DXGI.Resource screenResource;
        //            OutputDuplicateFrameInformation duplicateFrameInformation;

        //            // Try to get duplicated frame within given time
        //            duplicatedOutput.AcquireNextFrame(10000, out duplicateFrameInformation, out screenResource);

        //            if (i > 0)
        //            {
        //                // copy resource into memory that can be accessed by the CPU
        //                using (var screenTexture2D = screenResource.QueryInterface<Texture2D>())
        //                    device.ImmediateContext.CopySubresourceRegion(screenTexture2D, 0, null, smallerTexture, 0);

        //                // Generates the mipmap of the screen
        //                device.ImmediateContext.GenerateMips(smallerTextureView);

        //                // Copy the mipmap 1 of smallerTexture (size/2) to the staging texture
        //                device.ImmediateContext.CopySubresourceRegion(smallerTexture, 1, null, stagingTexture, 0);

        //                // Get the desktop capture texture
        //                var mapSource = device.ImmediateContext.MapSubresource(stagingTexture, 0, MapMode.Read, MapFlags.None);

        //                // Create Drawing.Bitmap
        //                var bitmap = new System.Drawing.Bitmap(width / 2, height / 2, PixelFormat.Format32bppArgb);
        //                var boundsRect = new System.Drawing.Rectangle(0, 0, width / 2, height / 2);

        //                // Copy pixels from screen capture Texture to GDI bitmap
        //                var mapDest = bitmap.LockBits(boundsRect, ImageLockMode.WriteOnly, bitmap.PixelFormat);
        //                var sourcePtr = mapSource.DataPointer;
        //                var destPtr = mapDest.Scan0;
        //                for (int y = 0; y < height / 2; y++)
        //                {
        //                    // Copy a single line 
        //                    Utilities.CopyMemory(destPtr, sourcePtr, width / 2 * 4);

        //                    // Advance pointers
        //                    sourcePtr = IntPtr.Add(sourcePtr, mapSource.RowPitch);
        //                    destPtr = IntPtr.Add(destPtr, mapDest.Stride);
        //                }

        //                // Release source and dest locks
        //                bitmap.UnlockBits(mapDest);
        //                device.ImmediateContext.UnmapSubresource(stagingTexture, 0);

        //                // Save the output
        //                bitmap.Save(outputFileName);

        //                // Capture done
        //                captureDone = true;
        //            }

        //            screenResource.Dispose();
        //            duplicatedOutput.ReleaseFrame();

        //        }
        //        catch (SharpDXException e)
        //        {
        //            if (e.ResultCode.Code != SharpDX.DXGI.ResultCode.WaitTimeout.Result.Code)
        //            {
        //                throw e;
        //            }
        //        }
        //    }

        //    // Display the texture using system associated viewer
        //    System.Diagnostics.Process.Start(Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, outputFileName)));

        //    // TODO: We should cleanp up all allocated COM objects here
        //}


        private static GreenHornEngine system;
        private static bool USER_ERROR = false;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (USER_ERROR == true)
            {
                try
                {
                    system = new GreenHornEngine();
                    system.Run();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    system.Dispose();
                }
            }
            else
            {
                try
                {
                    system = new GreenHornEngine();
                    system.Run();
                }
                finally
                {
                    system.Dispose();
                }
            }
        }

        private static void SystemRun()
        {
            //D3DX11Test test = new D3DX11Test();
            //test.Draw();


        }
    }
}
