using System;
using System.Net.Sockets;
using MagicMITM.Net.Security;
using MagicMITM.IO;

namespace MagicMITM.Net
{
    /// <summary>Доп. состояние сокета</summary>
    public class SocketStateObject
    {
        /// <summary>
        /// Базовая сессия
        /// </summary>
        public MitmSession Session { get; private set; }
        /// <summary>
        /// Источник.
        /// </summary>
        public Socket From { get; private set; }

        /// <summary>
        /// Цель.
        /// </summary>
        public Socket To { get; private set; }

        /// <summary>
        /// Буффер источника.
        /// </summary>
        public byte[] FromBuffer { get; private set; }
        public DataStream FromStream { get; private set; }

        /// <summary>
        /// Источник клиент или нет.
        /// </summary>
        public Boolean IsC2S { get; private set; }
        public Boolean Connected { get; private set; }

        public Encryptor Encryptor { get; set; }



        private readonly int bufferSize;

        /// <summary>
        /// Инициализация объекта состояния.
        /// </summary>
        /// <param name="from">Источник</param>
        /// <param name="to">Цель</param>
        /// <param name="isC2S">Источник клиент или нет</param>
        /// <param name="bufferSize">Размер буфера</param>
        public SocketStateObject(MitmSession session, Socket from, Socket to, int bufferSize, bool isC2S)
        {
            Session = session;

            From = from;
            To = to;
            IsC2S = isC2S;
            Connected = true;

            this.bufferSize = bufferSize;
            FromBuffer = new byte[bufferSize];
            FromStream = new DataStream();

            Encryptor = Encryptor.Default;
        }

        public void ResetFromBuffer()
        {
            FromBuffer = new byte[bufferSize];
        }

        public void BeginReceive()
        {
            
            try
            {
                From.BeginReceive(FromBuffer, 0, FromBuffer.Length, SocketFlags.None, OnReceive, null);
            }
            catch (Exception ex)
            {
                Logger.Process("BeginReceive", ex);
                Stop(From);
            }
        }
        private void OnReceive(IAsyncResult e)
        {
            try
            {
                var length = From.EndReceive(e);
                if (length <= 0 || !From.Connected)
                {
                    Logger.Process("OnReceive stop. Length: {0}, connected: {1}", length, From.Connected);
                    Stop(From);
                    return;
                }
                else
                {
                    var data = Encryptor.Decrypt(FromBuffer, 0, length);
                    FromStream.PushBack(data);
                }
            }
            catch (Exception ex)
            {
                Logger.Process("OnReceive", ex);
                Stop(From);
            }
            try
            {
                Session.ProcessStream(this, FromStream);
            }
            catch (Exception ex)
            {
                Logger.Process("OnReceive - ProcessStream", ex);
            }

            BeginReceive();
        }

        public void Send(byte[] buffer)
        {
            Send(buffer, 0, buffer.Length);
        }
        public void Send(byte[] buffer, int offset, int length)
        {
            lock (Encryptor)
            {
                var data = Encryptor.Encrypt(buffer, offset, length);
                BeginSend(data);
            }
        }
        private void BeginSend(byte[] buffer)
        {
            BeginSend(buffer, 0, buffer.Length);
        }
        private void BeginSend(byte[] buffer, int pos, int length)
        {
            try
            {
                lock(To)
                {
                    if (!To.Connected)
                    {
                        return;
                    }

                    var bytesToSend = new byte[length];
                    Buffer.BlockCopy(buffer, pos, bytesToSend, 0, length);

                    To.BeginSend(bytesToSend, 0, length, SocketFlags.None, null, null);
                    //To.Send(bytesToSend, 0, length, SocketFlags.None);
                }
            }
            catch (Exception ex)
            {
                Stop(To);
            }
        }

        protected void DisposeSocket(Socket skt)
        {
            Logger.Process("Dispose {0} socket", ((skt == From) == IsC2S ? "client" : "server"));
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
        public void Stop(Socket skt)
        {
            lock (To)
            {
                if (!Connected)
                {
                    return;
                }
                Connected = false;
            }
            DisposeSocket(skt);
            Session.OnDisconnected(this);
        }
    }
}