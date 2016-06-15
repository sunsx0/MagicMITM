using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Data
{
    public class PShopBase : DataSerializer
    {
        public uint RoledId;
        public uint ShopType;

        public PShopItem[] BuyList;
        public PShopItem[] SellList;

        public override DataStream Serialize(DataStream ds)
        {
            return ds.
                Write(RoledId).
                Write(ShopType).
                Write(BuyList).
                Write(SellList);
        }
        public override DataStream Deserialize(DataStream ds)
        {
            RoledId = ds.ReadUInt32();
            ShopType = ds.ReadUInt32();

            BuyList = ds.ReadArray<PShopItem>();
            SellList = ds.ReadArray<PShopItem>();

            return base.Deserialize(ds);
        }
    }
}
