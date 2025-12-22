using UnityEngine;

namespace Game.Player
{
    public class CameraFollow : MonoBehaviour
    {
        [Header("Target")]
        public Transform target;

        [Header("Offset")]
        public Vector3 offset = new Vector3(0f, 5f, -10f);
        public Quaternion offsetAngle = Quaternion.Euler(10f, 0f, 0f);

        [Header("Follow Speed")]
        public float followSpeed = 10f;

        private void LateUpdate()
        {
            Vector3 desiredPosition = target.position + target.TransformDirection(offset);
            Quaternion desiredRotation = target.rotation * offsetAngle;
            
            transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, followSpeed * Time.deltaTime);
        }
    }
}
