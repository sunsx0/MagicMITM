using System;
using System.Collections.Generic;
using System.Reflection;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets
{
    public class PacketsRegistry
    {
        private static Type[] emptyTypes = { };
        private static object[] emptyObjects = { };
        private static Assembly currentAssembly = Assembly.GetExecutingAssembly();

        public static PacketsRegistry Default
        {
            get
            {
                return new PacketsRegistry(currentAssembly);
            }
        }

        // ----------------------------
        private Dictionary<PacketIdentifier, Type> packets = new Dictionary<PacketIdentifier, Type>();

        public PacketsRegistry()
        {

        }
        public PacketsRegistry(Assembly assembly)
        {
            Register(assembly);
        }
        public PacketsRegistry(IEnumerable<Assembly> assemblies)
        {
            foreach(var assembly in assemblies)
            {
                Register(assembly);
            }
        }
        public PacketsRegistry(IEnumerable<Type> types)
        {
            Register(types);
        }

        public void Register(Assembly assembly)
        {
            Register(assembly.GetTypes());
        }
        public void Register(IEnumerable<Type> types)
        {
            foreach (var type in types)
            {
                Register(type);
            }
        }
        public void Register<T>() where T : GamePacket
        {
            Register(typeof(T));
        }
        public void Register(Type type)
        {
            if (GamePacket.IsGamePacket(type))
            {
                var packetIds = GamePacket.GetPacketIdentifiers(type);
                foreach (var packetId in packetIds)
                {
                    Register(type, packetId);
                }
            }
        }
        public void Register(Type type, uint packetId, PacketType packetType)
        {
            Register(type, new PacketIdentifier(packetId, packetType));
        }
        public void Register(Type type, PacketIdentifier packetId)
        {
            if (!GamePacket.IsGamePacket(type))
            {
                return;
            }

            packets[packetId] = type;
        }
        public GamePacket GetPacket(uint packetId, PacketType packetType)
        {
            return GetPacket(new PacketIdentifier(packetId, packetType));
        }
        public GamePacket GetPacket(PacketIdentifier packetId)
        {
            return GetPacketType(packetId).GetConstructor(emptyTypes).Invoke(emptyObjects) as GamePacket;
        }
        public bool TryGetPacket(uint packetId, PacketType packetType, out GamePacket gamePacket)
        {
            return TryGetPacket(new PacketIdentifier(packetId, packetType), out gamePacket);
        }
        public bool TryGetPacket(PacketIdentifier packetId, out GamePacket gamePacket)
        {
            Type type;
            if (TryGetPacketType(packetId, out type))
            {
                gamePacket = type.GetConstructor(emptyTypes).Invoke(emptyObjects) as GamePacket;
                return true;
            }
            else
            {
                gamePacket = null;
                return false;
            }
        }

        public Type GetPacketType(uint packetId, PacketType packetType)
        {
            return GetPacketType(new PacketIdentifier(packetId, packetType));
        }
        public Type GetPacketType(PacketIdentifier packetId)
        {
            Type type;
            if (!TryGetPacketType(packetId, out type))
            {
                throw new Exception("Unknown packet");
            }
            return type;
        }

        public bool TryGetPacketType(uint packetId, PacketType packetType, out Type type)
        {
            return TryGetPacketType(new PacketIdentifier(packetId, packetType), out type);
        }
        public bool TryGetPacketType(PacketIdentifier packetId, out Type type)
        {
            return packets.TryGetValue(packetId, out type);
        }

        public bool Contains(uint packetId, PacketType packetType)
        {
            return Contains(new PacketIdentifier(packetId, packetType));
        }
        public bool Contains(PacketIdentifier packetId)
        {
            return packets.ContainsKey(packetId);
        }

        public void Remove(PacketIdentifier packetId)
        {
            packets.Remove(packetId);
        }
    }
}
