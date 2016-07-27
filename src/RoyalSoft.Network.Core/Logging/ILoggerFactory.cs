namespace RoyalSoft.Network.Core.Logging
{
    public interface ILoggerFactory
    {
        ILog GetLogger<T>();
    }
}
