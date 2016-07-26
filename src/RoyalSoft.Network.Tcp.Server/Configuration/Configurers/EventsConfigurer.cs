#region using

using System;
using System.Linq;
using RoyalSoft.Network.Tcp.Server.Configuration.Interfaces;
using RoyalSoft.Network.Tcp.Server.Handlers;
using RoyalSoft.Network.Tcp.Server.Internal.Configuration;

#endregion

namespace RoyalSoft.Network.Tcp.Server.Configuration.Configurers
{
    public class EventsConfigurer : BaseConfigurer<IEvents>
    {
        private readonly Events _events;

        public EventsConfigurer()
        {
            _events = new Events();
        }

        public EventsConfigurer OnMessageReceived(Action<Message> action)
        {
            if(action == null)
                throw new ArgumentNullException();

            _events.OnMessageReceived += new MessageReceivedEventHandler(action);
            return this;
        }

        internal override IEvents Build()
        {
            return _events;
        }
    }
}
