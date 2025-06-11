using _MyGame.Scripts.Features.Cube;
using _MyGame.Scripts.Features.Hole;
using _MyGame.Scripts.Features.Tower;
using _MyGame.Scripts.Services;
using _MyGame.Scripts.Services.Cube;
using _MyGame.Scripts.Services.Tower;
using _MyGame.Scripts.Localization;
using _MyGame.Scripts.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _MyGame.Scripts.Core
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private GameConfig config;
        [SerializeField] private LocalizationConfig localizationConfig;
        [SerializeField] private CubeView cubePrefab;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(config);
            builder.RegisterInstance(localizationConfig);
            
            builder.RegisterInstance(cubePrefab);
            builder.RegisterComponentInHierarchy<Camera>();
            builder.RegisterComponentInHierarchy<BottomPanel>();
            builder.RegisterComponentInHierarchy<TowerZone>();
            builder.RegisterComponentInHierarchy<HoleView>();
            builder.RegisterComponentInHierarchy<MessageView>();
    
            builder.Register<TowerService>(Lifetime.Singleton);
            builder.Register<HoleDetector>(Lifetime.Singleton);
            builder.Register<CubeSpawner>(Lifetime.Singleton).AsImplementedInterfaces();
            
            builder.Register<NewCubeDropStrategy>(Lifetime.Singleton);
            builder.Register<ExistingCubeDropStrategy>(Lifetime.Singleton);
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
            
            builder.RegisterComponentInHierarchy<HorizontalScrollOrDrag>();
            
            builder.RegisterComponentInHierarchy<ScrollWithDragDetection>();
        }
    }
}