#region using

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using RoyalSoft.Network.Core.Thread;
using RoyalSoft.Network.Tcp.Server.Configuration.Interfaces;
using RoyalSoft.Network.Tcp.Server.Dispatchers;
using RoyalSoft.Network.Tcp.Server.Enum;
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

        private ConcurrentDictionary<EndPoint, ConnectionMonitor> _monitors;
        private ConcurrentDictionary<EndPoint, MessageDispatcher> _dispatchers; 

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint"></param> <param name="ssl"></param> <param name="compression"></param> <param name="events"></param> <param name="options"></param>
        protected internal DefaultHubServer(IPEndPoint endpoint, ISsl ssl, ICompression compression, IEvents events, IOptions options)
        {
            if(ssl == null)
                throw new ArgumentNullException(nameof(ssl));

            _endpoint = endpoint;
            _connectionsCount = 0;

            _monitors = new ConcurrentDictionary<EndPoint, ConnectionMonitor>();
            _dispatchers = new ConcurrentDictionary<EndPoint, MessageDispatcher>();

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
                try
                {
                    var client = _listener.AcceptTcpClient();

                    if (_connectionsCount >= _options.AllowedConnections)
                        client.Close();
                    else
                        AcceptConnectedClient(client);
                }
                catch (Exception)
                {
                }
            }
        }

        private void AcceptConnectedClient(TcpClient client)
        {
            IncrementConnections();

            var socket = client.Client;

            if (CreateMonitorForSocket(socket) && CreateDispatcherForSocket(socket))
                return;

            DecrementConnections();
            Cleanup(socket.RemoteEndPoint);
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
            Trace.WriteLine($"Execute status:{status} from {endpoint}.");

            if (status != ConnectionState.Closed) return;

            DecrementConnections();
            Cleanup(endpoint);
        }

        private void DispatcherOnOnMessageReceived(Message message)
        {
            Trace.WriteLine($"Execute message:{message.Endpoint} from {Convert.ToBase64String(message.Data)}.");
        }
    }
}
