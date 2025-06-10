using _MyGame.Scripts;
using _MyGame.Scripts.Features.Cube;
using _MyGame.Scripts.Services.Tower;
using UnityEngine;

public interface ICubeDragService
{
    GameObject BeginDrag(GameObject original, TowerService tower, CubeDragHandler handler);
    
    void EndDrag(GameObject dragged, Vector3 screenPos, CubeDragHandler handler);
}