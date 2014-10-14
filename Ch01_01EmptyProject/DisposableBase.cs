using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch01_01EmptyProject
{
    // base for proper implementing of dispose pattern (althought due to unclarifications not used)
    // http://msdn.microsoft.com/en-US/library/fs2xkftw%28v=vs.110%29.aspx
    // http://msdn.microsoft.com/en-us/library/b1yfkh5e%28v=vs.110%29.aspx
    
    public abstract class DisposableBase : IDisposable
    {
        bool disposed = false;
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;
            if (disposing)
            {
                //free managed objects    DisposeManagedObjects
            }

            //free any unmanaged objects here   DisposeUnmanagedObjects
            disposed = true;
         }
    }
}
