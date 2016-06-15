using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.Data;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Server
{
    [PacketIdentifier(0x08, PacketType.ServerContainer)]
    public class PlayerPositionS08 : GamePacket
    {
        public int Experience;
        public int Spirit;
        public uint RoleId;
        public Point3F Position;

        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(Experience);
            ds.Write(Spirit);
            ds.Write(RoleId);
            ds.Write(Position);
            return base.Serialize(ds);
        }
        public override DataStream Deserialize(DataStream ds)
        {
            Experience = ds.ReadInt32();
            Spirit = ds.ReadInt32();
            RoleId = ds.ReadUInt32();
            Position = ds.Read<Point3F>();

            return base.Deserialize(ds);
        }
    }
}
