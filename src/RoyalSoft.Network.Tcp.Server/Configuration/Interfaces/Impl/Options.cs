namespace RoyalSoft.Network.Tcp.Server.Configuration.Interfaces.Impl
{
    internal class Options : IOptions
    {
        public int AllowedConnections { get; internal set; }
        public int ReadTimeout { get; internal set; }
        public int WriteTimeout { get; set; }
    }
}
