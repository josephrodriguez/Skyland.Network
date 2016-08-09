#region using

using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using RoyalSoft.Network.Core.Compression;
using RoyalSoft.Network.Core.Logging;
using RoyalSoft.Network.Core.Pipeline;
using RoyalSoft.Network.Core.Pipeline.Handlers;
using RoyalSoft.Network.Core.Threading;
using RoyalSoft.Network.Tcp.Server.Configuration.Interfaces;
using RoyalSoft.Network.Tcp.Server.Dispatchers;
using RoyalSoft.Network.Tcp.Server.Enum;
using RoyalSoft.Network.Tcp.Server.Internal.Pipeline;
using RoyalSoft.Network.Tcp.Server.Monitors;

#endregion

namespace RoyalSoft.Network.Tcp.Server.Internal
{
    internal class DefaultHubServer : IHubServer
    {
        private static readonly object SyncObject = new object();

        private readonly IPEndPoint _endpoint;

        private int _connectionsCount;
        private Worker _worker;

        private readonly ConcurrentDictionary<EndPoint, ConnectionMonitor> _monitors;
        private readonly ConcurrentDictionary<EndPoint, MessageDispatcher> _dispatchers;

        private WeakReference<ICompressor> _compressorWeakRef; 

        private readonly ISslConfiguration _sslConfiguration;
        private readonly ICompression _compression;
        private readonly IEvents _events;
        private readonly IConfiguration _configuration;
        private readonly ILog _log;

        private TcpListener _listener;

        private IPipeline<TcpClient> _clientPipeline; 

        /// <summary>
        /// Return the connected clients count
        /// </summary>
        public int Count
        {
            get { return _connectionsCount; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint"></param> <param name="ssl"></param> <param name="compression"></param> <param name="events"></param> <param name="options"></param>
        protected internal DefaultHubServer(IPEndPoint endpoint, ISslConfiguration ssl, ICompression compression, IEvents events, IConfiguration options)
        {
            if(ssl == null)
                throw new ArgumentNullException(nameof(ssl));

            _endpoint = endpoint;
            _connectionsCount = 0;

            _monitors = new ConcurrentDictionary<EndPoint, ConnectionMonitor>();
            _dispatchers = new ConcurrentDictionary<EndPoint, MessageDispatcher>();

            _sslConfiguration = ssl;
            _compression = compression;
            _events = events;
            _configuration = options;
            _log = LoggerFactory.Current.GetLogger<DefaultHubServer>();
        }

        public void Start()
        {
            _log.Info("Starting Hub server: {0}", _endpoint);

            CreatePipeline();

            _listener = new TcpListener(_endpoint);
            _listener.Start();

            _worker = new Worker(ProcessClients);
            _worker.StartForever(TimeSpan.Zero);

            _log.Info("Started Hub server: {0}", _endpoint);
        }

        public void Stop()
        {
            _log.Info("Stopping Hub server");

            _listener.Stop();
            _worker.Cancel();

            _log.Info("Hub server stopped");
        }

        private void CreatePipeline()
        {
            _clientPipeline = new Pipeline<TcpClient>();
            _clientPipeline.OnCompleted += ClientPipelineOnOnCompleted;
            _clientPipeline.OnError += ClientPipelineOnOnError;

            _clientPipeline
                .Register(new FilterComponent())
                .Register(new ConfigurationComponent(_configuration));

            if (_sslConfiguration.Enabled)
                _clientPipeline.Register(new SslComponent(_sslConfiguration));
        }

        private void ClientPipelineOnOnError(object sender, ErrorArgs<TcpClient> args)
        {
        }

        private void ClientPipelineOnOnCompleted(TcpClient outputElement)
        {
        }

        private void ProcessClients()
        {
            try
            {
                var client = _listener.AcceptTcpClient();

                _clientPipeline.Execute(client);

                //Log information of socket
                _log.Debug("Connected socket ({0}, {1})", client.Client.RemoteEndPoint, client.Client.AddressFamily);

                //Raise OnClientConnected event
                _events.RaiseClientConnected(client.Client.RemoteEndPoint);

                if (_connectionsCount >= _configuration.AllowedConnections)
                {
                    client.Close();

                    _log.Debug("Rejected socket ({0}, {1})", client.Client.RemoteEndPoint, client.Client.AddressFamily);

                    _events.RaiseClientRejected(client.Client.RemoteEndPoint);

                }
                else
                {
                    AcceptConnectedClient(client);
                    _events.RaiseClientAccepted(client.Client.RemoteEndPoint);
                }
            }
            catch (Exception exception)
            {
                _log.Error(exception);
            }
        }

        private void AcceptConnectedClient(TcpClient client)
        {
            IncrementConnections();

            var socket = client.Client;

            if (ConfigureSocket(socket))
                return;

            DecrementConnections();
            Cleanup(socket.RemoteEndPoint);
        }

        private bool ConfigureSocket(Socket socket)
        {
            return 
                CreateMonitorForSocket(socket) && 
                CreateDispatcherForSocket(socket);
        }

        private void Cleanup(EndPoint endPoint)
        {
            ConnectionMonitor monitor;
            if (_monitors.TryRemove(endPoint, out monitor))
                monitor.Cancel();

            MessageDispatcher dispatcher;
            if (_dispatchers.TryRemove(endPoint, out dispatcher))
                dispatcher.Cancel();
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

        private bool CreateMonitorForSocket(Socket socket)
        {
            var monitor = new ConnectionMonitor(socket);
            monitor.OnStatusChanged += MonitorOnOnStatusChangedEvent;

            var result = _monitors.TryAdd(socket.RemoteEndPoint, monitor);
            if(!result)
                return false;

            monitor.Start();

            return true;
        }

        private bool CreateDispatcherForSocket(Socket socket)
        {
            var dispatcher = new MessageDispatcher(socket);
            dispatcher.OnMessageReceived += DispatcherOnOnMessageReceived;

            var result = _dispatchers.TryAdd(socket.RemoteEndPoint, dispatcher);
            if (!result)
                return false;

            dispatcher.Start();

            return true;
        }

        private void MonitorOnOnStatusChangedEvent(EndPoint endpoint, ConnectionState status)
        {
            if (status != ConnectionState.Closed) return;

            //Fire event handlers for disconnected client
            _events.RaiseClientDisconnected(endpoint);

            DecrementConnections();
            Cleanup(endpoint);
        }

        private void DispatcherOnOnMessageReceived(Message message)
        {
            if (_compression.Enabled)
            {
                var compressor = GetCompressor();
                message.Data = compressor.Decompress(message.Data);

                var e = Encoding.UTF8.GetString(message.Data);
                var t = e.Length;
            }

            _events.RaiseMessageReceived(message);
        }

        private ICompressor GetCompressor()
        {
            if(!_compression.Enabled) throw new Exception();

            ICompressor compressor;
            if(_compressorWeakRef != null && _compressorWeakRef.TryGetTarget(out compressor))
                return compressor;

            compressor = Activator.CreateInstance(_compression.Type) as ICompressor;
            if(compressor == null)
                throw new Exception();

            if(_compressorWeakRef == null)
                _compressorWeakRef = new WeakReference<ICompressor>(compressor);
            else
                _compressorWeakRef.SetTarget(compressor);

            return compressor;
        }
    }
}
