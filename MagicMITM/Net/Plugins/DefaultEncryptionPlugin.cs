using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.Net.Packets;
using MagicMITM.Net.Packets.Client;
using MagicMITM.Net.Packets.Server;
using MagicMITM.Net.Security;
using MagicMITM.Net.Security.Base;

namespace MagicMITM.Net.Plugins
{
    public class DefaultEncryptionPlugin : Plugin
    {
        public MD5Hash MD5Hash { get; private set; }

        public override void Initialize()
        {
            MD5Hash = new MD5Hash();

            Session.Handler.AddHandler<ServerInfoS01>(OnServerInfo);
            base.Initialize();
        }
        private void OnServerInfo(object sender, PacketEventArgs args)
        {
            var serverInfo = args.Packet as ServerInfoS01;

            if (serverInfo.CRC != null)
            {
                Session.Handler.AddHandler<LogginAnnounceC03>(OnLogginAnnounce, 100);
                Session.Handler.AddHandler<SMKeyS02>(OnSMKey, 100);
                Session.Handler.AddHandler<CMKeyC02>(OnCMKey, 100);
            }
            else
            {
                Session.Handler.AddHandler<LogginAnnounceC03>(OnLogginAnnounce, 100);
                Session.Handler.AddHandler<SMKeyS02>(OnSMKey, 100);
                Session.Handler.AddHandler<CMKeyC02>(OnCMKey, 100);
                // todo: 1.2.6 version
                //Session.Stop();
            }
        }
        private void OnLogginAnnounce(object sender, PacketEventArgs args)
        {
            var loggin = args.Packet as LogginAnnounceC03;
            MD5Hash.SetHash(loggin.LoginBytes, loggin.Hash);
        }
        private void OnSMKey(object sender, PacketEventArgs args)
        {
            var smkey = args.Packet as SMKeyS02;

            var encryption = new C2SEncryption();
            encryption.Initialize(MD5Hash.GetKey(smkey.Key));
            Session.ClientState.Encryptor = encryption;
        }
        private void OnCMKey(object sender, PacketEventArgs args)
        {
            var cmkey = args.Packet as CMKeyC02;

            var encryption = new S2CEncryption();
            encryption.Initialize(MD5Hash.GetKey(cmkey.Key));
            Session.ServerState.Encryptor = encryption;
        }

    }
}
