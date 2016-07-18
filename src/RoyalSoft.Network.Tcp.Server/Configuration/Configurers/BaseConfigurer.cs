#region using

#endregion

namespace RoyalSoft.Network.Tcp.Server.Configuration.Configurers
{
    public abstract class BaseConfigurer<TComponent>
    {
        internal abstract TComponent Build();
    }
}
