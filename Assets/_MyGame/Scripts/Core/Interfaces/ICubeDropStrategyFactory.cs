using _MyGame.Scripts.Services.Cube.Strategies;

namespace _MyGame.Scripts.Services.Cube
{
    public interface ICubeDropStrategyFactory
    {
        ICubeDropStrategy Get(bool isExisting);
    }
}