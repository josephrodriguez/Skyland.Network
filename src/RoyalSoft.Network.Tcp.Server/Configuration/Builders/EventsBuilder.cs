#region using

using System;
using System.Net;
using RoyalSoft.Network.Tcp.Server.Configuration.Interfaces;
using RoyalSoft.Network.Tcp.Server.Handlers;
using RoyalSoft.Network.Tcp.Server.Internal.Configuration;

#endregion

namespace RoyalSoft.Network.Tcp.Server.Configuration.Builders
{
    public class EventsBuilder : Builder<IEvents>
    {
        private readonly Events _events;

        public EventsBuilder()
        {
            _events = new Events();
        }

        public EventsBuilder OnMessageReceived(Action<Message> action)
        {
            if(action == null) throw new ArgumentNullException();

            _events.OnMessageReceived += new MessageReceivedEventHandler(action);
            return this;
        }

        public EventsBuilder OnClientConnected(Action<EndPoint> action)
        {
            if(action == null) throw new ArgumentNullException();

            _events.OnClientConnected += new ClientConnectedEventHandler(action);
            return this;
        }

        public EventsBuilder OnClientDisconnected(Action<EndPoint> action)
        {
            if(action == null) throw new ArgumentNullException();

            _events.OnClientDisconnected += new ClientDisconnectedEventHandler(action);
            return this;
        }

        public EventsBuilder OnClientAccepted(Action<EndPoint> action)
        {
            if (action == null) throw new ArgumentNullException();

            _events.OnClientAccepted += new ClientAcceptedEventHandler(action);
            return this;
        }

        public EventsBuilder OnClientRejected(Action<EndPoint> action)
        {
            if(action == null) throw new ArgumentNullException();

            _events.OnClientRejected += new ClientRejectedEventHandler(action);
            return this;
        }

        internal override IEvents Build()
        {
            return _events;
        }
    }
}
