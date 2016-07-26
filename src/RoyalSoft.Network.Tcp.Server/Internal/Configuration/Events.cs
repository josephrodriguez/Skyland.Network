#region using

using RoyalSoft.Network.Tcp.Server.Configuration.Interfaces;
using RoyalSoft.Network.Tcp.Server.Handlers;

#endregion

namespace RoyalSoft.Network.Tcp.Server.Internal.Configuration
{
    internal class Events : IEvents
    {
        internal event MessageReceivedEventHandler OnMessageReceived;

        public void RaiseMessageReceived(Message message)
        {
            if(OnMessageReceived == null) return;

            OnMessageReceived(message);
        }
    }
}
