using System;
using System.Collections.Generic;
using System.Linq;
using Core.Config;
using Features.Cube;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Services
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
                var index = Array.IndexOf(_gameConfig.cubes, sprite);
                if (index > -1)
                {
                    spriteIndexes.Add(index);
                }
            }

            var saveData = string.Join(",", spriteIndexes);
            PlayerPrefs.SetString(TowerSaveKey, saveData);
            PlayerPrefs.Save();
            Debug.Log($"Tower saved: {saveData}");
        }

        private void LoadTower()
        {
            if (!PlayerPrefs.HasKey(TowerSaveKey)) return;

            var saveData = PlayerPrefs.GetString(TowerSaveKey);
            if (string.IsNullOrEmpty(saveData)) return;

            Debug.Log($"Loading tower: {saveData}");
            var spriteIndexes = saveData.Split(',').Select(int.Parse).ToList();

            foreach (var index in spriteIndexes)
            {
                if (index < 0 || index >= _gameConfig.cubes.Length) continue;

                var cubeInstance = _container.Instantiate(_cubePrefab, _towerService.GetParent());
                var view = cubeInstance.GetComponent<CubeView>();

                view.SetSprite(_gameConfig.cubes[index]);

                var targetPos = _towerService.GetTopPosition();
                cubeInstance.transform.position = targetPos;

                _towerService.CommitAddCube(view);
            }
        }
    }
}