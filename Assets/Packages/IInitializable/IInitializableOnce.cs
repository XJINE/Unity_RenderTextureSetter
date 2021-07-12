public interface IInitializableOnce : IInitializable
{
    bool IsInitialized { get; }
}