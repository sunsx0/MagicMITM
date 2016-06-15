using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Data
{
    public class RoleStatsInfo : DataSerializer
    {
        public int FreeStats;  // Свободные статы(сила, кон и тд)
        public int AttackLevel;  // Уровень атаки
        public int DefLevel;  // Уровень защиты
        public int CriticalChance;  // Крит. шанс
        public int CriticalBonus;  // (Атака * (200+this) / 100)(?)
        public int Invisibility;  // Уровень скрытности
        public int ViewInvisibility;  // Уровень обнаружения
        public int MobAttack;  // Урон монстрам
        public int MobDef;  // Защита от монстров
        public int Con;  // Выносливость
        public int Int;  // Итнеллект
        public int Str;  // Сила
        public int Dex;  // Ловкость
        public int HP;
        public int HPMax;
        public int MP;
        public int MPMax; 
        public int HPRegen; 
        public int MPRegen; 
        public float WalkSpeed;  // Ходьба
        public float RunSpeed;  // Бег
        public float SwimSpeed;  // Плавание
        public float FlySpeed;  // Полет
        public int Accuracy;  // Меткость
        public int PhysAttackLow; 
        public int PhysAttackHigh; 
        public int AttackSpeed;  // 20/this
        public float AttackDist; 
        public int MetalAttackMin;  // Всегда 0
        public int WoodAttackMin;  // Всегда 0
        public int WaterAttackMin;  // Всегда 0
        public int FireAttackMin;  // Всегда 0
        public int EarthAttackMin;  // Всегда 0
        public int MetalAttackMax;  // Всегда 0
        public int WoodAttackMax;  // Всегда 0
        public int WaterAttackMax;  // Всегда 0
        public int FireAttackMax;  // Всегда 0
        public int EarthAttackMax;  // Всегда 0
        public int MagAttackMin; 
        public int MagAttackMax; 
        public int MetalDef; 
        public int WoodDef; 
        public int WaterDef; 
        public int FireDef; 
        public int EarthDef; 
        public int PhysDef; 
        public int Evasion;  // Уклон
        public int Vigor;  // Макс. чи

        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(FreeStats);
            ds.Write(AttackLevel);
            ds.Write(DefLevel);
            ds.Write(CriticalChance);
            ds.Write(CriticalBonus);
            ds.Write(Invisibility);
            ds.Write(ViewInvisibility);
            ds.Write(MobAttack);
            ds.Write(MobDef);
            ds.Write(Con);
            ds.Write(Int);
            ds.Write(Str);
            ds.Write(Dex);
            ds.Write(HP);
            ds.Write(HPMax);
            ds.Write(MP);
            ds.Write(MPMax);
            ds.Write(HPRegen);
            ds.Write(MPRegen);
            ds.Write(WalkSpeed);
            ds.Write(RunSpeed);
            ds.Write(SwimSpeed);
            ds.Write(FlySpeed);
            ds.Write(Accuracy);
            ds.Write(PhysAttackLow);
            ds.Write(PhysAttackHigh);
            ds.Write(AttackSpeed);
            ds.Write(AttackDist);
            ds.Write(MetalAttackMin);
            ds.Write(WoodAttackMin);
            ds.Write(WaterAttackMin);
            ds.Write(FireAttackMin);
            ds.Write(EarthAttackMin);
            ds.Write(MetalAttackMax);
            ds.Write(WoodAttackMax);
            ds.Write(WaterAttackMax);
            ds.Write(FireAttackMax);
            ds.Write(EarthAttackMax);
            ds.Write(MagAttackMin);
            ds.Write(MagAttackMax);
            ds.Write(MetalDef);
            ds.Write(WoodDef);
            ds.Write(WaterDef);
            ds.Write(FireDef);
            ds.Write(EarthDef);
            ds.Write(PhysDef);
            ds.Write(Evasion);
            ds.Write(Vigor);
            return base.Serialize(ds);
        }
        public override DataStream Deserialize(DataStream ds)
        {
            FreeStats = ds.ReadInt32();

            AttackLevel = ds.ReadInt32();
            DefLevel = ds.ReadInt32();

            CriticalChance = ds.ReadInt32();
            CriticalBonus = ds.ReadInt32();

            Invisibility = ds.ReadInt32();
            ViewInvisibility = ds.ReadInt32();

            MobAttack = ds.ReadInt32();
            MobDef = ds.ReadInt32();

            Con = ds.ReadInt32();
            Int = ds.ReadInt32();
            Str = ds.ReadInt32();
            Dex = ds.ReadInt32();

            HPMax = ds.ReadInt32();
            MPMax = ds.ReadInt32();
            HPRegen = ds.ReadInt32();
            MPRegen = ds.ReadInt32();

            WalkSpeed = ds.ReadSingle();
            RunSpeed = ds.ReadSingle();
            SwimSpeed = ds.ReadSingle();
            FlySpeed = ds.ReadSingle();

            Accuracy = ds.ReadInt32();

            PhysAttackLow = ds.ReadInt32();
            PhysAttackHigh = ds.ReadInt32();

            AttackSpeed = ds.ReadInt32();
            AttackDist = ds.ReadSingle();

            MetalAttackMin = ds.ReadInt32();
            WoodAttackMin = ds.ReadInt32();
            WaterAttackMin = ds.ReadInt32();
            FireAttackMin = ds.ReadInt32();
            EarthAttackMin = ds.ReadInt32();

            MetalAttackMax = ds.ReadInt32();
            WoodAttackMax = ds.ReadInt32();
            WaterAttackMax = ds.ReadInt32();
            FireAttackMax = ds.ReadInt32();
            EarthAttackMax = ds.ReadInt32();

            MagAttackMin = ds.ReadInt32();
            MagAttackMax = ds.ReadInt32();

            MetalDef = ds.ReadInt32();
            WoodDef = ds.ReadInt32();
            WaterDef = ds.ReadInt32();
            FireDef = ds.ReadInt32();
            EarthDef = ds.ReadInt32();

            PhysDef = ds.ReadInt32();

            Evasion = ds.ReadInt32();

            Vigor = ds.ReadInt32();

            return ds;
        }
    }
}
