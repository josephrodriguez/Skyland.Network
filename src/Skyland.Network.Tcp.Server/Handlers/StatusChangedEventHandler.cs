#region using

using System.Net;
using RoyalSoft.Network.Tcp.Server.Enum;

#endregion

namespace RoyalSoft.Network.Tcp.Server.Handlers
{
    public delegate void StatusChangedEventHandler(EndPoint endpoint, ConnectionState status);
}
