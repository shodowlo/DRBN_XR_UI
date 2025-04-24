using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

public class SearchBar : MonoBehaviour
{
    public TMP_InputField searchInputField;
    public GridLayoutGroup gridLayoutGroup;

    void Start()
    {
        searchInputField.onValueChanged.AddListener(OnSearchTextChanged);
    }

    void OnSearchTextChanged(string searchText)
    {
        string pattern = Regex.Escape(searchText).Replace("\\*", "\\d");

        foreach (Transform child in gridLayoutGroup.transform)
        {
            // Compare child name with search
            if (Regex.IsMatch(child.name, "^" + pattern, RegexOptions.IgnoreCase))
            {
                child.gameObject.SetActive(true);
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
