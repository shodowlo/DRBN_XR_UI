using UnityEngine;

public class debugcontroller : MonoBehaviour
{
    public GameObject objectToEnable; // Objet à activer à chaque Update
    public GameObject objectToPosition; // Objet à positionner et orienter au Start

    void Start()
    {
        if (objectToPosition != null)
        {
            // Position locale (0, 0, 0.1)
            objectToPosition.transform.localPosition = new Vector3(-0.2f, 0f, 0.5f);
            // Rotation locale (90, 180, 0)
            objectToPosition.transform.localEulerAngles = new Vector3(55f, 180f, 0f);
        }
    }

    void Update()
    {
        if (objectToEnable != null && !objectToEnable.activeSelf)
        {
            objectToEnable.SetActive(true);
        }
    }
}
