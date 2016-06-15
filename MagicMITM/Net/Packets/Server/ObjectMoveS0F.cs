using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.Data;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Server
{
    [PacketIdentifier(0x0F, PacketType.ServerContainer)]
    public class ObjectMoveS0F : GamePacket
    {
        public uint ObjectId;
        public Point3F Position;

        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(ObjectId);
            ds.Write(Position);
            return base.Serialize(ds);
        }
        public override DataStream Deserialize(DataStream ds)
        {
            ObjectId = ds.ReadUInt32();
            Position = ds.Read<Point3F>();

            return base.Deserialize(ds);
        }
    }
}
