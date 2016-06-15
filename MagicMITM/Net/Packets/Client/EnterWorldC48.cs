using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.Data;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Client
{
    [PacketIdentifier(0x48, PacketType.ClientPacket)]
    public class EnterWorldC48 : GamePacket
    {
        public EnterWorldC48()
        {

        }
        public EnterWorldC48(uint roleId)
        {
            RoleId = roleId;
        }

        public uint RoleId;

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
