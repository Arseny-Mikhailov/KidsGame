using UnityEngine;

namespace Core.Interfaces
{
    public interface ICubeDropStrategy
    {
        DropResult Handle(DragContext ctx, Vector2 screenPos);
    }
}