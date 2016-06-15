using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Client
{
    [PacketIdentifier(0x6A, PacketType.ClientContainer)]
    public class ShopBuyC6A : GamePacket
    {
        public uint Count;
        public uint ItemId;
        public uint ShopId;
        public uint Unk;

        public override DataStream Deserialize(DataStream ds)
        {
            Count = ds.ReadUInt32();
            ItemId = ds.ReadUInt32();
            ShopId = ds.ReadUInt32();
            Unk = ds.ReadUInt32();
            return base.Deserialize(ds);
        }
        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(Count);
            ds.Write(ItemId);
            ds.Write(ShopId);
            ds.Write(Unk);
            return base.Serialize(ds);
        }
    }
}
