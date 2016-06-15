using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Server
{
    [PacketIdentifier(0x77, PacketType.ServerPacket)]
    public class GetRoleIdByName_ReS77 : GamePacket
    {
        public int ResultCode;
        public uint UnkId;
        public string RoleName;
        public uint RoleId;
        public byte Reason;

        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(ResultCode);
            ds.Write(UnkId);
            ds.WriteUnicodeString(RoleName);
            ds.Write(RoleId);
            ds.Write(Reason);
            return base.Serialize(ds);
        }
        public override DataStream Deserialize(DataStream ds)
        {
            ResultCode = ds.ReadInt32();
            UnkId = ds.ReadUInt32();
            RoleName = ds.ReadUnicodeString();
            RoleId = ds.ReadUInt32();
            Reason = ds.ReadByte();

            return base.Deserialize(ds);
        }
    }
}
