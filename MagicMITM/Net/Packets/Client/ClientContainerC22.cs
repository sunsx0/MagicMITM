using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Client
{
    [PacketIdentifier(0x22, PacketType.ClientPacket)]
    public class ClientContainerC22 : GamePacket
    {
        private static BasePacket emptyPacket = new BasePacket();

        public uint PacketId { get; set; }
        public GamePacket Packet { get; set; }

        public ClientContainerC22() : this(0, emptyPacket)
        {

        }
        public ClientContainerC22(uint packetId) : this(packetId, emptyPacket)
        {

        }
        public ClientContainerC22(GamePacket gamePacket)
        {
            var packetId = GamePacket.GetOnePacketIdentifier(gamePacket);
            if (packetId.PacketType != PacketType.ClientContainer)
            {
                throw new ArgumentException("gamePacket is not a client container");
            }
            PacketId = packetId.PacketId;
            Packet = gamePacket;
        }
        public ClientContainerC22(uint packetId, GamePacket gamePacket)
        {
            PacketId = packetId;
            Packet = gamePacket;
        }
        public override DataStream Deserialize(DataStream ds)
        {
            ds.SaveEndianness();
            ds.IsLittleEndian = true;


            var size = ds.ReadCompactUInt32();

            PacketId = ds.ReadUInt16();
            Packet = new BasePacket(ds.ReadBytes((int)(size - 2)));

            ds.RestoreEndianness();
            return ds;
        }
        public override DataStream Serialize(DataStream ds)
        {
            ds.SaveEndianness();
            ds.IsLittleEndian = true;

            var cc = ds.Count;

            Packet.Serialize(ds);
            ds.PushFront(EndianBitConverter.Little.GetBytes((ushort)PacketId));
            ds.PushFront(EndianBitConverter.Little.GetCompactUInt32Bytes((ushort)(ds.Count - cc)));

            ds.RestoreEndianness();

            return ds;
        }
    }
}
