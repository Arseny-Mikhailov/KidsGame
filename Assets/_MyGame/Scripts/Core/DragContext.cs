using UnityEngine;

namespace _MyGame.Scripts.Core
{
    public class DragContext
    {
        public Vector3 OriginalPosition { get; }
        public GameObject Dragged { get; }
        public bool IsExisting { get; }

        public DragContext(GameObject original, GameObject dragged, bool isExisting)
        {
            OriginalPosition = original.transform.position;
            IsExisting       = isExisting;
            Dragged          = dragged;
        }
    }
}