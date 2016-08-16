namespace RoyalSoft.Network.Core.Pipeline.Handlers
{
    public delegate void PipelineCompletedEventHandler<in T>(T outputElement);
}
