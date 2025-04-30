using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Script_Reset_Option : MonoBehaviour
{
    public Slider UI_size;
    public Interface_Size interfaceSize;
    public TimeControl timeControl;

    void Start()
    {
    }

    public void ResetOption()
    {
        // Set slider value to 0.5
        float defaultSliderValue = 0.5f;
        UI_size.value = defaultSliderValue;

        // Save the current slider value
        PlayerPrefs.SetFloat("SliderValue", defaultSliderValue);

        // Reset the time scale to normal
        Time.timeScale = 1f;

        // Allow scale update
        interfaceSize.shouldUpdateScale = true;
        interfaceSize.UpdateScale(UI_size.value);

        if (timeControl != null)
        {
            timeControl.UpdateButtonStates();
        }
    }
}
