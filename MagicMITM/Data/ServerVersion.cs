using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;

namespace MagicMITM.Data
{
    public class ServerVersion : DataSerializer, IComparable<ServerVersion>
    {
        private byte[] VersionToBytes { get; set; }

        public ServerVersion()
        {

        }
        public ServerVersion(byte[] toBytes)
        {
            if (toBytes.Length != 4)
            {
                throw new Exception("(toBytes.Length != 4)");
            }

            VersionToBytes = new byte[toBytes.Length];
            Buffer.BlockCopy(toBytes, 0, VersionToBytes, 0, toBytes.Length);
        }

        public static ServerVersion Parse(string s)
        {
            var args = s.Split('.');
            if (args.Length < 3 || args.Length > 4) throw new Exception("ServerVersion parse error");

            var bs = new byte[4];
            for(var i = 0; i < bs.Length; i++)
            {
                bs[3 - i] = byte.Parse(args[args.Length - i - 1]);
            }

            return new ServerVersion(bs);
        }
        
        public int CompareTo(ServerVersion other)
        {
            for (var i = 0; i < VersionToBytes.Length; i++)
            {
                var comp = VersionToBytes[i].CompareTo(other.VersionToBytes[i]);
                if (comp != 0) return comp;
            }
            return 0;
        }

        public override bool Equals(object obj)
        {
            if (obj is ServerVersion)
            {
                var sv = obj as ServerVersion;
                for (var i = 0; i < VersionToBytes.Length; i++)
                {
                    if (sv.VersionToBytes[i] != VersionToBytes[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return base.Equals(obj);
            }
        }
        public override int GetHashCode()
        {
            return BitConverter.ToInt32(VersionToBytes, 0);
        }

        public override string ToString()
        {
            if (VersionToBytes == null) return "0";

            var i = 0;
            if (VersionToBytes[i] == 0) i++;

            if (i == VersionToBytes.Length) return "0";
            else
            {
                var res = VersionToBytes[i].ToString();
                while(++i < VersionToBytes.Length)
                {
                    res += "." + VersionToBytes[i];
                }
                return res;
            }
        }

        public override DataStream Serialize(DataStream ds)
        {
            ds.Write(VersionToBytes);
            return base.Serialize(ds);
        }
        public override DataStream Deserialize(DataStream ds)
        {
            VersionToBytes = ds.ReadBytes(4);
            return base.Deserialize(ds);
        }
    }
}
