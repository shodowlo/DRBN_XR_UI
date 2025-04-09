using UnityEngine;

public class Script_Camera : MonoBehaviour
{
    public Transform cameraTransform; // La caméra de l'utilisateur (souvent le HMD)
    private float fixedY;

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }

        fixedY = transform.position.y;
    }

    void LateUpdate()
    {
        Vector3 targetPosition = cameraTransform.position;
        targetPosition.y = fixedY; // ignore la hauteur de la tête

        // Le Canvas regarde la caméra (horizontalement seulement)
        transform.LookAt(targetPosition);

        // Facultatif : empêcher le canvas de se pencher vers le haut/bas
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        // Optionnel : faire suivre le canvas en XZ uniquement
        transform.position = new Vector3(cameraTransform.position.x, fixedY, cameraTransform.position.z - 2f); // "2f" = distance devant l'utilisateur, ajustable
    }
}
