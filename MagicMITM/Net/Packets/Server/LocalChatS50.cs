using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Server
{
    [PacketIdentifier(0x50, PacketType.ServerPacket)]
    public class LocalChatS50 : GamePacket
    {
        public static int Test2;
        public byte Channel;
        public byte Emotion;
        public uint RoleId;
        public byte[] MessageBytes;
        public uint Level;
        public byte[] Data;

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

        public override DataStream Deserialize(DataStream ds)
        {
            Channel = ds.ReadByte();
            Emotion = ds.ReadByte();
            RoleId = ds.ReadUInt32();
            MessageBytes = ds.ReadBytes();
            //Level = ds.ReadUInt32();
            //Data = ds.ReadBytes();
            ds.Reset();
            return base.Deserialize(ds);
        }
        /*
        public override DataStream Serialize(DataStream ds)
        {
            return base.Serialize(ds);
            ds.Write(Channel);
            ds.Write(Emotion);
            ds.Write(RoleId);
            ds.Write(MessageBytes, true);
            //ds.Write(Level);
            //ds.Write(Data, true);
            return base.Serialize(ds);
        }*/
    }
}
