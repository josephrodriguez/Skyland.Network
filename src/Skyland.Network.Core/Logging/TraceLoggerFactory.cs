#region using

using System;

#endregion

namespace RoyalSoft.Network.Core.Logging
{
    public class TraceLoggerFactory : LoggerFactory
    {
        protected override ILog GetLogger(Type type)
        {
            return new TraceLogger(type);
        }
    }
}
