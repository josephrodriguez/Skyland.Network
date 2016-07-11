#region using

using RoyalSoft.Network.Core.Compression;
using RoyalSoft.Network.Core.Configuration;
using RoyalSoft.Network.Tcp.Server.Configuration.Interfaces;

#endregion

namespace RoyalSoft.Network.Tcp.Server.Configuration.Configurers
{
    public class CompressionConfigurer : BaseConfigurer<ICompressionConfiguration>
    {
        public CompressionConfigurer(ICompressionConfiguration component) 
            : base(component)
        {
        }

        public CompressionConfigurer UseMethod(CompressionMethod method)
        {
            Component.Method = method;
            return this;
        }
    }
}
