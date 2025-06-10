using UnityEngine;
using UnityEngine.UI;

namespace _MyGame.Scripts.Features.Cube
{
    public class CubeView : MonoBehaviour
    {
        public void SetSprite(Sprite sprite)
        {
            GetComponent<Image>().sprite = sprite;
        }
    }
}