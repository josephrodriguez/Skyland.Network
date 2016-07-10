#region using

using RoyalSoft.Network.Tcp.Compression;

#endregion

namespace RoyalSoft.Network.Tcp.Client.Configuration
{
    public class CompressionConfigurer
    {
        public CompressionConfigurer UseMethod(CompressionMethod method)
        {
            return this;
        }
    }
}
