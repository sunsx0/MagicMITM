using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Server
{
    [PacketIdentifier(0xFD, PacketType.ServerContainer)]
    public class SilverInfoSFD : GamePacket
    {
        public int SilverCount;

        public override DataStream Deserialize(DataStream ds)
        {
            SilverCount = ds.ReadInt32();
            return base.Deserialize(ds);
        }
        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(SilverCount);
            return base.Serialize(ds);
        }
    }
}
