using UnityEngine;
using UnityEngine.UI;

public class MovePanelOnButtonPress : MonoBehaviour
{
    public RectTransform panel; // Panel to move
    public float moveDistance = -17.7f; // distance on axis X
    public float moveSpeed = 10f; // speed movement

    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private bool isMoving = false;

    void Start()
    {
        if (panel != null)
        {
            originalPosition = panel.localPosition;
            targetPosition = originalPosition;
        }
    }

    void Update()
    {
        if (isMoving)
        {
            panel.localPosition = Vector3.Lerp(panel.localPosition, targetPosition, Time.deltaTime * moveSpeed);

            if (Vector3.Distance(panel.localPosition, targetPosition) < 0.1f)
            {
                isMoving = false;
            }
        }
    }

    public void OnButtonPress()
    {
        isMoving = true;
        targetPosition = originalPosition + new Vector3(moveDistance, 0, 0);
    }

    public void ResetPanelPosition()
    {
        isMoving = true;
        targetPosition = originalPosition;
    }
}
