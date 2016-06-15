using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;
using MagicMITM.Data;

namespace MagicMITM.Net.Packets.Server
{
    [PacketIdentifier(0x12CF, PacketType.ServerPacket)]
    public class GetGuildBaseInfo_ReS12CF : GamePacket
    {
        public uint RoleId;
        public uint UnkId;
        public GGuildBase Guild;

        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(RoleId);
            ds.Write(UnkId);
            ds.Write(Guild);
            return base.Serialize(ds);
        }
        public override DataStream Deserialize(DataStream ds)
        {
            RoleId = ds.ReadUInt32();
            UnkId = ds.ReadUInt32();

            Guild = ds.Read<GGuildBase>();

            return base.Deserialize(ds);
        }
    }
}
