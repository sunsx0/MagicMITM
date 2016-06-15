using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Server
{
    [PacketIdentifier(0x75, PacketType.ServerPacket)]
    public class GetCustomData_ReS75 : GamePacket
    {
        public uint RetCode;
        public uint RoleId;
        public uint UnkId;
        public uint CusRoleId;
        public byte[] CustomData;
        public override DataStream Deserialize(DataStream ds)
        {
            RetCode = ds.ReadUInt32();
            RoleId = ds.ReadUInt32();
            UnkId = ds.ReadUInt32();
            CusRoleId = ds.ReadUInt32();
            CustomData = ds.ReadBytes();

            return base.Deserialize(ds);
        }
        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(RetCode);
            ds.Write(RoleId);
            ds.Write(UnkId);
            ds.Write(CusRoleId);
            ds.Write(CustomData, true);

            return base.Serialize(ds);
        }
    }
}
