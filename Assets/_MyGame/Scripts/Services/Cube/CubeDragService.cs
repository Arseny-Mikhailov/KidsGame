using _MyGame.Scripts.Core;
using _MyGame.Scripts.Features.Cube;
using _MyGame.Scripts.Services.Cube.Strategies;
using _MyGame.Scripts.Services.Tower;
using UnityEngine;
using VContainer;

namespace _MyGame.Scripts.Services.Cube
{
    public class CubeDragService : ICubeDragService
    {
        [Inject] private readonly ICubeFactory _cubeFactory;
        [Inject] private readonly TowerService _tower;
        [Inject] private readonly IObjectResolver _resolver;
        [Inject] private readonly Transform _dragLayer;
        [Inject] private readonly ICubeDropStrategyFactory _strategyFactory;
        [Inject] private readonly ProgressService _progressService;
        [Inject] private readonly MessageService _messageService;
        
        public DragContext BeginDrag(GameObject original)
        {
            var cubeView = original.GetComponent<CubeView>();
            var exists = _tower.Contains(cubeView);

            var dragged = exists
                ? original
                : _cubeFactory.Clone(original, _dragLayer);

            dragged.transform.SetParent(_dragLayer, true);

            if (dragged.TryGetComponent<CanvasGroup>(out var cg))
            {
                cg.blocksRaycasts = false;
            }

            if (exists)
            {
                return new DragContext(original, dragged, true);
            }
            
            var handler = dragged.GetComponent<CubeDragHandler>();
            
            handler.enabled = true;
            
            _resolver.Inject(handler);

            return new DragContext(original, dragged, false);
        }

        public void EndDrag(DragContext ctx, Vector2 screenPos)
        {
            var result = _strategyFactory.Get(ctx.IsExisting).Handle(ctx, screenPos);

            if (result.Success)
            {
                _progressService.SaveTowerState();
            }

            _messageService.Show(result.Message);
        }
        
    }
}