using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.Data;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Server
{
    [PacketIdentifier(0x04, PacketType.ServerContainer)]
    public class RolesListS04 : GamePacket
    {
        public RoleWorldInfo[] PlayersList;
        
        public override DataStream Deserialize(DataStream ds)
        {
            PlayersList = ds.ReadArray<RoleWorldInfo>(ds.ReadInt16());
            ds.Reset();

            return base.Deserialize(ds);
        }
    }
}
