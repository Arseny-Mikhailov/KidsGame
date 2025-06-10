using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

namespace _MyGame.Scripts
{
    [RequireComponent(typeof(CanvasGroup))]
    public class MessageView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI _messageText;
        [Tooltip("Сколько секунд показываем текст")]
        [SerializeField] private float       _displayDuration = 1.5f;
        [Tooltip("Длительность fade in/out")]
        [SerializeField] private float       _fadeDuration    = 0.3f;

        private Tween _tween;

        public void ShowMessage(string message)
        {
            _tween?.Kill();

            _messageText.text    = message;
            _canvasGroup.alpha   = 0;
            _canvasGroup.blocksRaycasts = false;
            
            _tween = DOTween.Sequence()
                .Append(_canvasGroup.DOFade(1f, _fadeDuration))
                .AppendInterval(_displayDuration)
                .Append(_canvasGroup.DOFade(0f, _fadeDuration))
                .OnComplete(() => _canvasGroup.blocksRaycasts = false);
        }
    }
}