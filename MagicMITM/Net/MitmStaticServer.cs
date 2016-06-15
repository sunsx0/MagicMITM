using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MagicMITM.Net
{
    public class MitmStaticServer<T> : MitmBaseServer<T> where T : MitmSession, new()
    {

        public GameServer GameServer { get; set; }

        public MitmStaticServer() : this(string.Empty)
        {

        }
        public MitmStaticServer(string host) : this(GameServer.Parse(host))
        {

        }
        public MitmStaticServer(GameServer gameServer)
        {
            GameServer = gameServer;
        }

        protected override bool TryGetEndPointFor(Socket client, out EndPoint endPoint)
        {
            endPoint = null;
            try
            {
                var host = GameServer.Host;
                var port = GameServer.Port;

                var ip = IPAddress.Any;
                if (!IPAddress.TryParse(host, out ip))
                {
                    var ips = Dns.GetHostAddresses(host);
                    if (ips.Length == 0)
                    {
                        return false;
                    }
                    ip = ips[0];
                }
                endPoint = new IPEndPoint(ip, port);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
