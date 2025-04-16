using UnityEngine;
using UnityEngine.UI;

public class MovePanelOnButtonPress : MonoBehaviour
{
    public RectTransform panel; // Le panel à déplacer
    public float moveDistance = -17.7f; // La distance de déplacement sur l'axe X
    public float moveSpeed = 10f; // La vitesse de déplacement

    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private bool isMoving = false;

    void Start()
    {
    //     Button moveButton = GetComponent<Button>();
    //     if (moveButton != null)
    //     {
    //         moveButton.onClick.AddListener(OnButtonPress);
    //     }

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

            // Vérifie si le panel a atteint la position cible
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
