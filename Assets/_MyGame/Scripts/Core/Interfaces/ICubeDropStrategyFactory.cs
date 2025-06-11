namespace Core.Interfaces
{
    public interface ICubeDropStrategyFactory
    {
        ICubeDropStrategy Get(bool isExisting);
    }
}