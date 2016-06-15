using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagicMITM.Net.Packets
{
    public class PacketEventArgs : EventArgs
    {
        public PacketIdentifier PacketId { get; set; }
        public GamePacket Packet { get; set; }
        public bool Cancel { get; set; }

        public PacketEventArgs(PacketIdentifier packetId, GamePacket gamePacket)
        {
            PacketId = packetId;
            Packet = gamePacket;
        }

        public override string ToString()
        {
            return PacketId.ToString();
        }
    }
}
