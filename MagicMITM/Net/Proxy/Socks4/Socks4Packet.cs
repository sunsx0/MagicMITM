using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace MagicMITM.Net.Proxy.Socks4
{
    class Socks4Packet
    {
        public Byte Version { get; set; }
        public Socks4ServiceCode ServiceCode { get; set; }
        public Int16 Port { get; set; }
        public String Address { get; set; }
        public String Identification { get; set; }

        public Socks4Packet()
        {
        }

        public Socks4Packet(Socks4ServiceCode code)
        {
            ServiceCode = code;
        }

        public void Read(BinaryReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            Version = reader.ReadByte();
            ServiceCode = (Socks4ServiceCode)reader.ReadByte();
            Port = BitConverter.ToInt16(reader.ReadBytes(2).Reverse().ToArray(), 0);

            var addressBytes = reader.ReadBytes(4);
            Address = String.Format("{0}.{1}.{2}.{3}", addressBytes[0], addressBytes[1], addressBytes[2], addressBytes[3]);

            var identBuffer = new List<Byte>();
            byte b;

            while ((b = reader.ReadByte()) != 0x0)
            {
                identBuffer.Add(b);
            }

            Identification = Encoding.Unicode.GetString(identBuffer.ToArray());
        }

        public void Write(BinaryWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");

            writer.Write((byte)0);
            writer.Write((byte)ServiceCode);
            writer.Write((ushort)0);
            writer.Write((uint)0);
        }
    }
}
