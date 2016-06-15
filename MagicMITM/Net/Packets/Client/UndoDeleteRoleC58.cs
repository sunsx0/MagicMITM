using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Client
{
    [PacketIdentifier(0x58, PacketType.ClientPacket)]
    public class UndoDeleteRoleC58 : GamePacket
    {
        public UndoDeleteRoleC58() : this(0)
        {

        }
        public UndoDeleteRoleC58(uint roleId)
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
