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
    public class LoginChangeEncryptionPlugin : Plugin
    {
        public MD5Hash ClientMD5Hash { get; private set; }
        public MD5Hash ServerMD5Hash { get; private set; }

        public event PacketEventHandler LogginAnnounce = (a, b) => { };

        public override void Initialize()
        {
            ClientMD5Hash = new MD5Hash();
            ServerMD5Hash = new MD5Hash();

            Session.Handler.AddHandler<ServerInfoS01>(OnServerInfo);
            base.Initialize();
        }
        private void OnServerInfo(object sender, PacketEventArgs args)
        {
            var serverInfo = args.Packet as ServerInfoS01;

            if (!string.IsNullOrEmpty(serverInfo.CRC))
            {
                Session.Handler.AddHandler<LogginAnnounceC03>(OnLogginAnnounce);
                Session.Handler.AddHandler<SMKeyS02>(OnSMKey);
                Session.Handler.AddHandler<CMKeyC02>(OnCMKey);
            }
            else
            {
                Session.Handler.AddHandler<LogginAnnounceC03>(OnLogginAnnounce);
                Session.Handler.AddHandler<SMKeyS02>(OnSMKey);
                Session.Handler.AddHandler<CMKeyC02>(OnCMKey);
            }
        }
        private void CopyLogginAnnounce(LogginAnnounceC03 loggin, MD5Hash md5Hash)
        {
            md5Hash.SetHash(loggin.Login, loggin.Hash);
        }
        private void OnLogginAnnounce(object sender, PacketEventArgs args)
        {
            CopyLogginAnnounce(args.Packet as LogginAnnounceC03, ClientMD5Hash);
            CopyLogginAnnounce(args.Packet as LogginAnnounceC03, ServerMD5Hash);
            LogginAnnounce(this, args);
            if (Enabled)
            {
                CopyLogginAnnounce(args.Packet as LogginAnnounceC03, ServerMD5Hash);
            }
        }
        private void OnSMKey(object sender, PacketEventArgs args)
        {
            var smkey = args.Packet as SMKeyS02;

            var encryption = new C2SEncryption();
            encryption.InitializeDec(ClientMD5Hash.GetKey(smkey.Key));
            encryption.InitializeEnc(ServerMD5Hash.GetKey(smkey.Key));
            Session.ClientState.Encryptor = encryption;


            Logger.Process("OnSMKey Client - " + BitConverter.ToString(ClientMD5Hash.GetKey(smkey.Key)));
            Logger.Process("OnSMKey Server - " + BitConverter.ToString(ServerMD5Hash.GetKey(smkey.Key)));
        }
        private void OnCMKey(object sender, PacketEventArgs args)
        {
            var cmkey = args.Packet as CMKeyC02;

            var encryption = new S2CEncryption();
            encryption.InitializeDec(ServerMD5Hash.GetKey(cmkey.Key));
            encryption.InitializeEnc(ClientMD5Hash.GetKey(cmkey.Key));
            Session.ServerState.Encryptor = encryption;

            Logger.Process("OnCMKey Server - " + BitConverter.ToString(ServerMD5Hash.GetKey(cmkey.Key)));
            Logger.Process("OnCMKey Client - " + BitConverter.ToString(ClientMD5Hash.GetKey(cmkey.Key)));
        }

    }
}
