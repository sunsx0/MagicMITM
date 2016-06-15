using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagicMITM.Net.Security
{
    public class Encryptor
    {
        private static Encryptor defaultEncryptor = new Encryptor();
        public static Encryptor Default
        {
            get
            {
                return defaultEncryptor;
            }
        }

        public virtual bool IsEncryptor { get { return true; } }
        public virtual bool IsDecryptor { get { return true; } }
        public virtual byte[] Encrypt(byte[] data, int offset, int length)
        {
            var res = new byte[length];
            Buffer.BlockCopy(data, offset, res, 0, length);
            return res;
        }
        public virtual byte[] Decrypt(byte[] data, int offset, int length)
        {
            var res = new byte[length];
            Buffer.BlockCopy(data, offset, res, 0, length);
            return res;
        }
    }
}
