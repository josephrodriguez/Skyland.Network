#region using

using System.Net;
using System.Net.Sockets;
using RoyalSoft.Network.Core.Asserts;
using RoyalSoft.Network.Tcp.Server.Configuration.Interfaces;

#endregion

namespace RoyalSoft.Network.Tcp.Server.Impl
{
    internal class DefaultHubServer : IHubServer
    {
        private readonly IPEndPoint _endpoint;

        internal ISslConfiguration Ssl { get; set; }
        internal ICompressionConfiguration Compression { get; set; }

        private TcpListener _listener;

        protected internal DefaultHubServer(IPEndPoint endpoint)
        {
            _endpoint = endpoint;
        }

        public void Start()
        {
            Assert.IsNull(_listener);

            _listener = new TcpListener(_endpoint);
            _listener.Start();
        }

        public void Stop()
        {
            Assert.IsNotNull(_listener);

            _listener.Stop();
        }
    }
}
