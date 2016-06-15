using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Client
{
    [PacketIdentifier(0x5A, PacketType.ClientPacket)]
    public class KeepAliveC5A : GamePacket
    {
        public byte Code = 0x5A;
        public override DataStream Deserialize(DataStream ds)
        {
            Code = ds.ReadByte();
            return base.Deserialize(ds);
        }
        public override DataStream Serialize(DataStream ds)
        {
            return base.Serialize(ds.PushBack(Code));
        }
    }
}
