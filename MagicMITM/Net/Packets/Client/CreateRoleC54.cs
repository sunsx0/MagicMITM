using MagicMITM.Data;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Client
{
    [PacketIdentifier(0x54, PacketType.ClientPacket)]
    public class CreateRoleC54 : GamePacket
    {
        public CreateRoleC54() : this(new RoleInfo())
        {

        } 
        public CreateRoleC54(string name, Gender gender, Occupation occupation, byte[] face)
        {
            Role = new RoleInfo();
            Role.Name = name;
            Role.Gender = gender;
            Role.Occupation = occupation;
            Role.Face = face;
        }
        public CreateRoleC54(RoleInfo role)
        {
            Role = role;
        }

        public uint AccountId;
        public uint UnkId;
        public RoleInfo Role;
        public byte[] RefId;

        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(AccountId);
            ds.Write(UnkId);
            ds.Write(Role);
            ds.Write(RefId, true);

            return base.Serialize(ds);
        }
        public override DataStream Deserialize(DataStream ds)
        {
            AccountId = ds.ReadUInt32();
            UnkId = ds.ReadUInt32();
            Role = ds.Read<RoleInfo>();
            RefId = ds.ReadBytes();

            return base.Deserialize(ds);
        }
    }
}
