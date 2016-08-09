namespace RoyalSoft.Network.Core.Pipeline.Internal
{
    public delegate void StageCompletedEventHandler<in TElement>(object sender, TElement element);
}
