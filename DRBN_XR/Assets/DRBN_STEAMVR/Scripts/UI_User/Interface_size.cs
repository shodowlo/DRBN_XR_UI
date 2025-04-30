using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Interface_Size : MonoBehaviour
{
    public Transform targetObject;
    public Slider slider;

    [Header("Scale minimal and maximal")]
    public float scaleMin = 0.003f;
    public float scaleMax = 0.009f;

    public bool shouldUpdateScale = false; // Flag to control when to update the scale

    void Awake()
    {
        // slider value
        slider.minValue = 0f;
        slider.maxValue = 1f;

        if (PlayerPrefs.HasKey("SliderValue"))
        {
            float savedSliderValue = PlayerPrefs.GetFloat("SliderValue");
            slider.value = savedSliderValue;
            OnSliderReleased(savedSliderValue);     // Use save value
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
        PlayerPrefs.SetFloat("SliderValue", value);
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
