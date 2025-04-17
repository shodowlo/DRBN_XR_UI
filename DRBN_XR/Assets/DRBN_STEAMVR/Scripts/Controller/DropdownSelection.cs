using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropdownSelection : MonoBehaviour
{
    public TMP_Dropdown dropdown;               // Référence au Dropdown UI
    public GameObject[] targetObjects;      // Liste d'objets correspondant aux options du Dropdown
    public SpawnPrefab anotherScript;     // Référence au script cible

    void Start()
    {
        // Ajouter un listener pour détecter les changements de valeur du Dropdown
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);

        // Initialiser la sélection avec le premier élément
        OnDropdownValueChanged(dropdown.value);
    }

    void OnDropdownValueChanged(int index)
    {
        // Mettre à jour le paramètre du script cible avec l'objet sélectionné
        if (index >= 0 && index < targetObjects.Length)
        {
            anotherScript.SetTargetObject(targetObjects[index]);
            Debug.Log("Objet sélectionné : " + targetObjects[index].name);
        }
        else
        {
            Debug.LogWarning("Index hors limites ou aucun objet assigné.");
        }
    }
}
