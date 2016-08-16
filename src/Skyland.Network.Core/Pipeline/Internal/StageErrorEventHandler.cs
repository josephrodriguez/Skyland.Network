#region using

using RoyalSoft.Network.Core.Pipeline.Handlers;

#endregion

namespace RoyalSoft.Network.Core.Pipeline.Internal
{
    public delegate void StageErrorEventHandler<TElement>(object sender, ErrorArgs<TElement> args);
}
