using DG.Tweening;
using UnityEngine;

namespace _MyGame.Scripts.Features.Cube
{
    public class CubeAnimator : MonoBehaviour
    {
        public void AnimateSettle(Vector3 position)
        {
            transform.DOMove(position, 0.3f).SetEase(Ease.OutCubic);
        }
        
        public void AnimateJumpTo(Vector3 position, float cubeHeight)
        {
            transform.DOJump(position, cubeHeight, 1, 0.4f)
                .SetEase(Ease.OutQuad);
        }

        public void Explode()
        {
            transform.DOScale(Vector3.zero, 0.3f)
                .SetEase(Ease.InBack)
                .OnComplete(() => Destroy(gameObject));
        }
    }
}