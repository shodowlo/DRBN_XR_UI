using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SimulationReset : MonoBehaviour
{
    public Slider slider;
    public Interface_Size interfaceSize;

    void Start()
    {
        // Ajouter un listener à l'événement onValueChanged
        slider.onValueChanged.AddListener(OnSliderValueChanged);

        // Restaurer la valeur du slider si elle a été sauvegardée
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
        // Sauvegarder la valeur actuelle du slider
        float currentSliderValue = slider.value;
        PlayerPrefs.SetFloat("SliderValue", currentSliderValue);

        // Réinitialiser la simulation en rechargeant la scène
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    // Méthode appelée lorsque la valeur du slider change
    void OnSliderValueChanged(float value)
    {
        interfaceSize.UpdateScale(value);
    }
}