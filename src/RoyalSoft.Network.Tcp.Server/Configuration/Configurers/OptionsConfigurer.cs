#region using

using System;
using RoyalSoft.Network.Tcp.Server.Configuration.Interfaces;
using RoyalSoft.Network.Tcp.Server.Internal.Configuration;

#endregion

namespace RoyalSoft.Network.Tcp.Server.Configuration.Configurers
{
    public class OptionsConfigurer : BaseConfigurer<IOptions>
    {
        private const int DefaultReadTimeout = 0x1388;
        private const int DefaultWriteTimeout = 0x1388;
        private const int DefaultMessageSize = 64*1024;
        private const int DefaultAllowedConnections = 0x1;

        private int _allowedConnections;
        private int _readTimeout, _writeTimeout, _maximumMessageSize;

        public OptionsConfigurer()
        {
            _allowedConnections = DefaultAllowedConnections;
            _readTimeout = DefaultReadTimeout;
            _writeTimeout = DefaultWriteTimeout;
            _maximumMessageSize = DefaultMessageSize;
        }

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

        public OptionsConfigurer MaximumMessageSize(int size)
        {
            if(size <= 0)
                throw new ArgumentOutOfRangeException();

            _maximumMessageSize = size;
            return this;
        }

        internal override IOptions Build()
        {
            return 
                new Options
                {
                    WriteTimeout = _writeTimeout,
                    ReadTimeout =  _readTimeout,
                    AllowedConnections = _allowedConnections,
                    MaximunMessageSize = _maximumMessageSize
                };
        }
    }
}
