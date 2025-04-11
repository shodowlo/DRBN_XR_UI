using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderLogger : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI valueText;

    public float minCustomValue = 1f;
    public float maxCustomValue = 100f;

    void Start()
    {
        if (slider != null)
        {
            slider.onValueChanged.AddListener(OnSliderValueChanged);
            OnSliderValueChanged(slider.value);
        }
        else
        {
            Debug.LogError("Slider non assigné !");
        }
    }

    void OnSliderValueChanged(float value)
    {
        float mapped = Mathf.Lerp(minCustomValue, maxCustomValue, (value - slider.minValue) / (slider.maxValue - slider.minValue));
        int intValue = Mathf.RoundToInt(mapped);

        //du debug
        //Debug.Log("Valeur du slider mappée : " + intValue);

        if (valueText != null)
        {
            valueText.text = intValue.ToString();
        }
    }
}
