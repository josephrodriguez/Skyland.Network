#region using

using System.Net;

#endregion

namespace RoyalSoft.Network.Tcp.Server.Configuration.Interfaces
{
    public interface IEvents
    {
        void RaiseClientConnected(EndPoint remoteEndpoint);

        void RaiseClientDisconnected(EndPoint remoteEndpoint);

        void RaiseClientAccepted(EndPoint endpoint);

        void RaiseClientRejected(EndPoint remoteEndpoint);

        void RaiseMessageReceived(Message message);
    }
}
