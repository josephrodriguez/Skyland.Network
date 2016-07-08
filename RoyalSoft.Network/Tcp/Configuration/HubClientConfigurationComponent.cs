#region using

using System.Net;

#endregion

namespace RoyalSoft.Network.Tcp.Configuration
{
    class HubClientConfigurationComponent : IHubClientConfiguration
    {
        public IPEndPoint Endpoint { get; }
    }
}
