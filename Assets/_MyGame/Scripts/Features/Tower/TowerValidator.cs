using System.Collections.Generic;
using Features.Cube;
using UnityEngine;

namespace Features.Tower
{
    public class TowerValidator
    {
        private readonly RectTransform _towerZone;
        private readonly Camera _camera;

        public TowerValidator(RectTransform towerZone, Camera camera)
        {
            _towerZone = towerZone;
            _camera = camera;
        }
    
        public bool IsOverValidSurface(Vector3 screenPos)
        {
            return RectTransformUtility.RectangleContainsScreenPoint(_towerZone, screenPos, _camera);
        }

        public bool IsOverExistingCubes(Vector3 screenPos, IReadOnlyList<CubeView> cubesInTower)
        {
            foreach (var cube in cubesInTower)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(cube.GetComponent<RectTransform>(), screenPos, _camera))
                {
                    return true;
                }
            }
            return false;
        }
    }
}