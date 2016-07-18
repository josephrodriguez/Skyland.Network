#region using

using RoyalSoft.Network.Tcp.Server.Configuration.Interfaces;
using RoyalSoft.Network.Tcp.Server.Configuration.Interfaces.Impl;

#endregion

namespace RoyalSoft.Network.Tcp.Server.Configuration.Configurers
{
    public class OptionsConfigurer : BaseConfigurer<IOptions>
    {
        private const int DefaultReadTimeout = 5000;
        private const int DefaultWriteTimeout = 500;
        private const int DefaultAllowedConnections = 3;

        private int _allowedConnections;
        private int _readTimeout, _writeTimeout;

        public OptionsConfigurer AllowedConnections(int count)
        {
            _allowedConnections = count;
            return this;
        }

        public OptionsConfigurer ReadTimeout(int timeout)
        {
            _readTimeout = timeout;
            return this;
        }

        public OptionsConfigurer WriteTimeout(int timeout)
        {
            _writeTimeout = timeout;
            return this;
        }

        internal override IOptions Build()
        {
            return 
                new Options
                {
                    WriteTimeout = _writeTimeout > 0 ? _writeTimeout : DefaultWriteTimeout,
                    ReadTimeout = _readTimeout > 0 ? _readTimeout : DefaultReadTimeout,
                    AllowedConnections = _allowedConnections > 0 ? _allowedConnections : DefaultAllowedConnections
                };
        }
    }
}
