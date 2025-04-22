using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropdownSelection : MonoBehaviour
{
    public TMP_Dropdown dropdown;               // Référence au Dropdown UI
    public ImageLoader imageLoader;             // Référence au script ImageLoader
    public SpawnPrefab anotherScript;           // Référence au script cible

    void Start()
    {
        // Vérifier si les références sont définies
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

        if (anotherScript == null)
        {
            Debug.LogError("DropdownSelection: SpawnPrefab reference is not set.");
            return;
        }

        // Ajouter un listener pour détecter les changements de valeur du Dropdown
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);

        // Initialiser la sélection avec le premier élément
        OnDropdownValueChanged(dropdown.value);
    }

    void OnDropdownValueChanged(int index)
    {
        if (index >= 0 && index < dropdown.options.Count)
        {
            string selectedOption = dropdown.options[index].text;
            GameObject prefabToSpawn = imageLoader.GetPrefabByName(selectedOption);

            if (prefabToSpawn != null)
            {
                anotherScript.SetTargetObject(prefabToSpawn);
                Debug.Log("Objet sélectionné : " + prefabToSpawn.name);
            }
            else
            {
                Debug.LogWarning("Préfab non trouvé pour l'option sélectionnée : " + selectedOption);
            }
        }
        else
        {
            Debug.LogWarning("Index hors limites ou aucun objet assigné.");
        }
    }
}
