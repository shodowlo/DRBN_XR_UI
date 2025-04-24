using UnityEngine;
using UnityEngine.UI;

public class ToggleButtons : MonoBehaviour
{
    public Button[] buttonsToEnable; // Buttons to enable

    private Button thisButton;

    void Start()
    {
        thisButton = GetComponent<Button>();

        if (thisButton == null)
        {
            return;
        }
    }

    public void OnButtonClick()
    {
        thisButton.interactable = false;

        if (buttonsToEnable != null)
        {
            foreach (var button in buttonsToEnable)
            {
                if (button != null)
                {
                    button.interactable = true;
                }
            }
        }
    }
}

