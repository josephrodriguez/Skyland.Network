#region using

using System.Diagnostics;
using RoyalSoft.Network.Core.Compression;

#endregion

namespace RoyalSoft.Network.Tcp.Server.Configuration.Interfaces
{
    public interface ICompressionConfiguration
    {
        bool Enabled { get; set; }
        CompressionMethod Method { get; set; }
    }
}
