#region using

using System;
using System.Net;
using RoyalSoft.Network.Core.Asserts;
using RoyalSoft.Network.Tcp.Server.Configuration.Interfaces;
using RoyalSoft.Network.Tcp.Server.Configuration.Interfaces.Impl;
using RoyalSoft.Network.Tcp.Server.Impl;

#endregion

namespace RoyalSoft.Network.Tcp.Server.Configuration.Configurers
{
    public class ServerConfigurer
    {
        private ISslConfiguration _sslConfig;
        private ICompressionConfiguration _compressionConfig;

        private readonly IPEndPoint _endpoint;

        protected internal ServerConfigurer(IPEndPoint endpoint)
        {
            _endpoint = endpoint;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public ServerConfigurer Compression(Action<CompressionConfigurer> action)
        {
            Assert.IsNotNull(action);
            Assert.IsNull(_compressionConfig);

            _compressionConfig = new CompressionConfiguration {Enabled = true};

            var configurer = new CompressionConfigurer(_compressionConfig);
            action(configurer);

            //Set defaults
            configurer.SetDefaultConfig();

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public ServerConfigurer Ssl(Action<SslConfigurer> action)
        {
            Assert.IsNotNull(action);
            Assert.IsNull(_sslConfig);

            _sslConfig = new SslConfiguration {Enabled = true};

            var configurer = new SslConfigurer(_sslConfig);
            action(configurer);

            //Set default values
            configurer.SetDefaultConfig();

            return this;
        }

        /// <summary>
        /// Create an instance of <see cref="P:RoyalSoft.Network.Tcp.Server.IHubServer"/> with configuration.
        /// </summary>
        /// <returns>Create an instance of <see cref="P:RoyalSoft.Network.Tcp.Server.IHubServer"/> with configuration.</returns>
        public IHubServer Build()
        {
            if (_sslConfig == null)
                _sslConfig = new SslConfiguration {Enabled = false};

            if(_compressionConfig == null)
                _compressionConfig = new CompressionConfiguration {Enabled = false};

            return 
                new DefaultHubServer(_endpoint)
                {
                    Ssl = _sslConfig,
                    Compression = _compressionConfig
                };
        }
    }
}
