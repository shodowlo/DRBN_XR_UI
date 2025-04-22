using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;
using UnityEngine.UI;

public class SpawnPrefab : MonoBehaviour
{
    private GameObject prefabToSpawn;
    public Transform spawnPoint;

    public GameObject labelUIPrefab;       // Le prefab avec TextMeshProUGUI
    public GameObject uiCanvas;                // Le canvas (pas obligatoire ici, juste pour contexte)
    public RectTransform scrollViewContent; // 👉 Le content de la ScrollView

    private InputDevice targetDevice;
    private bool previousButtonState = false;

    private float labelHeight = 28.7f; // Hauteur d’un label (en unités UI)

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
        //if (!targetDevice.isValid)
        //{
        //    Start();
        //    return;
        //}

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
            SpawnUILabel(prefabToSpawn.name, spawnedObject);
        }
        else
        {
            Debug.LogWarning("Prefab ou point d'apparition non assigné.");
        }
    }

    void SpawnUILabel(string labelText, GameObject spawnedObject)
    {
        if (labelUIPrefab != null && scrollViewContent != null)
        {
            GameObject labelUI = Instantiate(labelUIPrefab, uiCanvas.transform); // Important : scrollViewContent ici
            TextMeshProUGUI text = labelUI.GetComponentInChildren<TextMeshProUGUI>();
            if (text != null)
            {
                text.text = labelText;
            }

            // Chercher le bouton dans l’UI
            Button deleteButton = labelUI.GetComponentInChildren<Button>();
            if (deleteButton != null)
            {
                deleteButton.onClick.AddListener(() =>
                {
                    // Supprimer la molécule
                    Destroy(spawnedObject);

                    // Supprimer l'élément UI
                    Destroy(labelUI);

                    // Réduire la hauteur du content
                    Vector2 size = scrollViewContent.sizeDelta;
                    size.y -= labelHeight;
                    scrollViewContent.sizeDelta = size;
                });
            }

            DashedLineMol[] allDashedLines = labelUI.GetComponentsInChildren<DashedLineMol>(true);

            if (allDashedLines.Length > 0)
            {
                DashedLineMol dashedLine = allDashedLines[0];
                dashedLine.startPoint = spawnedObject.transform;
                dashedLine.targetObject = scrollViewContent.gameObject; // Assigner le parent de la ScrollView
                dashedLine.gameObject.SetActive(false); // Désactive pour éviter rendu imediat
            }

            // Augmenter la taille du content (en hauteur)
            Vector2 size = scrollViewContent.sizeDelta;
            size.y += labelHeight;
            scrollViewContent.sizeDelta = size;
        }
        else
        {
            Debug.LogWarning("Label UI Prefab ou Content de ScrollView non assigné.");
        }
    }


    public void SetTargetObject(GameObject obj)
    {
        prefabToSpawn = obj;
    }
}