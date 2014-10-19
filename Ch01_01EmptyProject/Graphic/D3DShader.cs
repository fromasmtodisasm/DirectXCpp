using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D11;
using SharpDX.Direct3D;
using SharpDX.DXGI;
using SharpDX.Windows;

using Device = SharpDX.Direct3D11.Device;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace Ch01_01EmptyProject.Graphic.Shaders
{
    public class D3DShader : IGraphicComposite
    {
        private Buffer vertexBuffer;
        private Buffer indicesBuffer;
        private Buffer lightBuffer;
        private int[] indices;

        private InputLayout inputLayout;
        private VertexShader vertexShader;

        private Device device;
        private PixelShader pixelShader;
        private CompilationResult vertexShaderByteCode;
        private CompilationResult pixelShaderByteCode;
        private Buffer constantMatrixBuffer;
        private int indexCount;
        private DeviceContext deviceContext;
        private SamplerState sampleState;
        private ShaderResourceView srw;
        private Buffer constantLightBuffer;

        [StructLayout(LayoutKind.Sequential)]
        public struct WorldViewProj
        {
            public Matrix worldMatrix;
            public Matrix viewMatrix;
            public Matrix projectionMatrix;
        }

        public struct LightBufferType
        {
            public Vector4 diffuseColor;
            public Vector3 lightDirection;
            // Added extra padding so structure is a multiple of 16 for CreateBuffer function requirements.
            public float padding;
        }

        public D3DShader(Device device)
        {
            this.device = device;

            ShaderName shader = ShaderName.Texture;

            IShaderEffect shaderEffect = ShaderFactory.Create(shader);

            try
            {
                vertexShaderByteCode = ShaderBytecode.CompileFromFile(shaderEffect.EffectShaderFileName, shaderEffect.VsFunctionName, shaderEffect.VsVersion.ToString());
            }
            catch (Exception ex)
            {

                throw new Exception("vertexShaderByteCode: " + ex);
            }
            try
            {
                pixelShaderByteCode = ShaderBytecode.CompileFromFile(shaderEffect.EffectShaderFileName, shaderEffect.PsFunctionName, shaderEffect.PsVersion.ToString());
            }
            catch (Exception ex)
            {

                throw new Exception("vertexShaderByteCode: " + ex);
            }

            try
            {
                vertexShader = new VertexShader(device, vertexShaderByteCode);
            }
            catch (Exception ex)
            {

                throw new Exception("vertexShader: " + ex);
            }

            try
            {
                pixelShader = new PixelShader(device, pixelShaderByteCode);
            }
            catch (Exception ex)
            {

                throw new Exception("pixelShader: " + ex);
            }

            // Now setup the layout of the data that goes into the shader.
            // It needs to match the VertexType structure in the Model and in the shader.

            InputElement[] inputElementDesc;
    
            if (shader == ShaderName.Texture)
            {
                inputElementDesc = VertexInputLayouts.TextureVertex();
            }

            else
            {
                inputElementDesc = VertexInputLayouts.ColorVertex();
            }

            CreateInputLayout(inputElementDesc);

            vertexShaderByteCode.Dispose();
            pixelShaderByteCode.Dispose();

            // Setup the description of the dynamic matrix constant buffer that is in the vertex shader.
            try
            {
                var matrixBufferDesc = new BufferDescription()
                       {
                           Usage = ResourceUsage.Dynamic,
                           //or size of constant buffer
                           SizeInBytes = Utilities.SizeOf<WorldViewProj>(),
                           BindFlags = BindFlags.ConstantBuffer,
                           CpuAccessFlags = CpuAccessFlags.Write,
                           OptionFlags = ResourceOptionFlags.None,
                           StructureByteStride = 0
                       };
                constantMatrixBuffer = new Buffer(device, matrixBufferDesc);

            }
            catch (Exception ex)
            {
                throw new Exception("BufferDescription: " + ex);
            }


            if (shader == ShaderName.Texture || shader == ShaderName.Diffuse)
            {
                try
                {
                    string textureFileName = @"D:\Github\DirectXCpp\Ch01_01EmptyProject\Graphic\Shaders\Textures\seafloor.dds";

                    srw = ShaderResourceView.FromFile(device, textureFileName);
                    SamplerStateDescription samplerDescription = new SamplerStateDescription()
                            {
                                Filter = Filter.MinMagMipLinear,

                                AddressU = TextureAddressMode.Wrap,
                                AddressV = TextureAddressMode.Wrap,
                                AddressW = TextureAddressMode.Wrap,

                                MipLodBias = 0,
                                MaximumAnisotropy = 1,
                                ComparisonFunction = Comparison.Always,
                                BorderColor = new Color4(255, 0, 0, 1),
                                MinimumLod = 0,
                                MaximumLod = 0
                            };

                    sampleState = new SamplerState(device, samplerDescription);

                }
                catch (Exception ex)
                {
                    throw new Exception("Could not initialize texture shader: " + ex);
                }
            }

            if (shader == ShaderName.Diffuse)
            {
               Vector4 diffuseColor = (Vector4)Color.DarkRed;
               Vector3 lightDirection = new Vector3(1, 1, 1);

                var lightBufferDesc = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Utilities.SizeOf<LightBufferType>(),
                    BindFlags = BindFlags.ConstantBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                constantLightBuffer = new Buffer(device, lightBufferDesc);

                DataStream mappedResource;
                deviceContext.MapSubresource(constantLightBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);

                var lightBuffer = new LightBufferType()
                {
                    diffuseColor = diffuseColor,
                    lightDirection = lightDirection,
                    padding = 0,
                };

                mappedResource.Write(lightBuffer);

				// Unlock the constant buffer.
				deviceContext.UnmapSubresource(constantLightBuffer, 0);

				// Set the position of the light constant buffer in the pixel shader.
				int bufferNumber = 0;

				// Finally set the light constant buffer in the pixel shader with the updated values.
                deviceContext.PixelShader.SetConstantBuffer(bufferNumber, constantLightBuffer);
            }
        }
        public void SetShaderParameters(DeviceContext deviceContext, WorldViewProj worldViewProj, int indexCount)
        {
            this.indexCount = indexCount;
            this.deviceContext = deviceContext;

            Matrix worldInverseTransposeMatrix = worldViewProj.worldMatrix;
            worldInverseTransposeMatrix.Invert();
            worldInverseTransposeMatrix.Transpose();

            worldViewProj.worldMatrix.Transpose();
            worldViewProj.viewMatrix.Transpose();
            worldViewProj.projectionMatrix.Transpose();

            DataStream mappedResource;

            deviceContext.MapSubresource(constantMatrixBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);

            mappedResource.Write(worldViewProj);

            //mappedResource.Write(worldInverseTransposeMatrix);

            // Unlock the constant buffer.
            deviceContext.UnmapSubresource(constantMatrixBuffer, 0);

            try
            {
                deviceContext.VertexShader.SetConstantBuffer(0, constantMatrixBuffer);
            }
            catch (Exception ex)
            {
                throw new Exception("D3D11 failed to set constant buffer" + ex);
            }

            // Set shader resource in the pixel shader.
            deviceContext.PixelShader.SetShaderResource(0, srw);
        }



        public void Render()
        {
            deviceContext.InputAssembler.InputLayout = inputLayout;
            try
            {
                deviceContext.VertexShader.Set(vertexShader);
                deviceContext.PixelShader.Set(pixelShader);
            }
            catch (Exception ex)
            {
                throw new Exception("D3D11 failed to set Vertex and Pixel shader: " + ex);
            }

            try
            {
                // Set the sampler state in the pixel shader.
                deviceContext.PixelShader.SetSampler(0, sampleState);
                deviceContext.DrawIndexed(indexCount, 0, 0);
            }
            catch (Exception ex)
            {
                throw new Exception("D3D11 failed to draw scene: " + ex);
            }
        }

        public void Dispose()
        {
            constantLightBuffer.Dispose();
            srw.Dispose();
            sampleState.Dispose();
            constantMatrixBuffer.Dispose();
            inputLayout.Dispose();
            pixelShader.Dispose();
            vertexShader.Dispose();
        }

        private void CreateInputLayout(InputElement[] inputElementDesc)
        {
            try
            {
                var signature = ShaderSignature.GetInputSignature(vertexShaderByteCode);
                inputLayout = new InputLayout(device, signature, inputElementDesc);
                signature.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("D3D11 could not create input Layout: " + ex);
            }
        }
    }
}