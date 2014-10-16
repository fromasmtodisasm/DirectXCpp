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

        private float PositionX;
        private float PositionY;
        private float PositionZ;
        private float RotationX;
        private float RotationY;
        private float RotationZ;

        public Matrix ViewMatrix
        {
            get { return V; }
            set { V = value; }
        }

        public void Render()
        {
            try
            {
                PositionX = 0; PositionY = 0; PositionZ = -10;
                // Setup the position of the camera in the world.
                var position = new Vector3(PositionX, PositionY, PositionZ);

                // Setup where the camera is looking by default.
                var lookAt = new Vector3(0, 0, 1);

                // Set the yaw (Y axis), pitch (X axis), and roll (Z axis) rotations in radians.
                var pitch = RotationX * 0.0174532925f;
                var yaw = RotationY * 0.0174532925f;
                var roll = RotationZ * 0.0174532925f;

                // Create the rotation matrix from the yaw, pitch, and roll values.
                var rotationMatrix = Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                // Transform the lookAt and up vector by the rotation matrix so the view is correctly rotated at the origin.
                lookAt = Vector3.TransformCoordinate(lookAt, rotationMatrix);
                var up = Vector3.TransformCoordinate(Vector3.UnitY, rotationMatrix);

                // Translate the rotated camera position to the location of the viewer.
                lookAt = position + lookAt;

                // Finally create the view matrix from the three updated vectors.
                V = Matrix.LookAtLH(position, lookAt, up);
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
