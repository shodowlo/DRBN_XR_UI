using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Interface_Size : MonoBehaviour
{
    public Transform targetObject;
    public Slider slider;

    [Header("Scale minimal and maximal")]
    public float scaleMin = 0.01f;
    public float scaleMax = 0.08f;

    public bool shouldUpdateScale = false; // Flag to control when to update the scale

    void Start()
    {
        // slider value
        slider.minValue = 0f;
        slider.maxValue = 1f;

        // Récupérer la valeur sauvegardée du slider
        if (PlayerPrefs.HasKey("SliderValue"))
        {
            float savedSliderValue = PlayerPrefs.GetFloat("SliderValue");
            slider.value = savedSliderValue;
            // Mettre à jour l'échelle avec la valeur sauvegardée
            OnSliderReleased(savedSliderValue);
        }

        // Get the EventTrigger component and add a listener for PointerUp
        EventTrigger trigger = slider.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = slider.gameObject.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener((eventData) => { OnSliderReleased(slider.value); });
        trigger.triggers.Add(entry);
    }

    public void OnSliderReleased(float value)
    {
        shouldUpdateScale = true; // Allow scale update
        UpdateScale(value);
        shouldUpdateScale = false; // Reset the flag
        PlayerPrefs.SetFloat("SliderValue", value); // Sauvegarder la nouvelle valeur
    }

    public void UpdateScale(float value)
    {
        if (shouldUpdateScale) // Only update scale if the flag is true
        {
            float scale = Mathf.Lerp(scaleMin, scaleMax, value);
            targetObject.localScale = new Vector3(scale, scale, scale);
        }
    }
}
