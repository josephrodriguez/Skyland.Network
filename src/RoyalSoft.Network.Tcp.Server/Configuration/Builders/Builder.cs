#region using

#endregion

namespace RoyalSoft.Network.Tcp.Server.Configuration.Builders
{
    public abstract class Builder<TComponent>
    {
        internal abstract TComponent Build();
    }
}
