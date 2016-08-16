using System.Net;
using RoyalSoft.Network.Tcp.Server.Configuration.Builders;
using RoyalSoft.Network.Tcp.Server.Internal;

namespace RoyalSoft.Network.Tcp.Server.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public static class Configure
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public static ServerBuilder WithEndpoint(IPAddress address, int port)
        {
            var endpoint = new IPEndPoint(address, port);
            
            return WithEndpoint(endpoint);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public static ServerBuilder WithEndpoint(IPEndPoint endpoint)
        {
            return new ServerBuilder(endpoint);
        }
    }
}
