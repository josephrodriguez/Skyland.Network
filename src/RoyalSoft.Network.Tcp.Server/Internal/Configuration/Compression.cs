#region using

using System;
using RoyalSoft.Network.Tcp.Server.Configuration.Interfaces;

#endregion

namespace RoyalSoft.Network.Tcp.Server.Internal.Configuration
{
    internal class Compression : ICompression
    {
        public bool Enabled { get; set; }
        public Type Type { get; set; }

        public override string ToString()
        {
            return Type.ToString();
        }
    }
}
