using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Data
{
    public class NpcWorldInfo : WorldObject
    {
        public uint Id;
        public uint Id2;

        public byte Direction1;
        public byte Direction2;
        public byte Direction3;

        public string Name;

        public override DataStream Deserialize(DataStream ds)
        {
            uint mask1, mask2;

            WorldId = ds.ReadUInt32();
            Id = ds.ReadUInt32();
            Id2 = ds.ReadUInt32();

            Position = ds.Read<Point3F>();

            Direction1 = ds.ReadByte();
            Direction2 = ds.ReadByte();
            Direction3 = ds.ReadByte();

            mask1 = ds.ReadUInt32();
            mask2 = ds.ReadUInt32();

            if ((mask1 & 0x40) > 0) ds.Skip(24);
            if ((mask1 & 0x1000) > 0) ds.Skip(4);
            if ((mask1 & 0x2000) > 0) Name = ds.ReadUnicodeString();
            if ((mask1 & 0x8000) > 0) ds.Skip(4);
            if ((mask1 & 0x40000000) > 0) ds.Skip(ds.ReadInt32() * 5);

            ds.Skip((int)mask2);

            return base.Deserialize(ds);
        }
    }
}
