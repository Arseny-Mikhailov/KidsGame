using _MyGame.Scripts.Core;
using _MyGame.Scripts.Features.Cube;
using _MyGame.Scripts.Features.Hole;
using _MyGame.Scripts.Services.Cube.Strategies;
using _MyGame.Scripts.Services.Tower;
using _MyGame.Scripts.Localization;
using UnityEngine;

namespace _MyGame.Scripts.Services.Cube
{
    public class ExistingCubeDropStrategy : ICubeDropStrategy
    {
        private readonly TowerService _tower;
        private readonly HoleDetector _holeDetector;
        private readonly LocalizationConfig _localizationConfig;

        public ExistingCubeDropStrategy(TowerService towerService, HoleDetector holeDetector, LocalizationConfig localizationConfig)
        {
            _tower = towerService;
            _holeDetector = holeDetector;
            _localizationConfig = localizationConfig;
        }

        public DropResult Handle(DragContext ctx, Vector2 screenPos)
        {
            var dragged = ctx.Dragged;
            var view    = dragged.GetComponent<CubeView>();
            var anim    = dragged.GetComponent<CubeAnimator>();

            if (_holeDetector.IsInHole(dragged.transform.position))
            {
                int idx = _tower.GetIndexOf(view);
                _tower.Remove(view);
                _tower.CollapseTowerFromIndex(idx);
                anim.Explode();
                
                return new DropResult(true, _localizationConfig.CubeThrownToHole.ToString());
            }

            dragged.transform.SetParent(_tower.GetParent(), true);
            anim.AnimateSettle(ctx.OriginalPosition);
            UnblockRaycasts(dragged);
            
            return new DropResult(false, _localizationConfig.CubeReturned.ToString());
        }

        private void UnblockRaycasts(GameObject go)
        {
            if (go.TryGetComponent<CanvasGroup>(out var cg))
                cg.blocksRaycasts = true;
        }
    }
}