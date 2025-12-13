using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    [Header("Wheel Colliders")]
    public WheelCollider frontLeftCollider;
    public WheelCollider frontRightCollider;
    public WheelCollider rearLeftCollider;
    public WheelCollider rearRightCollider;

    [Header("Wheel Meshes")]
    public Transform frontLeftMesh;
    public Transform frontRightMesh;
    public Transform rearLeftMesh;
    public Transform rearRightMesh;

    [Header("Car Settings")]
    public float motorForce = 1500f;
    public float brakeForce = 3000f;
    public float maxSteerAngle = 30f;

    [Header("Center of Mass")]
    public Transform centerOfMass;
    public Vector3 centerOfMassOffset = new Vector3(0, -0.9f, 0.2f);

    private float currentSteer = 0f;
    private float currentMotor = 0f;
    private float currentBrake = 0f;
    private Vector2 moveInput;
    private bool isBraking = false;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // Set center of mass lower for better stability
        if (centerOfMass != null)
        {
            rb.centerOfMass = centerOfMass.localPosition;
        }
        else
        {
            rb.centerOfMass = centerOfMassOffset;
        }

        // Configure wheel colliders for better stability
        ConfigureWheelCollider(frontLeftCollider);
        ConfigureWheelCollider(frontRightCollider);
        ConfigureWheelCollider(rearLeftCollider);
        ConfigureWheelCollider(rearRightCollider);
    }

    void FixedUpdate()
    {
        HandleMotor();
        HandleSteering();
        UpdateWheelMeshes();
    }

    // Called by Input System for movement (WASD or Arrow keys)
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // Called by Input System for brake (Space key)
    public void OnBrake(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isBraking = true;
        }
        else if (context.canceled)
        {
            isBraking = false;
        }
    }

    private void HandleMotor()
    {
        // Forward/Backward input (W/S or Up/Down)
        currentMotor = moveInput.y * motorForce;
        
        // Apply motor torque to REAR wheels for better control
        rearLeftCollider.motorTorque = currentMotor;
        rearRightCollider.motorTorque = currentMotor;

        // Apply brake
        if (isBraking)
        {
            currentBrake = brakeForce;
        }
        else
        {
            currentBrake = 0f;
        }

        frontLeftCollider.brakeTorque = currentBrake;
        frontRightCollider.brakeTorque = currentBrake;
        rearLeftCollider.brakeTorque = currentBrake;
        rearRightCollider.brakeTorque = currentBrake;
    }

    private void HandleSteering()
    {
        // Left/Right input (A/D or Left/Right)
        currentSteer = moveInput.x * maxSteerAngle;
        
        frontLeftCollider.steerAngle = currentSteer;
        frontRightCollider.steerAngle = currentSteer;
    }

    private void UpdateWheelMeshes()
    {
        UpdateWheelMesh(frontLeftCollider, frontLeftMesh);
        UpdateWheelMesh(frontRightCollider, frontRightMesh);
        UpdateWheelMesh(rearLeftCollider, rearLeftMesh);
        UpdateWheelMesh(rearRightCollider, rearRightMesh);
    }

    private void UpdateWheelMesh(WheelCollider collider, Transform mesh)
    {
        if (mesh == null) return;

        Vector3 pos;
        Quaternion rot;
        collider.GetWorldPose(out pos, out rot);
        
        mesh.position = pos;
        mesh.rotation = rot;
    }

    private void ConfigureWheelCollider(WheelCollider collider)
    {
        // Increase suspension distance for better stability
        collider.suspensionDistance = 0.2f;
        
        // Configure suspension spring
        JointSpring spring = collider.suspensionSpring;
        spring.spring = 35000f;
        spring.damper = 4500f;
        spring.targetPosition = 0.5f;
        collider.suspensionSpring = spring;

        // Configure wheel friction for better grip
        WheelFrictionCurve forwardFriction = collider.forwardFriction;
        forwardFriction.stiffness = 2f;
        collider.forwardFriction = forwardFriction;

        WheelFrictionCurve sidewaysFriction = collider.sidewaysFriction;
        sidewaysFriction.stiffness = 2f;
        collider.sidewaysFriction = sidewaysFriction;

        // Set wheel mass
        collider.mass = 20f;
    }
}