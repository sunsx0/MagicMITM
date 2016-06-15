using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagicMITM.Net
{
    public class Plugin
    {
        public MitmSession Session { get; internal set; }

        public virtual bool Enabled { get; set; }

        public virtual void Initialize()
        {
        }
    }
}
