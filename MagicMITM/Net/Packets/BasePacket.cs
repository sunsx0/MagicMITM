using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Net.Packets
{
    public class BasePacket : GamePacket
    {
        private static byte[] emptyBuffer = { };

        public byte[] Buffer { get; private set; }

        public BasePacket() : this(emptyBuffer)
        {

        }
        public BasePacket(byte[] buffer)
        {
            if (buffer == null)
            {
                Buffer = emptyBuffer;
                return;
            }

            Buffer = buffer;
        }

        public override DataStream Serialize(DataStream ds)
        {
            return ds.PushBack(Buffer);
        }
    }
}
