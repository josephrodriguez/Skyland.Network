#region using

using RoyalSoft.Network.Core.Configuration;
using RoyalSoft.Network.Tcp.Server.Configuration.Interfaces;

#endregion

namespace RoyalSoft.Network.Tcp.Server.Configuration.Configurers
{
    public class EventsConfigurer : BaseConfigurer<IEventsConfiguration>
    {
        public EventsConfigurer(IEventsConfiguration component) 
            : base(component)
        {
        }
    }
}
