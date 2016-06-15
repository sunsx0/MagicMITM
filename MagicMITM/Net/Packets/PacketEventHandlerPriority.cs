using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagicMITM.Net.Packets
{
    public struct PacketEventHandlerPriority : IComparable<PacketEventHandlerPriority>
    {
        public int Id { get; set; }
        public int Priority { get; set; }
        public PacketEventHandler Handler { get; set; }

        public PacketEventHandlerPriority(int id, int priority, PacketEventHandler handler)
        {
            Id = id;
            Priority = priority;
            Handler = handler;
        }

        public int CompareTo(PacketEventHandlerPriority other)
        {
            var res = Priority.CompareTo(other.Priority);
            if (res == 0) res = Id.CompareTo(other.Id);

            return res;
        }
    }
}
