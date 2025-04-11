using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SimulationReset : MonoBehaviour
{
    public Slider slider;
    private Interface_Size interfaceSize;

    void Start()
    {
        interfaceSize = slider.GetComponent<Interface_Size>();

        slider.onValueChanged.AddListener(OnSliderValueChanged);

        // if save state present, reload it
        if (PlayerPrefs.HasKey("SliderValue"))
        {
            float sliderValue = PlayerPrefs.GetFloat("SliderValue");
            slider.value = sliderValue;
        }
        else{
            float sliderValue = 0.25f;
            slider.value = sliderValue;
        }
    }

    public void ResetSimulation()
    {
        // save actual value
        float currentSliderValue = slider.value;
        PlayerPrefs.SetFloat("SliderValue", currentSliderValue);

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    void OnSliderValueChanged(float value)
    {
        interfaceSize.UpdateScale(value);
    }
}