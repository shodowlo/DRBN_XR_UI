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

// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.XR;

// public class SpawnPrefabWithDropdown : MonoBehaviour
// {
//     public Transform spawnPoint;          // Point d'apparition fixe
//     public Dropdown dropdown;             // Référence au Dropdown UI
//     public GameObject[] prefabsToSpawn;   // Tableau de préfabriqués à instancier

//     private InputDevice targetDevice;
//     private bool previousButtonState = false;
//     private GameObject selectedPrefab;    // Préfabriqué sélectionné

//     void Start()
//     {
//         // Initialiser le Dropdown
//         if (dropdown != null && prefabsToSpawn.Length > 0)
//         {
//             // Ajouter un listener pour détecter les changements de valeur du Dropdown
//             dropdown.onValueChanged.AddListener(OnDropdownValueChanged);

//             // Initialiser la sélection avec le premier élément
//             OnDropdownValueChanged(dropdown.value);
//         }
//         else
//         {
//             Debug.LogWarning("Dropdown ou préfabriqués non assignés.");
//         }

//         // Initialiser le contrôleur VR
//         List<InputDevice> inputDevices = new List<InputDevice>();
//         InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller, inputDevices);

//         if (inputDevices.Count > 0)
//         {
//             targetDevice = inputDevices[0];
//         }
//         else
//         {
//             Debug.LogWarning("Aucun contrôleur droit détecté.");
//         }
//     }

//     void Update()
//     {
//         if (!targetDevice.isValid)
//         {
//             // Rechercher à nouveau si le contrôleur a été déconnecté
//             Start();
//             return;
//         }

//         bool isPressed = false;
//         if (targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out isPressed)) // Touche A sur Oculus/Meta
//         {
//             if (isPressed && !previousButtonState)
//             {
//                 Spawn();
//             }
//             previousButtonState = isPressed;
//         }
//     }

//     void OnDropdownValueChanged(int index)
//     {
//         // Mettre à jour le préfabriqué sélectionné en fonction de l'index du Dropdown
//         if (index >= 0 && index < prefabsToSpawn.Length)
//         {
//             selectedPrefab = prefabsToSpawn[index];
//             Debug.Log("Préfabriqué sélectionné : " + selectedPrefab.name);
//         }
//         else
//         {
//             selectedPrefab = null;
//             Debug.LogWarning("Index hors limites ou aucun préfabriqué assigné.");
//         }
//     }

//     void Spawn()
//     {
//         if (selectedPrefab != null && spawnPoint != null)
//         {
//             Instantiate(selectedPrefab, spawnPoint.position, spawnPoint.rotation);
//         }
//         else
//         {
//             Debug.LogWarning("Préfabriqué ou point d'apparition non assigné.");
//         }
//     }
// }
