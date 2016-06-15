using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagicMITM.Net.Packets.Server
{
    [PacketIdentifier(0xC2, PacketType.ServerContainer)]
    public class InvalidTargetSC2 : GamePacket
    {
    }
}
