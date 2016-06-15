using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.Data;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Client
{
    [PacketIdentifier(0x4F, PacketType.ClientPacket)]
    public class PublicChatC4F : GamePacket
    {
        public byte Channel;
        public byte Emotion;
        public uint RoleId;
        public uint UnkId;
        public byte[] MessageBytes;
        public byte[] Data;

        public string Message
        {
            get
            {
                if (MessageBytes == null) return string.Empty;
                return Encoding.Unicode.GetString(MessageBytes);
            }
            set
            {
                MessageBytes = Encoding.Unicode.GetBytes(string.IsNullOrEmpty(value) ? string.Empty : value);
            }
        }

        public override DataStream Deserialize(DataStream ds)
        {
            Channel = ds.ReadByte();
            Emotion = ds.ReadByte();
            RoleId = ds.ReadUInt32();
            UnkId = ds.ReadUInt32();
            MessageBytes = ds.ReadBytes();
            Data = ds.ReadBytes();
            return base.Deserialize(ds);
        }
        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(Channel);
            ds.Write(Emotion);
            ds.Write(RoleId);
            ds.Write(UnkId);
            ds.Write(MessageBytes, true);
            ds.Write(Data, true);
            return base.Serialize(ds);
        }
    }
}
