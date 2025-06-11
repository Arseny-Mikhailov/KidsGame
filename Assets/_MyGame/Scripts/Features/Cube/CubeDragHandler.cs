using _MyGame.Scripts.Core;
using _MyGame.Scripts.Services.Cube;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;
using _MyGame.Scripts.Services.Tower;
using UnityEngine.UI;

namespace _MyGame.Scripts.Features.Cube
{
    public class CubeDragHandler : MonoBehaviour, IInitializePotentialDragHandler,
        IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public bool InTower { get; set; }
        [Inject] public TowerService TowerService { get; set; }
        [Inject] private ICubeDragService _dragService;

        private Canvas _canvas;
        private DragContext _ctx;
        private ScrollRect _scroll;
        
        private bool _dragTargetChosen;
        private bool _scrollDrag;
        private Vector2 _startPos;

        private void Awake()
        {
            InTower = false;
            _canvas = GetComponentInParent<Canvas>();
            _scroll = GetComponentInParent<ScrollRect>();
        }
        
        public void OnInitializePotentialDrag(PointerEventData e)
        {
            _startPos = e.position;
            _dragTargetChosen = false;
            _scrollDrag = false;

            if (_scroll != null)
            {
                _scroll.OnInitializePotentialDrag(e);
            }
        }

        public void OnBeginDrag(PointerEventData e)
        {
            if (InTower)
            {
                _ctx = _dragService.BeginDrag(gameObject);
            }
        }

        public void OnDrag(PointerEventData e)
        {
            if (InTower)
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    _canvas.transform as RectTransform,
                    e.position,
                    e.pressEventCamera,
                    out var localVector);
            
                _ctx.Dragged.transform.localPosition = localVector;
                return;
            }
            
            if (!_dragTargetChosen)
            {
                var delta = e.position - _startPos;
                _scrollDrag = Mathf.Abs(delta.x) > Mathf.Abs(delta.y);
                _dragTargetChosen = true;

                if (_scrollDrag)
                {
                    _scroll.OnBeginDrag(e);
                    
                    return;
                }

                _ctx = _dragService.BeginDrag(gameObject);
            }

            if (_scrollDrag)
            {
                _scroll.OnDrag(e);
            }
            else
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    (RectTransform)_canvas.transform,
                    e.position,
                    e.pressEventCamera,
                    out var local);
                _ctx.Dragged.transform.localPosition = local;
            }
        }

        public void OnEndDrag(PointerEventData e)
        {
            if (InTower)
            {
                _dragService.EndDrag(_ctx, e.position);
                _ctx = null;
                return;
            }
            //
            if (_scrollDrag)
            {
                _scroll.OnEndDrag(e);
            }
            else
            {
                _dragService.EndDrag(_ctx, e.position);
                _ctx = null;
            }
            _dragTargetChosen = false;
        }
    }
}