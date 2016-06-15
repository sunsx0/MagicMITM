using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Client
{
    [PacketIdentifier(0x03, PacketType.ClientPacket)]
    public class LogginAnnounceC03 : GamePacket
    {
        public byte[] LoginBytes;
        public byte[] Hash;
        public byte Unk1;
        public byte[] Unk2;

        public string Login
        {
            get
            {
                return Encoding.ASCII.GetString(LoginBytes);
            }
            set
            {
                LoginBytes = Encoding.ASCII.GetBytes(value);
            }
        }

        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(LoginBytes, true);
            ds.Write(Hash, true);
            ds.Write(Unk1);
            if (Unk2 != null)
            {
                ds.Write(Unk2, true);
            }
            return base.Serialize(ds);
        }
        public override DataStream Deserialize(DataStream ds)
        {
            LoginBytes = ds.ReadBytes();
            Hash = ds.ReadBytes();
            if (ds.CanReadBytes())
            {
                Unk1 = ds.ReadByte();
            }
            if (ds.CanReadBytes())
            {
                Unk2 = ds.ReadBytes();
            }

            return base.Deserialize(ds);
        }
    }
}
