using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPanel : MonoBehaviour
{
    public List<GameObject> objectsToHide;
    public GameObject objectToShow;

    public void ToggleObjects()
    {
        // Masquer les objets à cacher
        foreach (GameObject obj in objectsToHide)
        {
            if (obj != null)
                obj.SetActive(false);
        }

        // Afficher l'objet à montrer
        if (objectToShow != null)
            objectToShow.SetActive(true);
    }
}