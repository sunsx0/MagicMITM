using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Server
{
    [PacketIdentifier(0x12C3, PacketType.ServerPacket)]
    public class GuildChatS12C3 : GamePacket
    {
        public byte Type;
        public byte Emotion;
        public uint RoleId;
        public string Message;
        public byte[] ItemData;
        public uint Unk = 0;

        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(Type);
            ds.Write(Emotion);
            ds.Write(RoleId);
            ds.WriteUnicodeString(Message);
            ds.Write(ItemData, true);
            ds.Write(Unk);
            return base.Serialize(ds);
        }
        public override DataStream Deserialize(DataStream ds)
        {
            Type = ds.ReadByte();
            Emotion = ds.ReadByte();
            RoleId = ds.ReadUInt32();
            Message = ds.ReadUnicodeString();
            ItemData = ds.ReadBytes();
            Unk = ds.ReadUInt32();

            return base.Deserialize(ds);
        }
    }
}
