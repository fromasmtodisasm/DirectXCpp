using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct3D11;
using SharpDX;

namespace Ch01_01EmptyProject
{
    public class Camera : IRenderable, IDisposable
    {
        private Matrix V;

        public Matrix ViewMatrix
        {
            get { return V; }
            set { V = value; }
        }

        public void Render(DeviceContext deviceContext)
        {
            try
            {
                float theta = 1.5f * (float)Math.PI;
                float phi = 0.25f * (float)Math.PI;
                float radius = 5.0f;

                //Convert spherical to cartesian coords
                float x = radius * (float)Math.Sin(phi) * (float)Math.Cos(theta);
                float y = radius * (float)Math.Cos(phi);
                float z = radius * (float)Math.Sin(phi) * (float)Math.Sin(theta);

                //Build the view matrix
                Vector3 cameraPos = new Vector3(x, y, z);
                Vector3 cameraTarget = new Vector3();
                Vector3 cameraUp = new Vector3(0.0f, 1.0f, 0.0f);

                //view matrix
                V = Matrix.LookAtLH(cameraPos, cameraTarget, cameraUp);
            }
            catch (Exception ex)
            {
                throw new Exception("D3D failed to render camera: " + ex);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls
    
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).          
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources. 
        // ~Camera() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
