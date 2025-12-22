using UnityEngine;
using Game.Player;

namespace Extensions
{
    public class CameraTweaker: MonoBehaviour
    {
        [SerializeField] private CameraFollow cameraFollow;
        [SerializeField] private Vector3 offsetIncrement = new Vector3(0f, 1f, -1f);
        [SerializeField] private float angleIncrement = 5f;
        [SerializeField] private float followSpeedIncrement = 2f;
        
        public void FirstPersonTrigger()
        {
            cameraFollow.offset = new Vector3(0f, 1.5f, 0f);
            cameraFollow.offsetAngle = Quaternion.Euler(0f, 0f, 0f);
        }
        public void IncreaseOffset()
        {
            cameraFollow.offset += offsetIncrement;
        }
    }
}