using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Client
{
    [PacketIdentifier(0x14, PacketType.ClientContainer)]
    public class DropMoneyC14 : GamePacket
    {
        public DropMoneyC14()
        {

        }
        public DropMoneyC14(uint money)
        {
            Money = money;
        }

        public uint Money;

        public override DataStream Deserialize(DataStream ds)
        {
            Money = ds.ReadUInt32();
            return base.Deserialize(ds);
        }
        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(Money);
            return base.Serialize(ds);
        }
    }
}
