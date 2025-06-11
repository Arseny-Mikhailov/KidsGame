using Core.Config;
using UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Features.Cube
{
    public class CubeSpawner : IInitializable
    {
        [Inject] private readonly GameConfig _config;
        [Inject] private readonly CubeView _cubePrefab;
        [Inject] private BottomPanel _bottomPanel;
        [Inject] private readonly IObjectResolver _container;

        private Transform _spawnParent;

        public void Initialize()
        {
            _spawnParent = _bottomPanel.content;
            
            foreach (var sprite in _config.cubes)
            {
                var cube = _container.Instantiate(_cubePrefab, _spawnParent);
                var view    = cube.GetComponent<CubeView>();
                view.SetSprite(sprite);
            }
        }
    }
}