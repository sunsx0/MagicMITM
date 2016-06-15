using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;
using MagicMITM.Data;

namespace MagicMITM.Net.Packets.Server
{
    [PacketIdentifier(0x20, PacketType.ServerContainer)]
    public class TargetInfoS20 : GamePacket
    {
        public uint RoleId;

        public short Level;

        public byte Unk1;
        public byte Unk2;

        public HpMp Hp;
        public HpMp Mp;

        public uint NextTarget;

        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(RoleId);
            ds.Write(Level);
            ds.Write(Unk1);
            ds.Write(Unk2);
            ds.Write(Hp);
            ds.Write(Mp);
            ds.Write(NextTarget);

            return base.Serialize(ds);
        }
        public override DataStream Deserialize(DataStream ds)
        {
            RoleId = ds.ReadUInt32();

            Level = ds.ReadInt16();

            Unk1 = ds.ReadByte();
            Unk2 = ds.ReadByte();

            Hp = ds.Read<HpMp>();
            Mp = ds.Read<HpMp>();

            if (ds.CanReadBytes())
            {
                NextTarget = ds.ReadUInt32();
            }

            return base.Deserialize(ds);
        }
    }
}
