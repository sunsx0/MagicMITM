using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Server
{
    [PacketIdentifier(0x46, PacketType.ServerContainer)]
    public class NpcGreetingS46 : GamePacket
    {
        public uint WorldId;

        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(WorldId);
            return base.Serialize(ds);
        }
        public override DataStream Deserialize(DataStream ds)
        {
            WorldId = ds.ReadUInt32();
            return base.Deserialize(ds);
        }
    }
}
