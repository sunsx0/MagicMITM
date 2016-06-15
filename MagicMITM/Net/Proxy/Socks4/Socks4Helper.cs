using System;
using System.IO;

namespace MagicMITM.Net.Proxy.Socks4
{
    class Socks4Helper
    {
        public static Socks4Packet GetRequest(byte[] buffer)
        {
            if (buffer == null)
                throw new ArgumentNullException("buffer");

            var rtnVal = new Socks4Packet();

            using (var reader = new BinaryReader(new MemoryStream(buffer)))
            {
                rtnVal.Read(reader);
            }

            return rtnVal;
        }

        public static byte[] GetResponse(Socks4Packet response)
        {
            if (response == null)
                throw new ArgumentNullException("response");

            var buffer = new byte[8];

            using (var ms = new MemoryStream(buffer))
            {
                using (var writer = new BinaryWriter(ms))
                {
                    response.Write(writer);
                }
            }

            return buffer;
        }
    }
}
