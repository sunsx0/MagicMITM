using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.Net;
using System.Net;
using System.Threading;

namespace Debuging
{
    class Program
    {
        static void Main(string[] args)
        {

            //var server = new MitmProxyServer<TestSession>();
            var server = new MitmStaticServer<TestSession>("186.2.166.210");
            server.Start(new IPEndPoint(IPAddress.Any, 29001));

            Thread.Sleep(Timeout.Infinite);
        }
    }
}
