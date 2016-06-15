using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.Data;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Client
{
    [PacketIdentifier(0x52, PacketType.ClientPacket)]
    public class RoleListC52 : GamePacket
    {
        public RoleListC52()
        {

        }
        public RoleListC52(int slot)
        {
            Slot = slot;
        }

        public uint AccountId;
        public int Unk;
        public int Slot;

        public override DataStream Deserialize(DataStream ds)
        {
            AccountId = ds.ReadUInt32();
            Unk = ds.ReadInt32();
            Slot = ds.ReadInt32();

            return base.Deserialize(ds);
        }
        public override DataStream Serialize(DataStream ds)
        {
            ds.
                Write(AccountId).
                Write(Unk).
                Write(Slot);
            return base.Serialize(ds);
        }
    }
}
