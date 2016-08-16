#region using

using RoyalSoft.Network.Core.Compression;

#endregion

namespace RoyalSoft.Network.Tcp.Server.Configuration.Interfaces
{
    public interface ICompressConfiguration
    {
        bool Enabled { get; }
        ICompressor Compressor { get; set; }
    }
}
