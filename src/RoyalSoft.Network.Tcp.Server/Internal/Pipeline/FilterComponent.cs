#region using

using System.Net.Sockets;
using RoyalSoft.Network.Core.Pipeline;

#endregion

namespace RoyalSoft.Network.Tcp.Server.Internal.Pipeline
{
    class FilterComponent : IPipelineComponent<TcpClient>
    {
        public TcpClient Execute(TcpClient input)
        {
            return input;
        }
    }
}
