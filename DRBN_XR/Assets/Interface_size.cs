using UnityEngine;
using UnityEngine.UI;

public class Interface_Size : MonoBehaviour
{
    public Transform targetObject;
    public Slider slider;

    [Header("Échelle Z min et max")]
    public float zMin = -5f;
    public float zMax = 0.97f;

    void Start()
    {
        slider.minValue = 0f;
        slider.maxValue = 1f;

        slider.value = 1f;

        // Met à jour l’échelle Z à l’initialisation
        UpdateZPosition(slider.value);

        

        // Ajoute le listener
        slider.onValueChanged.AddListener(UpdateZPosition);
    }

    public void UpdateZPosition(float value)
    {
        float z = Mathf.Lerp(-zMin, zMax, value);
        Vector3 pos = targetObject.position;
        targetObject.position = new Vector3(pos.x, pos.y, z);
    }
}
