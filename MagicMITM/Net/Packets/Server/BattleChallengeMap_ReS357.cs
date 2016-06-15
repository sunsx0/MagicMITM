using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.Data;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Server
{
    [PacketIdentifier(0x357, PacketType.ServerPacket)]
    public class BattleChallengeMap_ReS357 : GamePacket
    {
        public uint RoleId;
        public ushort ResultCode;
        public uint Status;
        public uint MaxBonus;
        public GBattleChallenge[] Cities;
        public int RandomNumber;
        public uint UnkId;

        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(RoleId);
            ds.Write(ResultCode);
            ds.Write(Status);
            ds.Write(MaxBonus);
            ds.Write(Cities);
            ds.Write(RandomNumber);
            ds.Write(UnkId);

            return base.Serialize(ds);
        }
        public override DataStream Deserialize(DataStream ds)
        {
            RoleId = ds.ReadUInt32();
            ResultCode = ds.ReadUInt16();
            Status = ds.ReadUInt32();
            MaxBonus = ds.ReadUInt32();
            Cities = ds.ReadArray<GBattleChallenge>();
            RandomNumber = ds.ReadInt32();
            UnkId = ds.ReadUInt32();

            return base.Deserialize(ds);
        }
    }
}
