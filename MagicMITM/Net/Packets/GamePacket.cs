using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;
using MagicMITM.Data;

namespace MagicMITM.Net.Packets
{
    public class GamePacket
    {
        private byte[] data;
        public virtual DataStream Serialize(DataStream ds)
        {
            if (data != null && data.Length > 0)
            {
                ds.PushBack(data);
            }
            return ds;
        }
        public virtual DataStream Deserialize(DataStream ds)
        {
            var count = ds.Count - ds.Position;
            if (count > 0)
            {
                data = new byte[count];
                Buffer.BlockCopy(ds.Buffer, ds.Position, data, 0, count);
            }
            return ds;
        }

        // Extenssions
        private static Type gamePacketType = typeof(GamePacket);
        private static Type packetIdType = typeof(PacketIdentifier);

        public static bool IsGamePacket(Type type)
        {
            return type.IsSubclassOf(gamePacketType);
        }
        private static void ValidateGamePacket(Type type)
        {
            if (!IsGamePacket(type))
            {
                throw new Exception("Is no packet");
            }
        }
        
        public static PacketIdentifier GetOnePacketIdentifier<T>() where T : GamePacket
        {
            return GetOnePacketIdentifier(typeof(T));
        }
        public static PacketIdentifier GetOnePacketIdentifier(GamePacket gamePacket)
        {
            return GetOnePacketIdentifier(gamePacket.GetType());
        }
        public static PacketIdentifier GetOnePacketIdentifier(Type type)
        {
            ValidateGamePacket(type);

            var identifiers = GamePacket.GetPacketIdentifiers(type);
            /*
            if (identifiers.Length != 1)
            {
                throw new Exception("Can't select one packet identifier");
            }
            */

            return identifiers[0];
        }
        public static PacketIdentifier[] GetPacketIdentifiers<T>() where T : GamePacket
        {
            return GetPacketIdentifiers(typeof(T));
        }
        public static PacketIdentifier[] GetPacketIdentifiers(GamePacket gamePacket)
        {
            return GetPacketIdentifiers(gamePacket.GetType());
        }
        public static PacketIdentifier[] GetPacketIdentifiers(Type type)
        {
            var attributes = type.GetCustomAttributes(typeof(PacketIdentifier), false);
            return (attributes as PacketIdentifier[]);
        }
    }
}