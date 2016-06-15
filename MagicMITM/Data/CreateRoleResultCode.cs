using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Data
{
    public class CreateRoleResultCode : DataSerializer
    {
        public uint ResultCode;

        public override string ToString()
        {
            switch (ResultCode)
            {
                case 00: return "Персонаж успешно создан";
                case 25: return "Запрещено использовать такой ник";
                case 45: return "Ник уже используется";
                default: return "Unknown error: " + ResultCode;
            }
        }

        public override DataStream Deserialize(DataStream ds)
        {
            ResultCode = ds.ReadUInt32();
            return base.Deserialize(ds);
        }
        public override DataStream Serialize(DataStream ds)
        {
            return ds.Write(ResultCode);
        }
    }
}
