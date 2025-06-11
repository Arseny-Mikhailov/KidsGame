using UnityEngine;
using UnityEngine.UI;

namespace Features.Cube
{
    public class CubeView : MonoBehaviour
    {
        public void SetSprite(Sprite sprite)
        {
            GetComponent<Image>().sprite = sprite;
        }
    }
}