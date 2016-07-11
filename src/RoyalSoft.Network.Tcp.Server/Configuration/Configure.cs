using System.Net;
using RoyalSoft.Network.Tcp.Server.Configuration.Configurers;

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
        public static ServerConfigurer WithEndpoint(IPAddress address, int port)
        {
            var endpoint = new IPEndPoint(address, port);
            
            return WithEndpoint(endpoint);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public static ServerConfigurer WithEndpoint(IPEndPoint endpoint)
        {
            return new ServerConfigurer(endpoint);
        }
    }
}
