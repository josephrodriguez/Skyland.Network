#region using

using RoyalSoft.Network.Tcp.Compression;

#endregion

namespace RoyalSoft.Network.Tcp.Server.Configuration
{
    public class CompressionConfigurer
    {
        public CompressionConfigurer UseMethod(CompressionMethod method)
        {
            return this;
        }
    }
}
