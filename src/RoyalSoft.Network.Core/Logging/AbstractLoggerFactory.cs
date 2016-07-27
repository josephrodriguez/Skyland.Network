#region using

using System;

#endregion

namespace RoyalSoft.Network.Core.Logging
{
    public abstract class AbstractLoggerFactory : ILoggerFactory
    {
        protected abstract ILog GetLogger(Type type);

        public ILog GetLogger<T>()
        {
            return GetLogger(typeof(T));
        }
    }
}
