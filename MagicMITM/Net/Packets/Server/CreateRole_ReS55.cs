using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.Data;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Server
{
    [PacketIdentifier(0x55, PacketType.ServerPacket)]
    public class CreateRole_ReS55 : GamePacket
    {
        public CreateRoleResultCode ResultCode;
        public uint AccountId;
        public uint UnkId;
        public RoleInfo Role;

        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(ResultCode);
            ds.Write(AccountId);
            ds.Write(UnkId);
            ds.Write(Role);
            return base.Serialize(ds);
        }
        public override DataStream Deserialize(DataStream ds)
        {
            ResultCode = ds.Read<CreateRoleResultCode>();
            AccountId = ds.ReadUInt32();
            UnkId = ds.ReadUInt32();
            Role = ds.Read<RoleInfo>();

            return base.Deserialize(ds);
        }
    }
}
