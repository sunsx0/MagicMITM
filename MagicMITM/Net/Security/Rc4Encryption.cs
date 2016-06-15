namespace MagicMITM.Net.Security
{
    public class Rc4Encryption
    {
        private readonly byte[] table;
        private byte shift1;
        private byte shift2;

        public Rc4Encryption(byte[] key)
        {
            table = new byte[256];

            for (int i = 0; i < 256; i++)
            {
                table[i] = (byte)i;
            }

            byte shift = 0;

            for (uint i = 0; i < 256; i++)
            {
                var a = key[i % key.Length];
                shift += (byte)((a + table[i]) % 256);

                var b = table[i];
                table[i] = table[shift];
                table[shift] = b;
            }
        }

        public void Encrypt(byte[] data)
        {
            Encrypt(data, 0, data.Length);
        }

        public void Encrypt(byte[] data, int offset, int count)
        {
            for (var i = 0; i < count; i++)
            {
                shift1++;
                var a = table[shift1];

                shift2 += a;
                var b = table[shift2];

                table[shift2] = a;
                table[shift1] = b;

                var c = (byte)((a + b) % 256);
                var d = table[c];

                data[i + offset] = (byte)(data[i + offset] ^ d);
            }
        }

        public void Decrypt(byte[] data)
        {
            Encrypt(data, 0, data.Length);
        }

        public void Decrypt(byte[] data, int offset, int count)
        {
            Encrypt(data, offset, count);
        }
    }
}
