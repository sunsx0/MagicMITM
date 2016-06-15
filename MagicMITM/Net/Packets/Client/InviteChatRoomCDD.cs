using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;
using MagicMITM.Data;

namespace MagicMITM.Net.Packets.Client
{
    [PacketIdentifier(0xDD, PacketType.ClientPacket)]
    public class InviteChatRoomCDD : GamePacket
    {
        public InviteChatRoomCDD()
        {

        }
        public InviteChatRoomCDD(uint roleId, ushort roomId)
        {
            OtherRoleId = roleId;
            RoomId = roomId;
        }   
        public ushort RoomId;
        public uint OtherRoleId;
        public uint RoleId;
        public uint Unk1 = 0x0000E8A8;
        public uint Unk2 = 0x00180000;
        public uint Unk3 = 0x500B;
        public uint Unk4 = 0xD7;

        public override DataStream Deserialize(DataStream ds)
        {
            RoomId = ds.ReadUInt16();
            OtherRoleId = ds.ReadUInt32();
            RoleId = ds.ReadUInt32();
            Unk1 = ds.ReadUInt32();
            Unk2 = ds.ReadUInt32();
            Unk3 = ds.ReadUInt32();
            Unk4 = ds.ReadUInt32();

            return base.Deserialize(ds);
        }
        public override DataStream Serialize(DataStream ds)
        {
            ds.
                Write(RoomId).
                Write(OtherRoleId).
                Write(RoleId).
                Write(Unk1).
                Write(Unk2).
                Write(Unk3).
                Write(Unk4);
            return base.Serialize(ds);
        }
    }
}
