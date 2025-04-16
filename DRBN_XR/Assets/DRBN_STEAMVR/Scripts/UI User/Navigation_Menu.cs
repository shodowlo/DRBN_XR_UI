using UnityEngine;
using System.Collections.Generic;

public class NavigationMenu : MonoBehaviour
{
    public GameObject firstPanel;
    public GameObject secondPanel;

    public void PrintSecondPanel()               // Show the second menu
    {
        if (secondPanel != null)
        {
            secondPanel.SetActive(true);
        }

        if (firstPanel != null)
        {
            firstPanel.SetActive(false);
        }
    }

    public void BackToFirstPanel()               // return to the first panel
    {
        if (secondPanel != null)
        {
            secondPanel.SetActive(false);
        }

        if (firstPanel != null)
        {
            firstPanel.SetActive(true);
        }
    }
}
