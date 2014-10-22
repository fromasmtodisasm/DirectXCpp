using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct3D11;
using SharpDX;

namespace Ch01_01EmptyProject.Graphic
{
    public class Camera : IGraphicComposite
    {
        private Matrix V;
        private Vector3 cameraPos;
        private float rotationZ;
        private float rotationY;
        private float rotationX;

        public Vector3 Position
        {
            get { return cameraPos; }
            set { cameraPos = value; }
        }
        public Matrix ViewMatrix
        {
            get { return V; }
            set { V = value; }
        }

        public void SetRotation(float x, float y, float z)
        {
            rotationX = x;
            rotationY = y;
            rotationZ = z;
        }

        public void Render()
        {
            try
            {
                float theta = 1.5f * (float)Math.PI;
                float phi = 0.25f * (float)Math.PI;
                float radius = 5.0f;

                //Convert spherical to cartesian coords
                float x = -2.0f + radius * (float)Math.Sin(phi) * (float)Math.Cos(theta);
                float y = radius * (float)Math.Cos(phi);
                float z = radius * (float)Math.Sin(phi) * (float)Math.Sin(theta);

                cameraPos = new Vector3(x, y, z);
                //Build the view matrix
                Vector3 cameraTarget = new Vector3();
                Vector3 cameraUp = new Vector3(0.0f, 1.0f, 0.0f);

                //view matrix
                V = Matrix.LookAtLH(cameraPos, cameraTarget, cameraUp);
            }
            catch (Exception ex)
            {
                throw new Exception("D3D11 failed to render camera: " + ex);
            }
        }

        public void Dispose()
        {

        }
    }
}
