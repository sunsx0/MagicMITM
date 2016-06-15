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
    public class AccountRolesPlugin : Plugin
    {
        public List<RoleInfo> Roles { get; private set; }
        public RoleInfo SelectedRole { get; private set; }

        public override void Initialize()
        {
            Roles = new List<RoleInfo>();

            Session.Handler.AddHandler<CreateRole_ReS55>(OnRoleCreate);
            Session.Handler.AddHandler<RoleList_ReS53>(OnRoleList);
            Session.Handler.AddHandler<EnterWorldC48>(OnEnterWorld);
            Session.Handler.AddHandler<RoleLogoutS45>(OnLogout);

            base.Initialize();
        }
        private void OnRoleList(object sender, PacketEventArgs e)
        {
            var role = e.Packet as RoleList_ReS53;
            if (role.IsChar)
            {
                Roles.Add(role.Role);
            }
        }
        private void OnRoleCreate(object sender, PacketEventArgs e)
        {
            var role = e.Packet as CreateRole_ReS55;
            if (role.ResultCode.ResultCode == 0)
            {
                Roles.Add(role.Role);
            }
        }
        private void OnEnterWorld(object sender, PacketEventArgs e)
        {
            var info = e.Packet as EnterWorldC48;
            var roleId = info.RoleId;
            foreach (var role in Roles)
            {
                if (role.RoleId == roleId)
                {
                    SelectedRole = role;
                    break;
                }
            }
        }
        private void OnLogout(object sender, PacketEventArgs e)
        {
            var logout = e.Packet as RoleLogoutS45;
            if (logout.StatusCode == 1)
            {
                Roles.Clear();
                SelectedRole = null;
            }
        }
    }
}
