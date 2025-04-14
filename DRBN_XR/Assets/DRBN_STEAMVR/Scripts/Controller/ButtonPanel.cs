using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPanel : MonoBehaviour
{
    public List<GameObject> objectsToHide;
    public List<GameObject> objectsToShow;

    public void ToggleObjects()
    {
        // Masquer les objets à cacher
        foreach (GameObject obj in objectsToHide)
        {
            if (obj != null)
                obj.SetActive(false);
        }

        // Afficher les objets à montrer
        foreach (GameObject obj in objectsToShow)
        {
            if (obj != null)
                obj.SetActive(true);
        }
    }
}
