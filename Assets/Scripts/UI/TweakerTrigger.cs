using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

namespace UI
{
    public class TweakerTrigger : MonoBehaviour
    {
        [Header("Panel")]
        [SerializeField] private RectTransform panel;
        [SerializeField] private float panelMoveDuration = 0.5f;
        [SerializeField] private float panelX = 275f;

        [Header("Input (New Input System)")]
        [SerializeField] private InputActionReference toggleAction;

        private void OnEnable()
        {
            if (toggleAction?.action == null) return;
            toggleAction.action.Enable();
            toggleAction.action.performed += OnTogglePerformed;
        }

        private void OnDisable()
        {
            if (toggleAction?.action == null) return;
            toggleAction.action.performed -= OnTogglePerformed;
            toggleAction.action.Disable();
        }

        private void OnTogglePerformed(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
                TriggerPanel();
        }

        public void TriggerPanel()
        {
            var targetX = panel.anchoredPosition.x < 0 ? panelX : -panelX;
            var targetPosition = new Vector2(targetX, panel.anchoredPosition.y);
            panel.DOAnchorPos(targetPosition, panelMoveDuration).SetEase(Ease.InOutSine);
        }
    }
}
