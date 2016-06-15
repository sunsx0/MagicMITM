using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.Data;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Server
{
    [PacketIdentifier(0x39F, PacketType.ServerPacket)]
    public class PShopList_ReS39F : GamePacket
    {
        public uint UnkId;
        public PShopEntry[] Entries;

        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(UnkId);
            ds.Write(Entries);
            return base.Serialize(ds);
        }
        public override DataStream Deserialize(DataStream ds)
        {
            UnkId = ds.ReadUInt32();
            Entries = ds.ReadArray<PShopEntry>();

            return base.Deserialize(ds);
        }
    }
}
