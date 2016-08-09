#region using

using System;

#endregion

namespace RoyalSoft.Network.Tcp.Server.Configuration.Interfaces
{
    public interface ICompression
    {
        bool Enabled { get; }
        Type Type { get; set; }
    }
}
