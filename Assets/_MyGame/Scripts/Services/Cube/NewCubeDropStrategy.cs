using Core;
using Core.Interfaces;
using Features.Cube;
using Features.Hole;
using Localization;
using UnityEngine;

namespace Services.Cube
{
    public class NewCubeDropStrategy : ICubeDropStrategy
    {
        private readonly TowerService _tower;
        private readonly HoleDetector _holeDetector;
        private readonly LocalizationConfig _localizationConfig;

        public NewCubeDropStrategy(TowerService towerService, HoleDetector holeDetector, LocalizationConfig localizationConfig)
        {
            _tower = towerService;
            _holeDetector = holeDetector;
            _localizationConfig = localizationConfig;
        }

        public DropResult Handle(DragContext ctx, Vector2 screenPos)
        {
            var dragged = ctx.Dragged;
            var view = dragged.GetComponent<CubeView>();
            var anim = dragged.GetComponent<CubeAnimator>();
            
            dragged.transform.SetParent(_tower.GetParent(), true);
            UnblockRaycasts(dragged);
            
            if (_holeDetector.IsInHole(dragged.transform.position))
            {
                anim.Explode();
                
                return new DropResult(false, _localizationConfig.CubeDroppedToHole.ToString());
            }
            
            if (!_tower.CanPlaceCube(screenPos))
            {
                anim.Explode();
                
                return new DropResult(false, _localizationConfig.CubePlacementBlocked.ToString());
            }
            
            var topPos = _tower.GetTopPosition();
            
            if (_tower.Count > 0)
                anim.AnimateJumpTo(topPos, _tower.CubeHeight);
            
            _tower.CommitAddCube(view);
            
            return new DropResult(true, _localizationConfig.CubePlaced.ToString());
        }

        private void UnblockRaycasts(GameObject go)
        {
            if (go.TryGetComponent<CanvasGroup>(out var cg))
                cg.blocksRaycasts = true;
        }
    }
}