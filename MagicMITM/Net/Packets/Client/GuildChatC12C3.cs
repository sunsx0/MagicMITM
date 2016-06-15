using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.Data;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Client
{
    [PacketIdentifier(0x12C3, PacketType.ClientPacket)]
    public class GuildChatC12C3 : GamePacket
    {
        private static byte[] emptyData = { };

        public GuildChatC12C3() : this(0, string.Empty)
        {
        }
        public GuildChatC12C3(string message) : this(0, message)
        {
        }
        public GuildChatC12C3(byte type, string message)
        {
            Type = type;
            Message = message;
            ItemData = emptyData;
        }

        public byte Type;
        public byte Emotion;
        public uint RoleId;
        public string Message;
        public byte[] ItemData;
        public uint Unk = 0;

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
        public override DataStream Serialize(DataStream ds)
        {
            ds.
                Write(Type).
                Write(Emotion).
                Write(RoleId).
                WriteUnicodeString(Message).
                Write(ItemData, true).
                Write(Unk);
            return base.Serialize(ds);
        }
    }
}
