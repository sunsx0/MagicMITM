using System;
using System.Net;
using System.Net.Sockets;

namespace MagicMITM.Net
{
    public class GameServer
    {
        public string Name { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }

        public GameServer() : this("127.0.0.1", 29000)
        {
        }
        public GameServer(string host, int port) : this(host, port, string.Empty)
        {
        }
        public GameServer(string host, int port, string name)
        {
            Name = name;
            Host = host;
            Port = port;
        }
        public override bool Equals(object obj)
        {
            if (obj is GameServer)
            {
                var gs = obj as GameServer;
                return gs.Host == Host && Port == gs.Port;
            }
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Host.GetHashCode() ^ Port.GetHashCode();
        }
        public static GameServer Parse(string server)
        {
            var res = new GameServer();

            string[] args = server.Replace(" ", string.Empty).Split(':', ';', '=', '\t');
            if (args.Length == 0)
            {
                res.Host = "127.0.0.1";
                res.Port = 29000;
                return res;
            }
            if (args.Length == 1)
            {
                res.Host = args[0];
                res.Port = 29000;
                return res;
            }

            int port;

            if (int.TryParse(args[0], out port))
            {
                res.Port = port;
                res.Host = args[1];
            }
            else
            {
                if (int.TryParse(args[1], out port))
                {
                    res.Port = port;
                    res.Host = args[0];
                }
                else
                {
                    res.Host = args[0];
                    res.Port = 29000;
                }
            }
            return res;
        }

        public override string ToString()
        {
            string srvName = string.IsNullOrEmpty(Name) ? "" : Name + " ";
            return String.Format("{0}{1}:{2}", srvName, Host, Port);
        }
        public string ToShortString()
        {
            return string.IsNullOrEmpty(Name) ? String.Format("{0}:{1}", Host, Port) : Name;
        }
    }
}
