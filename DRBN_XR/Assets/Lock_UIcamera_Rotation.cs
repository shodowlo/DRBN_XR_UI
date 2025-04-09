using UnityEngine;

public class UIAnchor : MonoBehaviour
{
    public Transform target; // XR camera
    public float distance = 1.0f;   // Distance between the player and the UI

    void LateUpdate()
    {
        if (target != null)
        {
            // only take Y rotation
            Vector3 flatForward = new Vector3(target.forward.x, 0, target.forward.z).normalized;

            // Position : in front of the player
            Vector3 targetPos = target.position + flatForward * distance;
            transform.position = targetPos;

            // Rotation around Y only
            float yaw = target.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0, yaw, 0);
        }
    }
}
