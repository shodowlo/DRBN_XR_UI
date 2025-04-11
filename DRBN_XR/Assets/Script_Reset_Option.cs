using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Script_Reset_Option : MonoBehaviour
{
    public Slider UI_size;
    public float slider_Value;
    public Interface_Size interfaceSize;
    public TimeControl timeControl;


    void Start()
    {
        UI_size.onValueChanged.AddListener(OnSliderValueChanged);
    }
    
    public void ResetOption()
    {
        // save actual value
        PlayerPrefs.SetFloat("SliderValue", slider_Value);

        UI_size.value = slider_Value;

        Time.timeScale = 1f;

        if (timeControl != null)
        {
            timeControl.UpdateButtonStates();
        }
    }

    void OnSliderValueChanged(float value)
    {
        interfaceSize.UpdateScale(value);
    }
}
