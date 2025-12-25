using TMPro;
using UnityEngine;
using DG.Tweening;

namespace Extensions
{
    public class Speedometer : MonoBehaviour
    {
        [SerializeField] private TMP_Text speedText;
        [SerializeField] private Rigidbody carRigidbody;
        [SerializeField] private float speedMultiplier = 3.6f;

        [Header("Arrow Settings")]
        [SerializeField] private RectTransform speedometerArrow;
        [SerializeField] private float baseZRotation = 90f; // default base rotation to add degrees into
        [SerializeField] private float maxMappedDegrees = 120f; // cap of the mapped degrees
        [SerializeField] private float arrowTweenDuration = 0.18f;
        [SerializeField] private bool invertArrow = false;

        [Header("Speed Text Tween")]
        [SerializeField] private float speedTweenDuration = 0.25f;
        [SerializeField] private float punchOnIncreaseThreshold = 3f;
        [SerializeField] private Vector3 punchScaleAmount = default;

        private float displayedSpeed;
        private float lastTargetSpeed;
        private Tween speedTween;
        private Tween arrowTween;

        private void Reset()
        {
            punchScaleAmount = Vector3.one * 0.08f;
        }

        private void Update()
        {
            float targetSpeed = carRigidbody.linearVelocity.magnitude * speedMultiplier; // use velocity for 3D Rigidbody

            UpdateSpeedTextSmooth(targetSpeed);
            UpdateArrow(targetSpeed);
        }

        // Smooth numeric update using DOTween.To, with optional punch on noticeable acceleration
        private void UpdateSpeedTextSmooth(float targetSpeed)
        {
            // if target unchanged enough, do nothing
            if (Mathf.Approximately(targetSpeed, lastTargetSpeed))
                return;

            lastTargetSpeed = targetSpeed;

            if (speedTween != null && speedTween.IsActive())
                speedTween.Kill();

            speedTween = DOTween.To(() => displayedSpeed, v => {
                    displayedSpeed = v;
                    speedText.text = Mathf.RoundToInt(displayedSpeed).ToString();
                },
                targetSpeed, speedTweenDuration)
                .SetEase(Ease.OutQuart);

            // small punch when accelerating quickly
            if (targetSpeed - displayedSpeed > punchOnIncreaseThreshold)
            {
                speedText.transform.DOKill();
                speedText.transform.DOPunchScale(punchScaleAmount, 0.18f, 1, 0.5f);
            }
        }

        // Convert speed to degrees
        private void UpdateArrow(float speed)
        {
            float degrees = MapSpeedToDegrees(speed);
            float mapped = Mathf.Clamp(degrees, 0f, maxMappedDegrees);
            float targetZ = baseZRotation + (invertArrow ? -mapped : mapped);

            if (arrowTween != null && arrowTween.IsActive())
                arrowTween.Kill();

            arrowTween = speedometerArrow.DOLocalRotate(new Vector3(0f, 0f, targetZ), arrowTweenDuration)
                .SetEase(Ease.OutQuad);
        }
        
        private float MapSpeedToDegrees(float speed)
        {
            if (speed <= 0f) return 0f;
            if (speed <= 45f) return Mathf.Lerp(0f, 45f, speed / 45f);
            if (speed <= 100f) return Mathf.Lerp(45f, 90f, (speed - 45f) / (100f - 45f));
            if (speed <= 150f) return Mathf.Lerp(90f, 120f, (speed - 100f) / (150f - 100f));
            return 120f;
        }

        private void OnDisable()
        {
            speedTween?.Kill();
            arrowTween?.Kill();
        }
    }
}
