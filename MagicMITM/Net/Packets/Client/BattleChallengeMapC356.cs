using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.Data;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Client
{
    [PacketIdentifier(0x356, PacketType.ClientPacket)]
    public class BattleChallengeMapC356 : GamePacket
    {
        public uint RoleId;
        public uint GuildId;
        public uint UnkId;

        public override DataStream Deserialize(DataStream ds)
        {
            RoleId = ds.ReadUInt32();
            GuildId = ds.ReadUInt32();
            UnkId = ds.ReadUInt32();

            return base.Deserialize(ds);
        }
        public override DataStream Serialize(DataStream ds)
        {
            return base.Serialize(ds.
                Write(RoleId).
                Write(GuildId).
                Write(UnkId));
        }
    }
}
