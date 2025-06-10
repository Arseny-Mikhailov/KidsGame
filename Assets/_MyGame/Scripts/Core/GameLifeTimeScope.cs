using _MyGame.Scripts.Features.Cube;
using _MyGame.Scripts.Features.Hole;
using _MyGame.Scripts.Features.Tower;
using _MyGame.Scripts.Services;
using _MyGame.Scripts.Services.Cube;
using _MyGame.Scripts.Services.Tower;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _MyGame.Scripts.Core
{
    public class GameLifeTimeScope : LifetimeScope
    {
        [SerializeField] private GameConfig config;
        [SerializeField] private CubeView cubePrefab;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(config);
            builder.RegisterInstance(cubePrefab);

            builder.RegisterComponentInHierarchy<Camera>();
            builder.RegisterComponentInHierarchy<BottomPanel>();
            builder.RegisterComponentInHierarchy<TowerZone>();
            builder.RegisterComponentInHierarchy<HoleView>();
            builder.RegisterComponentInHierarchy<MessageView>();
            
            builder.Register<TowerService>(Lifetime.Singleton);
            builder.Register<HoleDetector>(Lifetime.Singleton);
            builder.Register<ICubeDragService, CubeDragService>(Lifetime.Singleton);
            builder.Register<CubeSpawner>(Lifetime.Singleton).AsImplementedInterfaces();
            
            builder.Register<MessageService>(Lifetime.Singleton)
                .AsSelf();
            
            builder.Register<ProgressService>(Lifetime.Singleton)
                .AsSelf()                
                .AsImplementedInterfaces();
        }
    }
}