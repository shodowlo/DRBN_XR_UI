using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    public bool isSelected = false;
    public Color selectedColor = Color.green;
    public Color unselectedColor = Color.white;

    private Button button;
    private Image buttonImage;
    private Vector3 baseLocalPosition;

    void Start()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();
        baseLocalPosition = transform.localPosition;

        if (button != null)
            button.onClick.AddListener(ToggleSelection);

        UpdateVisuals();
    }

    void ToggleSelection()
    {
        isSelected = !isSelected;
        UpdateVisuals();
    }

    void UpdateVisuals()
    {
        // Change color
        if (buttonImage != null)
            buttonImage.color = isSelected ? selectedColor : unselectedColor;

        // Change Z position
        Vector3 newPos = baseLocalPosition;
        newPos.z += isSelected ? 20f : 0f;
        transform.localPosition = newPos;
    }
}
