using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Server
{
    [PacketIdentifier(0xDC, PacketType.ServerPacket)]
    public class ChatRoomCreate_ReSDC : GamePacket
    {
        public ushort RetCode;
        public ushort RoomId;
        public string Title;
        public ushort Capacity;
        public byte Status;
        public uint UnkId;

        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(RetCode);
            ds.Write(RoomId);
            ds.WriteUnicodeString(Title);
            ds.Write(Capacity);
            ds.Write(Status);
            ds.Write(UnkId);
            return base.Serialize(ds);
        }
        public override DataStream Deserialize(DataStream ds)
        {
            RetCode = ds.ReadUInt16();
            RoomId = ds.ReadUInt16();
            Title = ds.ReadUnicodeString();
            Capacity = ds.ReadUInt16();
            Status = ds.ReadByte();
            UnkId = ds.ReadUInt32();
            return base.Deserialize(ds);
        }
    }
}
