using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Ch01_01EmptyProject
{
    class SystemTime
    {
        Timer renderLoop;

        public delegate bool RenderDelegate();
        private RenderDelegate Render;
        public delegate void ExitDelegate();
        private ExitDelegate Exit;
        private object lockObject = new Object();
        private bool IsStopped = false;

        public void Initialize(RenderDelegate render, ExitDelegate exit)
        {
            renderLoop = new Timer(1);
            Render = render;
            Exit = exit;

            renderLoop.Elapsed += renderLooop_Elapsed;
        }

        private void renderLooop_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (IsStopped)
                return;

            var result = true;
            lock (this)
            {
                result = Render();
            }
            if (!result)
                Stop();
        }

        private void Stop()
        {

            if (!IsStopped)
            {
                IsStopped = true;
                renderLoop.Stop();
                Exit();
            }
        }

        public void Run()
        {
            IsStopped = false;
            renderLoop.Start();
        }
 }
}
