using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.Data;
using MagicMITM.Net.Packets;
using MagicMITM.Net.Packets.Client;
using MagicMITM.Net.Packets.Server;

namespace MagicMITM.Net.Plugins
{
    public class AuthInfoPlugin : Plugin
    {
        public ServerInfoS01 ServerInfo { get; private set; }
        public LastLoginInfoS8F LastLoginInfo { get; private set; }
        public LogginAnnounceC03 LogginAnnounce { get; private set; }

        public override void Initialize()
        {
            Session.CompleteHandler.AddHandler<ServerInfoS01>(OnServerInfo);
            Session.CompleteHandler.AddHandler<LastLoginInfoS8F>(OnLastLoginInfo);
            Session.CompleteHandler.AddHandler<LogginAnnounceC03>(OnLoginAnnounce);
            base.Initialize();
        }
        private void OnServerInfo(object sender, PacketEventArgs e)
        {
            ServerInfo = e.Packet as ServerInfoS01;
        }
        private void OnLastLoginInfo(object sender, PacketEventArgs e)
        {
            LastLoginInfo = e.Packet as LastLoginInfoS8F;
        }
        private void OnLoginAnnounce(object sender, PacketEventArgs e)
        {
            LogginAnnounce = e.Packet as LogginAnnounceC03;
        }
    }
}
