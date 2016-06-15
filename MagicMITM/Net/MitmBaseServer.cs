using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using MagicMITM.Net.Packets;

namespace MagicMITM.Net
{
    public delegate void MitmSessionHandler<T>(object sender, T session);
    public abstract class MitmBaseServer<T> where T : MitmSession, new()
    {
        protected object startLock = new object();

        public event MitmSessionHandler<T> SessionPreAccepted = (a, b) => { };
        public event MitmSessionHandler<T> SessionAccepted = (a, b) => { };
        public event MitmSessionHandler<T> SessionStopped = (a, b) => { };

        public Socket BaseSocket { get; protected set; }
        public bool Started { get; protected set; }
        public IPEndPoint LocalEndPoint { get; protected set; }
        
        public PacketsRegistry PacketsRegistry { get; set; }
        
        public virtual void Start(IPEndPoint endPoint)
        {
            lock (startLock)
            {
                if (Started)
                {
                    return;
                }
                LocalEndPoint = endPoint;
                BaseSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                BaseSocket.Bind(endPoint);
                BaseSocket.Listen(100);

                if (PacketsRegistry == null)
                {
                    PacketsRegistry = PacketsRegistry.Default;
                }

                Started = true;
                BeginAccept(BaseSocket);
            }
        }
        public virtual void Stop()
        {
            lock(startLock)
            {
                if (!Started)
                {
                    return;
                }
                DisposeSocket(BaseSocket);
                Started = false;
            }
        }

        protected virtual void BeginAccept(Socket skt)
        {
            if (!Started || skt != BaseSocket)
            {
                DisposeSocket(skt);
                return;
            }
            skt.BeginAccept(EndAccept, skt);
        }
        protected virtual void EndAccept(IAsyncResult e)
        {
            var skt = e.AsyncState as Socket;
            Socket client = null;
            try
            {
                client = skt.EndAccept(e);
            }
            catch
            {
                DisposeSocket(client);
                DisposeSocket(skt);
                return;
            }
            if (!Started || skt != BaseSocket)
            {
                DisposeSocket(client);
                DisposeSocket(skt);
                return;
            }
            BeginAccept(skt);

            EndPoint endPoint;
            if (!TryGetEndPointFor(client, out endPoint))
            {
                DisposeSocket(client);
                return;
            }
            var server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.BeginConnect(endPoint, EndConnect, new[] { client, server });
            }
            catch
            {
                DisposeSocket(client);
                DisposeSocket(server);
            }
        }
        protected static void DisposeSocket(Socket skt)
        {
            try
            {
                skt.Shutdown(SocketShutdown.Both);
            }
            catch
            {

            }
            try
            {
                skt.Close();
            }
            catch
            {

            }
        }
        protected virtual void EndConnect(IAsyncResult e)
        {
            var sockets = e.AsyncState as Socket[];
            var client = sockets[0];
            var server = sockets[1];

            try
            {
                server.EndConnect(e);
                if (server.Connected)
                {
                    ProcessConnections(client, server);
                }
                else
                {
                    DisposeSocket(client);
                    DisposeSocket(server);
                }
            }
            catch
            {
                DisposeSocket(client);
                DisposeSocket(server);
            }
        }
        protected virtual bool TryGetEndPointFor(Socket client, out EndPoint endPoint)
        {
            endPoint = null;
            return false;
        }
        protected virtual void ProcessConnections(Socket client, Socket server)
        {
            T session = new T();
            session.ClientState = new SocketStateObject(session, client, server, 1024, true);
            session.ServerState = new SocketStateObject(session, server, client, 1024, false);
            ProcessSession(session);
        }
        protected virtual void ProcessSession(T session)
        {
            session.Stopped += OnSessionStopped;

            session.PacketsRegistry = PacketsRegistry;
            session.Plugins = new PluginManager(session);
            session.Handler = new PacketsHandler();
            session.CompleteHandler = new PacketsHandler();
            SessionPreAccepted(this, session);
            session.Initialize();
            SessionAccepted(this, session);

            session.ClientState.BeginReceive();
            session.ServerState.BeginReceive();
        }
        protected virtual void OnSessionStopped(object sender, EventArgs e)
        {
            T session = (T)sender;
            SessionStopped(this, session);
        }
    }
}
