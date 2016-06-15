using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;
using MagicMITM.Data;

namespace MagicMITM.Net.Packets.Client
{
    [PacketIdentifier(0x12CE, PacketType.ClientPacket)]
    public class GetGuildBaseInfoC12CE : GamePacket
    {
        private static uint[] emptyGuilds = { };

        public GetGuildBaseInfoC12CE() : this(emptyGuilds)
        {

        }
        public GetGuildBaseInfoC12CE(params uint[] guildsIds)
        {
            GuildsIds = guildsIds;
        }

        public uint RoleId;
        public uint UnkId;
        public uint[] GuildsIds;

        public override DataStream Deserialize(DataStream ds)
        {
            RoleId = ds.ReadUInt32();
            UnkId = ds.ReadUInt32();

            var count = (int)ds.ReadCompactUInt32();
            GuildsIds = new uint[count];
            for (var i = 0; i < count; i++)
            {
                GuildsIds[i] = ds.ReadUInt32();
            }

            return base.Deserialize(ds);
        }
        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(RoleId).Write(UnkId);
            ds.WriteCompactUInt32(GuildsIds.Length);
            foreach(var guildId in GuildsIds)
            {
                ds.Write(guildId);
            }

            return base.Serialize(ds);
        }
    }
}
