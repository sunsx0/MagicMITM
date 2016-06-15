using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.Data;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Server
{
    [PacketIdentifier(0x5C, PacketType.ServerPacket)]
    public class RoleBaseInfoS5C : GamePacket
    {
        public uint ResultCode;
        public uint MyRoleId;
        public uint UnkId;
        public GRoleBase RoleBase;

        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(ResultCode);
            ds.Write(MyRoleId);
            ds.Write(UnkId);
            ds.Write(RoleBase);
            return base.Serialize(ds);
        }
        public override DataStream Deserialize(DataStream ds)
        {
            ResultCode = ds.ReadUInt32();
            MyRoleId = ds.ReadUInt32();
            UnkId = ds.ReadUInt32();
            RoleBase = ds.Read<GRoleBase>();

            return base.Deserialize(ds);
        }
    }
}
