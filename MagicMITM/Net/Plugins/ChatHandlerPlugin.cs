using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.Net.Packets;
using MagicMITM.Net.Packets.Client;
using MagicMITM.Net.Packets.Server;

namespace MagicMITM.Net.Plugins
{
    public class ChatHandlerPlugin : Plugin
    {
        public Dictionary<byte, byte> EmotionReplacing { get; private set; }
        public override void Initialize()
        {
            EmotionReplacing = new Dictionary<byte, byte>();

            Session.Handler.AddHandler<PrivateChatS60>(OnPrivateChat);
            Session.Handler.AddHandler<WorldChatS85>(OnPublic);
            base.Initialize();
        }
        private byte GetEmotion(byte currentEmotion)
        {
            var nextEmotion = (byte)0;
            if (EmotionReplacing.TryGetValue(currentEmotion, out nextEmotion))
            {
                return nextEmotion;
            }
            return currentEmotion;
        }
        private void OnPrivateChat(object sender, PacketEventArgs e)
        {
            var chat = e.Packet as PrivateChatS60;
            chat.Emotion = GetEmotion(chat.Emotion);
        }
        private void OnPublic(object sender, PacketEventArgs e)
        {
            var chat = e.Packet as WorldChatS85;
            chat.Emotion = GetEmotion(chat.Emotion);
        }
    }
}
