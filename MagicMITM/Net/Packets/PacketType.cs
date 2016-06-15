using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagicMITM.Net.Packets
{
    public enum PacketType : uint
    {
        // C
        ClientPacket = 0,
        // C22
        ClientContainer = 2,
        // S
        ServerPacket = 1,
        // S00-22
        ServerContainer = 3
    }
}
