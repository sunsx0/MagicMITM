using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MagicMITM.Threading
{
    public class ActionTimer
    {
        private Timer timer;

        public ActionTimer(TimerCallback callBack) : this(callBack, null)
        {
        }
        public ActionTimer(TimerCallback callBack, object state)
        {
            timer = new Timer(callBack, state, Timeout.Infinite, Timeout.Infinite);
        }

        public void Start(int dueTime, int period)
        {
            timer.Change(dueTime, period);
        }
        public void Stop()
        {
            timer.Change(Timeout.Infinite, Timeout.Infinite);
        }
    }
}
