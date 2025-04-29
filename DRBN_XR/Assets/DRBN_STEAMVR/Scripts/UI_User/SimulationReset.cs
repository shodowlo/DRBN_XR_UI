// using UnityEngine;
// using UnityEngine.SceneManagement;
// using UnityEngine.UI;

// public class SimulationReset : MonoBehaviour
// {
//     public Slider slider;

//     public GameObject settings;
//     private Interface_Size interfaceSize;

//     void Start()
//     {
//         interfaceSize = slider.GetComponent<Interface_Size>();

//         // if save state present, reload it
//         if (PlayerPrefs.HasKey("SliderValue"))
//         {
//             float sliderValue = PlayerPrefs.GetFloat("SliderValue");
//             interfaceSize.OnSliderReleased(sliderValue) ;
//         }
//         else
//         {
//             float sliderValue = 0.25f;
//             interfaceSize.OnSliderReleased(sliderValue);
//         }

//         if (settings != null)
//         {
//             settings.SetActive(false);
//         }
//     }

//     public void ResetSimulation()
//     {
//         // Save actual value
//         float currentSliderValue = slider.value;
//         PlayerPrefs.SetFloat("SliderValue", currentSliderValue);

//         // Prevent scale update
//         interfaceSize.shouldUpdateScale = false;

//         // Reload the scene
//         Scene currentScene = SceneManager.GetActiveScene();
//         SceneManager.LoadScene(currentScene.name);
//     }
// }
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SimulationReset : MonoBehaviour
{
    public Slider slider;
    public GameObject settings;
    private Interface_Size interfaceSize;

    void Awake()
    {
        interfaceSize = slider.GetComponent<Interface_Size>();

        // if save state present, reload it
        if (PlayerPrefs.HasKey("SliderValue"))
        {
            float sliderValue = PlayerPrefs.GetFloat("SliderValue");
            interfaceSize.OnSliderReleased(sliderValue);
        }
        else
        {
            float sliderValue = 0.25f;
            interfaceSize.OnSliderReleased(sliderValue);
        }
    }

    void Start()
    {
        if (settings != null)
        {
            settings.SetActive(false);
        }
    }

    public void ResetSimulation()
    {
        // Save actual value
        float currentSliderValue = slider.value;
        PlayerPrefs.SetFloat("SliderValue", currentSliderValue);

        // Prevent scale update
        interfaceSize.shouldUpdateScale = false;

        // Reload the scene
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
