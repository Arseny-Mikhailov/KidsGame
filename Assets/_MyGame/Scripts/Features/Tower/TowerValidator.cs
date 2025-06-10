using UnityEngine;

namespace _MyGame.Scripts.Features.Tower
{
    public class TowerValidator
    {
        private readonly RectTransform _towerZone;
        private readonly Camera _camera;

        public TowerValidator(RectTransform towerZone, Camera camera, float cubeHeight)
        {
            _towerZone = towerZone;
            _camera = camera;
        }
        
        public bool IsOverValidSurface(Vector3 screenPos, Transform lastCube)
        {
            if (lastCube == null)
            {
                return RectTransformUtility.RectangleContainsScreenPoint(
                    _towerZone,
                    screenPos,
                    _camera
                );
            }

            return RectTransformUtility.RectangleContainsScreenPoint(
                lastCube.GetComponent<RectTransform>(),
                screenPos,
                _camera
            );
        }
    }
}