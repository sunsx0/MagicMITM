using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using MagicMITM.Net.Proxy;
using MagicMITM.Net.Proxy.Socks4;

namespace MagicMITM.Net
{
    public class MitmProxyServer<T> : MitmBaseServer<T> where T : MitmSession, new()
    {

        protected override bool TryGetEndPointFor(Socket client, out EndPoint endPoint)
        {
            endPoint = null;
            try
            {
                var socks4RequestBuffer = new byte[16];
                var bytesReceived = client.Receive(socks4RequestBuffer);
                var request = Socks4Helper.GetRequest(socks4RequestBuffer.Take(bytesReceived).ToArray());

                var host = request.Address;
                var port = request.Port;

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
        protected override void EndConnect(IAsyncResult e)
        {
            var sockets = e.AsyncState as Socket[];
            var client = sockets[0];
            var server = sockets[1];
            try
            {
                base.EndConnect(e);
                if (server.Connected)
                {
                    var response = new Socks4Packet(Socks4ServiceCode.Accepted);
                    var responseBytes = Socks4Helper.GetResponse(response);
                    client.Send(responseBytes);
                }
            }
            catch
            {
                DisposeSocket(client);
                DisposeSocket(server);
            }
        }
    }
}
