using UnityEngine;
using UnityEngine.UI;

public class TimeControl : MonoBehaviour
{
    public Button pauseButton;      // Référence au bouton pour mettre en pause
    public Button normalSpeedButton; // Référence au bouton pour la vitesse normale
    public Button doubleSpeedButton; // Référence au bouton pour la vitesse double

    void Start()
    {
        // Ajouter des listeners aux boutons
        pauseButton.onClick.AddListener(PauseTime);
        normalSpeedButton.onClick.AddListener(NormalSpeed);
        doubleSpeedButton.onClick.AddListener(DoubleSpeed);

        // Initialiser l'état des boutons
        UpdateButtonStates();
    }

    // Fonction pour mettre en pause la simulation
    void PauseTime()
    {
        Time.timeScale = 0f;
        UpdateButtonStates();
    }

    // Fonction pour revenir à la vitesse normale
    void NormalSpeed()
    {
        Time.timeScale = 1f;
        UpdateButtonStates();
    }

    // Fonction pour doubler la vitesse de la simulation
    void DoubleSpeed()
    {
        Time.timeScale = 2f;
        UpdateButtonStates();
    }

    // Fonction pour mettre à jour l'état des boutons
    public void UpdateButtonStates()
    {
        pauseButton.interactable = (Time.timeScale != 0f);
        normalSpeedButton.interactable = (Time.timeScale != 1f);
        doubleSpeedButton.interactable = (Time.timeScale != 2f);
    }
}
