using _MyGame.Scripts.Core;
using _MyGame.Scripts.Features.Cube;
using _MyGame.Scripts.Services.Cube;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VContainer;

public class ScrollWithDragDetection : MonoBehaviour, 
    IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Header("UI Refs")]
    public RectTransform contentTransform;
    public Canvas parentCanvas;
    public Image blockerImage;

    [Header("Settings")]
    public float startThreshold = 10f;
    public float verticalAngleThreshold = 45f;
    public float scrollSpeed = 1f;
    
    [Inject] private ICubeDragService _dragService;

    Vector2 _startPos;
    GameObject _pressedCube;
    DragContext _dragCtx;
    bool _isDraggingCube;

    public void OnPointerDown(PointerEventData eventData)
    {
        _startPos = eventData.position;
        _pressedCube = null;
        _dragCtx = null;
        _isDraggingCube = false;
        
        foreach (var go in eventData.hovered)
        {
            if (go.GetComponent<CubeView>() == null) continue;
            
            _pressedCube = go;
            break;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        var diff = eventData.position - _startPos;
        if (_dragCtx == null)
        {
            if (diff.magnitude < startThreshold) return;

            float angle = Vector2.Angle(diff, Vector2.right);
            bool isVertical = angle > verticalAngleThreshold 
                              && angle < 180 - verticalAngleThreshold;

            if (isVertical && _pressedCube != null)
            {
                _dragCtx = _dragService.BeginDrag(_pressedCube);
                if (_dragCtx.Dragged.TryGetComponent<CanvasGroup>(out var cg))
                    cg.blocksRaycasts = false;
                _isDraggingCube = true;
            }
            else
            {
                contentTransform.anchoredPosition += 
                    new Vector2(diff.x * scrollSpeed, 0);
                _startPos = eventData.position;
                return;
            }
        }

        if (_isDraggingCube)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentCanvas.transform as RectTransform,
                eventData.position,
                parentCanvas.worldCamera,
                out var localPos);

            ((RectTransform)_dragCtx.Dragged.transform).anchoredPosition = localPos;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_isDraggingCube && _dragCtx != null)
            _dragService.EndDrag(_dragCtx, eventData.position);
    }
}