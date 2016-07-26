#region using

using RoyalSoft.Network.Core.Compression;
using RoyalSoft.Network.Tcp.Server.Configuration.Interfaces;

#endregion

namespace RoyalSoft.Network.Tcp.Server.Internal.Configuration
{
    internal class Compression : ICompression
    {
        public bool Enabled { get; set; }
        public CompressionMethod Method { get; set; }

        public override string ToString()
        {
            return Method.ToString();
        }
    }
}
