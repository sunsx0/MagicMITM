using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Client
{
    [PacketIdentifier(0x74, PacketType.ClientPacket)]
    public class GetCustomDataC74 : GamePacket
    {
        public uint RoleId;
        public uint UnkId;
        public uint[] Roles;

        public override DataStream Deserialize(DataStream ds)
        {
            RoleId = ds.ReadUInt32();
            UnkId = ds.ReadUInt32();
            Roles = new uint[(int)ds.ReadCompactUInt32()];
            for(var i = 0; i < Roles.Length; i++)
            {
                Roles[i] = ds.ReadUInt32();
            }

            return base.Deserialize(ds);
        }
        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(RoleId);
            ds.Write(UnkId);
            if (Roles != null)
            {
                ds.WriteCompactUInt32(Roles.Length);
                foreach (var role in Roles)
                {
                    ds.Write(role);
                }
            }
            else
            {
                ds.WriteCompactUInt32(0);
            }
            return base.Serialize(ds);
        }
    }
}
