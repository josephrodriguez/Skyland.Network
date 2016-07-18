#region using

using RoyalSoft.Network.Core.Compression;
using RoyalSoft.Network.Tcp.Server.Configuration.Interfaces;
using RoyalSoft.Network.Tcp.Server.Configuration.Interfaces.Impl;

#endregion

namespace RoyalSoft.Network.Tcp.Server.Configuration.Configurers
{
    public class CompressionConfigurer : BaseConfigurer<ICompression>
    {
        private CompressionMethod _method;

        public CompressionConfigurer UseMethod(CompressionMethod method)
        {
            _method = method;
            return this;
        }

        internal override ICompression Build()
        {
            return 
                new Compression
                {
                    Enabled = true,
                    Method = _method
                };
        }
    }
}
