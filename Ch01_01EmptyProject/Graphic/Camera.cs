﻿using System;
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

        public void RastertekCameraRender()
        {
            // Setup the position of the camera in the world.
            var position = new Vector3(0, 2, -11);

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
            V = Matrix.LookAtLH(position, lookAt, up);

        }

        public void Render()
        {
            try
            {
                float theta = 1.5f * (float)Math.PI;
                float phi = 0.25f * (float)Math.PI;
                float radius = 5.0f;

                //Convert spherical to cartesian coords
                float x = -2 + radius * (float)Math.Sin(phi) * (float)Math.Cos(theta);
                float y =  radius * (float)Math.Cos(phi);
                float z =  radius * (float)Math.Sin(phi) * (float)Math.Sin(theta);

                //Build the view matrix
                cameraPos = new Vector3(x, y, z);
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
