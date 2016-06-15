using System;
using System.Security.Cryptography;
using System.Text;

namespace MagicMITM.Net.Security
{
    public class MD5Hash
    {
        private MD5 MD5;
        private HMACMD5 LoginMD5;

        private byte[] Hash;
        private byte[] Login;

        public MD5Hash()
        {
            MD5 = MD5.Create();
        }

        public byte[] GetHash(string login, string password, byte[] key)
        {
            byte[] loginData = Encoding.ASCII.GetBytes(login);
            byte[] authData = Encoding.ASCII.GetBytes(login + password);

            return GetHash(loginData, authData, key);
            
        }
        public byte[] GetHash(string login, byte[] authData, byte[] key)
        {
            byte[] loginData = Encoding.ASCII.GetBytes(login);

            return GetHash(loginData, authData, key);
        }
        public byte[] GetHash(byte[] login, byte[] authData, byte[] key)
        {
            var hash = new HMACMD5(MD5
                .ComputeHash(authData))
                .ComputeHash(key);

            SetHash(login, hash);

            return hash;
        }
        public void SetHash(string login, byte[] hash)
        {
            SetHash(Encoding.ASCII.GetBytes(login), hash);
        }
        public void SetHash(byte[] login, byte[] hash)
        {
            Login = login;
            LoginMD5 = new HMACMD5(login);
            Hash = hash;
        }
        public byte[] GetKey(byte[] key)
        {
            byte[] hash02 = new byte[key.Length + Hash.Length];

            Buffer.BlockCopy(Hash, 0, hash02, 0, Hash.Length);
            Buffer.BlockCopy(key, 0, hash02, Hash.Length, key.Length);

            return LoginMD5.ComputeHash(hash02);
        }
    }
}
