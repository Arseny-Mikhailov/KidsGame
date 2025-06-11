using _MyGame.Scripts.Core;
using UnityEngine;

namespace _MyGame.Scripts.Services.Cube
{
    public interface ICubeDragService
    {
        DragContext BeginDrag(GameObject original);
        
        void EndDrag( DragContext ctx, Vector2 screenPosition);
    }
}