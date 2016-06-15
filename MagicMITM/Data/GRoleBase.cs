using System;
using System.Text;
using MagicMITM.IO;
using System.Linq;

namespace MagicMITM.Data
{
    public class GRoleBase : DataSerializer
    {
        private static byte[] threeBytes = { 0x00, 0x00, 0x00 };

        public byte Version;
        public uint Id;
        public string Name;
        public int Race;
        public Occupation Occupation;
        public Gender Gender;
        public byte[] Custom_data;
        public byte[] Config_data;
        public uint Custom_stamp;
        public byte Status;
        public UnixTime Delete_time;
        public UnixTime Create_time;
        public UnixTime Lastlogin_time;
        public GRoleForbid[] Forbid;
        public byte[] Help_states;
        public uint Spouse;
        public uint Userid;
        public int Reserved1;

        public override DataStream Serialize(DataStream ds)
        {
            return ds.
                Write(Version).

                Write(Id).
                WriteUnicodeString(Name).

                Write(Race).
                Write(threeBytes, false).
                Write(Occupation).
                Write(Gender).

                Write(Custom_data, true).
                Write(Config_data, true).
                Write(Custom_stamp).

                Write(Status).

                Write(Delete_time).
                Write(Create_time).
                Write(Lastlogin_time).

                Write(Forbid).

                Write(Help_states, true).

                Write(Spouse).

                Write(Userid).
                Write(Reserved1);
        }
        public override DataStream Deserialize(DataStream ds)
        {
            Version = ds.ReadByte();

            Id = ds.ReadUInt32();
            Name = ds.ReadUnicodeString();

            Race = ds.ReadInt32();
            Occupation = ds.Skip(3).Read<Occupation>();
            Gender = ds.Read<Gender>();

            Custom_data = ds.ReadBytes();
            Config_data = ds.ReadBytes();
            Custom_stamp = ds.ReadUInt32();

            Status = ds.ReadByte();

            Delete_time = ds.Read<UnixTime>();
            Create_time = ds.Read<UnixTime>();
            Lastlogin_time = ds.Read<UnixTime>();

            Forbid = ds.ReadArray<GRoleForbid>();

            Help_states = ds.ReadBytes();

            Spouse = ds.ReadUInt32();

            Userid = ds.ReadUInt32();
            Reserved1 = ds.ReadInt32();
            return ds;
        }
    }
}