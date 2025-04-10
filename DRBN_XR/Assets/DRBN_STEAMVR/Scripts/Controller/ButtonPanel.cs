using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPanel : MonoBehaviour
{
    public List<Slider> slidersToHide;
    public Slider sliderToShow;

    public void ToggleSliders()
    {
        // Masquer les sliders à cacher
        foreach (Slider s in slidersToHide)
        {
            if (s != null)
                s.gameObject.SetActive(false);
        }

        // Afficher le slider à montrer
        if (sliderToShow != null)
            sliderToShow.gameObject.SetActive(true);
    }
}
