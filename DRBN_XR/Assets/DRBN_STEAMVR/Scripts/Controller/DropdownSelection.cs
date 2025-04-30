using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropdownSelection : MonoBehaviour
{
    public TMP_Dropdown dropdown;               // Dropdown UI
    public ImageLoader imageLoader;             // script ImageLoader
    public SpawnPrefab spawnPrefab;             // script SpawnPrefab
    public GameObject favoriteElement;
    public GameObject favoriteElement2;
    public GameObject messageNoFavorite;
    public Image targetImageController;
    public Image targetImageControllerBackground;

    void Start()
    {
        if (dropdown == null)
        {
            Debug.LogError("DropdownSelection: Dropdown reference is not set.");
            return;
        }

        if (imageLoader == null)
        {
            Debug.LogError("DropdownSelection: ImageLoader reference is not set.");
            return;
        }

        if (spawnPrefab == null)
        {
            Debug.LogError("DropdownSelection: SpawnPrefab reference is not set.");
            return;
        }

        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);

        // Initialize with first element
        OnDropdownValueChanged(dropdown.value);
    }

    public void OnDropdownValueChanged(int index)
    {
        Debug.Log("Dropdown value changed to: " + index);
        ForceUpdateSpawnPrefab();
    }

    public void ForceUpdateSpawnPrefab()
    {
        Debug.Log("ForceUpdateSpawnPrefab est appelé");
        UpdateSpawnPrefab();
    }

    private void UpdateSpawnPrefab()
    {
        if (dropdown.options.Count == 0)
        {
            spawnPrefab.SetTargetObject(null,null);
            Debug.Log("Dropdown is empty. No prefab will be spawned.");
            favoriteElement.gameObject.SetActive(false);
            favoriteElement2.gameObject.SetActive(false);
            messageNoFavorite.SetActive(true);
            targetImageController.enabled = false;
            targetImageControllerBackground.enabled = false;

            return;
        }
        else
        {
            favoriteElement.gameObject.SetActive(true);
            favoriteElement2.gameObject.SetActive(true);
            messageNoFavorite.SetActive(false);
            targetImageController.enabled = true;
            targetImageControllerBackground.enabled = true;
        }

        int index = dropdown.value;


        if (index == dropdown.options.Count)
        {
            index--;
        }
        if (index >= 0 && index < dropdown.options.Count)
        {
            //Pour l'image sur le controller droit
            TMP_Dropdown.OptionData selectedOptionDropdown = dropdown.options[index];

            if (selectedOptionDropdown.image != null)
            {
                targetImageController.sprite = selectedOptionDropdown.image;
            }
            else
            {
                Debug.LogWarning("Pas de sprite pour cette option.");
                targetImageController.enabled = false;
            }

            string selectedOption = dropdown.options[index].text;           
            GameObject prefabToSpawn = imageLoader.GetPrefabByName(selectedOption);

            if (prefabToSpawn != null)
            {
                spawnPrefab.SetTargetObject(prefabToSpawn, selectedOption);
                Debug.Log("Objet sélectionné : " + prefabToSpawn.name);
            }
            else
            {
                Debug.LogWarning("Préfab non trouvé pour l'option sélectionnée : " + selectedOption);
            }
        }
        else
        {
            Debug.LogWarning("Index de dropdown invalide : " + index);
        }
    }
}
