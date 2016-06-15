using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;
using MagicMITM.Data;

namespace MagicMITM.Net.Packets.Server
{
    [PacketIdentifier(0x53, PacketType.ServerPacket)]
    public class RoleList_ReS53 : GamePacket
    {
        public int Unk1;
        public int NextSlot;
        public uint AccountID;
        public uint UnkID;
        public bool IsChar;
        public RoleInfo Role;

        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(Unk1);
            ds.Write(NextSlot);
            ds.Write(AccountID);
            ds.Write(UnkID);
            ds.Write(IsChar);

            if(IsChar)
            {
                ds.Write(Role);
            }
            return base.Serialize(ds);
        }

        public override DataStream Deserialize(DataStream ds)
        {
            Unk1 = ds.ReadInt32();
            NextSlot = ds.ReadInt32();
            AccountID = ds.ReadUInt32();
            UnkID = ds.ReadUInt32();

            IsChar = ds.ReadBoolean();
            if (IsChar)
            {
                Role = ds.Read<RoleInfo>();
            }

            return base.Serialize(ds);
        }
    }
}
