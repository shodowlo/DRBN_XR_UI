using UnityEngine;
using System.Collections.Generic;

public class Option_menu : MonoBehaviour
{
    [Header("Menu Buttons")]                    // Buttons of the menu
    public List<GameObject> firstList;

    [Header("Option Buttons")]                  // Buttons of the option menu
    public List<GameObject> secondList;

    public void PrintSecondList()               // Show the option menu
    {
        foreach (GameObject btn in firstList)
            btn.SetActive(false);

        foreach (GameObject btn in secondList)
            btn.SetActive(true);
    }

    public void BackToFirstList()               // return to the first panel
    {
        foreach (GameObject btn in secondList)
            btn.SetActive(false);

        foreach (GameObject btn in firstList)
            btn.SetActive(true);
    }
}
