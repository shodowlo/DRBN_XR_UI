using UnityEngine;
using UnityEngine.UI;

public class togglePreference : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    public string preferenceKey = "startTutorial";
    public bool defaultValue = true;

    [SerializeField] private MonoBehaviour targetObject;
    [SerializeField] private string methodName;

    private void Start()
    {
        if (toggle == null)
            toggle = GetComponent<Toggle>();

        // Charger la préférence existante au démarrage
        bool startTutorial = PlayerPrefs.GetInt(preferenceKey, defaultValue ? 1 : 0) == 1;
        toggle.isOn = startTutorial;

        // Ajouter l'écouteur
        toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    private void OnToggleChanged(bool isOn)
    {
        PlayerPrefs.SetInt(preferenceKey, isOn ? 1 : 0);
        PlayerPrefs.Save();

        if (targetObject != null && !string.IsNullOrEmpty(methodName))
        {
            targetObject.Invoke(methodName, 0f);
        }
    }

    private void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnToggleChanged);
    }
}
