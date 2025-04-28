using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SliderLogger : MonoBehaviour
{
    [System.Serializable]
    public class SliderInfo
    {
        public Slider slider;
        public TextMeshProUGUI valueText;
        public TextMeshProUGUI valueTextPanel;
        public float minCustomValue = 1f;
        public float maxCustomValue = 100f;
        public float defaultValue = 0.5f;
        public Button showButton;
        public GameObject lineObject;
    }

    [Header("Sliders à gérer")]
    public List<SliderInfo> sliders = new List<SliderInfo>();

    [Header("Bouton de reset")]
    public Button resetButton;

    void Start()
    {
        foreach (var sliderInfo in sliders)
        {
            if (sliderInfo.slider != null)
            {
                SliderInfo capturedInfo = sliderInfo;

                // Listener pour la valeur du slider
                capturedInfo.slider.onValueChanged.AddListener((value) => OnSliderValueChanged(capturedInfo, value));
                OnSliderValueChanged(capturedInfo, capturedInfo.slider.value);

                // Listener pour le bouton d'affichage
                if (capturedInfo.showButton != null)
                {
                    capturedInfo.showButton.onClick.AddListener(() => ShowOnlySlider(capturedInfo));
                }
                else
                {
                    Debug.LogWarning($"Pas de bouton assigné pour {capturedInfo.slider.gameObject.name}");
                }
            }
            else
            {
                Debug.LogError("Slider non assigné dans la liste !");
            }
        }

        if (resetButton != null)
        {
            resetButton.onClick.AddListener(ResetSliders);
        }
        else
        {
            Debug.LogWarning("Aucun bouton de reset assigné !");
        }
    }

    void OnSliderValueChanged(SliderInfo sliderInfo, float value)
    {
        float mapped = Mathf.Lerp(
            sliderInfo.minCustomValue,
            sliderInfo.maxCustomValue,
            (value - sliderInfo.slider.minValue) / (sliderInfo.slider.maxValue - sliderInfo.slider.minValue)
        );

        int intValue = Mathf.RoundToInt(mapped);

        Debug.Log($"[{sliderInfo.slider.gameObject.name}] Valeur mappée : {intValue}");

        if (sliderInfo.valueText != null)
            sliderInfo.valueText.text = intValue.ToString();

        if (sliderInfo.valueTextPanel != null)
            sliderInfo.valueTextPanel.text = intValue.ToString();
    }

    public void ResetSliders()
    {
        foreach (var sliderInfo in sliders)
        {
            if (sliderInfo.slider != null)
            {
                sliderInfo.slider.value = sliderInfo.defaultValue;
            }
        }
    }

    public void ShowOnlySlider(SliderInfo sliderToShow)
    {
        foreach (var sliderInfo in sliders)
        {
            bool isTarget = sliderInfo == sliderToShow;

            if (sliderInfo.slider != null)
                sliderInfo.slider.gameObject.SetActive(isTarget);

            if (sliderInfo.valueText != null)
                sliderInfo.valueText.gameObject.SetActive(isTarget);

            if (sliderInfo.lineObject != null)
                sliderInfo.lineObject.SetActive(isTarget);
        }
    }
}
