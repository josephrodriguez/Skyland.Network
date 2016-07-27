#region using

using System;

#endregion

namespace RoyalSoft.Network.Core.Logging
{
    public class NullLoggerFactory : AbstractLoggerFactory
    {
        private static NullLogger _logger;

        protected override ILog GetLogger(Type type)
        {
            return _logger ?? (_logger = new NullLogger());
        }
    }
}
