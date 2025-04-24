using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropdownSelection : MonoBehaviour
{
    public TMP_Dropdown dropdown;               // Dropdown UI
    public ImageLoader imageLoader;             // script ImageLoader
    public SpawnPrefab spawnPrefab;             // script SpawnPrefab

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
            spawnPrefab.SetTargetObject(null);
            Debug.Log("Dropdown is empty. No prefab will be spawned.");
            return;
        }

        int index = dropdown.value;
        if (index == dropdown.options.Count)
        {
            index--;
        }
        if (index >= 0 && index < dropdown.options.Count)
        {
            string selectedOption = dropdown.options[index].text;           
            GameObject prefabToSpawn = imageLoader.GetPrefabByName(selectedOption);

            if (prefabToSpawn != null)
            {
                spawnPrefab.SetTargetObject(prefabToSpawn);
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
