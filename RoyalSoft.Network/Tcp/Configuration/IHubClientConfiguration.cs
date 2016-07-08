#region using

using System.Net;

#endregion

namespace RoyalSoft.Network.Tcp.Configuration
{
    public interface IHubClientConfiguration
    {
        IPEndPoint Endpoint { get; }
    }
}
