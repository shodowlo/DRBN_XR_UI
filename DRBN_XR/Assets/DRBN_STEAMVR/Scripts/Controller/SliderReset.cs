using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderReset : MonoBehaviour
{
    public List<Slider> slidersToReset;

    public void ResetSlidersToZero()
    {
        foreach (Slider slider in slidersToReset)
        {
            if (slider != null)
            {
                slider.value = 0f;
            }
        }
    }
}