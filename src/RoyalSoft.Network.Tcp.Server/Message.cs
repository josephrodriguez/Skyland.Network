using System.Net;

namespace RoyalSoft.Network.Tcp.Server
{
    public class Message
    {
        public EndPoint Endpoint { get; private set; }
        public byte[] Data { get; private set; }

        internal Message(EndPoint endpoint, byte[] data)
        {
            Endpoint = endpoint;
            Data = data;
        }
    }
}
