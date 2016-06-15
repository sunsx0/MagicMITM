using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Server
{
    [PacketIdentifier(0x34, PacketType.ServerContainer)]
    public class SelectTargetReS34 : GamePacket
    {
        public uint TargetId;

        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(TargetId);
            return base.Serialize(ds);
        }
        public override DataStream Deserialize(DataStream ds)
        {
            TargetId = ds.ReadUInt32();
            return base.Deserialize(ds);
        }
    }
}
