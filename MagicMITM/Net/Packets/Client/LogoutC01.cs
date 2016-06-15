using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Client
{
    [PacketIdentifier(0x01, PacketType.ClientContainer)]
    public class LogoutC01 : GamePacket
    {
        public LogoutC01() : this(1)
        {

        }
        public LogoutC01(int logoutType)
        {
            LogoutType = logoutType;
        }

        public int LogoutType;


        public override DataStream Deserialize(DataStream ds)
        {
            LogoutType = ds.ReadInt32();
            return base.Deserialize(ds);
        }
        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(LogoutType);
            return base.Serialize(ds);
        }
    }
}
