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
        private ISsl _sslConfig;
        private IEvents _events;
        private ICompression _compressionConfig;
        private IOptions _optionsConfig;

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


            var configurer = new CompressionConfigurer();
            action(configurer);

            //Build configuration
            _compressionConfig = configurer.Build();

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

            var configurer = new SslConfigurer();
            action(configurer);

            //Build SSL configuration
            _sslConfig = configurer.Build();

            return this;
        }

        public ServerConfigurer Options(Action<OptionsConfigurer> action)
        {
            Assert.IsNotNull(action);
            Assert.IsNull(_optionsConfig);

            var configurer = new OptionsConfigurer();
            action(configurer);

            //Build optional configuration
            _optionsConfig = configurer.Build();

            return this;
        }

        /// <summary>
        /// Create an instance of <see cref="P:RoyalSoft.Network.Tcp.Server.IHubServer"/> with configuration.
        /// </summary>
        /// <returns>Create an instance of <see cref="P:RoyalSoft.Network.Tcp.Server.IHubServer"/> with configuration.</returns>
        public IHubServer Create()
        {
            if (_sslConfig == null)
                _sslConfig = new Ssl {Enabled = false};

            if(_compressionConfig == null)
                _compressionConfig = new Compression {Enabled = false};

            return
                new DefaultHubServer(_endpoint, _sslConfig, _compressionConfig, _events, _optionsConfig);
        }
    }
}
