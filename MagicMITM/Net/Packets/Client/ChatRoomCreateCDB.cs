using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;
using MagicMITM.Data;

namespace MagicMITM.Net.Packets.Client
{
    [PacketIdentifier(0xDB, PacketType.ClientPacket)]
    public class ChatRoomCreateCDB : GamePacket
    {
        public ChatRoomCreateCDB() : this(string.Empty)
        {

        }
        public ChatRoomCreateCDB(string subj) : this(subj, string.Empty)
        {

        }
        public ChatRoomCreateCDB(string subj, string password, int size = 30)
        {
            Subject = subj;
            Size = (short)size;
            Password = password;
        }

        public uint RoleId;
        public string Subject;
        public short Size;
        public string Password;
        public uint UnkId;

        public override DataStream Deserialize(DataStream ds)
        {
            RoleId = ds.ReadUInt32();
            Subject = ds.ReadUnicodeString();
            Size = ds.ReadInt16();
            Password = ds.ReadUnicodeString();
            UnkId = ds.ReadUInt32();

            return base.Deserialize(ds);
        }

        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(RoleId);
            ds.WriteUnicodeString(Subject);
            ds.Write(Size);
            ds.WriteUnicodeString(Password);
            ds.Write(UnkId);
            return base.Serialize(ds);
        }
    }
}
