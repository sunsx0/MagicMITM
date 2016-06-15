using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.Data;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Server
{
    [PacketIdentifier(0x09, PacketType.ServerContainer)]
    public class NpcListS09 : GamePacket
    {
        public NpcWorldInfo[] NpcList;
        
        public override DataStream Deserialize(DataStream ds)
        {
            NpcList = ds.ReadArray<NpcWorldInfo>(ds.ReadInt16());
            ds.Reset();
            return base.Deserialize(ds);
        }
    }
}
