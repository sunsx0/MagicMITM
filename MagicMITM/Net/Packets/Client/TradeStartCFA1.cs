using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;
using MagicMITM.Data;

namespace MagicMITM.Net.Packets.Client
{
    [PacketIdentifier(0xFA1, PacketType.ClientPacket)]
    public class TradeStartCFA1 : GamePacket
    {
        public TradeStartCFA1()
        {

        }
        public TradeStartCFA1(uint partnerId)
        {
            PartnerId = partnerId;
        }

        public uint RoleId;
        public uint Localsid;
        public uint PartnerId;

        public override DataStream Deserialize(DataStream ds)
        {
            RoleId = ds.ReadUInt32();
            Localsid = ds.ReadUInt32();
            PartnerId = ds.ReadUInt32();
            return base.Deserialize(ds);
        }
        public override DataStream Serialize(DataStream ds)
        {
            ds.
                Write(RoleId).
                Write(Localsid).
                Write(PartnerId);
            return base.Serialize(ds);
        }
    }
}
