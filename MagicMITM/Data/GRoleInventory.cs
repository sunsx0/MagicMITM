using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Data
{
    public class GRoleInventory : DataSerializer
    {
        public uint Id;
        public int Position;
        public int Count;
        public int MaxCount;
        public byte[] Data;
        public uint ProcType;
        public UnixTime ExpireDate;
        public uint Guid1;
        public uint Guid2;
        public uint Mask;

        public override DataStream Serialize(DataStream ds)
        {
            return ds.
                Write(Id).
                Write(Position).
                Write(Count).
                Write(MaxCount).
                Write(Data, true).
                Write(ProcType).
                Write(ExpireDate).
                Write(Guid1).
                Write(Guid2).
                Write(Mask);
        }
        public override DataStream Deserialize(DataStream ds)
        {
            Id = ds.ReadUInt32();
            Position = ds.ReadInt32();
            Count = ds.ReadInt32();
            MaxCount = ds.ReadInt32();
            Data = ds.ReadBytes();
            ProcType = ds.ReadUInt32();
            ExpireDate = ds.Read<UnixTime>();
            Guid1 = ds.ReadUInt32();
            Guid2 = ds.ReadUInt32();
            Mask = ds.ReadUInt32();

            return base.Deserialize(ds);
        }
    }
}
