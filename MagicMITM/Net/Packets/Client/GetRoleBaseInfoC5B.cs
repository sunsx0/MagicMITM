using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.Data;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Client
{
    [PacketIdentifier(0x5B, PacketType.ClientPacket)]
    public class GetRoleBaseInfoC5B : GamePacket
    {
        static uint[] emptyArray = { };

        public uint RoleId;
        public uint Unk;
        public uint[] OtherRoles;

        public GetRoleBaseInfoC5B()
        {
            OtherRoles = emptyArray;
        }
        public GetRoleBaseInfoC5B(params uint[] otherRoles)
        {
            OtherRoles = otherRoles;
        }


        public override DataStream Deserialize(DataStream ds)
        {
            RoleId = ds.ReadUInt32();
            Unk = ds.ReadUInt32();

            var count = (int)ds.ReadCompactUInt32();
            OtherRoles = new uint[count];
            for (var i = 0; i < count; i++)
            {
                OtherRoles[i] = ds.ReadUInt32();
            }

            return base.Deserialize(ds);
        }
        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(RoleId);
            ds.Write(Unk);
            ds.WriteCompactUInt32(OtherRoles.Length);
            foreach (var role in OtherRoles)
            {
                ds.Write(role);
            }

            return base.Serialize(ds);
        }
    }
}
