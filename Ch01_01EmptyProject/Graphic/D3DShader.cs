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

// Q1? - ShaderQ should be divided to renderer, shaderBase and concrete shaders (example : MW)

//Q2 - using a cPerObject and cPerFrame notation !
// reorg classes - better structure (examle: sharpdx tutorail)

namespace Ch01_01EmptyProject.Graphic.Shaders
{
    public class D3DShader : IGraphicComposite
    {
        private Buffer constantMatrixBuffer;
        private Buffer constantLightBuffer;

        private InputLayout inputLayout;
        private VertexShader vertexShader;

        private Device device;

        private PixelShader pixelShader;

        private CompilationResult vertexShaderByteCode;
        private CompilationResult pixelShaderByteCode;

        private int indexCount;
        private DeviceContext deviceContext;
        private SamplerState sampleState;
        private ShaderResourceView textureResource;

        private DataStream mappedResource;
        private ShaderName shader;
        private Buffer constantCameraBuffer;
        private Textures textures;

        public Vector3 CameraPosition { get; set; }
        public ShaderResourceView[] TextureCollection { get; private set; }

        public D3DShader(Device device, IShaderEffect shaderEffect, ShaderName shader)
        {
            this.device = device;
            this.shader = shader;

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

            constantMatrixBuffer = GetConstantMatrixBuffer<BufferTypes.WorldViewProj>(device);

            //is there any differnece between loading one shader wih multiple effects
            //and more shaders with only one effect?
            if (shader != ShaderName.Color)
            {
                LoadTextureShader();
            }

            //shader.GetPerObjectBufferType<DiffuseLightBufferType>

            if (shader == ShaderName.ParallaxMapping || shader == ShaderName.Diffuse || shader == ShaderName.Bumpmaping || shader == ShaderName.LightingEffect)
            {
                constantLightBuffer = GetConstantMatrixBuffer<BufferTypes.DiffuseLightBufferType>(device);
            }
            if (shader == ShaderName.Ambient)
            {
                constantLightBuffer = GetConstantMatrixBuffer<BufferTypes.AmbientLightBufferType>(device);
            }

            if (shader == ShaderName.Specular || shader == ShaderName.DirectionalLightingParallaxMapping)
            {
                constantLightBuffer = GetConstantMatrixBuffer<BufferTypes.SpecularLightBufferType>(device);
            }

            if (shader == ShaderName.ParallaxMapping || shader == ShaderName.Bumpmaping || shader == ShaderName.Specular || shader == ShaderName.ParallaxMapping || shader == ShaderName.DirectionalLightingParallaxMapping)
            {
                constantCameraBuffer = GetConstantMatrixBuffer<BufferTypes.CameraBufferType>(device);
            }
        }

        public void WriteToSubresource<T>(Buffer constantBuffer, T writeTo) where T : struct
        {
            deviceContext.MapSubresource(constantBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);

            mappedResource.Write(writeTo);

            // Unlock the constant buffer.
            deviceContext.UnmapSubresource(constantBuffer, 0);
        }

        public void SetShaderParameters(DeviceContext deviceContext, BufferTypes.WorldViewProj WWPComputed, int indexCount)
        {
            //no time for refining this, it should be more flexible 
            //like: if usingCamera  ==> createCameraBuffer, if usingSpecularLightig....
            // i should probably use some behavioral/creatioal pattern
            // this is probably more about behavior.....

            this.indexCount = indexCount;
            this.deviceContext = deviceContext;

            //WriteToSubresource<WorldViewProjAdvanced>(constantMatrixBuffer, WWPComputed);
            WriteToSubresource<BufferTypes.WorldViewProj>(constantMatrixBuffer, WWPComputed);

            int bufferNumber = 0;

            try
            {
                deviceContext.VertexShader.SetConstantBuffer(bufferNumber, constantMatrixBuffer);
            }
            catch (Exception ex)
            {
                throw new Exception("D3D11 failed to set constant buffer" + ex);
            }

            // Set shader resource in the pixel shader.
            if (shader != ShaderName.Color)
            {
                //useTexture
                deviceContext.PixelShader.SetShaderResources(0, TextureCollection);
            }

            Vector4 diffuseColor;
            Vector3 lightDirection;
            Vector4 ambientColor;

            if (shader == ShaderName.ParallaxMapping || shader == ShaderName.Bumpmaping || shader == ShaderName.Specular || shader == ShaderName.DirectionalLightingParallaxMapping)
            {
                UseCamera();
            }

            if (shader == ShaderName.DirectionalLightingParallaxMapping)
            {
                BufferTypes.Material material = new BufferTypes.Material();
                material.ambient = new Vector4(0.3f, 0.3f, 0.3f, 1.0f);
                material.diffuse = new Vector4(0.7f, 0.7f, 0.7f, 1.0f);
                material.specular = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
                material.shininess = 90.0f;

                WriteToSubresource<BufferTypes.Material>(constantLightBuffer, material);

                // Finally set the light constant buffer in the pixel shader with the updated values.
                deviceContext.PixelShader.SetConstantBuffer(1, constantLightBuffer);

                //deviceContext.VertexShader.SetConstantBuffer(2, constantLightBuffer);
            }

            if (shader == ShaderName.ParallaxMapping ||  shader == ShaderName.Diffuse || shader == ShaderName.Bumpmaping || shader == ShaderName.LightingEffect) //|| shader == ShaderName.DirectionalLightingParallaxMapping
            {
                //another ugly part
                if (shader == ShaderName.Diffuse)
                {
                    diffuseColor = new Vector4(1, 0, 1, 1);
                    lightDirection = new Vector3(1, 0, 1);
                }
                else
                {
                    diffuseColor = new Vector4(1, 1, 1, 1f);
                    //Vector4 diffuseColor = new Vector4(1, 0, 1, 1);
                    lightDirection = new Vector3(1.0f, 0.0f, 1.0f);
                    //lightDirection = new Vector3(.4f, 0, 1);
                }
                var lightBuffer = new BufferTypes.DiffuseLightBufferType()
                {
                    diffuseColor = diffuseColor,
                    lightDirection = lightDirection,
                    padding = 0,
                };

                WriteToSubresource<BufferTypes.DiffuseLightBufferType>(constantLightBuffer, lightBuffer);

                // Finally set the light constant buffer in the pixel shader with the updated values.

                if (shader == ShaderName.ParallaxMapping)
                {
                    deviceContext.VertexShader.SetConstantBuffer(2, constantLightBuffer);
                }
                else
                {
                    deviceContext.PixelShader.SetConstantBuffer(0, constantLightBuffer);
                }
            }

            if (shader == ShaderName.Ambient)
            {
                diffuseColor = new Vector4(1, 1, 1f, 1f);
                lightDirection = new Vector3(1, .5f, 0);
                ambientColor = new Vector4(0.15f, 0.15f, 0.15f, 1.0f);

                var ambientLightBuffer = new BufferTypes.AmbientLightBufferType()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColor,
                    lightDirection = lightDirection,
                    padding = 0,
                };

                WriteToSubresource<BufferTypes.AmbientLightBufferType>(constantLightBuffer, ambientLightBuffer);

                // Finally set the light constant buffer in the pixel shader with the updated values.
                deviceContext.PixelShader.SetConstantBuffer(bufferNumber, constantLightBuffer);
            }

            if (shader == ShaderName.Specular)
            {
                diffuseColor = new Vector4(1, 1, 1f, 1f);
                lightDirection = new Vector3(1, 1, 1);
                ambientColor = new Vector4(0.15f, 0.15f, 0.15f, 1.0f);

                Vector4 specularColor = new Vector4(1, 1, 1, 1);
                float specularPower = 32;

                var specularLightBuffer = new BufferTypes.SpecularLightBufferType()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColor,
                    lightDirection = lightDirection,
                    specularColor = specularColor,
                    specularPower = specularPower,
                };

                WriteToSubresource<BufferTypes.SpecularLightBufferType>(constantLightBuffer, specularLightBuffer);
                bufferNumber = 0;
                // Finally set the light constant buffer in the pixel shader with the updated values.
                deviceContext.PixelShader.SetConstantBuffer(bufferNumber, constantLightBuffer);
            }

        }
        private void UseCamera()
        {
            var cameraBuffer = new BufferTypes.CameraBufferType()
            {
                cameraPosition = CameraPosition,
                padding = 0.0f,
            };

            WriteToSubresource<BufferTypes.CameraBufferType>(constantCameraBuffer, cameraBuffer);
            int bufferNumber = 1;

            deviceContext.VertexShader.SetConstantBuffer(bufferNumber, constantCameraBuffer);
        }

        public void Render()
        {
            deviceContext.InputAssembler.InputLayout = inputLayout;

            try
            {
                deviceContext.VertexShader.Set(vertexShader);
                deviceContext.PixelShader.Set(pixelShader);

                //when I have textture    
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

        public void Dispose()
        {
            if (constantLightBuffer != null)
            {
                constantLightBuffer.Dispose();
            }
            if (constantCameraBuffer != null)
            {
                constantCameraBuffer.Dispose();
            }
            if (textureResource != null)
            {
                textureResource.Dispose();
            }

            constantMatrixBuffer.Dispose();

            sampleState.Dispose();
            constantMatrixBuffer.Dispose();
            inputLayout.Dispose();
            pixelShader.Dispose();
            vertexShader.Dispose();
        }

        private void LoadTextureShader()
        {
            try
            {
                //Huge difference between texture sets in the resulting depth effect
                //need to find how to do normal/height maps right

                //this is ugly
                textures = new Textures(device, new TextureType[] { TextureType.GenericRockColor, TextureType.GenericRockNormalHeight });

                //textures = new Textures(device, new TextureType[] { TextureType.Stones, TextureType.Stones_NormalMap, TextureType.Stones_HeightMap });
                //textures = new Textures(device, new TextureType[] { TextureType.TestColorMap, TextureType.TestNormalMap, TextureType.TestHeightMap });

                //textures = new Textures(device, new TextureType[] { TextureType.Wall, TextureType.Wall_NS, TextureType.Wall_HS });
                //textures = new Textures(device, new TextureType[] { TextureType.Wall, TextureType.Dirt });
                //textures = new Textures(device, new TextureType[] { TextureType.Wall });

                TextureCollection = textures.Select(item => item).ToArray();

                SamplerStateDescription textureSamplerDescription = new SamplerStateDescription()
                {
                    Filter = Filter.MinMagMipLinear,

                    AddressU = TextureAddressMode.Wrap,
                    AddressV = TextureAddressMode.Wrap,
                    AddressW = TextureAddressMode.Wrap,

                    MipLodBias = 0,
                    ComparisonFunction = Comparison.Always,
                    BorderColor = Color.Green,
                    MinimumLod = 0,
                    MaximumLod = 0
                };

                sampleState = new SamplerState(device, textureSamplerDescription);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not initialize texture shader: " + ex);
            }
        }

        private Buffer GetConstantMatrixBuffer<T>(Device device) where T : struct
        {
            // Setup the description of the dynamic matrix constant buffer that is in the vertex shader.
            try
            {
                var matrixBufferDesc = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    //or size of constant buffer
                    SizeInBytes = Utilities.SizeOf<T>(),
                    BindFlags = BindFlags.ConstantBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };
                return new Buffer(device, matrixBufferDesc);

            }
            catch (Exception ex)
            {
                throw new Exception("BufferDescription: " + ex);
            }
        }

        //falling most times
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
