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
    public class FakeGoldHack : Plugin
    {
        public int CurrentSilver;

        public uint[] ItemIds = new uint[32];
        public uint[] ItemCounts = new uint[32];

        public ChatPlugin Chat;
        public bool Freeze = false;

        public override void Initialize()
        {
            Enabled = false;

            Chat = Session.Plugins.Register<ChatPlugin>();

            Session.Handler.AddHandler<ShopBuyC6A>(OnShopBuy);
            Session.Handler.AddHandler<SilverInfoSFD>(OnSilverUpdate);
            Session.Handler.AddHandler<PrivateChatC60>(OnPrivateMessage);

            base.Initialize();
        }
        public void UpdateSilver()
        {
            var packet = new SilverInfoSFD();
            packet.SilverCount = CurrentSilver;
            Session.Send(Session.ServerState, packet);
        }
        private void OnSilverUpdate(object sender, PacketEventArgs e)
        {
            CurrentSilver = (e.Packet as SilverInfoSFD).SilverCount;
        }
        private int FindSlot(uint itemId)
        {
            var minId = 31;
            for (var i = 0; i < ItemIds.Length; i++)
            {
                if (ItemIds[i] == 0)
                {
                    minId = Math.Min(i, minId);
                }
                if (ItemIds[i] == itemId)
                {
                    minId = i;
                    break;
                }
            }
            return minId;
        }
        private void OnShopBuy(object sender, PacketEventArgs e)
        {
            if (!Enabled) return;
            e.Cancel = true;

            var packet = e.Packet as ShopBuyC6A;

            var itemId = packet.ItemId;
            var count = packet.Count;

            var slotId = FindSlot(itemId);

            ItemIds[slotId] = itemId;
            ItemCounts[slotId] += count;

            UpdateCell(slotId, count);
        }
        private void UpdateCell(int cell, uint count)
        {
            var packet = new ShopBuyCompleteS63();
            packet.SlotCount = ItemCounts[cell];
            packet.SlotId = (byte)cell;
            packet.Count = count;
            packet.ItemId = ItemIds[cell];

            Session.Send(Session.ServerState, packet);

            CurrentSilver--;
            UpdateSilver();
        }
        private void OnPrivateMessage(object sender, PacketEventArgs e)
        {
            var chat = e.Packet as PrivateChatC60;
            try
            {
                if (chat.DstName == "gh")
                {
                    e.Cancel = true;

                    var args = chat.Message.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                    if (args[0] == "enable")
                    {
                        Enabled = true;
                        Chat.SendPrivate(true, 1, 0, "gh", "OK, enabled", null);
                        return;
                    }
                    if (Enabled)
                    {
                        switch(args[0])
                        {
                            case "addcash":
                                var cash = int.Parse(args[1]);
                                CurrentSilver += cash;
                                Chat.SendPrivate(true, "gh", "Current cash: " + CurrentSilver);
                                UpdateSilver();
                                break;
                            case "freeze":
                                Freeze = true;
                                Chat.SendPrivate(true, "gh", "Freezed");
                                break;
                            default:
                                Chat.SendPrivate(true, "gh", "Unknown command");
                                break;
                        }
                    }
                }
            }
            catch
            {
                if (Enabled)
                {
                    Chat.SendPrivate(true, "gh", "Error");
                }
            }
        }
    }
}
