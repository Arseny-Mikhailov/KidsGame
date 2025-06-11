using _MyGame.Scripts.Core;
using _MyGame.Scripts.Services.Cube;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;
using _MyGame.Scripts.Services.Tower;

namespace _MyGame.Scripts.Features.Cube
{
    public class CubeDragHandler : MonoBehaviour,
        IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [Inject] public TowerService TowerService { get; set; }
        [Inject] private ICubeDragService _dragService;

        private Canvas _canvas;
        private DragContext _ctx;

        private void Awake()
        {
            _canvas = GetComponentInParent<Canvas>();
        }

        public void OnBeginDrag(PointerEventData e)
        {
            _ctx = _dragService.BeginDrag(gameObject);
        }

        public void OnDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.transform as RectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out var local);
            
            _ctx.Dragged.transform.localPosition = local;
        }

        public void OnEndDrag(PointerEventData e)
        {
            _dragService.EndDrag(_ctx, e.position);
            _ctx = null;
        }
    }
}