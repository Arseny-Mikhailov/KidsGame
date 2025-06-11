using UnityEngine;

namespace Core.Interfaces
{
    public interface ICubeDragService
    {
        DragContext BeginDrag(GameObject original);
        
        void EndDrag( DragContext ctx, Vector2 screenPosition);
    }
}