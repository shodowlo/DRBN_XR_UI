using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class DropdownManager : MonoBehaviour
{
    public TMP_Dropdown dropdown; // Référence au DropdownTMP
    public GameObject toggleObject; // Référence au GameObject à surveiller
    public string optionName; // Nom de l'option à ajouter/retirer

    void Update()
    {
        // Vérifiez l'état du GameObject et mettez à jour le dropdown en conséquence
        if (toggleObject != null && dropdown != null && !string.IsNullOrEmpty(optionName))
        {
            if (toggleObject.activeSelf)
            {
                // Ajouter l'option au dropdown si elle n'existe pas déjà
                if (!dropdown.options.Any(option => option.text == optionName))
                {
                    dropdown.AddOptions(new List<TMP_Dropdown.OptionData> { new TMP_Dropdown.OptionData(optionName) });
                    Debug.Log("DropdownManager: Added item to dropdown: " + optionName);
                }
            }
            else
            {
                // Retirer l'option du dropdown si elle existe
                var options = dropdown.options.ToList();
                var optionToRemove = options.FirstOrDefault(option => option.text == optionName);
                if (optionToRemove != null)
                {
                    options.Remove(optionToRemove);
                    dropdown.ClearOptions();
                    dropdown.AddOptions(options);
                    Debug.Log("DropdownManager: Removed item from dropdown: " + optionName);
                }
            }
        }
        else
        {
            Debug.LogWarning("DropdownManager: toggleObject, dropdown, or optionName is not set.");
        }
    }
}
