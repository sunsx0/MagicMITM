using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using MagicMITM.IO;
using MagicMITM.Net.Packets;
using MagicMITM.Net.Packets.Client;
using MagicMITM.Net.Packets.Server;
using MagicMITM.Net.Plugins;

namespace MagicMITM.Net
{
    public class MitmSession
    {
        public SocketStateObject ServerState { get; set; }
        public SocketStateObject ClientState { get; set; }


        public PacketsRegistry PacketsRegistry { get; set; }
        public PacketsHandler Handler { get; set; }
        public PacketsHandler CompleteHandler { get; set; }
        public PluginManager Plugins { get; set; }

		public int PacketSizeLimit { get; set; }

        public Socket Server
        {
            get
            {
                return ServerState.From;
            }
        }
        public Socket Client
        {
            get
            {
                return ClientState.From;
            }
        }

        public event EventHandler Stopped = (a, b) => { };

		public MitmSession()
		{
			PacketSizeLimit = 4 * 1024 * 1024;
		}

        public virtual void Initialize()
        {
            Plugins.Register<DefaultEncryptionPlugin>();
            Plugins.Register<ContainersPlugin>();
        }
        public virtual void Stop()
        {
            ServerState.Stop(Server);
            ClientState.Stop(Client);
        }
        public virtual void OnDisconnected(SocketStateObject state)
        {
            if (!Server.Connected && !Client.Connected)
            {
                Stopped(this, new EventArgs());
            }
            else
            {
                Stop();
            }
        }
        public virtual void ProcessStream(SocketStateObject state, DataStream ds)
        {
            var goodPos = 0;
            while (true)
            {
                var id = 0U;
                var length = 0U;
                if (!ds.TryReadCompactUInt32(out id) || !ds.TryReadCompactUInt32(out length) || !ds.CanReadBytes((int)length))
                {
					if (length > PacketSizeLimit)
                    {
                        Logger.Process("packetId = {0}, packetLength = {1}, packetSizeLimit = {2}", id, length, PacketSizeLimit);
                        Stop();
					}
                    break;
                }
                var packetId = new PacketIdentifier(id, state.IsC2S ? PacketType.ClientPacket : PacketType.ServerPacket);
                var packetStream = new DataStream(ds.ReadBytes((int)length));
                packetStream.IsLittleEndian = false;
                goodPos = ds.Position;

                ProcessPacketStream(state, packetId, packetStream);
            }
            ds.Position = goodPos;
            ds.Flush();
        }
        public virtual void ProcessPacketStream(SocketStateObject state, PacketIdentifier packetId, DataStream packetStream)
        {
            if (Handler.Contains(packetId) || CompleteHandler.Contains(packetId))
            {
                GamePacket packet;
                if (!PacketsRegistry.TryGetPacket(packetId, out packet))
                {
                    packet = new BasePacket();
                }

                packet.Deserialize(packetStream);
                var args = Handler.HandlePacket(packetId, packet);
                if (args == null || !args.Cancel)
                {
                    if (args == null)
                    {
                        Send(state, packetId.PacketId, packetStream.Reset());
                    }
                    else
                    {
                        Send(state, packetId, packet);
                    }
                    CompleteHandler.HandlePacket(packetId, packet);
                }
            }
            else
            {
                Send(state, packetId.PacketId, packetStream);
            }
        }
        public virtual void Send(SocketStateObject state, GamePacket packet)
        {
            var packetId = GamePacket.GetOnePacketIdentifier(packet);
            Send(state, packetId, packet);
        }
        public virtual void Send(SocketStateObject state, PacketIdentifier packetId, GamePacket packet)
        {
            switch (packetId.PacketType)
            {
                case PacketType.ServerPacket:
                case PacketType.ClientPacket:
                    Send(state, packetId.PacketId, packet.Serialize(new DataStream { IsLittleEndian = false }));
                    return;
                case PacketType.ServerContainer:
                    Send(state, new ServerContainerS00(new[] { new PacketEventArgs(packetId, packet) }));
                    return;
                case PacketType.ClientContainer:
                    Send(state, new ClientContainerC22(packetId.PacketId, packet));
                    return;
            }
        }
        private DataStream sendStream = new DataStream();
        public virtual void Send(SocketStateObject state, uint packetId, DataStream packetStream)
        {
            lock(sendStream)
            {
                sendStream.Clear();
                sendStream.WriteCompactUInt32(packetId);
                sendStream.Write(packetStream);

                state.Send(sendStream.Buffer, 0, sendStream.Count);
            }
        }
    }
}
