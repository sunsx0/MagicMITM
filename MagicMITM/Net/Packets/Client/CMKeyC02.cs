using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Client
{
    [PacketIdentifier(0x02, PacketType.ClientPacket)]
    public class CMKeyC02 : GamePacket
    {
        public byte[] Key;
        public bool Force;

        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(Key, true);
            ds.Write(Force);
            return ds;
        }
        public override DataStream Deserialize(DataStream ds)
        {
            Key = ds.ReadBytes();
            Force = ds.ReadBoolean();
            return ds;
        }
    }
}
