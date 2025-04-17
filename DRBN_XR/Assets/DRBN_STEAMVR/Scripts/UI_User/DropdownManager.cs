using UnityEngine;
using UnityEngine.UI;
using TMPro; // Utilisez cette ligne si vous utilisez TextMeshPro

public class DropdownManager : MonoBehaviour
{
    public static DropdownManager Instance { get; private set; }
    public TMP_Dropdown dropdown; // Référence au composant TMP_Dropdown

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowDropdownItem(string itemName)
    {
        if (dropdown != null)
        {
            dropdown.captionText.text = itemName;
            dropdown.Show();
        }
    }
}
