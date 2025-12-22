using Game.Player;
using UnityEngine;

namespace Extensions
{
    public class CarTweaker : MonoBehaviour
    {
        [Header("Target Controller")]
        [SerializeField] private CarController car;

        [Header("Rigidbody")]
        [SerializeField] private Rigidbody rb;
        
        // ---------- CAR CORE ----------
        public void SetMass(float value)
        {
            rb.mass = Mathf.Clamp(value, 500f, 3000f);
        }
        public void SetMotorForce(float value)
        {
            car.motorForce = Mathf.Clamp(value, 0f, 5000f);
        }

        public void SetBrakeForce(float value)
        {
            car.brakeForce = Mathf.Clamp(value, 0f, 8000f);
        }

        public void SetMaxSteerAngle(float value)
        {
            car.maxSteerAngle = Mathf.Clamp(value, 5f, 45f);
        }

        public void SetTopSpeed(float value)
        {
            car.topSpeed = Mathf.Clamp(value, 50f, 200f);
        }
        public void SetSteeringResponse(float value)
        {
            car.steeringResponse = Mathf.Clamp(value, 1f, 15f);
        }
        

        // ---------- SUSPENSION ----------
        public void SetSuspensionRestDistance(float value)
        {
            car.suspensionRestDist = Mathf.Clamp(value, 0.05f, 1f);
        }

        public void SetSuspensionSpring(float value)
        {
            car.suspensionStrength = Mathf.Clamp(value, 10000f, 60000f);
        }

        public void SetSuspensionDamper(float value)
        {
            car.suspensionDamper = Mathf.Clamp(value, 1000f, 8000f);
        }
        
        
        // ---------- Tire & Wheel----------
        public void SetWheelRadius(float value)
        {
            car.wheelRadius = Mathf.Clamp(value, 0.1f, 2f);
        }
        
        public void SetTireMass(float value)
        {
            car.tireMass = Mathf.Clamp(value, 5f, 50f);
        }
        
        public void SetTireGrip(float value)
        {
            car.tireGrip = Mathf.Clamp(value, 0.1f, 1f);
        }
        
        public void SetLowSpeedBoost(float value)
        {
            car.lowSpeedBoost = Mathf.Clamp(value, 1f, 4f);
        }

        
        // ---------- DEFAULT ----------
        public void ResetToDefaults()
        {
            SetMass(1500f);
            SetMotorForce(3500f);
            SetBrakeForce(4000f);
            SetMaxSteerAngle(32f);
            SetTopSpeed(90f);
            SetSteeringResponse(8f);
            
            SetSuspensionRestDistance(0.15f);
            SetSuspensionSpring(20000f);
            SetSuspensionDamper(300f);
            
            SetWheelRadius(0.35f);
            SetTireMass(20f);
            SetTireGrip(0.5f);
            SetLowSpeedBoost(1.5f);
            
        }
    }
}
