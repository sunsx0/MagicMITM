using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Client
{
    [PacketIdentifier(0x48, PacketType.ClientContainer)]
    public class ChangePartyLeaderC48 : GamePacket
    {
        public uint RoleId;

        public ChangePartyLeaderC48()
        {

        }
        public ChangePartyLeaderC48(uint roleId)
        {
            RoleId = roleId;
        }

        public override DataStream Deserialize(DataStream ds)
        {
            RoleId = ds.ReadUInt32();
            return base.Deserialize(ds);
        }
        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(RoleId);
            return base.Serialize(ds);
        }
    }
}
