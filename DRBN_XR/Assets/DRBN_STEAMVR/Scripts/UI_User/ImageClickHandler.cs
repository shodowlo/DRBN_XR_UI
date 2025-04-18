using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic; // Ajout de l'importation pour List
using System.Linq; // Ajout de l'importation pour LINQ

public class ImageClickHandler : MonoBehaviour
{
    public string imageName; // Nom de l'image
    public TMP_Dropdown dropdown; // Référence au DropdownTMP
    private Button button; // Composant Button pour gérer les clics

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
            // Vérifiez si l'option existe déjà dans le dropdown
            if (!dropdown.options.Any(option => option.text == imageName))
            {
                dropdown.AddOptions(new List<string> { imageName });
                Debug.Log("ImageClickHandler: Added item to dropdown: " + imageName);
            }
            else
            {
                Debug.Log("ImageClickHandler: Item already exists in dropdown: " + imageName);
            }
        }
        else
        {
            Debug.LogWarning("ImageClickHandler: Dropdown or imageName is not set.");
        }
    }
}
