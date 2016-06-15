using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Data
{
    public class GGuildBase : DataSerializer
    {
        public uint GuildId;
        public string GuildName;

        public byte GuildLevel;
        public short MembersCount;

        public override DataStream Serialize(DataStream ds)
        {
            return ds.
                Write(GuildId).
                WriteUnicodeString(GuildName).

                Write(GuildLevel).
                Write(MembersCount);
        }
        public override DataStream Deserialize(DataStream ds)
        {
            GuildId = ds.ReadUInt32();
            GuildName = ds.ReadUnicodeString();

            GuildLevel = ds.ReadByte();
            MembersCount = ds.ReadInt16();

            return base.Deserialize(ds);
        }
    }
}
