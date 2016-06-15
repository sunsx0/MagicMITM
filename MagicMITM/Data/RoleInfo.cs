using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Data
{
    public class RoleInfo : DataSerializer
    {
        public uint RoleId;

        public Gender Gender;
        public byte Race;
        public Occupation Occupation;

        public int Level;

        public int Unk;

        public string Name;
        public byte[] Face;

        public EquipInfo[] EquipList;

        public bool Selected;

        public UnixTime DeleteTime;
        public UnixTime CreateTime;
        public UnixTime LastOnline;

        public Point3F Position;

        public int WorldId;

        public byte[] CustomStatus;
        public byte[] CharacterMode;
        public uint RefererRole;
        public uint CashAdd;
        public byte[] ReincarnationData;
        public byte[] RealmData;

        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(RoleId);

            ds.Write(Gender);
            ds.Write(Race);
            ds.Write(Occupation);

            ds.Write(Level);
            ds.Write(Unk);

            ds.WriteUnicodeString(Name);
            ds.Write(Face, true);

            ds.Write(EquipList);
            ds.Write(Selected);

            ds.Write(DeleteTime);
            ds.Write(CreateTime);
            ds.Write(LastOnline);

            ds.Write(Position);
            ds.Write(WorldId);

            ds.Write(CustomStatus, true);
            ds.Write(CharacterMode, true);
            ds.Write(RefererRole);
            ds.Write(CashAdd);
            ds.Write(ReincarnationData, true);
            ds.Write(RealmData, true);

            return base.Serialize(ds);
        }
        public override DataStream Deserialize(DataStream ds)
        {
            RoleId = ds.ReadUInt32();
            Gender = ds.Read<Gender>();

            Race = ds.ReadByte();
            Occupation = ds.Read<Occupation>();

            Level = ds.ReadInt32();
            Unk = ds.ReadInt32();
            Name = ds.ReadUnicodeString();
            Face = ds.ReadBytes();

            EquipList = ds.ReadArray<EquipInfo>();
            Selected = ds.ReadBoolean();

            DeleteTime = ds.Read<UnixTime>();
            CreateTime = ds.Read<UnixTime>();
            LastOnline = ds.Read<UnixTime>();

            Position = ds.Read<Point3F>();
            WorldId = ds.ReadInt32();

            CustomStatus = ds.ReadBytes();
            CharacterMode = ds.ReadBytes();
            RefererRole = ds.ReadUInt32();
            CashAdd = ds.ReadUInt32();
            ReincarnationData = ds.ReadBytes();
            RealmData = ds.ReadBytes();

            return base.Deserialize(ds);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
