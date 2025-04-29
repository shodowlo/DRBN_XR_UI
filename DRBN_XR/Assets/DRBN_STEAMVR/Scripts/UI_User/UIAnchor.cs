using UnityEngine;

public class UIAnchor : MonoBehaviour
{
    public Transform target; // XR camera
    public float distance = 1.0f;   // Distance between the player and the UI
    public float moveSpeed = 10f;   // Speed of the movement

    private float lastYaw = 0f;     // Store the last yaw angle
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private bool isMoving = false;

    void Start()
    {
        if (target != null)
        {
            UpdateTargetPositionAndRotation();
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Get the current yaw angle of the target
            float currentYaw = target.eulerAngles.y;

            // Normalize the yaw angle to be between 0 and 360 degrees
            currentYaw = currentYaw % 360;
            if (currentYaw < 0) currentYaw += 360;

            // Check if the current yaw has crossed a multiple of 30 degrees
            if (Mathf.Abs(currentYaw - lastYaw) >= 30f)
            {
                // Round the yaw to the nearest multiple of 30 degrees
                float roundedYaw = Mathf.Round(currentYaw / 30f) * 30f;

                // Update the last yaw
                lastYaw = roundedYaw + 1;

                // Update the target position and rotation
                UpdateTargetPositionAndRotation(roundedYaw + 1);

                // Start moving
                isMoving = true;
            }
        }
        else
        {
            Debug.LogWarning("Target is not assigned!");
        }
    }

    void Update()
    {
        if (isMoving)
        {
            // Smoothly move the position
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);

            // Smoothly rotate the object
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * moveSpeed);

            // Check if the movement is complete
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f && Quaternion.Angle(transform.rotation, targetRotation) < 1f)
            {
                isMoving = false;
            }
        }
    }

    private void UpdateTargetPositionAndRotation(float yaw = 0f)
    {
        // only take Y rotation
        Vector3 flatForward = new Vector3(target.forward.x, 0, target.forward.z).normalized;

        // Position : in front of the player
        targetPosition = target.position + flatForward * distance;

        // Rotation around Y only
        targetRotation = Quaternion.Euler(0, yaw, 0);
    }
}