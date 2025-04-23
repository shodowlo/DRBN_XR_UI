// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;
// using System.Collections.Generic; // Ajout de l'importation pour List
// using System.Linq; // Ajout de l'importation pour LINQ

// public class ImageClickHandler : MonoBehaviour
// {
//     public string imageName; // Nom de l'image
//     public TMP_Dropdown dropdown; // Référence au DropdownTMP
//     private Button button; // Composant Button pour gérer les clics

//     void Start()
//     {
//         button = GetComponent<Button>();
//         if (button != null)
//         {
//             button.onClick.AddListener(OnImageClick);
//         }
//         else
//         {
//             Debug.LogWarning("ImageClickHandler: Button component not found.");
//         }
//     }

//     void OnImageClick()
//     {
//         if (dropdown != null && !string.IsNullOrEmpty(imageName))
//         {
//             // Vérifiez si l'option existe déjà dans le dropdown
//             if (!dropdown.options.Any(option => option.text == imageName))
//             {
//                 dropdown.AddOptions(new List<string> { imageName });
//                 Debug.Log("ImageClickHandler: Added item to dropdown: " + imageName);
//             }
//             else
//             {
//                 Debug.Log("ImageClickHandler: Item already exists in dropdown: " + imageName);
//             }
//         }
//         else
//         {
//             Debug.LogWarning("ImageClickHandler: Dropdown or imageName is not set.");
//         }
//     }
// }

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
                dropdown.AddOptions(new List<TMP_Dropdown.OptionData> { new TMP_Dropdown.OptionData(imageName) });
                Debug.Log("ImageClickHandler: Added item to dropdown: " + imageName);
            }
            else
            {
                Debug.Log("ImageClickHandler: Item already exists in dropdown: " + imageName);
            }

            // Obtenir la référence au script DropdownSelection1 à partir du composant TMP_Dropdown
            DropdownSelection dropdownSelection = dropdown.GetComponent<DropdownSelection>();
            if (dropdownSelection != null)
            {
                dropdownSelection.ForceUpdateSpawnPrefab();
                Debug.Log("ImageClickHandler: ForceUpdateSpawnPrefab called.");
            }
            else
            {
                Debug.LogWarning("ImageClickHandler: DropdownSelection1 component not found on TMP_Dropdown.");
            }
        }
    }
}