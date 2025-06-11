using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;
using Core;
using Core.Interfaces;

namespace Features.Cube.Drag
{
    public class CubeDragHandler : MonoBehaviour, IInitializePotentialDragHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [Inject] private ICubeDragService _dragService;
        [Inject] private SceneContext _sceneContext;

        private IDragStrategy _currentDragStrategy;

        private Vector2 _startPosition;

        public void OnInitializePotentialDrag(PointerEventData eventData)
        {
            _startPosition = eventData.position;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _currentDragStrategy = GetDragStrategy(eventData);

            _currentDragStrategy.OnBeginDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _currentDragStrategy.OnDrag(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _currentDragStrategy.OnEndDrag(eventData);
        }

        /// <summary>
        /// Определяет стратегию драга в зависимости от нахождения куба внутри башни или внутри скролла
        /// </summary>
        private IDragStrategy GetDragStrategy(PointerEventData eventData)
        {
            var delta = eventData.position - _startPosition;
            var isInScrollRect = _sceneContext.BottomScroll != null && transform.IsChildOf(_sceneContext.BottomScroll.content);
            var isHorizontalDrag = Mathf.Abs(delta.x) > Mathf.Abs(delta.y);

            return isInScrollRect && isHorizontalDrag 
                ? new ScrollRectDragStrategy(_sceneContext.BottomScroll) 
                : new CubeDragStrategy(_dragService, _sceneContext.DragCanvas, gameObject);
        }
    }
}