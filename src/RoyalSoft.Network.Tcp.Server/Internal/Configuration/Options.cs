using RoyalSoft.Network.Tcp.Server.Configuration.Interfaces;

namespace RoyalSoft.Network.Tcp.Server.Internal.Configuration
{
    internal class Options : IOptions
    {
        public int AllowedConnections { get; internal set; }
        public int MaximunMessageSize { get; internal set; }
        public int ReadTimeout { get; internal set; }
        public int WriteTimeout { get; set; }
    }
}
