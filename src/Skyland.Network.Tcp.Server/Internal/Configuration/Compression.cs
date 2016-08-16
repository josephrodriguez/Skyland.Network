#region using

using System;
using RoyalSoft.Network.Core.Compression;
using RoyalSoft.Network.Tcp.Server.Configuration.Interfaces;

#endregion

namespace RoyalSoft.Network.Tcp.Server.Internal.Configuration
{
    internal class Compression : ICompressConfiguration
    {
        public bool Enabled { get; set; }
        public ICompressor Compressor { get; set; }
    }
}
