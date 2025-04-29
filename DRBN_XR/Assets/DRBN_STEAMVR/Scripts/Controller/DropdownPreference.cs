using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Dropdown))]
public class DropdownPreference : MonoBehaviour
{
    public string playerPrefKey = "turn";
    private TMP_Dropdown dropdown;
    public int defaultValue = 0;

    [SerializeField] private MonoBehaviour targetObject;
    [SerializeField] private string methodName;

    void Awake()
    {
        dropdown = GetComponent<TMP_Dropdown>();

        int savedIndex = PlayerPrefs.GetInt(playerPrefKey, defaultValue);
        dropdown.value = savedIndex;

        dropdown.onValueChanged.AddListener(SavePreference);
    }

    void SavePreference(int index)
    {
        PlayerPrefs.SetInt(playerPrefKey, index);
        PlayerPrefs.Save();
        Debug.Log("Saved dropdown index: " + index);

        if (targetObject != null && !string.IsNullOrEmpty(methodName))
        {
            targetObject.Invoke(methodName, 0f);
        }
    }

    void OnDestroy()
    {
        dropdown.onValueChanged.RemoveListener(SavePreference);
    }
}
