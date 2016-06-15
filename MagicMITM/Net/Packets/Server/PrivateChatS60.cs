using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Server
{
    [PacketIdentifier(0x60, PacketType.ServerPacket)]
    public class PrivateChatS60 : GamePacket
    {
        public byte Channel;
        public byte Emotion;
        public string SrcName;
        public uint SrcRoleId;
        public string DstName;
        public uint DstRoleId;
        public string Message;
        public byte[] Data;
        public int SrcLevel;

        public override DataStream Deserialize(DataStream ds)
        {
            Channel = ds.ReadByte();
            Emotion = ds.ReadByte();
            SrcName = ds.ReadUnicodeString();
            SrcRoleId = ds.ReadUInt32();
            DstName = ds.ReadUnicodeString();
            DstRoleId = ds.ReadUInt32();
            Message = ds.ReadUnicodeString();
            Data = ds.ReadBytes();
            SrcLevel = ds.ReadInt32();

            return base.Deserialize(ds);
        }
        public override DataStream Serialize(DataStream ds)
        {
            ds.
                Write(Channel).
                Write(Emotion).
                WriteUnicodeString(SrcName).
                Write(SrcRoleId).
                WriteUnicodeString(DstName).
                Write(DstRoleId).
                WriteUnicodeString(Message).
                Write(Data, true).
                Write(SrcLevel);

            return base.Serialize(ds);
        }
    }
}
