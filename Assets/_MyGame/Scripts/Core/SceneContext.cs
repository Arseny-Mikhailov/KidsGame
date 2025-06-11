using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class SceneContext : MonoBehaviour
    {
        [field: SerializeField] public Canvas DragCanvas { get; private set; }
        [field: SerializeField] public ScrollRect BottomScroll { get; private set; }
    }
}