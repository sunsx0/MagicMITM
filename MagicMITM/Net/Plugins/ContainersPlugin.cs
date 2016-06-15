using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.Net.Packets;
using MagicMITM.Net.Packets.Client;
using MagicMITM.Net.Packets.Server;
using MagicMITM.Net.Security;
using MagicMITM.Net.Security.Base;
using MagicMITM.IO;

namespace MagicMITM.Net.Plugins
{
    public class ContainersPlugin : Plugin
    {
        public bool C2SEnabled { get; set; }
        public bool S2CEnabled { get; set; }
        public override bool Enabled
        {
            get
            {
                return C2SEnabled | S2CEnabled;
            }

            set
            {
                C2SEnabled = S2CEnabled = value;
            }
        }

        public override void Initialize()
        {
            Session.Handler.AddHandler<ClientContainerC22>(OnContainerC22);
            Session.Handler.AddHandler<ServerContainerS00>(OnContainerS00);
            Session.CompleteHandler.AddHandler<ClientContainerC22>(OnContainerC22Complete);
            Session.CompleteHandler.AddHandler<ServerContainerS00>(OnContainerS00Complete);

            Enabled = true;
        }

        private void OnContainerC22(object sender, PacketEventArgs args)
        {
            if (!C2SEnabled) return;
            var container = args.Packet as ClientContainerC22;

            var packetId = new PacketIdentifier(container.PacketId, PacketType.ClientContainer);
            if (Session.Handler.Contains(packetId))
            {
                GamePacket packet = container.Packet;
                if (container.Packet is BasePacket && Session.PacketsRegistry.TryGetPacket(packetId, out packet))
                {
                    var buffer = (container.Packet as BasePacket).Buffer;
                    packet.Deserialize(new DataStream(buffer) { IsLittleEndian = true });
                    container.Packet = packet;
                }
                else
                {
                    packet = container.Packet;
                }

                var nextArgs = Session.Handler.HandlePacket(packetId, packet);
                args.Cancel |= nextArgs.Cancel;
            }
        }
        private void OnContainerS00(object sender, PacketEventArgs args)
        {
            if (!S2CEnabled) return;
            var container = args.Packet as ServerContainerS00;

            var cancel = true;
            foreach(var arg in container.Packets)
            {
                if (Session.Handler.Contains(arg.PacketId))
                {
                    GamePacket packet = arg.Packet;
                    if (arg.Packet is BasePacket && Session.PacketsRegistry.TryGetPacket(arg.PacketId, out packet))
                    {
                        var buffer = (arg.Packet as BasePacket).Buffer;
                        if (arg.PacketId.PacketType == PacketType.ServerContainer)
                        {
                            packet.Deserialize(new DataStream(buffer) { IsLittleEndian = true });
                        }
                        else
                        {
                            packet.Deserialize(new DataStream(buffer) { IsLittleEndian = false });
                        }
                        arg.Packet = packet;
                    }
                    else
                    {
                        packet = arg.Packet;
                    }

                    Session.Handler.HandlePacket(arg);
                }
                if (!arg.Cancel) cancel = false;
            }
            args.Cancel = cancel;
        }
        private void OnContainerC22Complete(object sender, PacketEventArgs args)
        {
            if (!C2SEnabled) return;
            var container = args.Packet as ClientContainerC22;

            Session.CompleteHandler.HandlePacket(new PacketIdentifier(container.PacketId, PacketType.ClientContainer), container.Packet);
        }
        private void OnContainerS00Complete(object sender, PacketEventArgs args)
        {
            if (!S2CEnabled) return;
            var container = args.Packet as ServerContainerS00;

            foreach(var packet in container.Packets)
            {
                if (!packet.Cancel)
                {
                    Session.CompleteHandler.HandlePacket(packet);
                }
            }
        }
    }
}
