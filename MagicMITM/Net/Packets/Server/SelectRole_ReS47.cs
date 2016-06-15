using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Server
{
    [PacketIdentifier(0x47, PacketType.ServerPacket)]
    public class SelectRole_ReS47 : GamePacket
    {
        public bool Unk1;
        public uint Unk2;

        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(Unk1);
            ds.Write(Unk2);
            return base.Serialize(ds);
        }
        public override DataStream Deserialize(DataStream ds)
        {
            Unk1 = ds.ReadBoolean();
            Unk2 = ds.ReadUInt32();

            return base.Deserialize(ds);
        }
    }
}
