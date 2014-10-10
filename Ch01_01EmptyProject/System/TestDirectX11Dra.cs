// Copyright (c) 2010-2012 SharpDX - Alexandre Mutel
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPFOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
// Adapted for COMP30019 by Jeremy Nicholson, 10 Sep 2012
//
using System;
using System.Diagnostics;
using System.Windows.Forms;

using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;
using Buffer = SharpDX.Direct3D11.Buffer;
using Device = SharpDX.Direct3D11.Device;
using MapFlags = SharpDX.Direct3D11.MapFlags;

namespace Ch01_01EmptyProject
{
    /// <summary>
    /// SharpDX MiniCube Direct3D 11 Sample
    /// with alterations for UniMelb COMP30019 Week8
    /// </summary>
    internal class D3DX11Test
    {
        // Let's make a structure for the data that's we're going to pass as uniform variables
        // to the shader --- in this case, we'll have a transformation matrix for mapping vertices
        // from world coordinates to frustum coordinates, a camera position, the intensity of an
        // ambient light source, and the position and intensity of a point light source.
        struct S_SHADER_GLOBALS
        {
            Matrix projectionMatrix;
            Vector4 cameraPosition;
            Vector4 ambientColour;
            Vector4 pointPosition;
            Vector4 pointColour;
            public S_SHADER_GLOBALS(Matrix pM, Vector4 cP, Vector4 aC, Vector4 pP, Vector4 pC)
            {
                this.projectionMatrix = pM;
                this.cameraPosition = cP;
                this.ambientColour = aC;
                this.pointPosition = pP;
                this.pointColour = pC;
            }
        }
        public void Draw()
        {
            // Form: the window in which we will show our application
            var form = new RenderForm("COMP30019 - Week 8");

            // SwapChain description: set up our double buffer
            var desc = new SwapChainDescription()
            {
                // We need one spare buffer here; if we needed more,
                // we could set a larger value
                BufferCount = 1,
                // Various properties of how the OS/Graphics card
                // will handle the window
                ModeDescription =
                    new ModeDescription(form.ClientSize.Width, form.ClientSize.Height,
                                        new Rational(60, 1), Format.R8G8B8A8_UNorm),
                IsWindowed = true,
                OutputHandle = form.Handle,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput
            };

            // Used for debugging dispose object references
            // Configuration.EnableObjectTracking = true;

            // Disable throws on shader compilation errors - hopefully
            // we won't have any of these! :)
            //Configuration.ThrowOnShaderCompileError = false;

            // Create Device and SwapChain
            Device device;
            SwapChain swapChain;
            Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.None, desc, out device, out swapChain);
            var context = device.ImmediateContext;

            // Ignore all windows events
            var factory = swapChain.GetParent<Factory>();
            factory.MakeWindowAssociation(form.Handle, WindowAssociationFlags.IgnoreAll);

            // Compile Vertex and Pixel shaders, given in the accompanying
            // shader file
            var vertexShaderByteCode = ShaderBytecode.CompileFromFile(@"C:\Users\jamesss\Documents\GitHub\DirectXCpp\Ch01_01EmptyProject\System\week8.fx", "VS", "vs_4_0");
            var vertexShader = new VertexShader(device, vertexShaderByteCode);

            var pixelShaderByteCode = ShaderBytecode.CompileFromFile(@"C:\Users\jamesss\Documents\GitHub\DirectXCpp\Ch01_01EmptyProject\System\week8.fx", "PS", "ps_4_0");
            var pixelShader = new PixelShader(device, pixelShaderByteCode);

            var signature = ShaderSignature.GetInputSignature(vertexShaderByteCode);
            // Layout from VertexShader input signature: this tells the
            // program how to interpret the shader source code
            // Note that we need to keep track of the byte offset for each
            // element (RGBA*32 bits= 4*4 bytes= 16 bytes)
            var layout = new InputLayout(device, signature, new[]
                    {
                        new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0, 0),
                        new InputElement("COLOR", 0, Format.R32G32B32A32_Float, 16, 0),
                        new InputElement("NORMAL", 0, Format.R32G32B32A32_Float, 32, 0)
                    });

            // Create a set of variables for our objects
            // 8 vertices for the cube:
            Vector4 front_bottom_left = new Vector4(-1.0f, -1.0f, -1.0f, 1.0f);
            Vector4 front_top_left = new Vector4(-1.0f, 1.0f, -1.0f, 1.0f);
            Vector4 front_top_right = new Vector4(1.0f, 1.0f, -1.0f, 1.0f);
            Vector4 front_bottom_right = new Vector4(1.0f, -1.0f, -1.0f, 1.0f);
            Vector4 back_bottom_left = new Vector4(-1.0f, -1.0f, 1.0f, 1.0f);
            Vector4 back_bottom_right = new Vector4(1.0f, -1.0f, 1.0f, 1.0f);
            Vector4 back_top_left = new Vector4(-1.0f, 1.0f, 1.0f, 1.0f);
            Vector4 back_top_right = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            // Colours:
            Vector4 red = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);
            Vector4 green = new Vector4(0.0f, 1.0f, 0.0f, 1.0f);
            Vector4 blue = new Vector4(0.0f, 0.0f, 1.0f, 1.0f);
            Vector4 yellow = new Vector4(1.0f, 1.0f, 0.0f, 1.0f);
            Vector4 magenta = new Vector4(1.0f, 0.0f, 1.0f, 1.0f);
            Vector4 cyan = new Vector4(0.0f, 1.0f, 1.0f, 1.0f);
            // Normals:
            // *** These need to be calculated for each polygon (face) ***
            Vector4 normal = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);

            // Instantiate Vertex buffer from vertex data
            var vertices = Buffer.Create(device, BindFlags.VertexBuffer, new[]
                                  {
                                      // Cube vertices:
                                      // Front face
                                      front_bottom_left, red, normal,
                                      front_top_left, red, normal,
                                      front_top_right, red, normal,
                                      front_bottom_left, red, normal,
                                      front_top_right, red, normal,
                                      front_bottom_right, red, normal,

                                      // Back face
                                      back_bottom_left, green, normal,
                                      back_top_right, green, normal,
                                      back_top_left, green, normal,
                                      back_bottom_left, green, normal,
                                      back_bottom_right, green, normal,
                                      back_top_right, green, normal,

                                      // Top face
                                      front_top_left, blue, normal,
                                      back_top_left, blue, normal,
                                      back_top_right, blue, normal,
                                      front_top_left, blue, normal,
                                      back_top_right, blue, normal,
                                      front_top_right, blue, normal,

                                      // Bottom face
                                      front_bottom_left, yellow, normal,
                                      back_bottom_right, yellow, normal,
                                      back_bottom_left, yellow, normal,
                                      front_bottom_left, yellow, normal,
                                      front_bottom_right, yellow, normal,
                                      back_bottom_right, yellow, normal,

                                      // Left face
                                      front_bottom_left, magenta, normal,
                                      back_bottom_left, magenta, normal,
                                      back_top_left, magenta, normal,
                                      front_bottom_left, magenta, normal,
                                      back_top_left, magenta, normal,
                                      front_top_left, magenta, normal,

                                      // Right face
                                      front_bottom_right, cyan, normal,
                                      back_top_right, cyan, normal,
                                      back_bottom_right, cyan, normal,
                                      front_bottom_right, cyan, normal,
                                      front_top_right, cyan, normal,
                                      back_top_right, cyan, normal,
                            });


            // Create Constant Buffer: this is how we pass the relevant constant variables
            // (the projection matrix, light sources, etc.) to the shader
            var constantBuffer = new Buffer(device, Utilities.SizeOf<S_SHADER_GLOBALS>(), ResourceUsage.Default, BindFlags.ConstantBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);



            // Prepare all the stages: 
            // Give the graphics card the shader structure
            context.InputAssembler.InputLayout = layout;
            // State how to interpret the vertices (in this case, as a
            // list of triangles
            context.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
            // Set up the relevant buffer to process the vertices
            // (primitives, fragments ...) using the shaders
            context.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(vertices, Utilities.SizeOf<Vector4>() * 3, 0));
            context.VertexShader.SetConstantBuffer(0, constantBuffer);
            context.VertexShader.Set(vertexShader);
            context.PixelShader.Set(pixelShader);

            // Prepare matrices:
            // Here we have our "eye" - it has a position, the direction
            // that we're "looking" towards, and which direction is "up"
            // Note that LookAtLH wants a 3D vector, but our shader is going to want
            // a 4D vector (in homogeneous coordinates)
            Vector3 eyePos3 = new Vector3(0.0f, 0.0f, -5.0f);
            Vector4 eyePos4 = new Vector4(0.0f, 0.0f, -5.0f, 1.0f);
            var view = Matrix.LookAtLH(eyePos3, new Vector3(0, 0, 0), Vector3.UnitY);
            // We'll just use the identity matrix as our projection matrix
            // for now - this would give us orthonormal projection, but 
            // we'll recalculate it later
            Matrix proj = Matrix.Identity;

            // Use the clock
            var clock = new Stopwatch();
            clock.Start();

            // Declare texture for rendering (even though we aren't
            // using texture here :) ); we also set up our swap chain back buffer
            bool userResized = true;
            Texture2D backBuffer = null;
            RenderTargetView renderView = null;
            Texture2D depthBuffer = null;
            DepthStencilView depthView = null;

            // Set up the light, a grey ambient light and a white positional light
            // The positional light is currently located above and behind the cube
            // (because it's + in Y and + in Z)
            Vector4 lightAmbCol = new Vector4(0.4f, 0.4f, 0.4f, 1.0f);
            Vector4 lightPntCol = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            Vector4 lightPntPos = new Vector4(1.0f, 2.0f, 3.0f, 1.0f);

            // Handle events where we resize our window
            form.UserResized += (sender, args) => userResized = true;

            // Main loop: render the objects for each frame of our scene
            RenderLoop.Run(form, () =>
            {
                // If the window was resized, we need to reorganise 
                // various properties of the window (don't worry about
                // this right now)
                if (userResized)
                {
                    // Dispose all previous allocated resources
                    //ComObject.Dispose(ref backBuffer);
                    //ComObject.Dispose(ref renderView);
                    //ComObject.Dispose(ref depthBuffer);
                    //ComObject.Dispose(ref depthView);

                    // Resize the backbuffer
                    swapChain.ResizeBuffers(desc.BufferCount, form.ClientSize.Width, form.ClientSize.Height, Format.Unknown, SwapChainFlags.None);

                    // Get the backbuffer from the swapchain
                    backBuffer = Texture2D.FromSwapChain<Texture2D>(swapChain, 0);

                    // Renderview on the backbuffer
                    renderView = new RenderTargetView(device, backBuffer);

                    // Create the depth buffer
                    depthBuffer = new Texture2D(device, new Texture2DDescription()
                    {
                        Format = Format.D32_Float_S8X24_UInt,
                        ArraySize = 1,
                        MipLevels = 1,
                        Width = form.ClientSize.Width,
                        Height = form.ClientSize.Height,
                        SampleDescription = new SampleDescription(1, 0),
                        Usage = ResourceUsage.Default,
                        BindFlags = BindFlags.DepthStencil,
                        CpuAccessFlags = CpuAccessFlags.None,
                        OptionFlags = ResourceOptionFlags.None
                    });

                    // Create the depth buffer view
                    depthView = new DepthStencilView(device, depthBuffer);

                    // Setup targets and viewport for rendering
                    var vp = new Viewport(0, 0, form.ClientSize.Width, form.ClientSize.Height, 0.0f, 1.0f);
                    
                    context.Rasterizer.SetViewports(new ViewportF[]{vp});
                    context.OutputMerger.SetTargets(depthView, renderView);

                    // Setup new projection matrix with correct aspect ratio
                    proj = Matrix.PerspectiveFovLH((float)Math.PI / 4.0f, form.ClientSize.Width / (float)form.ClientSize.Height, 0.1f, 100.0f);

                    // We are done resizing
                    userResized = false;
                }

                // Keep track of how much time has passed
                var time = clock.ElapsedMilliseconds / 1000.0f;

                // Combine the eye with the projection matrix so that we
                // can correctly transform co-ordinates into the frustum
                var viewProj = Matrix.Multiply(view, proj);

                // Clear views
                context.ClearDepthStencilView(depthView, DepthStencilClearFlags.Depth, 1.0f, 0);
                context.ClearRenderTargetView(renderView, new Color4(0.0f, 0.0f, 0.0f, 1.0f));

                // Update WorldViewProj Matrix, to account for the scene
                // transformations according to the clock
                var worldViewProj = Matrix.RotationX(time) * Matrix.RotationY(time * 2) * Matrix.RotationZ(time * .7f) * viewProj;

                // Do some tricky matrix algebra so that we handle our
                // left-handed system properly
                worldViewProj.Transpose();
                // Update our device handler with our struct containing all of the
                // constant variables for the shader
                // There are a couple of efficiency concerns here:
                // 1) We're creating a new variable every iteration through the render loop, and more importantly:
                // 2) We're updating all of the globals for every iteration through the render loop
                // Number (2) can mean that we're wasting a lot of effort re-writing variables to the graphics
                // card that haven't changed between iterations.
                S_SHADER_GLOBALS shaderGlobals = new S_SHADER_GLOBALS(worldViewProj, eyePos4, lightAmbCol, lightPntPos, lightPntCol);
                context.UpdateSubresource(ref shaderGlobals, constantBuffer);

                // Draw the cube
                context.Draw(36, 0);

                // Present! (Swap the buffers)
                swapChain.Present(0, PresentFlags.None);
            });
            // The main loop ends here - this means the window has been
            // closed, so the program can end

            // Release all resources
            signature.Dispose();
            vertexShaderByteCode.Dispose();
            vertexShader.Dispose();
            pixelShaderByteCode.Dispose();
            pixelShader.Dispose();
            vertices.Dispose();
            layout.Dispose();
            constantBuffer.Dispose();
            depthBuffer.Dispose();
            depthView.Dispose();
            renderView.Dispose();
            backBuffer.Dispose();
            context.ClearState();
            context.Flush();
            device.Dispose();
            context.Dispose();
            swapChain.Dispose();
            factory.Dispose();
        }
    }
}

