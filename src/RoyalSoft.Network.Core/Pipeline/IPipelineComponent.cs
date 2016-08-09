namespace RoyalSoft.Network.Core.Pipeline
{
    public interface IPipelineComponent<T>
    {
        T Execute(T input);
    }
}
