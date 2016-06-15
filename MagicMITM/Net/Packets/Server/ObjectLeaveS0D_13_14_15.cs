using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;
using MagicMITM.Data;

namespace MagicMITM.Net.Packets.Server
{
    [PacketIdentifier(0x0D, PacketType.ServerContainer)]
    [PacketIdentifier(0x13, PacketType.ServerContainer)]
    [PacketIdentifier(0x14, PacketType.ServerContainer)]
    [PacketIdentifier(0x15, PacketType.ServerContainer)]
    public class ObjectLeaveS0D_13_14_15 : GamePacket
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
