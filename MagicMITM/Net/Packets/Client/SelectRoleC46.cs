using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.Data;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Client
{
    [PacketIdentifier(0x46, PacketType.ClientPacket)]
    public class SelectRoleC46 : GamePacket
    {
        public uint RoleId;
        public byte Unk;

        public override DataStream Deserialize(DataStream ds)
        {
            RoleId = ds.ReadUInt32();
            Unk = ds.ReadByte();
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
