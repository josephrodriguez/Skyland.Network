#region using

using System;
using System.Net.Sockets;
using RoyalSoft.Network.Core.Pipeline;
using RoyalSoft.Network.Tcp.Server.Configuration.Interfaces;

#endregion

namespace RoyalSoft.Network.Tcp.Server.Internal.Pipeline
{
    class ConfigurationComponent : IPipelineComponent<TcpClient>
    {
        private readonly IConfiguration _configuration;

        public ConfigurationComponent(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TcpClient Execute(TcpClient client)
        {
            //Configure buffers size
            client.SendBufferSize = _configuration.SendBufferSize;
            client.ReceiveBufferSize = _configuration.ReceiveBufferSize;

            //Configure timeouts
            client.SendTimeout = _configuration.SendTimeout;
            client.ReceiveTimeout = _configuration.ReceiveTimeout;

            var stream = client.GetStream();
            if(stream == null)
                throw new Exception();

            stream.ReadTimeout = _configuration.ReadTimeout;
            stream.WriteTimeout = _configuration.WriteTimeout;

            return client;
        }
    }
}
