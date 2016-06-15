using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.Data;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Client
{
    [PacketIdentifier(0x39E, PacketType.ClientPacket)]
    public class PShopListC39E : GamePacket
    {
        public PShopListC39E() : this(0xFF)
        {

        }
        public PShopListC39E(uint shopType)
        {
            ShopType = shopType;
        }

        public uint RoleId;
        public uint UnkId;
        public uint ShopType;


        public override DataStream Deserialize(DataStream ds)
        {
            RoleId = ds.ReadUInt32();
            UnkId = ds.ReadUInt32();
            ShopType = ds.ReadUInt32();

            return base.Deserialize(ds);
        }
        public override DataStream Serialize(DataStream ds)
        {
            ds.
                Write(RoleId).
                Write(UnkId).
                Write(ShopType);
            return base.Serialize(ds);
        }
    }
}
