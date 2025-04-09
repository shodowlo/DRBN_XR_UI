using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;

public class Script_UI_Apparition : MonoBehaviour
{
    public GameObject canvasToToggle;  // Le Canvas à afficher/masquer
    public List<GameObject> gameObjectsToToggle;  // Liste des GameObjects à désactiver
    public GameObject gameObjectToToggle;  // Le GameObject spécifique à activer

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
        // Change l'état du canvas
        if (canvasToToggle != null)
        {
            canvasToToggle.SetActive(!canvasToToggle.activeSelf);
        }
    }

    void ToggleGameObjects()
    {
        // Si le canvas est actif, on gère les GameObjects
        if (canvasToToggle != null && canvasToToggle.activeSelf)
        {
            // Désactive tous les GameObjects dans la liste
            foreach (var obj in gameObjectsToToggle)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                }
            }

            // Si le gameObjectToToggle est désactivé, on l'active
            if (gameObjectToToggle != null && !gameObjectToToggle.activeSelf)
            {
                gameObjectToToggle.SetActive(true);
            }
        }
    }
}
