using System.Collections.Generic;
using Features.Cube;
using Features.Tower;
using UnityEngine;

namespace Services
{
    public class TowerService
    {
        private readonly TowerState _state;
        private readonly TowerValidator _validator;
        private readonly TowerPositioning _positioning;
        private readonly Transform _parent;
        private readonly Camera _camera;
        private readonly float _cubeHeight;

        public IReadOnlyList<CubeView> CubesInTower => _state.All;

        public TowerService(TowerZone towerZone, Camera camera)
        {
            _state = new TowerState();
            _validator = new TowerValidator(towerZone.rect, camera);
            _positioning = new TowerPositioning(towerZone.rect, towerZone.cubeHeight, towerZone.cubeWidth);
            _parent = towerZone.transform;
            _camera = camera;
            _cubeHeight = towerZone.cubeHeight;
        }
        
        public Transform GetParent() => _parent;
        public void Remove(CubeView cube) => _state.Remove(cube); 
        public int GetIndexOf(CubeView cube) => _state.GetIndexOf(cube);
        public bool Contains(CubeView cube) => _state.Contains(cube);
        public int Count => _state.Count;
        public float CubeHeight => _cubeHeight;
        
        public bool CanPlaceCube(Vector3 screenPos)
        {
            if (_state.Count == 0)
            {
                return true;
            }

            var last = _state.Count > 0 ? _state.All[^1].transform : null;
            
            if (!_validator.IsOverValidSurface(screenPos))
            {
                return false;
            }
            
            if (!_validator.IsOverExistingCubes(screenPos, _state.All))
            {
                return false;
            }

            var nextWorldPos = _positioning.GetTopPosition(last);
            var scaledCubeHeight = _cubeHeight * (last != null ? last.lossyScale.y : _parent.lossyScale.y);
            var topEdgeWorldPos = nextWorldPos + Vector3.up * (scaledCubeHeight * 0.5f);
            var topEdgeScreenPos = _camera.WorldToScreenPoint(topEdgeWorldPos);

            if (!(topEdgeScreenPos.y > Screen.height)) return true;

            Debug.Log("Tower is too high!");

            return false;
        }

        public void CommitAddCube(CubeView cube)
        {
            _state.Add(cube);
        }

        public Vector3 GetTopPosition()
        {
            var last = _state.Count > 0 ? _state.All[^1].transform : null;
            return _positioning.GetTopPosition(last);
        }
        
        public void CollapseTowerFromIndex(int startIndex)
        {
            for (var i = startIndex; i < _state.Count; i++)
            {
                var cubeView = _state.All[i];
                var animator = cubeView.GetComponent<CubeAnimator>();
                var dropDistance = _cubeHeight * cubeView.transform.lossyScale.y;
                var targetPos = cubeView.transform.position - Vector3.up * dropDistance;

                if (animator != null)
                {
                    animator.AnimateSettle(targetPos);
                }
                else
                {
                    cubeView.transform.position = targetPos;
                }
            }
        }
    }
}