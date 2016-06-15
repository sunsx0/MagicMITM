using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.Net.Packets;
using MagicMITM.Net.Packets.Client;
using MagicMITM.Net.Packets.Server;

namespace MagicMITM.Net.Plugins
{
    public class ChatPlugin : Plugin
    {
        public AccountRolesPlugin AccountRoles;
        public uint DefaultId = 1023;

        public override void Initialize()
        {
            AccountRoles = Session.Plugins.Register<AccountRolesPlugin>();

            base.Initialize();
        }

        public void SendPrivate(bool toClient, string name, string message)
        {
            SendPrivate(toClient, 0, 0, name, message, null);
        }
        public void SendPrivate(bool toClient, byte channel, byte emotion, string name, string message, byte[] data)
        {
            if (toClient)
            {
                var chat = new PrivateChatS60
                {
                    Channel = channel,
                    Emotion = emotion,
                    DstName = AccountRoles.SelectedRole.Name,
                    DstRoleId = AccountRoles.SelectedRole.RoleId,
                    SrcLevel = 150,
                    SrcName = name,
                    SrcRoleId = DefaultId,
                    Data = data,
                    Message = message
                };
                Session.Send(Session.ServerState, chat);
            }
            else
            {
                var chat = new PrivateChatC60
                {
                    Channel = channel,
                    Emotion = emotion,
                    DstName = name,
                    DstRoleId = 0,
                    Message = message,
                    Data = data,
                    SrcLevel = AccountRoles.SelectedRole.Level,
                    SrcName = AccountRoles.SelectedRole.Name,
                    SrcRoleId = AccountRoles.SelectedRole.RoleId
                };
                Session.Send(Session.ClientState, chat);
            }
        }
        public void SendPublic(bool toClient, byte channel, byte emotion, string name, string message, byte[] data)
        {
            if (toClient)
            {
                var chat = new WorldChatS85
                {
                    Channel = channel,
                    Emotion = emotion,
                    MessageBytes = Encoding.Unicode.GetBytes(message),
                    NameBytes = Encoding.Unicode.GetBytes(name),
                    RoleId = DefaultId,
                    Data = data
                };
                Session.Send(Session.ServerState, chat);
            }
            else
            {
                var chat = new PublicChatC4F
                {
                    Channel = channel,
                    Emotion = emotion,
                    Message = message,
                    RoleId = AccountRoles.SelectedRole.RoleId,
                    UnkId = 0,
                    Data = data
                };
                Session.Send(Session.ClientState, chat);
            }
        }
    }
}
