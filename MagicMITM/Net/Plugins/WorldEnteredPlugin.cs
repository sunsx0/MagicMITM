using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.Net.Packets;
using MagicMITM.Net.Packets.Client;
using MagicMITM.Net.Packets.Server;

namespace MagicMITM.Net.Plugins
{
    public class WorldEnteredPlugin : Plugin
    {
        public event EventHandler EnteredChanged = (a, b) => { };
        private bool entered = false;
        public bool Entered
        {
            get
            {
                return entered;
            }
            private set
            {
                entered = value;
                EnteredChanged(this, new EventArgs());
            }
        }
        public override void Initialize()
        {
            Session.CompleteHandler.AddHandler<ServerContainerS00>(OnContainer);
            Session.CompleteHandler.AddHandler<RoleListC52>(OnRoleList);
            base.Initialize();
        }
        private void OnContainer(object sender, PacketEventArgs e)
        {
            if (!entered)
            {
                Entered = true;
            }
        }
        private void OnRoleList(object sender, PacketEventArgs e)
        {
            if (entered)
            {
                Entered = false;
            }
        }
        private void OnLogout(object sender, PacketEventArgs e)
        {
            var logout = e.Packet as RoleLogoutS45;
            if (logout.StatusCode == 1)
            {
                if (entered)
                {
                    Entered = false;
                }
            }
        }
    }
}
