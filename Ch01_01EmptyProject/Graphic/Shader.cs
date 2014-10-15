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

namespace Ch01_01EmptyProject
{
    public class Shader
    {
        private Buffer vertexBuffer;
        private Vertex[] vertices;
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

        [StructLayout(LayoutKind.Sequential)]
        public struct WorldViewProj
        {
            public Matrix worldMatrix;
            public Matrix viewMatrix;
            public Matrix projectionMatrix;
        }

        public WorldViewProj SetWorldViewMatrix(Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix)
        {
            var wwp = new WorldViewProj();
            wwp.projectionMatrix = projectionMatrix;
            wwp.viewMatrix = viewMatrix;
            wwp.worldMatrix = worldMatrix;

            return wwp;
        }

        public Shader(Device device)
        {
            try
            {
                this.device = device;

                string vertexShaderFileName = @"Graphic\Shaders\ColorShader2.fx";

              string vsFunctionName = "ColorVertexShader";
              string psFunctionName = "ColorPixelShader";

                try
                {
                    vertexShaderByteCode = ShaderBytecode.CompileFromFile(vertexShaderFileName, vsFunctionName, "vs_5_0");
                }
                catch (Exception ex)
                {

                    throw new Exception("vertexShaderByteCode: " + ex);
                }
                try
                {
                    pixelShaderByteCode = ShaderBytecode.CompileFromFile(vertexShaderFileName, psFunctionName, "ps_5_0");
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


                InputElement[] inputElementDesc = SpecifyInputLayoutDescriptionForVertex();

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
                        SizeInBytes = Utilities.SizeOf<Matrix>(),
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


            }
            catch (Exception ex)
            {
                throw new Exception("Could not initialize the shader: " + ex);
            }
        }

        private void SetShaderParameters(DeviceContext deviceContext, WorldViewProj worldViewProj)
        {
            worldViewProj.worldMatrix.Transpose();
            worldViewProj.viewMatrix.Transpose();
            worldViewProj.projectionMatrix.Transpose();

            DataStream mappedResource;
            deviceContext.MapSubresource(constantMatrixBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);

            mappedResource.Write(worldViewProj);

            deviceContext.UnmapSubresource(constantMatrixBuffer, 0);
        }


        public void Render(DeviceContext deviceContext, WorldViewProj worldViewProj, int indexCount)
        {
            try
            {
                SetShaderParameters(deviceContext, worldViewProj);
                deviceContext.InputAssembler.InputLayout = inputLayout;

                try
                {
                    deviceContext.VertexShader.SetConstantBuffer(0, constantMatrixBuffer);
                }
                catch (Exception ex)
                {
                    throw new Exception("D3D11 failed to set constant buffer" + ex);
                }

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
                    deviceContext.UpdateSubresource(ref worldViewProj, constantMatrixBuffer);
                }
                catch (Exception ex)
                {

                    throw new Exception("D3D11 failed to pass data to shader" + ex);
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

        // Now setup the layout of the data that goes into the shader.
        // It needs to match the VertexType structure in the Model and in the shader.
        private InputElement[] SpecifyInputLayoutDescriptionForVertex()
        {
            return new InputElement[]
            {
            new InputElement()
            {
            SemanticName = "POSITION",
            SemanticIndex = 0,
            Format = Format.R32G32B32A32_Float,
            Slot = 0,
            AlignedByteOffset = 0,
            Classification = InputClassification.PerVertexData,
            InstanceDataStepRate = 0
            },
           new InputElement()
            {
                SemanticName = "COLOR",
                SemanticIndex = 0,
                Format = Format.R32G32B32A32_Float,
                Slot = 0,
                AlignedByteOffset = 12,
                Classification = InputClassification.PerVertexData,
                InstanceDataStepRate = 0
            }
        };
        }
    }
}
