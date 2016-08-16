#region using

using RoyalSoft.Network.Core.Compression;

#endregion

namespace RoyalSoft.Network.Tcp.Client.Configuration
{
    public class CompressionConfigurer
    {
        public CompressionConfigurer Use<T>() where T : ICompressor
        {
            return this;
        }
    }
}
