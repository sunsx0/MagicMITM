using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Data
{
    public class FriendForm : DataSerializer
    {
        /// <summary>
        /// (8:00-12:00) (12:00-18:00) (18:00-24:00) (-)
        /// 0-15
        /// </summary>
        public ushort Time { get; set; }
        /// <summary>
        /// (задания) (данжи) (пвп) (пейзажи) (общение)
        /// 0-31
        /// </summary>
        public ushort Love { get; set; }
        /// <summary>
        /// (спорт) (книги) (путишествия) (еда) (интернет) (музыка) (фильмы) (тв) (игры)
        /// 0-511
        /// </summary>
        public ushort Hobbies { get; set; }
        /// <summary>
        /// 1+ : (<18) (18-22) (23-27) (28-33) (>33)
        /// 1-4
        /// </summary>
        public byte Age { get; set; }
        /// <summary>
        /// 1+ : знаки зодиака
        /// 1-12
        /// </summary>
        public byte Sign { get; set; }
        /// <summary>
        /// (наставник) (друзья) (супруга)
        /// 0-7
        /// </summary>
        public ushort Search { get; set; }

        public override DataStream Deserialize(DataStream ds)
        {
            Time = ds.ReadUInt16();
            Love = ds.ReadUInt16();
            Hobbies = ds.ReadUInt16();
            Age = ds.ReadByte();
            Sign = ds.ReadByte();
            Search = ds.ReadUInt16();

            return base.Deserialize(ds);
        }
        public override DataStream Serialize(DataStream ds)
        {
            return ds.
                Write(Time).
                Write(Love).
                Write(Hobbies).
                Write(Age).
                Write(Sign).
                Write(Search);
        }
    }
}
