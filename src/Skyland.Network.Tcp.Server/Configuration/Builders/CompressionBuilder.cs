#region using

using System;
using RoyalSoft.Network.Core.Compression;
using RoyalSoft.Network.Tcp.Server.Configuration.Interfaces;
using RoyalSoft.Network.Tcp.Server.Internal.Configuration;

#endregion

namespace RoyalSoft.Network.Tcp.Server.Configuration.Builders
{
    public class CompressionBuilder : Builder<ICompressConfiguration>
    {
        private bool _enabled;
        private ICompressor _compressor;

        public CompressionBuilder Gzip()
        {
            if(_compressor != null)
                throw new Exception();

            _compressor = new GZipCompressor();
            _enabled = true;

            return this;
        }

        internal override ICompressConfiguration Build()
        {
            return 
                new Compression
                {
                    Enabled = _enabled,
                    Compressor = _compressor
                };
        }
    }
}
