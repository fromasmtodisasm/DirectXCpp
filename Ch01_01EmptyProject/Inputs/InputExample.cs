    ////using System;
    ////using System.Collections.Generic;
    ////using System.ComponentModel;
    ////using System.Data;
    ////using System.Drawing;
    ////using System.Linq;
    ////using System.Text;
    ////using System.Windows.Forms;
    ////using Microsoft.DirectX;
    ////using Microsoft.DirectX.Direct3D;
    ////using Microsoft.DirectX.DirectInput;
     
    ////namespace DX_Tut
    ////{
    ////    public partial class frmMain : Form
    ////    {
    ////        private const float FOV = (float)Math.PI / 4.0f;
    ////        private const float ASPECT_RATIO = 4.0f / 3.0f;
    ////        private const float NEAR_Z = 1.0f;
    ////        private const float FAR_Z = 10000.0f;
     
    ////        Microsoft.DirectX.Direct3D.Device Device;
    ////        DisplayMode Display_Mode;
    ////        PresentParameters Screen;
     
    ////        Microsoft.DirectX.DirectInput.Device Mouse_Device;
    ////        MouseState Mouse_State;
    ////        Vector2 Mouse;
     
    ////        Microsoft.DirectX.DirectInput.Device Keyboard_Device;
    ////        KeyboardState Keyboard_State;
     
    ////        CustomVertex.PositionColored[] Vertex_List = new CustomVertex.PositionColored[4];
     
    ////        Matrix View_Matrix;
    ////        Matrix Projection_Matrix;
     
    ////        Vector3 Camera_Position;
    ////        Vector3 Camera_Angle;
     
    ////        bool Fullscreen_Enabled;
    ////        bool Running;
     
    ////        private void DirectX_Initialize()
    ////        {
    ////            Screen = new PresentParameters();
    ////            if (Fullscreen_Enabled == true)
    ////            {
    ////                Display_Mode.Width = 800;
    ////                Display_Mode.Height = 600;
    ////                Display_Mode.Format = Format.R5G6B5;
    ////                Screen.Windowed = false;
    ////                Screen.BackBufferCount = 1;
    ////                Screen.BackBufferWidth = Display_Mode.Width;
    ////                Screen.BackBufferHeight = Display_Mode.Height;
    ////            }
    ////            else
    ////            {
    ////                Screen.Windowed = true;
    ////            }
    ////            Screen.SwapEffect = SwapEffect.Copy;
    ////            Screen.BackBufferFormat = Display_Mode.Format;
    ////            Screen.AutoDepthStencilFormat = DepthFormat.D16;
    ////            Screen.EnableAutoDepthStencil = true;
     
    ////            Device = new Microsoft.DirectX.Direct3D.Device(0, Microsoft.DirectX.Direct3D.DeviceType.Hardware, this.Handle, CreateFlags.SoftwareVertexProcessing, Screen);
    ////        }
     
    ////        private void DirectInput_Initialize_Mouse()
    ////        {
    ////            Mouse_Device = new Microsoft.DirectX.DirectInput.Device(SystemGuid.Mouse);
    ////            Mouse_Device.SetDataFormat(DeviceDataFormat.Mouse);
    ////            Mouse_Device.SetCooperativeLevel(this, CooperativeLevelFlags.Background | CooperativeLevelFlags.NonExclusive);
    ////            Mouse_Device.Acquire();
    ////        }
     
    ////        private void Mouse_Controls()
    ////        {
    ////            Mouse_State = Mouse_Device.CurrentMouseState;
     
    ////            Mouse.X += Mouse_State.X;
    ////            Mouse.Y += Mouse_State.Y;
     
    ////            if (Mouse.X > this.Width) Mouse.X = this.Width;
    ////            if (Mouse.X < 0) Mouse.X = 0;
     
    ////            if (Mouse.Y > this.Height) Mouse.Y = this.Height;
    ////            if (Mouse.Y < 0) Mouse.Y = 0;
     
    ////            Camera_Angle.X += Mouse_State.Y;
    ////            Camera_Angle.Y += Mouse_State.X;
     
    ////            if (Camera_Angle.X >= 90) Camera_Angle.X = 90;
    ////            if (Camera_Angle.X <= -90) Camera_Angle.X = -90;
    ////        }
     
    ////        private void DirectInput_Initialize_Keyboard()
    ////        {
    ////            Keyboard_Device = new Microsoft.DirectX.DirectInput.Device(SystemGuid.Keyboard);
    ////            Keyboard_Device.SetDataFormat(DeviceDataFormat.Keyboard);
    ////            Keyboard_Device.SetCooperativeLevel(this, CooperativeLevelFlags.Background | CooperativeLevelFlags.NonExclusive);
    ////            Keyboard_Device.Acquire();
    ////        }
     
    ////        private bool DirectInput_Key_State(Microsoft.DirectX.DirectInput.Key Key_Code)
    ////        {
    ////            Keyboard_State = Keyboard_Device.GetCurrentKeyboardState();
    ////            return Keyboard_State[Key_Code];
    ////        }
     
    ////        private void Keyboard_Controls()
    ////        {
    ////            const int Camera_Speed = 5;
     
    ////            if (DirectInput_Key_State(Key.W)) //Move Forward
    ////            {
    ////                Camera_Position.X -= (float)Math.Sin(Camera_Angle.Y * Math.PI / 180) * Camera_Speed;
    ////                Camera_Position.Z -= (float)Math.Cos(Camera_Angle.Y * Math.PI / 180) * Camera_Speed;
    ////            }
     
    ////            if (DirectInput_Key_State(Key.S)) //Move Backward
    ////            {
    ////                Camera_Position.X += (float)Math.Sin(Camera_Angle.Y * Math.PI / 180) * Camera_Speed;
    ////                Camera_Position.Z += (float)Math.Cos(Camera_Angle.Y * Math.PI / 180) * Camera_Speed;
    ////            }
     
    ////            if (DirectInput_Key_State(Key.A)) //Move Left
    ////            {
    ////                Camera_Position.X += (float)Math.Cos(Camera_Angle.Y * Math.PI / 180) * Camera_Speed;
    ////                Camera_Position.Z -= (float)Math.Sin(Camera_Angle.Y * Math.PI / 180) * Camera_Speed;
    ////            }
     
    ////            if (DirectInput_Key_State(Key.D)) //Move Right
    ////            {
    ////                Camera_Position.X -= (float)Math.Cos(Camera_Angle.Y * Math.PI / 180) * Camera_Speed;
    ////                Camera_Position.Z += (float)Math.Sin(Camera_Angle.Y * Math.PI / 180) * Camera_Speed;
    ////            }
     
    ////            if (DirectInput_Key_State(Key.Q)) //Look Left
    ////                Camera_Angle.Y -= 1;
     
    ////            if (DirectInput_Key_State(Key.E)) //Look Right
    ////                Camera_Angle.Y += 1;
     
    ////            if (DirectInput_Key_State(Key.R)) //Look Up
    ////                Camera_Angle.X -= 1;
     
    ////            if (DirectInput_Key_State(Key.F)) //Look Down
    ////                Camera_Angle.X += 1;
     
    ////            if (DirectInput_Key_State(Key.C)) //Move Up
    ////                Camera_Position.Y -= Camera_Speed;
     
    ////            if (DirectInput_Key_State(Key.Z)) //Move Down
    ////                Camera_Position.Y += Camera_Speed;
    ////        }
     
    ////        private void Settings()
    ////        {
    ////            Device.SetRenderState(RenderStates.ZEnable, true);
    ////            Device.SetRenderState(RenderStates.ZBufferWriteEnable, true);
    ////            Device.SetRenderState(RenderStates.CullMode, (int)Cull.None);
    ////            Device.SetRenderState(RenderStates.Lighting, false);
    ////        }
     
    ////        private Vector3 Create_Vertex(float X, float Y, float Z)
    ////        {
    ////            Vector3 Vertex;
     
    ////            Vertex.X = X;
    ////            Vertex.Y = Y;
    ////            Vertex.Z = Z;
     
    ////            return Vertex;
    ////        }
     
    ////        private CustomVertex.PositionColored Create_Custom_Vertex(float X, float Y, float Z, int Color)
    ////        {
    ////            CustomVertex.PositionColored Vertex = new CustomVertex.PositionColored();
    ////            Vertex.Position = new Vector3(X, Y, Z);
    ////            Vertex.Color = Color;
     
    ////            return Vertex;
    ////        }
     
    ////        private void Setup_Matrices()
    ////        {
    ////            View_Matrix = Matrix.LookAtRH(Create_Vertex(Camera_Position.X, Camera_Position.Y, Camera_Position.Z), Create_Vertex(0, 0, 0), Create_Vertex(0, 1, 0));
    ////            Device.Transform.View = View_Matrix;
     
    ////            Projection_Matrix = Matrix.PerspectiveFovRH(FOV, ASPECT_RATIO, NEAR_Z, FAR_Z);
    ////            Device.Transform.Projection = Projection_Matrix;
    ////        }
     
    ////        private void Camera_Control()
    ////        {
    ////            Matrix Camera_Translation_Matrix;
    ////            Matrix Camera_Angle_Matrix_X;
    ////            Matrix Camera_Angle_Matrix_Y;
     
    ////            View_Matrix = Matrix.Identity;
    ////            Camera_Translation_Matrix = Matrix.Identity;
     
    ////            Camera_Translation_Matrix = Matrix.Translation(Camera_Position.X, Camera_Position.Y, -Camera_Position.Z);
    ////            View_Matrix = Matrix.Multiply(View_Matrix, Camera_Translation_Matrix);
     
    ////            Camera_Angle_Matrix_Y = Matrix.RotationY((Convert.ToSingle(Math.PI) * Camera_Angle.Y) / 180);
    ////            View_Matrix = Matrix.Multiply(View_Matrix, Camera_Angle_Matrix_Y);
     
    ////            Camera_Angle_Matrix_X = Matrix.RotationX((Convert.ToSingle(Math.PI) * Camera_Angle.X) / 180);
    ////            View_Matrix = Matrix.Multiply(View_Matrix, Camera_Angle_Matrix_X);
     
    ////            Device.Transform.View = View_Matrix;
    ////        }
     
    ////        private void Create_Polygon()
    ////        {
    ////            Vertex_List[0] = Create_Custom_Vertex(-50, 100, 0, Color.FromArgb(255, 255, 255).ToArgb());
    ////            Vertex_List[1] = Create_Custom_Vertex(50, 100, 0, Color.FromArgb(255, 255, 255).ToArgb());
    ////            Vertex_List[2] = Create_Custom_Vertex(-50, 0, 0, Color.FromArgb(255, 255, 255).ToArgb());
    ////            Vertex_List[3] = Create_Custom_Vertex(50, 0, 0, Color.FromArgb(255, 255, 255).ToArgb());
    ////        }
     
    ////        private void Draw_Polygon()
    ////        {
    ////            Device.VertexFormat = CustomVertex.PositionColored.Format;
    ////            Device.DrawUserPrimitives(PrimitiveType.TriangleStrip, 2, Vertex_List);
    ////        }
     
    ////        private void Create_Platform()
    ////        {
    ////            Vertex_List[0] = Create_Custom_Vertex(-1000, 0, -1000, Color.FromArgb(255, 0, 0).ToArgb());
    ////            Vertex_List[1] = Create_Custom_Vertex(1000, 0, -1000, Color.FromArgb(0, 255, 0).ToArgb());
    ////            Vertex_List[2] = Create_Custom_Vertex(-1000, 0, 1000, Color.FromArgb(0, 0, 255).ToArgb());
    ////            Vertex_List[3] = Create_Custom_Vertex(1000, 0, 1000, Color.FromArgb(255, 0, 255).ToArgb());
    ////        }
     
    ////        private void Reset_Device()
    ////        {
    ////            if (this.WindowState != FormWindowState.Minimized && Fullscreen_Enabled == false)
    ////            {
    ////                Running = false;
    ////                Device.Reset(Screen);
    ////                Settings();
    ////                Setup_Matrices();
    ////                Application.DoEvents();
    ////                Running = true;
    ////            }
    ////        }
     
    ////        private void Render()
    ////        {
    ////            Device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.FromArgb(0, 0, 0), 1.0f, 0);
    ////            Device.BeginScene();
    ////            Create_Platform();
    ////            Draw_Polygon();
    ////            Create_Polygon();
    ////            Draw_Polygon();
    ////            Device.EndScene();
    ////            Device.Present();
    ////        }
     
    ////        private void Game_Loop()
    ////        {
    ////            do
    ////            {
    ////                Mouse_Controls();
    ////                Keyboard_Controls();
    ////                Camera_Control();
    ////                Render();
    ////                if (DirectInput_Key_State(Key.Escape)) Shutdown();
    ////                Application.DoEvents();
    ////            } while (Running == true);
    ////        }
     
    ////        private void Main()
    ////        {
    ////            if (MessageBox.Show("Click Yes to go to fullscreen (Recommended)", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
    ////                Fullscreen_Enabled = true;
     
    ////            this.Show();
    ////            this.KeyPreview = true;
    ////            this.Width = 330;
    ////            this.Height = 250;
    ////            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque, true);
    ////            this.Text = "DirectX Tutorial";
    ////            if (Fullscreen_Enabled == true)
    ////                this.FormBorderStyle = FormBorderStyle.None;
     
    ////            Camera_Position.X = 0;
    ////            Camera_Position.Y = -50;
    ////            Camera_Position.Z = 250;
     
    ////            DirectX_Initialize();
    ////            DirectInput_Initialize_Mouse();
    ////            DirectInput_Initialize_Keyboard();
    ////            Settings();
    ////            Setup_Matrices();
    ////            Running = true;
    ////        }
     
    ////        void Shutdown()
    ////        {
    ////            if (Running == true)
    ////            {
    ////                Running = false;
    ////                Mouse_Device.Unacquire();
    ////                Mouse_Device.Dispose();
    ////                Keyboard_Device.Unacquire();
    ////                Keyboard_Device.Dispose();
    ////                Device.Dispose();
    ////                Application.Exit();
    ////            }
    ////        }
     
    ////        public frmMain()
    ////        {
    ////            InitializeComponent();
    ////        }
     
    ////        private void frmMain_Load(object sender, EventArgs e)
    ////        {
    ////            Main();
    ////        }
     
    ////        private void frmMain_Paint(object sender, PaintEventArgs e)
    ////        {
    ////            Game_Loop();
    ////        }
     
    ////        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
    ////        {
    ////            Shutdown();
    ////        }
     
    ////        private void frmMain_Resize(object sender, EventArgs e)
    ////        {
    ////            Reset_Device();
    ////        }
    ////    }
    ////}