using System;

namespace Ch01_01EmptyProject
{
    interface ID3D : IDisposable
    {
        void DrawScene();
        void PresentRenderedScene();
    }
}
