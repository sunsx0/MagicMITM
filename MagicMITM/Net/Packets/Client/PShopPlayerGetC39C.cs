using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.Data;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Client
{
    [PacketIdentifier(0x39C, PacketType.ClientPacket)]
    public class PShopPlayerGetC39C : GamePacket
    {
        public PShopPlayerGetC39C()
        {

        }
        public PShopPlayerGetC39C(uint otherRoleId)
        {
            OtherRoleId = otherRoleId;
        }

        public uint RoleId;
        public uint OtherRoleId;

        public override DataStream Deserialize(DataStream ds)
        {
            RoleId = ds.ReadUInt32();
            OtherRoleId = ds.ReadUInt32();
            return base.Deserialize(ds);
        }
        public override DataStream Serialize(DataStream ds)
        {
            ds.
                Write(RoleId).
                Write(OtherRoleId);
            return base.Serialize(ds);
        }
    }
}
