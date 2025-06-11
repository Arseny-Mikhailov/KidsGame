using Core;
using Core.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Features.Cube.Drag
{
    public class CubeDragStrategy : IDragStrategy
    {
        private readonly ICubeDragService _dragService;
        private readonly Canvas _canvas;
        private readonly GameObject _targetObject;
        
        private DragContext _ctx;

        public CubeDragStrategy(ICubeDragService dragService, Canvas canvas, GameObject targetObject)
        {
            _dragService = dragService;
            _canvas = canvas;
            _targetObject = targetObject;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _ctx = _dragService.BeginDrag(_targetObject);
        }

        public void OnDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                (RectTransform)_canvas.transform,
                eventData.position,
                eventData.pressEventCamera,
                out var local);
            
            _ctx.Dragged.transform.localPosition = local;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _dragService.EndDrag(_ctx, eventData.position);
        }
    }
}