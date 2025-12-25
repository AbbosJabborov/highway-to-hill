using Game.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Extensions
{
    public class CarTweaker : MonoBehaviour
    {
        [Header("Target Controller")]
        [SerializeField] private CarController car;

        [Header("Rigidbody")]
        [SerializeField] private Rigidbody rb;

        [Header("UI Sliders")]
        [SerializeField] private Slider massSlider;
        [SerializeField] private Slider motorForceSlider;
        [SerializeField] private Slider brakeForceSlider;
        [SerializeField] private Slider maxSteerAngleSlider;
        [SerializeField] private Slider topSpeedSlider;
        [SerializeField] private Slider steeringResponseSlider;
        [SerializeField] private Slider suspensionRestSlider;
        [SerializeField] private Slider suspensionSpringSlider;
        [SerializeField] private Slider suspensionDamperSlider;
        [SerializeField] private Slider wheelRadiusSlider;
        [SerializeField] private Slider tireMassSlider;
        [SerializeField] private Slider tireGripSlider;
        [SerializeField] private Slider lowSpeedBoostSlider;

        // ---------- CAR CORE ----------
        public void SetMass(float value)
        {
            rb.mass = Mathf.Clamp(value, 500f, 3000f);
        }
        public void SetMotorForce(float value)
        {
            car.motorForce = Mathf.Clamp(value, 0f, 8000f);
        }

        public void SetBrakeForce(float value)
        {
            car.brakeForce = Mathf.Clamp(value, 0f, 10000f);
        }

        public void SetMaxSteerAngle(float value)
        {
            car.maxSteerAngle = Mathf.Clamp(value, 5f, 45f);
        }

        public void SetTopSpeed(float value)
        {
            car.topSpeed = Mathf.Clamp(value, 50f, 300f);
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
            const float defMass = 1500f;
            const float defMotor = 5500f;
            const float defBrake = 7000f;
            const float defSteer = 32f;
            const float defTopSpeed = 100f;
            const float defSuspRest = 0.15f;
            const float defSuspSpring = 20000f;
            const float defSuspDamper = 3000f;
            const float defWheelRadius = 0.5f;
            const float defTireMass = 20f;
            const float defTireGrip = 0.5f;
            const float defLowSpeedBoost = 1.5f;
            const float defSteeringResponse = 8f;

            SetMass(defMass);
            SetMotorForce(defMotor);
            SetBrakeForce(defBrake);
            SetMaxSteerAngle(defSteer);
            SetTopSpeed(defTopSpeed);

            SetSuspensionRestDistance(defSuspRest);
            SetSuspensionSpring(defSuspSpring);
            SetSuspensionDamper(defSuspDamper);

            SetWheelRadius(defWheelRadius);
            SetTireMass(defTireMass);
            SetTireGrip(defTireGrip);
            SetLowSpeedBoost(defLowSpeedBoost);
            SetSteeringResponse(defSteeringResponse);

            // Update UI sliders without invoking their onValueChanged events
            if (massSlider != null) massSlider.SetValueWithoutNotify(defMass);
            if (motorForceSlider != null) motorForceSlider.SetValueWithoutNotify(defMotor);
            if (brakeForceSlider != null) brakeForceSlider.SetValueWithoutNotify(defBrake);
            if (maxSteerAngleSlider != null) maxSteerAngleSlider.SetValueWithoutNotify(defSteer);
            if (topSpeedSlider != null) topSpeedSlider.SetValueWithoutNotify(defTopSpeed);
            if (steeringResponseSlider != null) steeringResponseSlider.SetValueWithoutNotify(defSteeringResponse);

            if (suspensionRestSlider != null) suspensionRestSlider.SetValueWithoutNotify(defSuspRest);
            if (suspensionSpringSlider != null) suspensionSpringSlider.SetValueWithoutNotify(defSuspSpring);
            if (suspensionDamperSlider != null) suspensionDamperSlider.SetValueWithoutNotify(defSuspDamper);

            if (wheelRadiusSlider != null) wheelRadiusSlider.SetValueWithoutNotify(defWheelRadius);
            if (tireMassSlider != null) tireMassSlider.SetValueWithoutNotify(defTireMass);
            if (tireGripSlider != null) tireGripSlider.SetValueWithoutNotify(defTireGrip);
            if (lowSpeedBoostSlider != null) lowSpeedBoostSlider.SetValueWithoutNotify(defLowSpeedBoost);
        }
    }
}
