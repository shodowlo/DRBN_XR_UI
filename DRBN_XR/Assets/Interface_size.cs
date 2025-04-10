using UnityEngine;
using UnityEngine.UI;

public class Interface_Size : MonoBehaviour
{
    public Transform targetObject;
    public Slider slider;

    [Header("Scale minimal and maximal")]
    public float scaleMin = 0.01f;
    public float scaleMax = 0.08f;

    void Start()
    {
        // slider value
        slider.minValue = 0f;
        slider.maxValue = 1f;

        UpdateScale(slider.value);

        slider.onValueChanged.AddListener(UpdateScale);
    }

    public void UpdateScale(float value)
    {
        float scale = Mathf.Lerp(scaleMin, scaleMax, value);

        targetObject.localScale = new Vector3(scale, scale, scale);
    }
}