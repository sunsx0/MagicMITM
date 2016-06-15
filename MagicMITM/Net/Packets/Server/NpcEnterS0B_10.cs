using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.Data;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Server
{
    [PacketIdentifier(0x0B, PacketType.ServerContainer)]
    [PacketIdentifier(0x10, PacketType.ServerContainer)]
    public class NpcEnterS0B_10 : GamePacket
    {
        public NpcWorldInfo Npc;
        
        public override DataStream Deserialize(DataStream ds)
        {
            Npc = ds.Read<NpcWorldInfo>();
            ds.Reset();
            return base.Deserialize(ds);
        }
    }
}
