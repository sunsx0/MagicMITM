using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Server
{
    [PacketIdentifier(0x01, PacketType.ServerPacket)]
    public class ServerInfoS01 : GamePacket
    {
        public byte[] Key;
        public byte[] ServerVersion;
        public byte AuthType;
        public string CRC;
        public byte Unk;

        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(Key, true);
            ds.Write(ServerVersion, false);
            ds.Write(AuthType);

            if (CRC != null)
            {
                ds.WriteAsciiString(CRC);
                ds.Write(Unk);
            }
            return ds;
        }
        public override DataStream Deserialize(DataStream ds)
        {
            Key = ds.ReadBytes();
            ServerVersion = ds.ReadBytes(4);
            AuthType = ds.ReadByte();

            if (ds.CanReadBytes(1))
            {
                CRC = ds.ReadAsciiString();
                if (CRC == null) CRC = string.Empty;
            }
            if (ds.CanReadBytes(1))
            {
                Unk = ds.ReadByte();
            }
            return ds;
        }
    }
}
