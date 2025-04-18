using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ImageClickHandler : MonoBehaviour
{
    public string imageName; // Nom de l'image
    public TMP_Dropdown dropdown; // Référence au DropdownTMP
    private Button button; // Composant Button pour gérer les clics
    public GameObject objectToDisable; // Référence au GameObject à désactiver

    void Start()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnImageClick);
        }
        else
        {
            Debug.LogWarning("ImageClickHandler: Button component not found.");
        }
    }

    void OnImageClick()
    {
        if (dropdown != null && !string.IsNullOrEmpty(imageName))
        {
            dropdown.AddOptions(new List<string> { imageName });
            Debug.Log("ImageClickHandler: Added item to dropdown: " + imageName);
        }
        else
        {
            Debug.LogWarning("ImageClickHandler: Dropdown or imageName is not set.");
        }

        if (objectToDisable != null)
        {
            objectToDisable.SetActive(true);
        }
        else
        {
            Debug.LogWarning("ImageClickHandler: objectToDisable is not set.");
        }
    }
}
