#region using

using System.Net;

#endregion

namespace RoyalSoft.Network.Tcp.Client
{
    public interface IHubClient
    {
        void Send(IPEndPoint endpoint, byte[] message);
        void Send(string host, int port, byte[] message);
    }
}
