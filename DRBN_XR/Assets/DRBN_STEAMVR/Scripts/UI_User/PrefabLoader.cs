// using UnityEngine;
// using UnityEngine.UI;
// using TMPro; // Utilisez cette ligne si vous utilisez TextMeshPro
// using System.Xml;
// using System.Collections.Generic;
// using System.Linq;

// public class PrefabLoader : MonoBehaviour
// {
//     public string xmlFilePath; // Chemin vers votre fichier XML
//     public GameObject prefabContainer; // Conteneur pour les prefabs

//     void Start()
//     {
//         Debug.Log("PrefabLoader: Start method called.");
//         LoadPrefabsFromXML();
//     }

//     void LoadPrefabsFromXML()
//     {
//         Debug.Log("PrefabLoader: LoadPrefabsFromXML method called.");
//         XmlDocument xmlDoc = new XmlDocument();
//         try
//         {
//             xmlDoc.Load(xmlFilePath);
//             Debug.Log("PrefabLoader: XML file loaded successfully.");
//         }
//         catch (System.Exception e)
//         {
//             Debug.LogError("PrefabLoader: Error loading XML file: " + e.Message);
//             return;
//         }

//         XmlNodeList prefabNodes = xmlDoc.SelectNodes("Prefabs/Prefab");
//         if (prefabNodes == null || prefabNodes.Count == 0)
//         {
//             Debug.LogWarning("PrefabLoader: No prefab nodes found in XML file.");
//             return;
//         }

//         Debug.Log("PrefabLoader: Found " + prefabNodes.Count + " prefab nodes.");

//         List<GameObject> loadedPrefabs = new List<GameObject>();

//         foreach (XmlNode prefabNode in prefabNodes)
//         {
//             string prefabName = prefabNode.Attributes["name"]?.Value;
//             string prefabPath = prefabNode.Attributes["path"]?.Value;

//             if (string.IsNullOrEmpty(prefabName) || string.IsNullOrEmpty(prefabPath))
//             {
//                 Debug.LogWarning("PrefabLoader: Invalid prefab node in XML file.");
//                 continue;
//             }

//             GameObject prefab = Resources.Load<GameObject>(prefabPath);
//             if (prefab != null)
//             {
//                 loadedPrefabs.Add(prefab);
//                 Debug.Log("PrefabLoader: Loaded prefab: " + prefabName);
//             }
//             else
//             {
//                 Debug.LogWarning("PrefabLoader: Prefab not found at path: " + prefabPath);
//             }
//         }

//         // Trier les prefabs par nom
//         loadedPrefabs = loadedPrefabs.OrderBy(prefab => prefab.name).ToList();
//         Debug.Log("PrefabLoader: Prefabs sorted.");

//         // Vérifier les prefabs chargés
//         foreach (var prefab in loadedPrefabs)
//         {
//             Debug.Log("PrefabLoader: Prefab to display: " + prefab.name);
//         }

//         DisplayPrefabs(loadedPrefabs);
//     }

//     void DisplayPrefabs(List<GameObject> prefabs)
//     {
//         Debug.Log("PrefabLoader: DisplayPrefabs method called.");
//         if (prefabs.Count == 0)
//         {
//             Debug.LogWarning("PrefabLoader: No prefabs loaded.");
//             return;
//         }

//         GridLayoutGroup gridLayout = prefabContainer.GetComponent<GridLayoutGroup>();
//         if (gridLayout == null)
//         {
//             gridLayout = prefabContainer.AddComponent<GridLayoutGroup>();
//         }

//         gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
//         gridLayout.constraintCount = 4;

//         foreach (GameObject prefab in prefabs)
//         {
//             GameObject instance = Instantiate(prefab, prefabContainer.transform);
//             Debug.Log("PrefabLoader: Instantiated prefab: " + prefab.name);

//             // Si vous avez besoin d'ajouter un composant Text ou TextMeshProUGUI pour afficher le nom du prefab
//             TextMeshProUGUI prefabNameText = instance.GetComponentInChildren<TextMeshProUGUI>();
//             if (prefabNameText != null)
//             {
//                 prefabNameText.text = prefab.name;
//                 Debug.Log("PrefabLoader: Displayed prefab: " + prefab.name);
//             }
//             else
//             {
//                 Debug.LogWarning("PrefabLoader: TextMeshProUGUI component not found in prefab instance.");
//             }
//         }

//         // Ajoutez une ScrollRect si nécessaire
//         ScrollRect scrollRect = prefabContainer.GetComponentInParent<ScrollRect>();
//         if (scrollRect == null)
//         {
//             scrollRect = prefabContainer.AddComponent<ScrollRect>();
//             scrollRect.content = prefabContainer.GetComponent<RectTransform>();
//             scrollRect.viewport = prefabContainer.transform.parent.GetComponent<RectTransform>();
//             scrollRect.horizontal = false;
//             scrollRect.vertical = true;
//             scrollRect.movementType = ScrollRect.MovementType.Clamped;
//             scrollRect.inertia = false;
//             Debug.Log("PrefabLoader: ScrollRect added.");
//         }
//     }
// }

using UnityEngine;
using UnityEngine.UI;
using TMPro; // Utilisez cette ligne si vous utilisez TextMeshPro
using System.Xml;
using System.Collections.Generic;
using System.Linq;

public class PrefabLoader : MonoBehaviour
{
    public string xmlFilePath; // Chemin vers votre fichier XML
    public GameObject imageContainer; // Conteneur pour les images
    public GameObject clickableImagePrefab; // Prefab de l'image cliquable

    void Start()
    {
        Debug.Log("ImageLoader: Start method called.");
        LoadImagesFromXML();
    }

    void LoadImagesFromXML()
    {
        Debug.Log("ImageLoader: LoadImagesFromXML method called.");
        XmlDocument xmlDoc = new XmlDocument();
        try
        {
            xmlDoc.Load(xmlFilePath);
            Debug.Log("ImageLoader: XML file loaded successfully.");
        }
        catch (System.Exception e)
        {
            Debug.LogError("ImageLoader: Error loading XML file: " + e.Message);
            return;
        }

        XmlNodeList imageNodes = xmlDoc.SelectNodes("Images/Image");
        if (imageNodes == null || imageNodes.Count == 0)
        {
            Debug.LogWarning("ImageLoader: No image nodes found in XML file.");
            return;
        }

        Debug.Log("ImageLoader: Found " + imageNodes.Count + " image nodes.");

        List<GameObject> loadedImages = new List<GameObject>();

        foreach (XmlNode imageNode in imageNodes)
        {
            string imageName = imageNode.Attributes["name"]?.Value;
            string imagePath = imageNode.Attributes["path"]?.Value;
            string dropdownItemName = imageNode.Attributes["dropdownItem"]?.Value;

            Debug.Log("ImageLoader: Processing image node with name: " + imageName + ", path: " + imagePath + ", dropdownItem: " + dropdownItemName);

            if (string.IsNullOrEmpty(imageName) || string.IsNullOrEmpty(imagePath) || string.IsNullOrEmpty(dropdownItemName))
            {
                Debug.LogWarning("ImageLoader: Invalid image node in XML file.");
                continue;
            }

            Sprite imageSprite = Resources.Load<Sprite>(imagePath);
            if (imageSprite != null)
            {
                GameObject imageInstance = Instantiate(clickableImagePrefab, imageContainer.transform);
                Image imageComponent = imageInstance.GetComponent<Image>();
                if (imageComponent != null)
                {
                    imageComponent.sprite = imageSprite;
                }

                ClickableImage clickableImage = imageInstance.GetComponent<ClickableImage>();
                if (clickableImage != null)
                {
                    clickableImage.dropdownItemName = dropdownItemName;
                }

                loadedImages.Add(imageInstance);
                Debug.Log("ImageLoader: Loaded image: " + imageName);
            }
            else
            {
                Debug.LogWarning("ImageLoader: Image not found at path: " + imagePath);
            }
        }

        // Vérifier les images chargées
        foreach (var image in loadedImages)
        {
            Debug.Log("ImageLoader: Image to display: " + image.name);
        }
    }

}
