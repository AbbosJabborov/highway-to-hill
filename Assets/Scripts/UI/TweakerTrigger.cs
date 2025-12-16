using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

namespace UI
{
    public class TweakerTrigger : MonoBehaviour
    {
        [Header("Panel")]
        [SerializeField] private RectTransform panel;
        [SerializeField] private Button triggerButton;
        [SerializeField] private float panelMoveDuration = 0.5f;
        public void TriggerPanel()
        {
            Vector2 targetPosition;
            if (panel.anchoredPosition.x < 0)
            {
                targetPosition = new Vector2(0, panel.anchoredPosition.y);
            }
            else
            {
                targetPosition = new Vector2(-panel.rect.width, panel.anchoredPosition.y);
            }
            panel.DOAnchorPos(targetPosition, panelMoveDuration).SetEase(Ease.InOutSine);
        }
    }
}