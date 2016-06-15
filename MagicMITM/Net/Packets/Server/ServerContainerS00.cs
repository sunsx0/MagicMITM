using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets.Server
{
    [PacketIdentifier(0x00, PacketType.ServerPacket)]
    public class ServerContainerS00 : GamePacket
    {
        public ServerContainerS00()
        {
            Packets = new List<PacketEventArgs>();
        }
        public ServerContainerS00(IEnumerable<GamePacket> packets)
        {
            var args = packets.Select(x => new PacketEventArgs(GamePacket.GetOnePacketIdentifier(x), x));
            Packets = new List<PacketEventArgs>(args);
        }
        public ServerContainerS00(IEnumerable<PacketEventArgs> packets)
        {
            Packets = new List<PacketEventArgs>(packets);
        }

        public List<PacketEventArgs> Packets { get; set; }

        public override DataStream Serialize(DataStream ds)
        {
            var temp = new DataStream();

            foreach(var packetArgs in Packets)
            {
                if (packetArgs.Cancel) continue;

                temp.Clear();

                var packet = packetArgs.Packet;
                var packetId = packetArgs.PacketId;
                if (packetId.PacketType == PacketType.ServerContainer)
                {
                    temp.IsLittleEndian = true;

                    packet.Serialize(temp);
                    temp.PushFront(EndianBitConverter.Little.GetBytes((ushort)packetId.PacketId));
                    temp.PushFront(EndianBitConverter.Little.GetCompactUInt32Bytes((ushort)(temp.Count)));

                    ds.WriteCompactUInt32(0x22);
                    ds.Write(temp);
                }
                else
                {
                    temp.IsLittleEndian = false;

                    packet.Serialize(temp);

                    ds.WriteCompactUInt32(packetId.PacketId);
                    ds.Write(temp);
                }
            }
            return ds;
        }
        public override DataStream Deserialize(DataStream ds)
        {
            ds.SaveEndianness();

            ds.IsLittleEndian = true;
            while (ds.CanReadBytes(1))
            {
                var packetId = ds.ReadCompactUInt32();
                var packetLength = (int)ds.ReadCompactUInt32();

                PacketIdentifier containerId;
                byte[] buffer;

                if (packetId == 0x22)
                {
                    var length = ds.ReadCompactUInt32();

                    containerId = new PacketIdentifier(ds.ReadUInt16(), PacketType.ServerContainer);
                    buffer = ds.ReadBytes((int)(length - 2));
                }
                else
                {
                    containerId = new PacketIdentifier(packetId, PacketType.ServerPacket);
                    buffer = ds.ReadBytes(packetLength);
                }

                var packet = new BasePacket(buffer);

                Packets.Add(new PacketEventArgs(containerId, packet));
            }

            ds.RestoreEndianness();

            return base.Deserialize(ds);
        }
    }
}
