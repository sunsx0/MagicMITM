using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicMITM.IO;
using System.IO;

namespace MagicMITM.Net.Packets
{
    public class PacketsHandler
    {
        protected Dictionary<PacketIdentifier, SortedSet<PacketEventHandlerPriority>> handlers;
        public PacketsHandler()
        {
            handlers = new Dictionary<PacketIdentifier, SortedSet<PacketEventHandlerPriority>>();
        }

        public virtual void AddHandler<T>(PacketEventHandler handler, int priority = 0) where T : GamePacket
        {
            var packetId = GamePacket.GetOnePacketIdentifier<T>();

            AddHandler(packetId, handler);
        }
        public virtual void AddHandler(uint packetId, PacketType packetType, PacketEventHandler handler, int priority = 0)
        {
            AddHandler(new PacketIdentifier(packetId, packetType), handler);
        }
        private int handlerId = 0;
        public virtual void AddHandler(PacketIdentifier packetId, PacketEventHandler handler, int priority = 0)
        {
            SortedSet<PacketEventHandlerPriority> handlersList;
            if (!handlers.TryGetValue(packetId, out handlersList))
            {
                handlersList = new SortedSet<PacketEventHandlerPriority>();
                handlers.Add(packetId, handlersList);
            }
            handlersList.Add(new PacketEventHandlerPriority(handlerId++, priority, handler));
        }
        public bool Contains(PacketIdentifier packetId)
        {
			return handlers.ContainsKey(packetId);
        }
        public PacketEventArgs HandlePacket(PacketIdentifier packetId, GamePacket gamePacket)
        {
			PacketEventArgs eventArgs;
            SortedSet<PacketEventHandlerPriority> handlersList;
            if (handlers.TryGetValue(packetId, out handlersList))
            {
				eventArgs = new PacketEventArgs(packetId, gamePacket);
                foreach (var handler in handlersList.Reverse())
                {
                    handler.Handler(this, eventArgs);
                }
                return eventArgs;
            }
            return null;
        }
        public PacketEventArgs HandlePacket(PacketEventArgs eventArgs)
        {
            SortedSet<PacketEventHandlerPriority> handlersList;
            if (handlers.TryGetValue(eventArgs.PacketId, out handlersList))
            {
                foreach (var handler in handlersList.Reverse())
                {
                    handler.Handler(this, eventArgs);
                }
                return eventArgs;
            }
            return eventArgs;
        }
    }
}
