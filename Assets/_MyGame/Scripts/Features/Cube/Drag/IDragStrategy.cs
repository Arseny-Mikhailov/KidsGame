using UnityEngine.EventSystems;

namespace Features.Cube.Drag
{
    public interface IDragStrategy
    {
        void OnBeginDrag(PointerEventData eventData);
        void OnDrag(PointerEventData eventData);
        void OnEndDrag(PointerEventData eventData);
    }
}