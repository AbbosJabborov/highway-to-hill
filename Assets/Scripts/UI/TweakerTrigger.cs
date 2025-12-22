using UnityEngine;
using DG.Tweening;

namespace UI
{
    public class TweakerTrigger : MonoBehaviour
    {
        [Header("Panel")]
        [SerializeField] private RectTransform panel;
        [SerializeField] private float panelMoveDuration = 0.5f;
        public void TriggerPanel()
        {
            var targetPosition = panel.anchoredPosition.x < 0 ? new Vector2(0, panel.anchoredPosition.y) : new Vector2(-panel.rect.width, panel.anchoredPosition.y);
            panel.DOAnchorPos(targetPosition, panelMoveDuration).SetEase(Ease.InOutSine);
        }
    }
}