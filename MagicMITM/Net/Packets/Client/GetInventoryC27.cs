using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Client
{
    [PacketIdentifier(0x27, PacketType.ClientContainer)]
    public class GetInventoryC27 : GamePacket
    {
        public GetInventoryC27()
        {

        }
        public GetInventoryC27(bool f1, bool f2, bool f3)
        {

        }

        public bool Flag1;
        public bool Flag2;
        public bool Flag3;

        public override DataStream Deserialize(DataStream ds)
        {
            Flag1 = ds.ReadBoolean();
            Flag2 = ds.ReadBoolean();
            Flag3 = ds.ReadBoolean();
            return base.Deserialize(ds);
        }
        public override DataStream Serialize(DataStream ds)
        {
            ds.
                Write(Flag1).
                Write(Flag2).
                Write(Flag3);
            return base.Serialize(ds);
        }
    }
}
