#region using



#endregion

namespace RoyalSoft.Network.Tcp.Server.Configuration.Interfaces
{
    public interface IEvents
    {
        void RaiseMessageReceived(Message message);
    }
}
