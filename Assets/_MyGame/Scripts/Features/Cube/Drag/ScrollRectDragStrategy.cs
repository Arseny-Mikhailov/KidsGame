using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Features.Cube.Drag
{

    public class ScrollRectDragStrategy : IDragStrategy
    {
        private readonly ScrollRect _scrollRect;

        public ScrollRectDragStrategy(ScrollRect scrollRect)
        {
            _scrollRect = scrollRect;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _scrollRect.OnBeginDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _scrollRect.OnDrag(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _scrollRect.OnEndDrag(eventData);
        }
    }
}