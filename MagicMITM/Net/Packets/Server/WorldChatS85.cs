using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Server
{
    [PacketIdentifier(0x85, PacketType.ServerPacket)]
    public class WorldChatS85 : GamePacket
    {
        public byte Channel;
        public byte Emotion;
        public uint RoleId;
        public string Name
        {
            get
            {
                return Encoding.Unicode.GetString(NameBytes);
            }
            set
            {
                NameBytes = Encoding.Unicode.GetBytes(value);
            }
        }
        public string Message
        {
            get
            {
                return Encoding.Unicode.GetString(MessageBytes);
            }
            set
            {
                MessageBytes = Encoding.Unicode.GetBytes(value);
            }
        }
        public byte[] Data;

        public byte[] NameBytes;
        public byte[] MessageBytes;

        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(Channel);
            ds.Write(Emotion);
            ds.Write(RoleId);
            ds.Write(NameBytes, true);
            ds.Write(MessageBytes, true);
            ds.Write(Data, true);

            return base.Serialize(ds);
        }
        public override DataStream Deserialize(DataStream ds)
        {
            Channel = ds.ReadByte();
            Emotion = ds.ReadByte();
            RoleId = ds.ReadUInt32();
            NameBytes = ds.ReadBytes();
            MessageBytes = ds.ReadBytes();
            Data = ds.ReadBytes();

            return base.Deserialize(ds);
        }
    }
}
