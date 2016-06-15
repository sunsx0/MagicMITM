using MagicMITM.IO;

namespace MagicMITM.Data
{
    public class Gender : DataSerializer
    {
        private static Gender male = new Gender(0);
        private static Gender female = new Gender(1);

        public static Gender Male { get { return male; } }
        public static Gender Female { get { return female; } }

        public byte GenderId { get; set; }

        public Gender()
        {
        }
        public Gender(byte gender)
        {
            GenderId = gender;
        }

        public override string ToString()
        {
            return GenderId >= 1 ? "Женский" : "Мужской";
        }
        public string ToShortString()
        {
            return GenderId >= 1 ? "Ж" : "М";
        }

        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(GenderId);
            return base.Serialize(ds);
        }
        public override DataStream Deserialize(DataStream ds)
        {
            GenderId = ds.ReadByte();
            return base.Deserialize(ds);
        }
    }
}
