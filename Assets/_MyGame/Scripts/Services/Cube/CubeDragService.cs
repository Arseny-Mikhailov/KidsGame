using _MyGame.Scripts.Features.Cube;
using _MyGame.Scripts.Services.Tower;
using DG.Tweening;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _MyGame.Scripts.Services.Cube
{
    public class CubeDragService : ICubeDragService
    {
        [Inject] private readonly HoleDetector _holeDetector;
        [Inject] private readonly Camera _camera;
        [Inject] private readonly ProgressService _progressService;
        [Inject] private readonly MessageService  _messageService;
        
        private readonly Transform _dragLayer = GameObject.FindWithTag("DragLayer").transform;

        public GameObject BeginDrag(GameObject original, TowerService tower, CubeDragHandler handler)
        {
            handler.TowerService = tower;
            var isExisting = tower.Contains(original.GetComponent<CubeView>());
            handler.IsExisting = isExisting;

            GameObject dragObj;
            if (isExisting)
            {
                dragObj = original;
                dragObj.transform.SetParent(_dragLayer, true);
            }
            else
            {
                dragObj = Object.Instantiate(original, _dragLayer);
                var newHandler = dragObj.GetComponent<CubeDragHandler>()!;
                newHandler.enabled = true;
                newHandler.TowerService = tower;
                LifetimeScope.Find<LifetimeScope>().Container.Inject(newHandler);
                newHandler.IsExisting = false;
                newHandler.OriginalPosition = newHandler.transform.position;
            }

            if (dragObj.TryGetComponent<CanvasGroup>(out var cg))
                cg.blocksRaycasts = false;

            return dragObj;
        }

        public void EndDrag(GameObject dragged, Vector3 screenPos, CubeDragHandler handler)
        {
            var view = dragged.GetComponent<CubeView>()!;
            var anim = dragged.GetComponent<CubeAnimator>()!;
            var tower = handler.TowerService!;

            var isInHole = _holeDetector.IsInHole(dragged.transform.position);

            if (handler.IsExisting)
            {
                if (isInHole)
                {
                    int idx = tower.GetIndexOf(view);
                    tower.Remove(view);
                    tower.CollapseTowerFromIndex(idx);
                    _progressService.SaveTowerState();
                    anim.Explode();
                    _messageService.Show("Кубик выброшен в дыру");
                }
                else
                {
                    dragged.transform.SetParent(tower.GetParent(), true);
                    anim.AnimateSettle(handler.OriginalPosition);
                    if (dragged.TryGetComponent<CanvasGroup>(out var cga))
                        cga.blocksRaycasts = true;
                    _messageService.Show("Отмена: кубик возвращён");
                }

                return;
            }
            
            dragged.transform.SetParent(tower.GetParent(), true);
            if (dragged.TryGetComponent<CanvasGroup>(out var cg))
                cg.blocksRaycasts = true;

            if (_holeDetector.IsInHole(dragged.transform.position))
            {
                anim.Explode();
                _progressService.SaveTowerState();
                _messageService.Show("Кубик разбился в дыре");
                return;
            }

            if (tower.CanPlaceCube(screenPos))
            {
                if (tower.Count == 0)
                {
                    RectTransformUtility.ScreenPointToWorldPointInRectangle(
                        tower.GetParent() as RectTransform,
                        screenPos,
                        _camera,
                        out var targetPos
                    );
                    dragged.transform.DOMove(targetPos, 0.1f);
                }
                else
                {
                    var targetPos = tower.GetTopPosition();
                    anim.AnimateJumpTo(targetPos, handler.TowerService!
                        .GetType()
                        .GetField("_cubeHeight", System.Reflection
                        .BindingFlags.NonPublic | System.Reflection
                        .BindingFlags.Instance)!
                        .GetValue(handler.TowerService) as float? ?? 1f);
                }
                tower.CommitAddCube(view);
                _progressService.SaveTowerState();
                _messageService.Show("Кубик поставлен в башню");
            }
            else
            {
                anim.Explode();
                _messageService.Show("Нельзя: упёрлись в потолок/мимо башни");
            }
        }
    }
}