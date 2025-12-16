using UnityEngine;

namespace Game.Navigation
{
    public class WaypointManager : MonoBehaviour
    {
        public GameObject markerPrefab;
        public Camera cam;

        public void CreateMarker(Transform target)
        {
            GameObject marker = Instantiate(markerPrefab, transform);
            var ui = marker.GetComponent<WaypointMarker>();
            ui.target = target;
            ui.cam = cam;
        }
    }
}

