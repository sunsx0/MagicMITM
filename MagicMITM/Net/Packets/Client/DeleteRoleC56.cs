using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Client
{
    [PacketIdentifier(0x56, PacketType.ClientPacket)]
    public class DeleteRoleC56 : GamePacket
    {
        public DeleteRoleC56() : this(0)
        {

        }
        public DeleteRoleC56(uint roleId)
        {
            RoleId = roleId;
        }

        public uint RoleId;
        public uint Unk;

        public override DataStream Deserialize(DataStream ds)
        {
            RoleId = ds.ReadUInt32();
            Unk = ds.ReadUInt32();
            return base.Deserialize(ds);
        }
        public override DataStream Serialize(DataStream ds)
        {
            ds.
                Write(RoleId).
                Write(Unk);
            return base.Serialize(ds);
        }
    }
}
