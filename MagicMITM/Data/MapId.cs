using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Data
{
    public class MapId : DataSerializer
    {

        public static short[] zones_old =
        {
            00, 01, 02, 03, 04, 05, 06, 07, 08, 09,
            10, 12, 11, 18, 13, 14, 15, 16, 17, 19,
            20, 21, 22, 23, 24, 25, 31, 26, 33, 27,
            34, 28, 29, 30, 32, 35, 38, 39, 40, 35,
            36, 41, 42, 43, 44, 46, 45, 47, 48, 49,
            50, 51, 52, 53, 54, 55, 56, 57, 58, 59
        };
        public static short[] zones =
        {
            00, 01, 02, 03, 04, 05, 06, 07, 08, 09,
            10, 11, 12, 13, 14, 15, 16, 17, 18, 19,
            20, 21, 22, 23, 24, 25, 26, 27, 28, 29,
            30, 31, 32, 33, 34, 35, 36, 37, 38, 39,
            40, 41, 42, 43, 44, 46, 45, 47, 48, 49,
            50, 51, 52, 53, 54, 55, 56, 57, 58, 59
        };
        static string[] names =
        {
            "Null",
            "Замерзшие земли",
            "Ледяной путь",
            "Ущелье лавин",
            "Лесной хребет",
            "Древний путь",
            "Роковой город",
            "Город истоков",
            "Великая стена",
            "Равнина побед",
            "Город мечей",
            "Сломанные горы",
            "Крепость-компас",
            "Светлые горы",
            "Деревня огня",
            "Перечный луг",
            "Равнина ветров",
            "Поселок ветров",
            "Изумрудный лес",
            "Земли драконов",
            "Город оборотней",
            "Шелковые горы",
            "Портовый город",
            "Город драконов",
            "Пахучий склон",
            "Плато заката",
            "Река Риошу",
            "Длинный откос",
            "Безопасный путь",
            "Небесной озеро",
            "Небесный скалы",
            "Долина орхидей",
            "Персиковый док",
            "Высохшее море",
            "Горы лебедя",
            "Город Перьев",
            "Тренога Юй-Вана",
            "Бездушная топь",
            "Туманная чаща",
            "Поле костей",
            "Южные горы",
            "Белые горы",
            "Черные горы",
            "Горы мечтателей",
            "Порт мечты",
            "Остров Рваных облаков",
            "Остров Разбитой мечты",
            "Город Цунами",
            "Дол иллюзий",
            "Деревня Падающих",
            "Город Единства",
            "Поляна снежного",
            "Лунная гавань"
        };

        public MapId()
        {

        }
        public MapId(short id)
        {
            Id = id;
        }

        public short Id;
        public short RealId
        {
            get
            {
                return zones[Id];
            }
        }

        public override DataStream Deserialize(DataStream ds)
        {
            Id = ds.ReadInt16();
            return base.Deserialize(ds);
        }
        public override DataStream Serialize(DataStream ds)
        {
            return ds.Write(Id);
        }

        public override string ToString()
        {
            return ToString(zones);
        }
        public string ToString(short[] zones)
        {
            if (Id < 0 || Id >= zones.Length) return "null";
            return names[zones[Id]];
        }
    }
}
