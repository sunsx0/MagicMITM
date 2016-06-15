using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagicMITM.Net.Security.Base
{
    public class S2CEncryption : Encryptor
    {
        public MppcPacker Packer { get; set; }
        public MppcUnpacker Unpacker { get; set; }
        public Rc4Encryption Rc4Enc { get; set; }
        public Rc4Encryption Rc4Dec { get; set; }

        public void Initialize(byte[] key)
        {
            InitializeEnc(key);
            InitializeDec(key);
        }
        public void InitializeEnc(byte[] key)
        {
            Packer = new MppcPacker();
            Rc4Enc = new Rc4Encryption(key);
        }
        public void InitializeDec(byte[] key)
        {
            Unpacker = new MppcUnpacker();
            Rc4Dec = new Rc4Encryption(key);
        }
        public override byte[] Encrypt(byte[] data, int offset, int length)
        {
            data = base.Decrypt(data, offset, length);
            if (Packer != null)
            {
                data = Packer.Pack(data);
            }
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
            if (Unpacker != null)
            {
                data = Unpacker.Unpack(data);
            }
            return data;
        }
    }
}
