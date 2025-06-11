using _MyGame.Scripts.Core;
using _MyGame.Scripts.Features.Cube;
using _MyGame.Scripts.Features.Hole;
using _MyGame.Scripts.Services.Cube.Strategies;
using _MyGame.Scripts.Services.Tower;
using UnityEngine;

namespace _MyGame.Scripts.Services.Cube
{
    public class NewCubeDropStrategy : ICubeDropStrategy
    {
        private readonly TowerService _tower;
        private readonly HoleDetector _holeDetector;

        public NewCubeDropStrategy(TowerService towerService, HoleDetector holeDetector)
        {
            _tower = towerService;
            _holeDetector = holeDetector;
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
                return new DropResult(false, "Кубик разбился в дыре");
            }
            
            if (!_tower.CanPlaceCube(screenPos))
            {
                anim.Explode();
                return new DropResult(false, "Нельзя: упёрлись в потолок/мимо башни");
            }
            
            var topPos = _tower.GetTopPosition();
            
            if (_tower.Count > 0)
                anim.AnimateJumpTo(topPos, _tower.CubeHeight);
            
            _tower.CommitAddCube(view);
            return new DropResult(true, "Кубик поставлен в башню");
        }

        private void UnblockRaycasts(GameObject go)
        {
            if (go.TryGetComponent<CanvasGroup>(out var cg))
                cg.blocksRaycasts = true;
        }
    }
}