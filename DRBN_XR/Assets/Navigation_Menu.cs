using UnityEngine;
using System.Collections.Generic;

public class Navigation_Menu : MonoBehaviour
{
    public GameObject firstPanel;
    public GameObject secondPanel;

    public void PrintSecondPanel()               // Show the option menu
    {
        secondPanel.SetActive(true);
        firstPanel.SetActive(false);
    }

    public void BackToFirstPanel()               // return to the first panel
    {
        secondPanel.SetActive(false);
        firstPanel.SetActive(true);
    }
}
