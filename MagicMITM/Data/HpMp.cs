using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Data
{
    public class HpMp : DataSerializer
    {
        public int Value;
        public int Max;

        public decimal Percent
        {
            get
            {
                if (Max == 0)
                {
                    return 100;
                }

                return 100 * ((decimal)Value / (decimal)Max);
            }
        }

        public override DataStream Deserialize(DataStream ds)
        {
            Value = ds.ReadInt32();
            Max = ds.ReadInt32();
            return base.Deserialize(ds);
        }
        public override DataStream Serialize(DataStream ds)
        {
            return ds.Write(Value).Write(Max);
        }
        public override string ToString()
        {
            return string.Format("{0}/{1} ({2}%)", Value, Max, Percent);
        }
    }
}
