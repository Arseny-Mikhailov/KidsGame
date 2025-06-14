using Core.Config;
using Core.Interfaces;
using Factory;
using Features.Cube;
using Features.Hole;
using Features.Tower;
using Localization;
using Services;
using Services.Cube;
using UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.DI
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private GameConfig config;
        [SerializeField] private LocalizationConfig localizationConfig;
        [SerializeField] private CubeView cubePrefab;
        [SerializeField] private SceneContext sceneContext;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(config);
            builder.RegisterInstance(localizationConfig);
            builder.RegisterInstance(sceneContext);
            builder.RegisterInstance(cubePrefab);
            
            builder.RegisterComponentInHierarchy<Camera>();
            builder.RegisterComponentInHierarchy<BottomPanel>();
            builder.RegisterComponentInHierarchy<TowerZone>();
            builder.RegisterComponentInHierarchy<HoleView>();
            builder.RegisterComponentInHierarchy<MessageView>();
    
            builder.Register<TowerService>(Lifetime.Singleton);
            builder.Register<HoleDetector>(Lifetime.Singleton);
            builder.Register<NewCubeDropStrategy>(Lifetime.Singleton);
            builder.Register<ExistingCubeDropStrategy>(Lifetime.Singleton);
            builder.Register<CubeSpawner>(Lifetime.Singleton).AsImplementedInterfaces();
            
            builder.Register<CubeDropStrategyFactory>(Lifetime.Singleton)
                .As<ICubeDropStrategyFactory>();
            
            builder.Register<CubeDragService>(Lifetime.Singleton)
                .As<ICubeDragService>();
            
            builder.Register<MessageService>(Lifetime.Singleton)
                .AsSelf();
            
            builder.Register<ProgressService>(Lifetime.Singleton)
                .AsSelf()  
                .AsImplementedInterfaces();
            
            builder.RegisterInstance(GameObject
                .FindWithTag("DragLayer").transform);
            
            builder.Register<CubeFactory>(Lifetime.Singleton)
                .As<ICubeFactory>()
                .WithParameter(cubePrefab);
        }
    }
}