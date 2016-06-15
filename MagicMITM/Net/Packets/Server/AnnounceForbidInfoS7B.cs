using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;
using MagicMITM.Data;

namespace MagicMITM.Net.Packets.Server
{
    [PacketIdentifier(0x7B, PacketType.ServerPacket)]
    public class AnnounceForbidInfoS7B : GamePacket
    {
        public uint AccountId;
        public uint Localsid;
        public GRoleForbid Forbid;
        public bool Disconnect;

        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(AccountId);
            ds.Write(Localsid);
            ds.Write(Forbid);
            ds.Write(Disconnect);

            return base.Serialize(ds);
        }
        public override DataStream Deserialize(DataStream ds)
        {
            AccountId = ds.ReadUInt32();
            Localsid = ds.ReadUInt32();
            Forbid = ds.Read<GRoleForbid>();
            Disconnect = ds.ReadBoolean();

            return base.Deserialize(ds);
        }
    }
}
