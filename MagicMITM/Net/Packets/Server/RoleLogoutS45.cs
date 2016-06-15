using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.Data;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Server
{
    [PacketIdentifier(0x45, PacketType.ServerPacket)]
    public class RoleLogoutS45 : GamePacket
    {
        public uint StatusCode;
        public uint RoleId;
        public uint ProviderLinkId;
        public uint ConnectionId;

        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(StatusCode);
            ds.Write(RoleId);
            ds.Write(ProviderLinkId);
            ds.Write(ConnectionId);

            return base.Serialize(ds);
        }
        public override DataStream Deserialize(DataStream ds)
        {
            StatusCode = ds.ReadUInt32();
            RoleId = ds.ReadUInt32();
            ProviderLinkId = ds.ReadUInt32();
            ConnectionId = ds.ReadUInt32();

            return base.Deserialize(ds);
        }

    }
}
