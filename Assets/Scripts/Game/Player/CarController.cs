using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class CarController : MonoBehaviour
    {
        [Header("Wheel Colliders")]
        public WheelCollider frontLeft;
        public WheelCollider frontRight;
        public WheelCollider rearLeft;
        public WheelCollider rearRight;

        [Header("Wheel Meshes")]
        public Transform frontLeftMesh;
        public Transform frontRightMesh;
        public Transform rearLeftMesh;
        public Transform rearRightMesh;

        [Header("Driving")]
        public float motorForce = 3500f;           // ↑ arcade value
        public float brakeForce = 4000f;
        public float maxSteerAngle = 32f;
        public float steeringSmooth = 0.6f;

        [Header("Acceleration Boost")]
        public float lowSpeedBoost = 1.6f;         // NEW
        public float boostFadeSpeed = 12f;          // m/s

        [Header("Arcade Drift")]
        public float driftSidewaysStiffness = 0.4f;
        public float normalSidewaysStiffness = 1.0f;
        public float driftForwardStiffness = 0.8f;
        public float yawAssist = 3.0f;

        private Rigidbody rb;
        private Vector2 moveInput;
        private bool isBraking;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            HandleMotor();
            HandleSteering();
            HandleDrift();
            UpdateWheelMeshes();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            moveInput = context.ReadValue<Vector2>();
        }

        public void OnBrake(InputAction.CallbackContext context)
        {
            isBraking = context.performed;
        }

        // ---------- MOTOR ----------
        void HandleMotor()
        {
            float speed = rb.linearVelocity.magnitude;

            // Low-speed punch (fades out with speed)
            float boost =
                Mathf.Lerp(lowSpeedBoost, 1f, speed / boostFadeSpeed);

            float torque = moveInput.y * motorForce * boost;

            rearLeft.motorTorque  = torque;
            rearRight.motorTorque = torque;

            float brake = isBraking ? brakeForce : 0f;

            frontLeft.brakeTorque  = brake;
            frontRight.brakeTorque = brake;
            rearLeft.brakeTorque   = brake;
            rearRight.brakeTorque  = brake;
        }

        // ---------- STEERING ----------
        void HandleSteering()
        {
            float speed = rb.linearVelocity.magnitude;

            // Steering falloff happens later now
            float steerFactor = Mathf.InverseLerp(15f, 35f, speed);
            float steerLimit =
                Mathf.Lerp(maxSteerAngle, maxSteerAngle * 0.5f, steerFactor);

            float targetSteer = moveInput.x * steerLimit;

            frontLeft.steerAngle =
                Mathf.Lerp(frontLeft.steerAngle, targetSteer, steeringSmooth);

            frontRight.steerAngle =
                Mathf.Lerp(frontRight.steerAngle, targetSteer, steeringSmooth);
        }

        // ---------- DRIFT ----------
        void HandleDrift()
        {
            if (!isBraking || rb.linearVelocity.magnitude < 3f)
            {
                RestoreRearGrip();
                return;
            }

            SetRearFriction(driftSidewaysStiffness, driftForwardStiffness);

            float yaw = moveInput.x * yawAssist;
            rb.AddTorque(Vector3.up * yaw, ForceMode.Acceleration);
        }

        void RestoreRearGrip()
        {
            SetRearFriction(normalSidewaysStiffness, 1.2f);
        }

        void SetRearFriction(float sideways, float forward)
        {
            WheelFrictionCurve sf;
            WheelFrictionCurve ff;

            sf = rearLeft.sidewaysFriction;
            sf.stiffness = sideways;
            rearLeft.sidewaysFriction = sf;

            sf = rearRight.sidewaysFriction;
            sf.stiffness = sideways;
            rearRight.sidewaysFriction = sf;

            ff = rearLeft.forwardFriction;
            ff.stiffness = forward;
            rearLeft.forwardFriction = ff;

            ff = rearRight.forwardFriction;
            ff.stiffness = forward;
            rearRight.forwardFriction = ff;
        }

        // ---------- VISUALS ----------
        void UpdateWheelMeshes()
        {
            UpdateWheel(frontLeft, frontLeftMesh);
            UpdateWheel(frontRight, frontRightMesh);
            UpdateWheel(rearLeft, rearLeftMesh);
            UpdateWheel(rearRight, rearRightMesh);
        }

        void UpdateWheel(WheelCollider col, Transform mesh)
        {
            if (!mesh) return;

            col.GetWorldPose(out Vector3 pos, out Quaternion rot);
            mesh.position = pos;
            mesh.rotation = rot;
        }
    }
}