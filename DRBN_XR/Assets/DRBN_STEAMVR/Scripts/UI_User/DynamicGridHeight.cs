using UnityEngine;
using UnityEngine.UI;

public class DynamicGridHeight : MonoBehaviour
{
    public RectTransform content;
    public GridLayoutGroup gridLayoutGroup;
    private float heightIncrement = 40f; // La valeur d'incrémentation de la hauteur
    private bool needsAdjustment = true;
    
    void Start()
    {
        // Assurez-vous que le content et le gridLayoutGroup sont assignés
        if (content == null)
        {
            content = GetComponent<RectTransform>();
        }

        if (gridLayoutGroup == null)
        {
            gridLayoutGroup = GetComponentInParent<GridLayoutGroup>();
        }
        AdjustContentHeight();
    }

    void LateUpdate()
    {
        if (needsAdjustment)
        {
            AdjustContentHeight();
            needsAdjustment = false;
        }
    }

    public void AdjustContentHeight()
    {
        int childCount = content.childCount;
        float newHeight = Mathf.CeilToInt((float)childCount / 2) * heightIncrement;
        content.sizeDelta = new Vector2(content.sizeDelta.x, newHeight + 10);
    }

    // Appeler cette méthode chaque fois que vous ajoutez un nouvel élément
    public void OnElementAdded()
    {
        AdjustContentHeight();
    }
}
