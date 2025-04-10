using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class spawnPrefab : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public Transform spawnPoint;

    private InputDevice targetDevice;
    private bool previousButtonState = false;

    void Start()
    {
        List<InputDevice> inputDevices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller, inputDevices);

        if (inputDevices.Count > 0)
        {
            targetDevice = inputDevices[0];
        }
        else
        {
            Debug.LogWarning("Aucun contrôleur droit détecté.");
        }
    }

    void Update()
    {
        if (!targetDevice.isValid)
        {
            // Rechercher à nouveau si le contrôleur a été déconnecté
            Start();
            return;
        }

        bool isPressed = false;
        if (targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out isPressed)) // Touche A sur Oculus/Meta
        {
            if (isPressed && !previousButtonState)
            {
                Spawn();
            }
            previousButtonState = isPressed;
        }
    }

    void Spawn()
    {
        if (prefabToSpawn != null && spawnPoint != null)
        {
            Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("Prefab ou point d'apparition non assigné.");
        }
    }
}
