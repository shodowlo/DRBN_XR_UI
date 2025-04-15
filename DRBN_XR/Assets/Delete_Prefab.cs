using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DeletePrefab : MonoBehaviour
{
    private InputDevice leftController;
    private InputDevice rightController;
    private bool previousButtonState = false;

    void Start()
    {
        InitializeControllers();
    }

    void InitializeControllers()
    {
        List<InputDevice> leftInputDevices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller, leftInputDevices);

        if (leftInputDevices.Count > 0)
        {
            leftController = leftInputDevices[0];
        }

        List<InputDevice> rightInputDevices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller, rightInputDevices);

        if (rightInputDevices.Count > 0)
        {
            rightController = rightInputDevices[0];
        }
    }

    void Update()
    {
        if (!rightController.isValid)
        {
            InitializeControllers();
            return;
        }

        bool isPressed = false;
        if (rightController.TryGetFeatureValue(CommonUsages.primaryButton, out isPressed) && isPressed && !previousButtonState)
        {
            DeletePointedObject();
        }

        previousButtonState = isPressed;
    }

    void DeletePointedObject()
    {
        if (leftController.isValid)
        {
            // Obtenir la position et la rotation du contrôleur gauche
            if (leftController.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position) &&
                leftController.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rotation))
            {
                Ray ray = new Ray(position, rotation * Vector3.forward);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    GameObject hitObject = hit.collider.gameObject;
                    Destroy(hitObject);
                }
            }
        }
    }
}
