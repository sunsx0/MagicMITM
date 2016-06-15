using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.Net;
using MagicMITM.Net.Packets;
using MagicMITM.Net.Packets.Client;
using MagicMITM.Net.Packets.Server;
using MagicMITM.Net.Plugins;

namespace Debuging
{
    public class TestSession : MitmSession
    {
        public WorldEnteredPlugin WorldEntered { get; private set; }
        public ChatPlugin Chat { get; private set; }
        public ChatHandlerPlugin ChatHandler { get; private set; }
        public AccountRolesPlugin AccountRoles { get; private set; }

        const char Smile = (char)58000;
        static string PWFormat(string str)
        {
            return str.Replace('*', Smile);
        }
        public override void Initialize()
        {
            base.Initialize();
            WorldEntered = Plugins.Register<WorldEnteredPlugin>();
            Chat = Plugins.Register<ChatPlugin>();
            ChatHandler = Plugins.Register<ChatHandlerPlugin>();
            AccountRoles = Plugins.Register<AccountRolesPlugin>();

           // WorldEntered.EnteredChanged += WorldEntered_EnteredChanged;

            Handler.AddHandler<TargetInfoS20>(OnTargetInfo);
            Handler.AddHandler<PrivateChatC60>(OnPrivateChat);

            Handler.AddHandler<RoleList_ReS53>(OnRoleList);
            Handler.AddHandler<GetCustomData_ReS75>(OnCustomData);
        }

        private static int RoleIds = 0;
        private void OnCustomData(object sender, PacketEventArgs e)
        {
            var data = e.Packet as GetCustomData_ReS75;
            var sb = new StringBuilder();

            sb.AppendLine("S75:");
            sb.AppendLine(data.RetCode.ToString());
            sb.AppendLine(data.RoleId.ToString());
            sb.AppendLine(data.UnkId.ToString());
            sb.AppendLine(data.CusRoleId.ToString());
            sb.AppendLine(data.RetCode.ToString());
            sb.AppendLine("Length = " + data.CustomData.Length + " : " + BitConverter.ToString(data.CustomData));
            sb.AppendLine("ASCII: " + Encoding.ASCII.GetString(data.CustomData));
            sb.AppendLine("Unicode: " + Encoding.Unicode.GetString(data.CustomData));

            Chat.SendPublic(true, 14, 0, "cmd", sb.ToString(), null);
        }
        private void OnRoleList(object sender, PacketEventArgs e)
        {
            var role = e.Packet as RoleList_ReS53;
            if (role.IsChar)
            {
                //role.Role.Name = "[testing-role-" + (++RoleIds) + "]";
                role.Role.Name += "_BETA";
            }
        }
        private void WorldEntered_EnteredChanged(object sender, EventArgs e)
        {
            if (WorldEntered.Entered)
            {
                for(var i = 0; i < 20; i++)
                {
                    Chat.SendPublic(true, (byte)i, 1, "mitm[" + i + "]", 
                        PWFormat("На этом сервере тестируются будущие обновления, он может быть нестабилен. Пожалуйста, сообщайте о возникших проблемах на форум*<0><0:00>"), null);
                }
            }
        }
        private void OnTargetInfo(object sender, PacketEventArgs e)
        {
            var target = e.Packet as TargetInfoS20;
            /*
            Chat.SendPublic(true, 11, 0, string.Empty, string.Format("\rTarget id: {0}\rLevel: {1}\rHP: {2}\rMP: {3}", 
                target.RoleId,
                target.Level,
                target.Hp,
                target.Mp), null);
                */
        }
        private byte MsgType = 0;
        private byte Emotion = 0;
        private void OnPrivateChat(object sender, PacketEventArgs e)
        {
            var chat = e.Packet as PrivateChatC60;
            try
            {
                if (chat.DstName == "cmd")
                {
                    e.Cancel = true;
                    Chat.SendPrivate(true, MsgType, Emotion, "cmd", chat.Message.Replace("*", ((char)58000).ToString()).Replace("\\r", "\r"), null);
                }
                if (chat.DstName == "msgtype")
                {
                    e.Cancel = true;
                    MsgType = byte.Parse(chat.Message);
                    Chat.SendPrivate(true, MsgType, Emotion, "cmd", "OK, msgtype = " + MsgType, null);
                }
                if (chat.DstName == "msgemotion")
                {
                    e.Cancel = true;
                    Emotion = byte.Parse(chat.Message);
                    Chat.SendPrivate(true, MsgType, Emotion, "cmd", "OK, msg emotion = " + Emotion, null);
                }
                if (chat.DstName == "custom")
                {
                    e.Cancel = true;
                    var uids = chat.Message.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(uint.Parse).ToArray();

                    var packet = new GetCustomDataC74();
                    packet.RoleId = AccountRoles.SelectedRole.RoleId;
                    packet.Roles = uids;

                    Send(ClientState, packet);
                }
            }
            catch
            {
                e.Cancel = true;
                Chat.SendPrivate(true, 20, Emotion, "cmd", "Error, try:\r&msgemotion 3&\r&msgtype 21&\r&cmd some text*<0><0:01>&", null);
            }
        }
    }
}
