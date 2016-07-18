#region using

using RoyalSoft.Network.Core.Compression;

#endregion

namespace RoyalSoft.Network.Tcp.Server.Configuration.Interfaces.Impl
{
    internal class Compression : ICompression
    {
        public bool Enabled { get; set; }
        public CompressionMethod Method { get; set; }

        public override string ToString()
        {
            return $"{Method}";
        }
    }
}
