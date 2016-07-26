namespace RoyalSoft.Network.Tcp.Server.Configuration.Interfaces
{
    public interface IOptions
    {
        int AllowedConnections { get; }
        int MaximunMessageSize { get; }
        int ReadTimeout { get; }
        int WriteTimeout { get; set; }
    }
}
