using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;

public class Script_UI_Apparition : MonoBehaviour
{
    public GameObject canvasToToggle;
    public List<GameObject> gameObjectsToHide;
    public GameObject gameObjectToShow;

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
        if (targetDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out isPressed)) // Touche Y
        {
            if (isPressed && !previousButtonState)
            {
                ToggleCanvas();
                ToggleGameObjects();
            }
            previousButtonState = isPressed;
        }
    }

    void ToggleCanvas()
    {
        if (canvasToToggle != null)
        {
            canvasToToggle.SetActive(!canvasToToggle.activeSelf);
        }
    }

    void ToggleGameObjects()
    {
        if (canvasToToggle != null && canvasToToggle.activeSelf)
        {
            // Hide all the gameobjects who need it
            foreach (var obj in gameObjectsToHide)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                }
            }

            // if not active, activate
            if (gameObjectToShow != null && !gameObjectToShow.activeSelf)
            {
                gameObjectToShow.SetActive(true);
            }
        }
    }
}
