using UnityEngine;
using VContainer;

namespace _MyGame.Scripts.Features.Tower
{
    public class TowerPositioning
    {
        private readonly RectTransform _towerZone;
        private readonly float _cubeHeight;
        private readonly float _maxOffset;
        
        public TowerPositioning(RectTransform towerZone, float cubeHeight, float maxOffset)
        {
            _towerZone = towerZone;
            _cubeHeight = cubeHeight;
            _maxOffset = maxOffset;
        }
        
        public Vector3 GetTopPosition(Transform lastCube)
        {
            if (lastCube == null)
            {
                var zoneScaleY = _towerZone.lossyScale.y;
                var bottomY = _towerZone.position.y - (_towerZone.rect.height * 0.5f * zoneScaleY);
                
                return new Vector3(
                    _towerZone.position.x,
                    bottomY + (_cubeHeight * 0.5f * zoneScaleY), 
                    _towerZone.position.z);
            }
            
            var scaledCubeHeight = _cubeHeight * lastCube.lossyScale.y;
            
            var nextCenterY = lastCube.position.y + scaledCubeHeight;
            
            var scaledMaxOffset = _maxOffset * lastCube.lossyScale.x;
            var offsetX = Random.Range(-scaledMaxOffset, scaledMaxOffset);

            return new Vector3(
                lastCube.position.x + offsetX,
                nextCenterY,
                lastCube.position.z
            );
        }
    }
}