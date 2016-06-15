using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Server
{
    [PacketIdentifier(0x63, PacketType.ServerContainer)]
    public class ShopBuyCompleteS63 : GamePacket
    {
        public uint ItemId;
        public uint Unk1;
        public uint Count;
        public uint SlotCount;
        public byte Unk2;
        public byte SlotId;
        public override DataStream Deserialize(DataStream ds)
        {
            ItemId = ds.ReadUInt32();
            Unk1 = ds.ReadUInt32();
            Count = ds.ReadUInt32();
            SlotCount = ds.ReadUInt32();
            Unk2 = ds.ReadByte();
            SlotId = ds.ReadByte();

            return base.Deserialize(ds);
        }
        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(ItemId);
            ds.Write(Unk1);
            ds.Write(Count);
            ds.Write(SlotCount);
            ds.Write(Unk2);
            ds.Write(SlotId);

            return base.Serialize(ds);
        }
    }
}
