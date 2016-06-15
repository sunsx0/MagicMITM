using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagicMITM.Net.Security.Base
{
    public class C2SEncryption : Encryptor
    {
        public Rc4Encryption Rc4Enc { get; set; }
        public Rc4Encryption Rc4Dec { get; set; }

        public void Initialize(byte[] key)
        {
            InitializeEnc(key);
            InitializeDec(key);
        }
        public void InitializeEnc(byte[] key)
        {
            Rc4Enc = new Rc4Encryption(key);
        }
        public void InitializeDec(byte[] key)
        {
            Rc4Dec = new Rc4Encryption(key);
        }
        public override byte[] Encrypt(byte[] data, int offset, int length)
        {
            data = base.Decrypt(data, offset, length);
            if (Rc4Enc != null)
            {
                Rc4Enc.Encrypt(data);
            }
            return data;
        }
        public override byte[] Decrypt(byte[] data, int offset, int length)
        {
            data = base.Decrypt(data, offset, length);
            if (Rc4Dec != null)
            {
                Rc4Dec.Decrypt(data);
            }
            return data;
        }
    }
}
