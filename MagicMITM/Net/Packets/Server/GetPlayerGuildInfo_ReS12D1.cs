using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.Data;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Server
{
    [PacketIdentifier(0x12D1, PacketType.ServerPacket)]
    public class GetPlayerGuildInfo_ReS12D1 : GamePacket
    {
        public uint RoleId;
        public uint UnkId;
        public string Name;
        public uint GuildId;

        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(RoleId);
            ds.Write(UnkId);
            ds.WriteUnicodeString(Name);
            ds.Write(GuildId);

            return base.Serialize(ds);
        }
        public override DataStream Deserialize(DataStream ds)
        {
            RoleId = ds.ReadUInt32();
            UnkId = ds.ReadUInt32();
            ds.ReadUInt32();
            Name = ds.ReadUnicodeString();
            GuildId = ds.ReadUInt32();

            return base.Deserialize(ds);
        }
    }
}
