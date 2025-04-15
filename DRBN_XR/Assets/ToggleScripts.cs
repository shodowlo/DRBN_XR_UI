using UnityEngine;
using UnityEngine.UI;

public class ToggleScripts: MonoBehaviour
{
    public MonoBehaviour[] scriptsToEnable;    // Liste de scripts à activer
    public MonoBehaviour[] scriptsToDeactivate; // Liste de scripts à désactiver
    public Button[] buttonsToEnable;           // Liste de boutons à activer

    private Button thisButton;                 // Référence au bouton actuel

    void Start()
    {
        // Obtenir la référence au bouton actuel
        thisButton = GetComponent<Button>();

        if (thisButton == null)
        {
            Debug.LogError("Le script doit être attaché à un GameObject avec un composant Button.");
            return;
        }

        // Ajouter un listener pour coordonner les actions
        thisButton.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        Togglescripts(scriptsToEnable, true);

        Togglescripts(scriptsToDeactivate, false);

        thisButton.interactable = false;

        EnableOtherButtons();
    }

    void Togglescripts(MonoBehaviour[] scripts, bool enable)
    {
        if (scripts != null)
        {
            foreach (var script in scripts)
            {
                if (script != null)
                {
                    script.enabled = enable;
                }
            }
        }
    }

    void EnableOtherButtons()
    {
        if (buttonsToEnable != null)
        {
            foreach (var button in buttonsToEnable)
            {
                if (button != null)
                {
                    button.interactable = true;
                }
            }
        }
    }
}
