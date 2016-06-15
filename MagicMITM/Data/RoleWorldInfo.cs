using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Data
{
    public class RoleWorldInfo : WorldObject
    {
        public byte Angle;

        public uint GuildId;
        public GuildStatus GuildStatus;

        public override DataStream Deserialize(DataStream ds)
        {
            uint mask, mask2;

            WorldId = ds.ReadUInt32();
            Position = ds.Read<Point3F>();
            ds.Skip(4);
            Angle = ds.ReadByte();
            ds.Skip(1);
            mask = ds.ReadUInt32();
            mask2 = ds.ReadUInt32();

            if ((mask & 0x01) > 0) ds.Skip(1);
            if ((mask & 0x02) > 0) ds.Skip(1);
            if ((mask & 0x08) > 0) ds.Skip(1);
            if ((mask & 0x40) > 0) ds.Skip(24);
            if ((mask & 0x400) > 0) ds.Skip(8);
            if ((mask & 0x800) > 0)
            {
                GuildId = ds.ReadUInt32();
                GuildStatus = ds.Read<GuildStatus>();
            }
            if ((mask & 0x1000) > 0) ds.Skip(1);
            if ((mask & 0x10000) > 0) ds.ReadBytes();
            if ((mask & 0x80000) > 0) ds.Skip(6);
            if ((mask & 0x100000) > 0) ds.Skip(5);
            if ((mask & 0x800000) > 0) ds.Skip(4);
            if ((mask & 0x1000000) > 0) ds.Skip(1);
            //if ((mask & 0x4000000) > 0) ds.Skip(1);
            if ((mask & 0x8000000) > 0) ds.Skip(1);
            if ((mask & 0x10000000) > 0) ds.Skip(1);
            if ((mask & 0x20000000) > 0) ds.Skip(4);

            if ((mask2 & 0x02) > 0) ds.ReadUInt16(); // title id
            if ((mask2 & 0x04) > 0) ds.ReadByte(); // reborns count
            if ((mask2 & 0x08) > 0) ds.ReadByte(); // real m level
            if ((mask2 & 0x20) > 0) ds.ReadByte();

            return ds;
        }
    }
}
