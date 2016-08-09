#region using

using RoyalSoft.Network.Core.Logging;

#endregion

namespace RoyalSoft.Network.Tcp.Server.Configuration.Builders
{
    public class LoggingBuilder : Builder<ILoggerFactory>
    {
        private ILoggerFactory _factory;

        public void Console() {
            _factory = new ConsoleLoggerFactory();
        }

        public void Trace() {
            _factory = new TraceLoggerFactory();   
        }

        internal override ILoggerFactory Build()
        {
            return _factory ?? new NullLoggerFactory();
        }
    }
}
