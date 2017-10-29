using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Processing.Helpers
{
    public static class TimeManagerHelper
    {
        private const int INTERVAL = 1000;
        private static Timer _timer = new Timer(INTERVAL);

        public static void StartTimer(this Action action, bool start)
        {
            _timer.Elapsed += (sender, e) => action();

            if (start)
            {
                _timer.Start();
            }
            else
            {
                _timer.Stop();
            }
        }
    }
}
