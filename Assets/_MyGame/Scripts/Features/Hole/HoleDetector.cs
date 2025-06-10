using _MyGame.Scripts.Features.Hole;
using UnityEngine;

public class HoleDetector
{
    private readonly RectTransform _holeRect;
    private readonly Camera _camera;

    public HoleDetector(HoleView view, Camera mainCamera)
    {
        _holeRect = view.rect;
        _camera = mainCamera;
    }
    public bool IsInHole(Vector3 worldPosition)
    {
        Vector2 screenPos = _camera.WorldToScreenPoint(worldPosition);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _holeRect, screenPos, _camera, out Vector2 localPoint
        );
        
        var rx = _holeRect.rect.width / 2f;
        var ry = _holeRect.rect.height / 2f;
        var x = localPoint.x / rx;
        var y = localPoint.y / ry;

        return x * x + y * y <= 1f;
    }
}