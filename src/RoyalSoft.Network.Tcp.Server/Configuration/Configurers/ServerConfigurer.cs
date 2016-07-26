#region using

using System;
using System.Net;
using RoyalSoft.Network.Core.Asserts;
using RoyalSoft.Network.Tcp.Server.Configuration.Interfaces;
using RoyalSoft.Network.Tcp.Server.Internal;
using RoyalSoft.Network.Tcp.Server.Internal.Configuration;

#endregion

namespace RoyalSoft.Network.Tcp.Server.Configuration.Configurers
{
    public class ServerConfigurer
    {
        private ISsl _sslConfig;
        private IEvents _events;
        private ICompression _compressionConfig;
        private IOptions _options;

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

            _compressionConfig = configurer.Build();
            return this;
        }

        public ServerConfigurer Events(Action<EventsConfigurer> configureAction)
        {
            Assert.IsNotNull(configureAction);
            Assert.IsNull(_events);

            var configurer = new EventsConfigurer();
            configureAction(configurer);

            _events = configurer.Build();
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
            Assert.IsNull(_options);

            var configurer = new OptionsConfigurer();
            action(configurer);

            //Build optional configuration
            _options = configurer.Build();

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

            if (_options == null)
                _options = GetDefaultOptions();

            return
                new DefaultHubServer(_endpoint, _sslConfig, _compressionConfig, _events, _options);
        }

        private static IOptions GetDefaultOptions()
        {
            var configurer = new OptionsConfigurer();
            return configurer.Build();
        }
    }
}
