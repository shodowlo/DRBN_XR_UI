using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;


public class Script_UI_Apparition_Temp : MonoBehaviour
{
    public GameObject sliderToToggle;  // Le Canvas à afficher/masquer
    private InputDevice targetDevice;
    private bool previousButtonState = false;

    void Start()
    {
        var inputDevices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.HeldInHand, inputDevices);
        targetDevice = inputDevices[0];
    }

    void Update()
    {
        bool isPressed = false;
        if (targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out isPressed)) // Touche Y
        {
            if (isPressed && !previousButtonState)
            {
                ToggleCanvas();
            }
            previousButtonState = isPressed;
        }
    }

    void ToggleCanvas()
    {
        // Change l'état du canvas
        if (sliderToToggle != null)
        {
            sliderToToggle.SetActive(!sliderToToggle.activeSelf);
        }
    }
}
