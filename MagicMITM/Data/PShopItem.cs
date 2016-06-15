using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Data
{
    public class PShopItem : DataSerializer, IComparable<PShopItem>
    {
        public GRoleInventory Item;
        public uint Price;
        public int Reserved1;
        public int Reserved2;

        public override DataStream Serialize(DataStream ds)
        {
            return ds.
                Write(Item).
                Write(Price).
                Write(Reserved1).
                Write(Reserved2);
        }
        public override DataStream Deserialize(DataStream ds)
        {
            Item = ds.Read<GRoleInventory>();
            Price = ds.ReadUInt32();
            Reserved1 = ds.ReadInt32();
            Reserved2 = ds.ReadInt32();

            return base.Deserialize(ds);
        }

        public int CompareTo(PShopItem item)
        {
            return Price.CompareTo(item.Price);
        }
    }
}
