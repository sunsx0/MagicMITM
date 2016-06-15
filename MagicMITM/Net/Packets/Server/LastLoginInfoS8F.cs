using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;
using MagicMITM.Data;

namespace MagicMITM.Net.Packets.Server
{
    [PacketIdentifier(0x8F, PacketType.ServerPacket)]
    public class LastLoginInfoS8F : GamePacket
    {
        public uint AccountID;
        public uint UnkID;
        public UnixTime LastLoginTime;
        public byte[] LastLoginIP;
        public byte[] CurrentIP;

        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(AccountID);
            ds.Write(UnkID);
            ds.Write(LastLoginTime);
            ds.Write(LastLoginIP, false);
            ds.Write(CurrentIP, false);

            return base.Serialize(ds);
        }
        public override DataStream Deserialize(DataStream ds)
        {
            AccountID = ds.ReadUInt32();
            UnkID = ds.ReadUInt32();
            LastLoginTime = ds.Read<UnixTime>();
            LastLoginIP = ds.ReadBytes(4);
            CurrentIP = ds.ReadBytes(4);

            return base.Deserialize(ds);
        }

        private string IpToString(byte[] ip)
        {
            return String.Format("{0}.{1}.{2}.{3}", ip[3], ip[2], ip[1], ip[0]);
        }
    }
}
