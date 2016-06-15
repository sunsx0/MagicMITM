using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;
using MagicMITM.Data;

namespace MagicMITM.Net.Packets.Client
{
    [PacketIdentifier(0x76, PacketType.ClientPacket)]
    public class GetRoleIdByNameC76 : GamePacket
    {
        public GetRoleIdByNameC76() : this(string.Empty)
        {

        }
        public GetRoleIdByNameC76(string name)
        {
            Name = name;
        }
        public string Name;
        public uint Unk;
        public byte Reason;

        public override DataStream Deserialize(DataStream ds)
        {
            Name = ds.ReadUnicodeString();
            Unk = ds.ReadUInt32();
            Reason = ds.ReadByte();

            return base.Deserialize(ds);
        }
        public override DataStream Serialize(DataStream ds)
        {
            ds.
                WriteUnicodeString(Name).
                Write(Unk).
                Write(Reason);
            return base.Serialize(ds);
        }
    }
}
