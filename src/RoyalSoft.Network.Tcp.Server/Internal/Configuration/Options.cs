using RoyalSoft.Network.Tcp.Server.Configuration.Interfaces;

namespace RoyalSoft.Network.Tcp.Server.Internal.Configuration
{
    internal class Options : IConfiguration
    {
        public int AllowedConnections { get; internal set; }
        public int MaximunMessageSize { get; internal set; }
        public int ReadTimeout { get; internal set; }
        public int WriteTimeout { get; internal set; }
        public int ReceiveBufferSize { get; internal set; }
        public int SendBufferSize { get; internal set; }
        public int SendTimeout { get; internal set; }
        public int ReceiveTimeout { get; internal set; }
    }
}
