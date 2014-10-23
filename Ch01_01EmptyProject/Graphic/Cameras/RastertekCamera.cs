using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct3D11;
using SharpDX;

namespace Ch01_01EmptyProject.Graphic
{
    public class RastertekCamera : IGraphicComposite
    {
        private Matrix V;
        private Vector3 position = new Vector3(0, 0, -6.0f);
        private float rotationZ;
        private float rotationY;
        private float rotationX;

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
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
            // Setup the position of the camera in the world.
            //var position = new Vector3(Position.X, Position.Y, Position.Z);

            // Setup where the camera is looking by default.
            var lookAt = new Vector3(0, 0, 1);

            // Set the yaw (Y axis), pitch (X axis), and roll (Z axis) rotations in radians.
            var pitch = rotationX * 0.0174532925f;
            var yaw = rotationY * 0.0174532925f;
            var roll = rotationZ * 0.0174532925f;

            // Create the rotation matrix from the yaw, pitch, and roll values.
            var rotationMatrix = Matrix.RotationYawPitchRoll(yaw, pitch, roll);

            // Transform the lookAt and up vector by the rotation matrix so the view is correctly rotated at the origin.
            lookAt = Vector3.TransformCoordinate(lookAt, rotationMatrix);
            var up = Vector3.TransformCoordinate(Vector3.UnitY, rotationMatrix);

            // Translate the rotated camera position to the location of the viewer.
            lookAt = position + lookAt;

            // Finally create the view matrix from the three updated vectors.
            ViewMatrix = Matrix.LookAtLH(position, lookAt, up);
            
            
            
            //// Calculate the rotation in radians.
            //var yaw = rotationY * 0.0174532925f;
            //// Setup where the camera is looking by default.
            //var lookAt = new Vector3((float)Math.Sin(yaw) + position.X, position.Y, (float)Math.Cos(yaw) + position.Z);

            //// Create the view matrix from the three vectors.
            //ViewMatrix = Matrix.LookAtLH(position, lookAt, Vector3.UnitY);

        }
        public void Dispose()
        {

        }
    }
}
