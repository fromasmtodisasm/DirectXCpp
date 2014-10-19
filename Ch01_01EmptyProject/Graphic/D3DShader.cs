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
using Ch01_01EmptyProject.Graphic.Structures;

namespace Ch01_01EmptyProject.Graphic.Shaders
{
    public class D3DShader : IGraphicComposite
    {
        private Buffer vertexBuffer;
        private Buffer indicesBuffer;
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

        [StructLayout(LayoutKind.Sequential)]
        public struct WorldViewProj
        {
            public Matrix worldMatrix;
            public Matrix viewMatrix;
            public Matrix projectionMatrix;
        }
        public D3DShader(Device device, IShaderEffect shaderEffect)
        {
            try
            {
                this.device = device;

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
                InputElement[] inputElementDesc = InputLayoutFactory.Create(shaderEffect.VertexType);

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


                if (shaderEffect.VertexType == VertexType.TextureVertex)
                {
                    try
                    {
                        string textureFileName = @"D:\Github\DirectXCpp\Ch01_01EmptyProject\Graphic\Shaders\Textures\wall.dds";

                        try
                        {
                            srw = ShaderResourceView.FromFile(device, textureFileName);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("textureLoad: " + ex);
                        }


                        SamplerStateDescription samplerDescription = new SamplerStateDescription()
                                {
                                    Filter = Filter.MinMagMipLinear,

                                    AddressU = TextureAddressMode.Wrap,
                                    AddressV = TextureAddressMode.Wrap,
                                    AddressW = TextureAddressMode.Wrap,

                                    MipLodBias = 0,
                                    MaximumAnisotropy = 1,
                                    ComparisonFunction = Comparison.Always,
                                    BorderColor = Color.Green,
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

            }
            catch (Exception ex)
            {
                throw new Exception("Could not initialize the shader: " + ex);

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
            try
            {
                deviceContext.InputAssembler.InputLayout = inputLayout;

                try
                {
                    deviceContext.VertexShader.Set(vertexShader);
                    deviceContext.PixelShader.Set(pixelShader);

                    int slotSampler = 0;
                    deviceContext.PixelShader.SetSampler(slotSampler, sampleState);
                }
                catch (Exception ex)
                {

                    throw new Exception("D3D11 failed to set Vertex and Pixel shader: " + ex);
                }

                try
                {
                    deviceContext.DrawIndexed(indexCount, 0, 0);
                }
                catch (Exception ex)
                {
                    throw new Exception("D3D11 failed to draw scene: " + ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("D3D11 failed to render shaders: " + ex);
            }
        }

        public void Dispose()
        {
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