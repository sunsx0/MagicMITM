using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Data
{
    public class GBattleChallenge : DataSerializer
    {
        public MapId MapId;
        public uint Challenger;
        public uint Deposit;
        public uint Bonus;
        public UnixTime EndTime;

        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(MapId);
            ds.Write(Challenger);
            ds.Write(Deposit);
            ds.Write(Bonus);
            ds.Write(EndTime);
            return base.Serialize(ds);
        }
        public override DataStream Deserialize(DataStream ds)
        {
            MapId = ds.Read<MapId>();
            Challenger = ds.ReadUInt32();
            Deposit = ds.ReadUInt32();
            Bonus = ds.ReadUInt32();
            EndTime = ds.Read<UnixTime>();

            return base.Deserialize(ds);
        }
    }
}
