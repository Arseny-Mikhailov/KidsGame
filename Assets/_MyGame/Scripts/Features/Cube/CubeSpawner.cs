using _MyGame.Scripts.Core;
using _MyGame.Scripts.Services.Tower;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _MyGame.Scripts.Features.Cube
{
    public class CubeSpawner : IInitializable
    {
        [Inject] private TowerService _tower;
        
        private readonly GameConfig _config;
        private readonly Transform _spawnParent;
        private readonly CubeView _cubePrefab;
        private readonly IObjectResolver _container;

        [Inject]
        public CubeSpawner(GameConfig config, CubeView cubePrefab, BottomPanel bottomPanel, IObjectResolver container)
        {
            _config = config;
            _cubePrefab = cubePrefab;
            _spawnParent = bottomPanel.content;
            _container = container;
        }

        public void Initialize()
        {
            foreach (var sprite in _config.cubes)
            {
                var cube = _container.Instantiate(_cubePrefab, _spawnParent);

                var view = cube.GetComponent<CubeView>();
                var handler = cube.GetComponent<CubeDragHandler>();

                view.SetSprite(sprite);
                handler.TowerService = _tower;
            }
        }
    }
}