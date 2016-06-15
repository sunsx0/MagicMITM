using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.Data;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Server
{
    [PacketIdentifier(0x39D, PacketType.ServerPacket)]
    public class PShopPlayerGet_ReS39D : GamePacket
    {
        public uint ResultCode;
        public uint UnkId;

        public PShopBase Shop;

        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(ResultCode);
            ds.Write(UnkId);
            ds.Write(Shop);

            return base.Serialize(ds);
        }
        public override DataStream Deserialize(DataStream ds)
        {
            ResultCode = ds.ReadUInt32();
            UnkId = ds.ReadUInt32();
            Shop = ds.Read<PShopBase>();

            return base.Deserialize(ds);
        }
    }
}
