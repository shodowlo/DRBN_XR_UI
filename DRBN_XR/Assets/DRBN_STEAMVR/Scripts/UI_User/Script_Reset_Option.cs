using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Script_Reset_Option : MonoBehaviour
{
    public Slider UI_size;
    public Interface_Size interfaceSize;
    public TimeControl timeControl;

    public void ResetOption()
    {
        float defaultSliderValue = 0.5f;
        UI_size.value = defaultSliderValue;

        // Save the current slider value
        PlayerPrefs.SetFloat("SliderValue", defaultSliderValue);

        // Reset the time scale to normal
        Time.timeScale = 1f;

        interfaceSize.OnSliderReleased(UI_size.value);

        if (timeControl != null)
        {
            timeControl.UpdateButtonStates();
        }
    }
}
