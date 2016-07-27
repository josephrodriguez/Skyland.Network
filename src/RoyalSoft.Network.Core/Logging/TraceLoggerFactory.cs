#region using

using System;

#endregion

namespace RoyalSoft.Network.Core.Logging
{
    public class TraceLoggerFactory : AbstractLoggerFactory
    {
        protected override ILog GetLogger(Type type)
        {
            throw new NotImplementedException();
        }
    }
}
