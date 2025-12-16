using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Extensions
{
    public class CarResetPosition: MonoBehaviour
    {
        private Rigidbody rb;
        private Quaternion initialRotation;
        private Vector3 initialPosition;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            initialPosition = transform.position;
            initialRotation = transform.rotation;
        }

        public void ResetCarPosition(InputAction.CallbackContext context)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.position = initialPosition;
            transform.rotation = initialRotation;
        }
    }
}