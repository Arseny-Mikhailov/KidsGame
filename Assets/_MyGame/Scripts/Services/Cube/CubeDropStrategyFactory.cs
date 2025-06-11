using Core.Interfaces;

namespace Services.Cube
{
    public class CubeDropStrategyFactory : ICubeDropStrategyFactory
    {
        private readonly NewCubeDropStrategy      _newStrategy;
        private readonly ExistingCubeDropStrategy _existingStrategy;

        public CubeDropStrategyFactory(NewCubeDropStrategy newStrategy,
            ExistingCubeDropStrategy existingStrategy)
        {
            _newStrategy      = newStrategy;
            _existingStrategy = existingStrategy;
        }

        public ICubeDropStrategy Get(bool isExisting)
        {
            return isExisting ? _existingStrategy : _newStrategy;
        }
    }
}