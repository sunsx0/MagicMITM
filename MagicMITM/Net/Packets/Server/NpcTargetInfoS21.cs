using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;
using MagicMITM.Data;

namespace MagicMITM.Net.Packets.Server
{
    [PacketIdentifier(0x21, PacketType.ServerContainer)]
    public class NpcTargetInfoS21 : GamePacket
    {
        public uint WorldId;

        public HpMp Hp;

        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(WorldId);
            ds.Write(Hp);
            return base.Serialize(ds);
        }
        public override DataStream Deserialize(DataStream ds)
        {
            WorldId = ds.ReadUInt32();

            Hp = ds.Read<HpMp>();
            return base.Deserialize(ds);
        }
    }
}
