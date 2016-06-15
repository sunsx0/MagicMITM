using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Data
{
    public class EquipInfo : DataSerializer
    {

        public int ID;
        public int CellID;

        public int Count;
        public int MaxCount;

        public byte[] ItemData;

        public uint ProcType;
        public UnixTime ExpireDate;

        public uint Unk1;
        public uint Unk2;

        public uint Mask;

        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(ID);
            ds.Write(CellID);
            ds.Write(Count);
            ds.Write(MaxCount);
            ds.Write(ItemData, true);
            ds.Write(ProcType);
            ds.Write(ExpireDate);
            ds.Write(Unk1);
            ds.Write(Unk2);
            ds.Write(Mask);

            return base.Serialize(ds);
        }
        public override DataStream Deserialize(DataStream ds)
        {
            ID = ds.ReadInt32();
            CellID = ds.ReadInt32();

            Count = ds.ReadInt32();
            MaxCount = ds.ReadInt32();

            ItemData = ds.ReadBytes();

            ProcType = ds.ReadUInt32();
            ExpireDate = ds.Read<UnixTime>();
            Unk1 = ds.ReadUInt32();
            Unk2 = ds.ReadUInt32();
            Mask = ds.ReadUInt32();

            return base.Deserialize(ds);
        }
    }
}
