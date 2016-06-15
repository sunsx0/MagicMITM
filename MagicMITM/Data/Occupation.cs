using MagicMITM.IO;

namespace MagicMITM.Data
{
    public class Occupation : DataSerializer
    {
        public byte OccupationId { get; set; }

        private static Occupation warrior      = new Occupation(0);
        private static Occupation mage         = new Occupation(1);
        private static Occupation shaman       = new Occupation(2);
        private static Occupation druid        = new Occupation(3);
        private static Occupation warewolf     = new Occupation(4);
        private static Occupation assassin     = new Occupation(5);
        private static Occupation archer       = new Occupation(6);
        private static Occupation priest       = new Occupation(7);
        private static Occupation seeker       = new Occupation(8);
        private static Occupation mystic       = new Occupation(9);
        private static Occupation duskblade    = new Occupation(10);
        private static Occupation stormbringer = new Occupation(11);

        public static Occupation Warrior      { get { return warrior; } }
        public static Occupation Mage         { get { return mage; } }
        public static Occupation Shaman       { get { return shaman; } }
        public static Occupation Druid        { get { return druid; } }
        public static Occupation Warewolf     { get { return warewolf; } }
        public static Occupation Assassin     { get { return assassin; } }
        public static Occupation Archer       { get { return archer; } }
        public static Occupation Priest       { get { return priest; } }
        public static Occupation Seeker       { get { return seeker; } }
        public static Occupation Mystic       { get { return mystic; } }
        public static Occupation Duskblade    { get { return duskblade; } }
        public static Occupation Stormbringer { get { return stormbringer; } }

        /*
            Duskblade = 10,
    Stormbringer = 11,
        */

        public Occupation()
        {
        }
        public Occupation(byte occupation)
        {
            OccupationId = occupation;
        }


        static string[] toString =
        {
            "Воин", "Маг", "Шаман", "Друид", "Оборотень",
            "Убийца", "Лучник", "Жрец", "Страж", "Мистик",
            "Призрак", "Жнец"
        };
        static string[] toShortString =
        {
            "Вар", "Маг", "Шам", "Дру", "Обор",
            "Син", "Лук", "Жрец", "Сик", "Мист",
            "Призрак", "Жнец"
        };
        public override string ToString()
        {
            if (OccupationId >= 0 && OccupationId < toString.Length)
            {
                return toString[OccupationId];
            }
            else
            {
                return "Unknown";
            }
        }
        public string ToShortString()
        {
            if (OccupationId >= 0 && OccupationId < toShortString.Length)
            {
                return toShortString[OccupationId];
            }
            else
            {
                return "Unk";
            }
        }

        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(OccupationId);
            return base.Serialize(ds);
        }
        public override DataStream Deserialize(DataStream ds)
        {
            OccupationId = ds.ReadByte();
            return base.Deserialize(ds);
        }
    }
}
