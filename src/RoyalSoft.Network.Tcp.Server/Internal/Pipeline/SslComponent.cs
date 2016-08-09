#region using

using System.Net.Security;
using System.Net.Sockets;
using RoyalSoft.Network.Core.Pipeline;
using RoyalSoft.Network.Tcp.Server.Configuration.Interfaces;

#endregion

namespace RoyalSoft.Network.Tcp.Server.Internal.Pipeline
{
    class SslComponent : IPipelineComponent<TcpClient>
    {
        private readonly ISslConfiguration _configuration;

        public SslComponent(ISslConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TcpClient Execute(TcpClient client)
        {
            var sslStream = new SslStream(client.GetStream(), false);


            return client;
        }
    }
}
