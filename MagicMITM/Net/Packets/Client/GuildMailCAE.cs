using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.Data;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Client
{
    [PacketIdentifier(0xAE, PacketType.ClientContainer)]
    public class GuildMailCAE : GamePacket
    {
        static uint[] emptyUids = { };
        
        public GuildMailCAE() : this(emptyUids, string.Empty, string.Empty)
        {

        }
        public GuildMailCAE(uint[] uids, string title, string message)
        {
            RolesIds = uids;
            Title = title;
            Message = message;
        }

        public uint Unk1 = 2031091712;
        public byte Unk2 = 0;
        public uint RoleId;
        public uint Unk3 = 10539000;
        public string Title;
        public string Message;
        public uint[] RolesIds;
        public uint Unk4 = 1076202124;
        public uint Unk5 = 1697788;
        public uint Unk6 = 500328296;
        public uint Unk7 = 14026396;
        public byte Unk8 = 0;


        public override DataStream Deserialize(DataStream ds)
        {
            ds.SaveEndianness();
            ds.IsLittleEndian = false;


            Unk1 = ds.ReadUInt32();
            Unk2 = ds.ReadByte();
            RoleId = ds.ReadUInt32();
            Unk3 = ds.ReadUInt32();
            Title = ds.ReadUnicodeString();
            Message = ds.ReadUnicodeString();

            var count = (int)ds.ReadCompactUInt32();
            RolesIds = new uint[count];
            for (var i = 0; i < count; i++)
            {
                RolesIds[i] = ds.ReadUInt32();
            }
            Unk4 = ds.ReadUInt32();
            Unk5 = ds.ReadUInt32();
            Unk6 = ds.ReadUInt32();
            Unk7 = ds.ReadUInt32();
            Unk8 = ds.ReadByte();

            ds.RestoreEndianness();
            return base.Deserialize(ds);
        }
        public override DataStream Serialize(DataStream ds)
        {
            ds.SaveEndianness();
            ds.IsLittleEndian = false;

            ds.Write(Unk1).
                Write(Unk2).
                Write(RoleId).
                Write(Unk3).
                WriteUnicodeString(Title).
                WriteUnicodeString(Message);

            ds.Write((ushort)RolesIds.Length);
            foreach(var role in RolesIds)
            {
                ds.Write(role);
            }
            ds.Write(Unk4).
                Write(Unk5).
                Write(Unk6).
                Write(Unk7).
                Write(Unk8);

            ds.RestoreEndianness();

            return base.Serialize(ds);
        }
    }
}
