using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;
using MagicMITM.Data;

namespace MagicMITM.Net.Packets.Server
{
    [PacketIdentifier(0x0C, PacketType.ServerContainer)] // enter slice
    [PacketIdentifier(0x11, PacketType.ServerContainer)] // enter world
    public class RoleEnterS0C_11 : GamePacket
    {
        public RoleWorldInfo Role;
        
        public override DataStream Deserialize(DataStream ds)
        {
            Role = ds.Read<RoleWorldInfo>();
            ds.Reset();
            return base.Deserialize(ds);
        }
    }
}
