using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Data
{
    public class WorldObject : DataSerializer
    {
        public uint WorldId;
        public Point3F Position;

        public override int GetHashCode()
        {
            return WorldId.GetHashCode();
        }
    }
}
