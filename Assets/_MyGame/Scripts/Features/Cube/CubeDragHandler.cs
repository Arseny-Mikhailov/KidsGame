using _MyGame.Scripts.Services.Tower;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

namespace _MyGame.Scripts.Features.Cube
{
    public class CubeDragHandler : MonoBehaviour,
        IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [Inject] private ICubeDragService _dragService;

        public TowerService TowerService { get; set; }
        public bool IsExisting { get; set; }
        public Vector3 OriginalPosition { get; set; }

        private Canvas _canvas;
        private GameObject _dragObj;

        private void Awake()
        {
            _canvas = GetComponentInParent<Canvas>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            OriginalPosition = transform.position;
            _dragObj = _dragService.BeginDrag(gameObject, TowerService, this);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_dragObj == null) return;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.transform as RectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out var localPoint
            );

            _dragObj.transform.localPosition = localPoint;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_dragObj == null) return;
            _dragService.EndDrag(_dragObj, eventData.position, this);
            _dragObj = null;
        }
    }
}