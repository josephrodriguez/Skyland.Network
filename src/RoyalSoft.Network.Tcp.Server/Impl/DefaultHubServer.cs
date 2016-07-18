#region using

using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using RoyalSoft.Network.Core.Communication;
using RoyalSoft.Network.Core.Thread;
using RoyalSoft.Network.Tcp.Server.Configuration.Interfaces;

#endregion

namespace RoyalSoft.Network.Tcp.Server.Impl
{
    internal class DefaultHubServer : IHubServer
    {
        private static readonly object SyncObject = new object();

        private readonly IPEndPoint _endpoint;

        private int _connectionsCount;
        private Worker _worker;

        private ConcurrentDictionary<EndPoint, AdvancedTcpClient> _clients; 

        private readonly ISsl _ssl;
        private readonly ICompression _compression;
        private readonly IEvents _events;
        private readonly IOptions _options;

        private TcpListener _listener;

        /// <summary>
        /// Return the connected clients count
        /// </summary>
        public int Count
        {
            get { return _connectionsCount; }
        }

        protected internal DefaultHubServer(IPEndPoint endpoint, ISsl ssl, ICompression compression, IEvents events, IOptions options)
        {
            if(ssl == null)
                throw new ArgumentNullException(nameof(ssl));

            _endpoint = endpoint;
            _connectionsCount = 0;

            _clients = new ConcurrentDictionary<EndPoint, AdvancedTcpClient>();

            _ssl = ssl;
            _compression = compression;
            _events = events;
            _options = options;
        }

        public void Start()
        {
            _listener = new TcpListener(_endpoint);

            _worker = new Worker(BeginAcceptClients);
            _worker.Start();
        }

        public void Stop()
        {
            _listener.Stop();

            _worker.Cancel();
        }

        private void BeginAcceptClients()
        {
            _listener.Start();

            while (true) {
                var client = _listener.AcceptTcpClient();

                if (_connectionsCount >= _options.AllowedConnections)
                    client.Close();
                else
                    AcceptConnectedClient(client);
            }
        }

        private void AcceptConnectedClient(TcpClient client)
        {
            var advanced = new AdvancedTcpClient(client);

            if (!_clients.TryAdd(client.Client.RemoteEndPoint, advanced))
                return;

            IncrementConnections();

            advanced.StartMonitor();
            advanced.StartReading();
        }

        private void IncrementConnections()
        {
            lock (SyncObject) {
                _connectionsCount++;
            }
        }

        private void DecrementConnections()
        {
            lock (SyncObject) {
                _connectionsCount--;
            }
        }
    }
}
