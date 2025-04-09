using UnityEngine;
using UnityEngine.UI;

public class Interface_Size : MonoBehaviour
{
    public Transform targetObject;
    public Slider slider;

    [Header("Échelle min et max")]
    public float scaleMin = 0.01f;
    public float scaleMax = 0.08f;

    void Start()
    {
        slider.minValue = 0f;
        slider.maxValue = 1f;

        slider.value = 0.5f;

        // Met à jour l’échelle à l’initialisation
        UpdateScale(slider.value);

        // Ajoute le listener
        slider.onValueChanged.AddListener(UpdateScale);
    }

    public void UpdateScale(float value)
    {
        float scale = Mathf.Lerp(scaleMin, scaleMax, value);
        targetObject.localScale = new Vector3(scale, scale, scale);
    }
}
