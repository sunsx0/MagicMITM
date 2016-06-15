using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Client
{
    [PacketIdentifier(0x23, PacketType.ClientContainer)]
    public class OpenNpcDialogC23  : GamePacket
    {
        public OpenNpcDialogC23()
        {

        }
        public OpenNpcDialogC23(uint worldId)
        {
            WorldId = worldId;
        }

        public uint WorldId;

        public override DataStream Deserialize(DataStream ds)
        {
            WorldId = ds.ReadUInt32();
            return base.Deserialize(ds);
        }
        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(WorldId);
            return base.Serialize(ds);
        }
    }
}
