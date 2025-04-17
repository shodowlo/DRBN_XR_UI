using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DeletePrefab : MonoBehaviour
{
    private InputDevice leftController;
    private InputDevice rightController;
    private bool previousButtonState = false;
    private GameObject leftGrabbedObject = null; // Variable pour suivre l'objet attrapé par le contrôleur gauche

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
        if (rightController.TryGetFeatureValue(CommonUsages.secondaryButton, out isPressed) && isPressed && !previousButtonState)
        {
            DeleteGrabbedObject();
        }

        previousButtonState = isPressed;

        // Vérifier si le contrôleur gauche attrape un objet
        CheckForGrabbing(leftController, ref leftGrabbedObject);
    }

    void CheckForGrabbing(InputDevice controller, ref GameObject grabbedObject)
    {
        if (controller.isValid)
        {
            bool gripPressed = false;
            if (controller.TryGetFeatureValue(CommonUsages.gripButton, out gripPressed) && gripPressed)
            {
                if (grabbedObject == null)
                {
                    // Essayer d'attraper un objet
                    if (controller.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position) &&
                        controller.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rotation))
                    {
                        Ray ray = new Ray(position, rotation * Vector3.forward);
                        if (Physics.Raycast(ray, out RaycastHit hit))
                        {
                            grabbedObject = hit.collider.gameObject;
                            Debug.Log("Objet attrapé : " + grabbedObject.name);
                        }
                    }
                }
            }
            else
            {
                // Relâcher l'objet si le bouton de préhension n'est pas pressé
                grabbedObject = null;
            }
        }
    }

    void DeleteGrabbedObject()
    {
        if (leftGrabbedObject != null)
        {
            Destroy(leftGrabbedObject);
            leftGrabbedObject = null;
            Debug.Log("Objet attrapé par la manette gauche supprimé.");
        }
    }
}
