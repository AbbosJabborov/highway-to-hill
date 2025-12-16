using System.Collections.Generic;
using Game.Player;
using UnityEngine;

namespace Extensions
{
    public class CarTweaker : MonoBehaviour
    {
        [Header("Target Controller")]
        [SerializeField] private CarController car;

        [Header("Wheels")]
        [SerializeField] private List<WheelCollider> wheels;

        // ---------- CAR CORE ----------
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
            car.maxSteerAngle = Mathf.Clamp(value, 5f, 60f);
        }

        public void SetSteeringSmooth(float value)
        {
            car.steeringSmooth = Mathf.Clamp01(value);
        }

        // ---------- DRIFT ----------
        public void SetDriftSidewaysStiffness(float value)
        {
            car.driftSidewaysStiffness = Mathf.Clamp(value, 0.1f, 1f);
        }

        public void SetNormalSidewaysStiffness(float value)
        {
            car.normalSidewaysStiffness = Mathf.Clamp(value, 0.5f, 2f);
        }

        public void SetDriftForwardStiffness(float value)
        {
            car.driftForwardStiffness = Mathf.Clamp(value, 0.1f, 1.5f);
        }

        public void SetYawAssist(float value)
        {
            car.yawAssist = Mathf.Clamp(value, 0f, 5f);
        }

        // ---------- SUSPENSION ----------
        public void SetSuspensionDistance(float value)
        {
            foreach (var wheel in wheels)
            {
                wheel.suspensionDistance = Mathf.Clamp(value, 0.05f, 0.5f);
            }
        }

        public void SetSuspensionSpring(float value)
        {
            foreach (var wheel in wheels)
            {
                JointSpring spring = wheel.suspensionSpring;
                spring.spring = Mathf.Clamp(value, 5000f, 60000f);
                wheel.suspensionSpring = spring;
            }
        }

        public void SetSuspensionDamper(float value)
        {
            foreach (var wheel in wheels)
            {
                JointSpring spring = wheel.suspensionSpring;
                spring.damper = Mathf.Clamp(value, 500f, 10000f);
                wheel.suspensionSpring = spring;
            }
        }

        // ---------- DEFAULT ----------
        public void ResetToDefaults()
        {
            SetMotorForce(1500f);
            SetBrakeForce(3000f);
            SetMaxSteerAngle(30f);
            SetSteeringSmooth(0.6f);

            SetDriftSidewaysStiffness(0.4f);
            SetNormalSidewaysStiffness(1.0f);
            SetDriftForwardStiffness(0.8f);
            SetYawAssist(2.5f);

            SetSuspensionDistance(0.15f);
            SetSuspensionSpring(35000f);
            SetSuspensionDamper(4500f);
        }
    }
}
