using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class CarController : MonoBehaviour
    {
        [System.Serializable]
        public class Wheel
        {
            public Transform transform;
            public bool steer;
            public bool drive;
        }

        [Header("Wheels")]
        public Wheel[] wheels;

        [Header("Suspension")]
        public float suspensionRestDist = 0.5f;
        public float suspensionStrength = 20000f;
        public float suspensionDamper = 3000f;
        public float wheelRadius = 0.35f;

        [Header("Grip")]
        [Range(0f, 1f)] public float tireGrip = 0.9f;
        public float tireMass = 20f;

        [Header("Steering")]
        public float maxSteerAngle = 35f;
        public float steeringResponse = 8f;

        [Header("Engine")]
        public float motorForce = 8000f;
        public float brakeForce = 12000f;
        public float topSpeed = 45f;

        [Header("Arcade")]
        public float lowSpeedBoost = 1.5f;

        private Rigidbody rb;
        private Vector2 moveInput;
        private bool brake;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.mass = 1200f;
            rb.centerOfMass = new Vector3(0f, -0.8f, 0.2f);
        }

        void FixedUpdate()
        {
            foreach (var wheel in wheels)
            {
                ApplySuspension(wheel);
                ApplyGrip(wheel);
                ApplyDrive(wheel);
            }
        }

        // ---------- INPUT ----------
        public void OnMove(InputAction.CallbackContext ctx)
        {
            moveInput = ctx.ReadValue<Vector2>();
        }

        public void OnBrake(InputAction.CallbackContext ctx)
        {
            brake = ctx.performed;
        }

        // ---------- SUSPENSION ----------
        void ApplySuspension(Wheel wheel)
        {
            RaycastHit hit;
            Vector3 origin = wheel.transform.position;
            Vector3 dir = -wheel.transform.up;

            float rayLength = suspensionRestDist + wheelRadius;

            if (!Physics.Raycast(origin, dir, out hit, rayLength))
                return;

            float offset = suspensionRestDist - (hit.distance - wheelRadius);

            Vector3 vel = rb.GetPointVelocity(origin);
            float velAlongSpring = Vector3.Dot(wheel.transform.up, vel);

            float force =
                (offset * suspensionStrength) -
                (velAlongSpring * suspensionDamper);

            rb.AddForceAtPosition(wheel.transform.up * force, origin);
        }

        // ---------- LATERAL GRIP ----------
        void ApplyGrip(Wheel wheel)
        {
            RaycastHit hit;
            if (!Physics.Raycast(wheel.transform.position, -wheel.transform.up,
                out hit, suspensionRestDist + wheelRadius))
                return;

            Vector3 vel = rb.GetPointVelocity(wheel.transform.position);
            Vector3 lateralDir = wheel.transform.right;

            float lateralVel = Vector3.Dot(lateralDir, vel);
            float desiredVelChange = -lateralVel * tireGrip;

            float accel = desiredVelChange / Time.fixedDeltaTime;

            rb.AddForceAtPosition(
                lateralDir * (tireMass * accel),
                wheel.transform.position
            );
        }

        // ---------- DRIVE / BRAKE ----------
        void ApplyDrive(Wheel wheel)
        {
            if (!wheel.drive)
                return;

            RaycastHit hit;
            if (!Physics.Raycast(wheel.transform.position, -wheel.transform.up,
                out hit, suspensionRestDist + wheelRadius))
                return;

            Vector3 forward = wheel.transform.forward;

            float speed = Vector3.Dot(transform.forward, rb.linearVelocity);
            float speed01 = Mathf.Clamp01(Mathf.Abs(speed) / topSpeed);

            float boost = Mathf.Lerp(lowSpeedBoost, 1f, speed01);

            float torque = moveInput.y * motorForce * boost;

            if (brake)
                torque = -Mathf.Sign(speed) * brakeForce;

            rb.AddForceAtPosition(forward * torque, wheel.transform.position);
        }

        // ---------- VISUAL STEERING ----------
        void Update()
        {
            foreach (var wheel in wheels)
            {
                if (!wheel.steer)
                    continue;

                float target =
                    moveInput.x * maxSteerAngle;

                Quaternion steerRot =
                    Quaternion.Euler(0f, target, 0f);

                wheel.transform.localRotation =
                    Quaternion.Slerp(
                        wheel.transform.localRotation,
                        steerRot,
                        steeringResponse * Time.deltaTime
                    );
            }
        }
    }
}
