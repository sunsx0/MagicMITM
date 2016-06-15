using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Data
{
    public class PShopEntry : DataSerializer
    {
        public uint RoleId;
        public uint ShopType;
        public UnixTime CreateTime;
        public uint InvState;

        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(RoleId);
            ds.Write(ShopType);
            ds.Write(CreateTime);
            ds.Write(InvState);
            return base.Serialize(ds);
        }
        public override DataStream Deserialize(DataStream ds)
        {
            RoleId = ds.ReadUInt32();
            ShopType = ds.ReadUInt32();
            CreateTime = ds.Read<UnixTime>();
            InvState = ds.ReadUInt32();

            return base.Deserialize(ds);
        }
    }
}
