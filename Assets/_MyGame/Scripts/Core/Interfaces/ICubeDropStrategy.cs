using _MyGame.Scripts.Core;
using UnityEngine;

namespace _MyGame.Scripts.Services.Cube.Strategies
{
    public interface ICubeDropStrategy
    {
        DropResult Handle(DragContext ctx, Vector2 screenPos);
    }
}