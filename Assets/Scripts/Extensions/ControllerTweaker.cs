using System.Collections.Generic;
using Game.Player;
using UnityEngine;

namespace Extensions
{
    public class ControllerTweaker : MonoBehaviour
    {
        [SerializeField] private CarController carController;
        [SerializeField] private List<WheelCollider> wheelColliders;
        
        public void SetCarSettings(CarController carController, float motorForce, float brakeForce, float maxSteerAngle, float steeringSmooth)
        {
            carController.motorForce = motorForce;
            carController.brakeForce = brakeForce;
            carController.maxSteerAngle = maxSteerAngle;
            carController.steeringSmooth = steeringSmooth;
        }
        public void SetDriftSettings(CarController carController, float driftSidewaysStiffness, float normalSidewaysStiffness, float driftForwardStiffness, float yawAssist)
        {
            carController.driftSidewaysStiffness = driftSidewaysStiffness;
            carController.normalSidewaysStiffness = normalSidewaysStiffness;
            carController.driftForwardStiffness = driftForwardStiffness;
            carController.yawAssist = yawAssist;
        }
        public void SetWheelSettings(List<WheelCollider> WheelColliders, float suspensionDistance, float spring, float damper)
        {
            foreach (var wheel in WheelColliders)
            {
                JointSpring jointSpring = wheel.suspensionSpring;
                jointSpring.spring = spring;
                jointSpring.damper = damper;
                wheel.suspensionSpring = jointSpring;
                wheel.suspensionDistance = suspensionDistance;  
            }
        }
    }
}
