#region using

using RoyalSoft.Network.Core.Compression;

#endregion

namespace RoyalSoft.Network.Tcp.Server.Configuration.Interfaces
{
    public interface ICompression
    {
        bool Enabled { get; }
        CompressionMethod Method { get; }
    }
}
