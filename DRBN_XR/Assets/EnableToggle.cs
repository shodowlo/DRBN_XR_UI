using UnityEngine;
using UnityEngine.UI;

public class EnableToggle : MonoBehaviour
{

    public MonoBehaviour scriptToToggle;    // Script to toggle

    public void OnButtonClick()
    {
        if (scriptToToggle != null)
        {
            // Activer ou désactiver le script
            scriptToToggle.enabled = !scriptToToggle.enabled;
        }
    }
}