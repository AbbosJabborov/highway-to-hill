using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class CarController : MonoBehaviour
    {
        [Header("Wheel Colliders")]
        [Tooltip("Wheel meshes to visually represent the wheels.")]
        public WheelCollider frontLeft;
        public WheelCollider frontRight;
        public WheelCollider rearLeft;
        public WheelCollider rearRight;

        [Header("Wheel Meshes")]
        [Tooltip("Wheel meshes to visually represent the wheels.")]
        public Transform frontLeftMesh;
        public Transform frontRightMesh;
        public Transform rearLeftMesh;
        public Transform rearRightMesh;

        [Header("Driving")]
        public float motorForce = 1500f;
        public float brakeForce = 3000f;
        public float maxSteerAngle = 30f;
        public float steeringSmooth = 0.6f;

        [Header("Arcade Drift")]
        public float driftSidewaysStiffness = 0.4f;
        public float normalSidewaysStiffness = 1.0f;
        public float driftForwardStiffness = 0.8f;
        public float yawAssist = 2.5f;

        // [Header("Center of Mass")]
        // public Vector3 centerOfMassOffset = new Vector3(0f, -1.1f, 0.2f);

        private Rigidbody rb;
        private Vector2 moveInput;
        private bool isBraking;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            //rb.centerOfMass = centerOfMassOffset;
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
        
        void HandleMotor()
        {
            float torque = moveInput.y * motorForce;

            rearLeft.motorTorque  = torque;
            rearRight.motorTorque = torque;

            float brake = isBraking ? brakeForce : 0f;

            frontLeft.brakeTorque  = brake;
            frontRight.brakeTorque = brake;
            rearLeft.brakeTorque   = brake;
            rearRight.brakeTorque  = brake;
        }
        
        void HandleSteering()
        {
            float speed = rb.linearVelocity.magnitude;
            float steerLimit = Mathf.Lerp(maxSteerAngle, maxSteerAngle * 0.4f, speed / 25f);

            float targetSteer = moveInput.x * steerLimit;

            frontLeft.steerAngle =
                Mathf.Lerp(frontLeft.steerAngle, targetSteer, steeringSmooth);

            frontRight.steerAngle =
                Mathf.Lerp(frontRight.steerAngle, targetSteer, steeringSmooth);
        }
        
        void HandleDrift()
        {
            if (!isBraking || rb.linearVelocity.magnitude < 2f)
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
