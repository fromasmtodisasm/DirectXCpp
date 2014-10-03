using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;

namespace Ch01_01EmptyProject
{
    public class GameTimer
    {
        private double deltaTime;
        private bool stopped;
        private TimerTick tt;

        public double DeltaTime
        {
            get { return deltaTime; }
            set { deltaTime = value; }
        }
        public GameTimer()
        {
            tt = new TimerTick();
        }

        private void Start()
        {
            throw new NotImplementedException();
        }

        private void Stop()
        {
            throw new NotImplementedException();
        }

        private void Tick()
        {
            if (stopped)
            {
                deltaTime = 0.0;
                return;
            }
            tt.Tick();
        }

        public void Reset() { throw new NotImplementedException(); }
    }
}
