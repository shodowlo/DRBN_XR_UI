using UnityEngine;
using UnityEngine.UI;

public class ToggleScripts: MonoBehaviour
{
    public MonoBehaviour[] scriptsToEnable;
    public MonoBehaviour[] scriptsToDeactivate;

    private Button thisButton;  // This button

    void Start()
    {
        thisButton = GetComponent<Button>();

        if (thisButton == null)
        {
            Debug.LogError("Le script doit être attaché à un GameObject avec un composant Button.");
            return;
        }

        thisButton.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        Togglescripts(scriptsToEnable, true);

        Togglescripts(scriptsToDeactivate, false);

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
}
