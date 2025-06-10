using System.Collections.Generic;
using _MyGame.Scripts.Features.Cube;
using _MyGame.Scripts.Features.Tower;
using UnityEngine;

namespace _MyGame.Scripts.Services.Tower
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
            _validator = new TowerValidator(towerZone.rect, camera, towerZone.cubeHeight);
            _positioning = new TowerPositioning(towerZone.rect, towerZone.cubeHeight, towerZone.cubeWidth);
            _parent = towerZone.transform;
            _camera = camera;
            _cubeHeight = towerZone.cubeHeight;
        }

        public bool CanPlaceCube(Vector3 screenPos)
        {
            Transform last = _state.Count > 0 ? _state.All[^1].transform : null;

            if (!_validator.IsOverValidSurface(screenPos, last))
            {
                return false;
            }

            Vector3 nextWorldPos = _positioning.GetTopPosition(last);
            float scaledCubeHeight = _cubeHeight * (last != null ? last.lossyScale.y : _parent.lossyScale.y);
            Vector3 topEdgeWorldPos = nextWorldPos + Vector3.up * (scaledCubeHeight * 0.5f);
            Vector3 topEdgeScreenPos = _camera.WorldToScreenPoint(topEdgeWorldPos);

            if (topEdgeScreenPos.y > Screen.height)
            {
                Debug.Log("Tower is too high!");
                return false;
            }

            return true;
        }

        public void CommitAddCube(CubeView cube)
        {
            _state.Add(cube);
        }

        public Vector3 GetTopPosition()
        {
            Transform last = _state.Count > 0 ? _state.All[^1].transform : null;
            return _positioning.GetTopPosition(last);
        }
        
        public Transform GetParent() => _parent;
        
        public bool Remove(CubeView cube) => _state.Remove(cube);
        public int GetIndexOf(CubeView cube) => _state.GetIndexOf(cube);
        public void CollapseTowerFromIndex(int startIndex)
        {
            for (int i = startIndex; i < _state.Count; i++)
            {
                var cubeView = _state.All[i];
                var animator = cubeView.GetComponent<CubeAnimator>();
                
                float dropDistance = _cubeHeight * cubeView.transform.lossyScale.y;
                
                Vector3 targetPos = cubeView.transform.position - Vector3.up * dropDistance;

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
        public bool Contains(CubeView cube) => _state.Contains(cube);
        public int Count => _state.Count;
    }
}