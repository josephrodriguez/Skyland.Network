namespace RoyalSoft.Network.Tcp.Server.Configuration.Interfaces
{
    public interface IConfiguration
    {
        int AllowedConnections { get; }
        int MaximunMessageSize { get; }
        int ReadTimeout { get; }
        int WriteTimeout { get; }

        int ReceiveBufferSize { get; }
        int SendBufferSize { get; }

        int SendTimeout { get; }
        int ReceiveTimeout { get; }
    }
}
