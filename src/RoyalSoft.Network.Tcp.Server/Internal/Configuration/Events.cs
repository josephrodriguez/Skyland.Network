#region using

using System.Net;
using RoyalSoft.Network.Tcp.Server.Configuration.Interfaces;
using RoyalSoft.Network.Tcp.Server.Handlers;

#endregion

namespace RoyalSoft.Network.Tcp.Server.Internal.Configuration
{
    internal class Events : IEvents
    {
        internal event MessageReceivedEventHandler OnMessageReceived;

        internal event ClientAcceptedEventHandler OnClientAccepted;

        internal event ClientRejectedEventHandler OnClientRejected;

        internal event ClientConnectedEventHandler OnClientConnected;

        internal event ClientDisconnectedEventHandler OnClientDisconnected;

        public void RaiseClientConnected(EndPoint endpoint)
        {
            if(OnClientConnected == null) return;
            OnClientConnected(endpoint);
        }

        public void RaiseClientDisconnected(EndPoint endpoint)
        {
            if(OnClientDisconnected == null) return;
            OnClientDisconnected(endpoint);
        }

        public void RaiseClientAccepted(EndPoint endpoint)
        {
            if(OnClientAccepted == null) return;
            OnClientAccepted(endpoint);
        }

        public void RaiseClientRejected(EndPoint endpoint)
        {
            if(OnClientRejected == null) return;
            OnClientRejected(endpoint);
        }

        public void RaiseMessageReceived(Message message)
        {
            if(OnMessageReceived == null) return;

            OnMessageReceived(message);
        }
    }
}
