using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SearchBar : MonoBehaviour
{
    public TMP_InputField searchInputField;
    public GridLayoutGroup gridLayoutGroup;

    void Start()
    {
        // Ajoutez un listener pour détecter les changements dans l'InputField
        searchInputField.onValueChanged.AddListener(OnSearchTextChanged);
    }

    void OnSearchTextChanged(string searchText)
    {
        // Parcourez tous les enfants du GridLayoutGroup
        foreach (Transform child in gridLayoutGroup.transform)
        {
            // Vérifiez si le nom de l'enfant commence par le texte saisi
            if (child.name.ToLower().StartsWith(searchText.ToLower()))
            {
                // Affichez l'enfant
                child.gameObject.SetActive(true);
            }
            else
            {
                // Cachez l'enfant
                child.gameObject.SetActive(false);
            }
        }
    }
}