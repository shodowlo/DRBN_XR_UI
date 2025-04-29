using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class ImageClickHandler : MonoBehaviour
{
    public string imageName; // image's name
    public TMP_Dropdown dropdown; // Dropdown for the selected prefab
    public ImageLoader imageLoader; // Script ImageLoader
    private Button button; // Composant Button pour gérer les clics

    public Sprite image;


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
            // if prefab is in dropdown, delete it
            if (dropdown.options.Any(option => option.text == imageName))
            {
                imageLoader.RemoveOptionFromDropdown(imageName);
                Debug.Log("ImageClickHandler: Removed existing item from dropdown: " + imageName);
            }
            else
            {
                // add the prefab
                dropdown.AddOptions(new List<TMP_Dropdown.OptionData> { new TMP_Dropdown.OptionData(imageName,image,Color.white) });
                Debug.Log("ImageClickHandler: Added item to dropdown: " + imageName);
            }

            DropdownSelection dropdownSelection = dropdown.GetComponent<DropdownSelection>();
            if (dropdownSelection != null)
            {
                dropdownSelection.ForceUpdateSpawnPrefab();
                Debug.Log("ImageClickHandler: ForceUpdateSpawnPrefab called.");
            }
            else
            {
                Debug.LogWarning("ImageClickHandler: DropdownSelection component not found on TMP_Dropdown.");
            }
        }
    }
}
