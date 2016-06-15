using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Client
{
    [PacketIdentifier(0x02, PacketType.ClientContainer)]
    public class SetTargetC02 : GamePacket
    {
        public SetTargetC02()
        {

        }
        public SetTargetC02(uint id)
        {
            Id = id;
        }
        public uint Id;

        public override DataStream Deserialize(DataStream ds)
        {
            Id = ds.ReadUInt32();
            return base.Deserialize(ds);
        }
        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(Id);
            return base.Serialize(ds);
        }
    }
}
