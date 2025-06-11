using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class MessageView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private TextMeshProUGUI messageText;
        [SerializeField] private float displayDuration = 1.5f;
        [SerializeField] private float fadeDuration    = 0.3f;

        private Tween _tween;

        public void ShowMessage(string message)
        {
            _tween?.Kill();

            messageText.text = message;
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            
            _tween = DOTween.Sequence()
                .Append(canvasGroup.DOFade(1f, fadeDuration))
                .AppendInterval(displayDuration)
                .Append(canvasGroup.DOFade(0f, fadeDuration))
                .OnComplete(() => canvasGroup.blocksRaycasts = false);
        }
    }
}