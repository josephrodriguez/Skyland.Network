#region using

using System;
using System.Net;
using RoyalSoft.Network.Core.Asserts;
using RoyalSoft.Network.Core.Logging;
using RoyalSoft.Network.Tcp.Server.Configuration.Interfaces;
using RoyalSoft.Network.Tcp.Server.Internal;
using RoyalSoft.Network.Tcp.Server.Internal.Configuration;

#endregion

namespace RoyalSoft.Network.Tcp.Server.Configuration.Builders
{
    public class ServerConfigurer
    {
        private ISslConfiguration _sslConfig;
        private IEvents _events;
        private ICompression _compressionConfig;
        private IConfiguration _options;

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
        public ServerConfigurer Compression(Action<CompressionBuilder> action)
        {
            Assert.IsNotNull(action);
            Assert.IsNull(_compressionConfig);

            var builder = new CompressionBuilder();
            action(builder);

            _compressionConfig = builder.Build();
            return this;
        }

        public ServerConfigurer Events(Action<EventsBuilder> configureAction)
        {
            Assert.IsNotNull(configureAction);
            Assert.IsNull(_events);

            var builder = new EventsBuilder();
            configureAction(builder);

            _events = builder.Build();
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public ServerConfigurer Ssl(Action<SslBuilder> action)
        {
            Assert.IsNotNull(action);
            Assert.IsNull(_sslConfig);

            var configurer = new SslBuilder();
            action(configurer);

            //Build SSL configuration
            _sslConfig = configurer.Build();

            return this;
        }

        public ServerConfigurer Options(Action<ConfigurationBuilder> action)
        {
            Assert.IsNotNull(action);
            Assert.IsNull(_options);

            var configurer = new ConfigurationBuilder();
            action(configurer);

            //Build optional configuration
            _options = configurer.Build();

            return this;
        }

        public ServerConfigurer Logging(Action<LoggingBuilder> action)
        {
            var builder = new LoggingBuilder();
            action(builder);

            LoggerFactory.Current = builder.Build();
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

        private static IConfiguration GetDefaultOptions()
        {
            var configurer = new ConfigurationBuilder();
            return configurer.Build();
        }
    }
}
