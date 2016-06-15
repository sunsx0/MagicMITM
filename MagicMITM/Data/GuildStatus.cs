using System;
using System.Collections.Generic;
using System.Text;
using MagicMITM.IO;
using MagicMITM.Net;

namespace MagicMITM.Data
{
    public class GuildStatus : DataSerializer
    {
        public byte Status { get; set; }

        public override string ToString()
        {
            switch (Status)
            {
                case 2: return "Мастер";
                case 3: return "Маршал";
                case 4: return "Майор";
                case 5: return "Капитан";
                case 6: return "Член";
                default: return "null";
            }
        }

        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(Status);
            return base.Serialize(ds);
        }
        public override DataStream Deserialize(DataStream ds)
        {
            Status = ds.ReadByte();
            return base.Deserialize(ds);
        }
    }
}
