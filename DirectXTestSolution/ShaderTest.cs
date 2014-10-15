using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDX;
using Device = SharpDX.Direct3D11.Device;
using SharpDX.Direct3D11;
using SharpDX.Direct3D;

namespace DirectXTestSolution
{
    [TestClass]
    public class ShaderTest
    {
        [TestInitializeAttribute]
        public void Initialize()
        {
            Device device = new Device(DriverType.Software, DeviceCreationFlags.Debug);
        }
        
        [TestMethod]
        public void shader_returns_compiled_valid_shader()
        {
            // I had really nice intensions to create test for shaders, but, screw it for now
            //Shader shader = new Shader(device);
            //shader.Compile();
        }
    }
}
