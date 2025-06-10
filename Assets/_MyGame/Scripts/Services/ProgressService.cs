using System;
using System.Collections.Generic;
using System.Linq;
using _MyGame.Scripts.Core;
using _MyGame.Scripts.Features.Cube;
using _MyGame.Scripts.Services.Tower;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _MyGame.Scripts.Services
{
    public class ProgressService : IInitializable
    {
        private const string TowerSaveKey = "TowerCubes";

        private readonly TowerService _towerService;
        private readonly GameConfig _gameConfig;
        private readonly CubeView _cubePrefab;
        private readonly IObjectResolver _container;

        public ProgressService(TowerService towerService, GameConfig gameConfig, CubeView cubePrefab, IObjectResolver container)
        {
            _towerService = towerService;
            _gameConfig = gameConfig;
            _cubePrefab = cubePrefab;
            _container = container;
        }

        public void Initialize()
        {
            LoadTower();
        }

        public void SaveTowerState()
        {
            var cubesInTower = _towerService.CubesInTower;
            if (cubesInTower.Count == 0)
            {
                PlayerPrefs.DeleteKey(TowerSaveKey);
                return;
            }

            var spriteIndexes = new List<int>();
            foreach (var cubeView in cubesInTower)
            {
                var sprite = cubeView.GetComponent<UnityEngine.UI.Image>().sprite;
                int index = Array.IndexOf(_gameConfig.cubes, sprite);
                if (index > -1)
                {
                    spriteIndexes.Add(index);
                }
            }

            string saveData = string.Join(",", spriteIndexes);
            PlayerPrefs.SetString(TowerSaveKey, saveData);
            PlayerPrefs.Save();
            Debug.Log($"Tower saved: {saveData}");
        }

        private void LoadTower()
        {
            if (!PlayerPrefs.HasKey(TowerSaveKey)) return;

            string saveData = PlayerPrefs.GetString(TowerSaveKey);
            if (string.IsNullOrEmpty(saveData)) return;

            Debug.Log($"Loading tower: {saveData}");
            var spriteIndexes = saveData.Split(',').Select(int.Parse).ToList();

            foreach (var index in spriteIndexes)
            {
                if (index < 0 || index >= _gameConfig.cubes.Length) continue;

                var cubeInstance = _container.Instantiate(_cubePrefab, _towerService.GetParent());
                var view = cubeInstance.GetComponent<CubeView>();
                var handler = cubeInstance.GetComponent<CubeDragHandler>();

                view.SetSprite(_gameConfig.cubes[index]);
                handler.TowerService = _towerService;

                var targetPos = _towerService.GetTopPosition();
                cubeInstance.transform.position = targetPos;

                _towerService.CommitAddCube(view);
            }
        }
    }
}