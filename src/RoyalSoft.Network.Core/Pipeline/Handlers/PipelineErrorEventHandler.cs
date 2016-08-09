#region using


#endregion

namespace RoyalSoft.Network.Core.Pipeline.Handlers
{
    public delegate void PipelineErrorEventHandler<T>(object sender, ErrorArgs<T> args);
}
