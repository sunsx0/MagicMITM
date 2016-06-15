using System;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Data
{
    public class GRoleForbid : DataSerializer
    {
        public byte Type;
        public int Time;
        public UnixTime Createtime;
        public string Reason;

        public override DataStream Serialize(DataStream ds)
        {
            return ds.
                Write(Type).
                Write(Time).
                Write(Createtime).
                WriteUnicodeString(Reason);
        }
        public override DataStream Deserialize(DataStream ds)
        {
            Type = ds.ReadByte();
            Time = ds.ReadInt32();
            Createtime = ds.Read<UnixTime>();
            Reason = ds.ReadUnicodeString();
            return ds;
        }
    }
}