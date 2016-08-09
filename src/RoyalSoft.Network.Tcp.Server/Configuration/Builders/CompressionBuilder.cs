#region using

using System;
using RoyalSoft.Network.Core.Compression;
using RoyalSoft.Network.Tcp.Server.Configuration.Interfaces;
using RoyalSoft.Network.Tcp.Server.Internal.Configuration;

#endregion

namespace RoyalSoft.Network.Tcp.Server.Configuration.Builders
{
    public class CompressionBuilder : Builder<ICompression>
    {
        private Type _type;

        public CompressionBuilder Use<T>() where T : ICompressor
        {
            _type = typeof (T);
            return this;
        }

        internal override ICompression Build()
        {
            return 
                new Compression
                {
                    Enabled = true,
                    Type = _type
                };
        }
    }
}
