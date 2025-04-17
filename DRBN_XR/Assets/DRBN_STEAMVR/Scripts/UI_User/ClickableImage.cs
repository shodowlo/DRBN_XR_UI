using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickableImage : MonoBehaviour, IPointerClickHandler
{
    public string dropdownItemName; // Nom de l'élément à afficher dans le dropdown

    public void OnPointerClick(PointerEventData eventData)
    {
        // Appeler une méthode pour afficher l'élément dans le dropdown
        DropdownManager.Instance.ShowDropdownItem(dropdownItemName);
    }
}
