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
        
        void Update()
        {
            float speed = carRigidbody.linearVelocity.magnitude * speedMultiplier;
            speedText.text = Mathf.RoundToInt(speed).ToString();
            SpeedAnimUpdate();
        }
        void SpeedAnimUpdate()
        {
            speedText.transform.DOKill();
            speedText.transform.DOPunchScale(Vector3.one * 0.1f, 0.2f, 1, 0.5f);
        }
    }
}
