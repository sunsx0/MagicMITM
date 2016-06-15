using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.Data;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Server
{
    [PacketIdentifier(0x04, PacketType.ServerPacket)]
    public class OnlineAnnounceS04 : GamePacket
    {
        public uint AccountId;
        public uint UnkId;

        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(AccountId);
            ds.Write(UnkId);
            return base.Serialize(ds);
        }
        public override DataStream Deserialize(DataStream ds)
        {
            AccountId = ds.ReadUInt32();
            UnkId = ds.ReadUInt32();

            return base.Deserialize(ds);
        }
    }
}
