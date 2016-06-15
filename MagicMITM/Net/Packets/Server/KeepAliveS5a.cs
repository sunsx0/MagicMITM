using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Server
{
    [PacketIdentifier(0x5A, PacketType.ServerPacket)]
    public class KeepAliveS5A : GamePacket
    {
        public byte Type = 0x5A;

        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(Type);
            return base.Serialize(ds);
        }
        public override DataStream Deserialize(DataStream ds)
        {
            Type = ds.ReadByte();
            return base.Deserialize(ds);
        }
    }
}
