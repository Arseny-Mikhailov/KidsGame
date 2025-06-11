using UnityEngine;
using UnityEngine.EventSystems;

public class HorizontalScrollOrDrag : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform contentTransform;
    public float scrollSpeed = 1f;
    public float dragThreshold = 20f;
    
    private Vector2 _startPos;
    private bool _isScrolling;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startPos = eventData.position;
        _isScrolling = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_isScrolling)
        {
            float deltaX = eventData.delta.x * scrollSpeed;
            contentTransform.anchoredPosition += new Vector2(deltaX, 0);
            return;
        }
        
        Vector2 diff = eventData.position - _startPos;
        if (diff.magnitude < dragThreshold) return;
        
        bool isHorizontal = Mathf.Abs(diff.x) > Mathf.Abs(diff.y);
        if (isHorizontal)
        {
            _isScrolling = true;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_isScrolling)
        {
        }
        _isScrolling = false;
    }
}