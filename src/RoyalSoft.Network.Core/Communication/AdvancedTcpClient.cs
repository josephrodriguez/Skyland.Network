#region using

using System;
using System.Diagnostics;
using System.Net.Sockets;
using RoyalSoft.Network.Core.Thread;

#endregion

namespace RoyalSoft.Network.Core.Communication
{
    /// <summary>
    /// Decorator pattern
    /// </summary>
    public class AdvancedTcpClient
    {
        private Worker _readWorker, _statusWorker;

        private TcpClient _component;

        public AdvancedTcpClient(TcpClient client)
        {
            _component = client;
        }

        public void StartReading()
        {
            _readWorker = new Worker(ExecuteReadOnBackground);
            _readWorker.StartForever(TimeSpan.FromSeconds(10));
        }

        public void CancelRead()
        {
            _readWorker.Cancel();
        }

        public void StartMonitor()
        {
            _statusWorker = new Worker(ExecuteStatusCheckOnBackground);
            _statusWorker.StartForever(TimeSpan.FromSeconds(1));
        }

        public void CancelMonitor()
        {
            _statusWorker.Cancel();
        }

        private void ExecuteReadOnBackground()
        {
            var stream = _component.GetStream();
            if (!stream.CanRead || !stream.DataAvailable)
                return;

            while (stream.DataAvailable)
            {
                var bufferSize = Math.Min(_component.ReceiveBufferSize, _component.Available);
                var buffer = new byte[bufferSize];

                var count = stream.Read(buffer, 0, buffer.Length);
                if (count <= 0)
                    return;
            }

            Trace.WriteLine($"Execute read from {_component.Client.RemoteEndPoint}.");
        }

        private void ExecuteStatusCheckOnBackground()
        {
            var connected =
                _component.Available > 0 ||
                (_component.Connected && !_component.Client.Poll(1000, SelectMode.SelectRead));

            if(!connected)

            Trace.WriteLine($"Execute status:{connected} from {_component.Client.RemoteEndPoint}.");
        }
    }
}
