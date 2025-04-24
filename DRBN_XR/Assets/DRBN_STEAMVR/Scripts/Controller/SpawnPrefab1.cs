using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;
using UnityEngine.UI;

public class SpawnPrefab : MonoBehaviour
{
    private GameObject prefabToSpawn;
    public Transform spawnPoint;

    public GameObject labelUIPrefab;       // Prefab with TextMeshProUGUI
    public GameObject uiCanvas;
    public RectTransform scrollViewContent; // 👉 Le content de la ScrollView

    private InputDevice targetDevice;
    private bool previousButtonState = false;

    private float labelHeight = 28.7f;

    private List<GameObject> spawnedObjects = new List<GameObject>();
    private List<GameObject> spawnedLabels = new List<GameObject>();

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
            spawnedObjects.Add(spawnedObject);
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
            GameObject labelUI = Instantiate(labelUIPrefab, uiCanvas.transform); // scrollViewContent
            TextMeshProUGUI text = labelUI.GetComponentInChildren<TextMeshProUGUI>();

            spawnedLabels.Add(labelUI);

            if (text != null)
            {
                text.text = labelText;
            }

            Button deleteButton = labelUI.GetComponentInChildren<Button>();
            if (deleteButton != null)
            {
                deleteButton.onClick.AddListener(() =>
                {
                    Destroy(spawnedObject);
                    spawnedObjects.Remove(spawnedObject);

                    Destroy(labelUI);
                    spawnedLabels.Remove(labelUI);

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
                dashedLine.targetObject = scrollViewContent.gameObject;
                dashedLine.gameObject.SetActive(false);
            }

            Vector2 size = scrollViewContent.sizeDelta;
            size.y += labelHeight;
            scrollViewContent.sizeDelta = size;
        }
        else
        {
            Debug.LogWarning("Label UI Prefab ou Content de ScrollView non assigné.");
        }
    }

    public void ClearAll()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj != null)
                Destroy(obj);
        }
        spawnedObjects.Clear();

        foreach (GameObject label in spawnedLabels)
        {
            if (label != null)
                Destroy(label);
        }
        spawnedLabels.Clear();

        scrollViewContent.sizeDelta = new Vector2(scrollViewContent.sizeDelta.x, 0);
    }



    public void SetTargetObject(GameObject obj)
    {
        prefabToSpawn = obj;
    }
}