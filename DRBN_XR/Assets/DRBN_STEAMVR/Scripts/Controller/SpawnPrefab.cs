using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;
using UnityEngine.UI;
using System;

public class SpawnPrefab : MonoBehaviour
{
    private GameObject prefabToSpawn;
    private String prefabName;
    public Transform spawnPoint;
    private InputDevice targetDevice;
    public DeleteMenu deleteMenu;
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

        bool isPressed = false;

        if (targetDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out isPressed))
        {
            if (isPressed && !previousButtonState)
            {
                Spawn();
            }
            previousButtonState = isPressed;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            Spawn();
        }
    }

    void Spawn()
    {
        if (prefabToSpawn != null && spawnPoint != null)
        {
            GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);
            deleteMenu.AddEntry(prefabName, spawnedObject);
        }
        else
        {
            Debug.LogWarning("Prefab ou point d'apparition non assigné.");
        }
    }



    public void SetTargetObject(GameObject obj, String name)
    {
        prefabName = name;
        prefabToSpawn = obj;
    }
}