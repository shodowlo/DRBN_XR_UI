// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;
// using System.Xml;
// using System.Collections.Generic;
// using System.Linq;

// public class ImageLoader : MonoBehaviour
// {
//     public string xmlFilePath; //= "XML/PrefabList"; // Chemin vers votre fichier XML dans le dossier Resources
//     public GameObject imageContainer; // Conteneur pour les images
//     public GameObject imageTextPrefab; // Prefab pour l'image et le texte

//     void Start()
//     {
//         LoadImagesFromXML();
//     }

//     void LoadImagesFromXML()
//     {
//         TextAsset xmlFile = Resources.Load<TextAsset>(xmlFilePath);
//         if (xmlFile == null)
//         {
//             Debug.LogError("ImageLoader: XML file not found at path: " + xmlFilePath);
//             return;
//         }

//         XmlDocument xmlDoc = new XmlDocument();
//         try
//         {
//             xmlDoc.LoadXml(xmlFile.text);
//         }
//         catch (System.Exception e)
//         {
//             Debug.LogError("ImageLoader: Error loading XML file: " + e.Message);
//             return;
//         }

//         XmlNodeList imageNodes = xmlDoc.SelectNodes("Images/Image");
//         if (imageNodes == null || imageNodes.Count == 0)
//         {
//             Debug.LogWarning("ImageLoader: No image nodes found in XML file.");
//             return;
//         }


//         List<(Sprite image, string name)> loadedImages = new List<(Sprite, string)>();
        
//         foreach (XmlNode imageNode in imageNodes)
//         {
//             string imageName = imageNode.Attributes["name"]?.Value;
//             string imagePath = imageNode.Attributes["path"]?.Value;

//             if (string.IsNullOrEmpty(imageName) || string.IsNullOrEmpty(imagePath))
//             {
//                 Debug.LogWarning("ImageLoader: Invalid image node in XML file.");
//                 continue;
//             }

//             Sprite image = Resources.Load<Sprite>(imagePath);
//             if (image != null)
//             {
//                 loadedImages.Add((image, imageName));
//             }
//             else
//             {
//                 Debug.LogWarning("ImageLoader: Image not found at path: " + imagePath);
//             }
//         }

//         // Trier les images par nom
//         loadedImages = loadedImages.OrderBy(image => image.name).ToList();

//         DisplayImages(loadedImages);
//     }

//     void DisplayImages(List<(Sprite image, string name)> images)
//     {
//         if (images.Count == 0)
//         {
//             Debug.LogWarning("ImageLoader: No images loaded.");
//             return;
//         }

//         GridLayoutGroup gridLayout = imageContainer.GetComponent<GridLayoutGroup>();
//         if (gridLayout == null)
//         {
//             gridLayout = imageContainer.AddComponent<GridLayoutGroup>();
//         }

//         gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
//         gridLayout.constraintCount = 4;

//         foreach (var (image, name) in images)
//         {
//             // Instancier le prefab
//             GameObject instance = Instantiate(imageTextPrefab, imageContainer.transform);
//             Image imageComponent = instance.GetComponent<Image>();
//             TextMeshProUGUI textComponent = instance.GetComponentInChildren<TextMeshProUGUI>();

//             if (imageComponent != null)
//             {
//                 imageComponent.sprite = image;
//                 imageComponent.preserveAspect = true; // Pour conserver le ratio d'aspect de l'image
//             }
//             else
//             {
//                 Debug.LogWarning("ImageLoader: Image component not found in prefab instance.");
//             }

//             if (textComponent != null)
//             {
//                 textComponent.text = name; // Afficher le nom de l'image dans le texte
//             }
//             else
//             {
//                 Debug.LogWarning("ImageLoader: TextMeshProUGUI component not found in prefab instance.");
//             }
//         }

//         // Ajoutez une ScrollRect si nécessaire
//         ScrollRect scrollRect = imageContainer.GetComponentInParent<ScrollRect>();
//         if (scrollRect == null)
//         {
//             scrollRect = imageContainer.AddComponent<ScrollRect>();
//             scrollRect.content = imageContainer.GetComponent<RectTransform>();
//             scrollRect.viewport = imageContainer.transform.parent.GetComponent<RectTransform>();
//             scrollRect.horizontal = false;
//             scrollRect.vertical = true;
//             scrollRect.movementType = ScrollRect.MovementType.Clamped;
//             scrollRect.inertia = false;
//         }
//     }
// }

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Xml;
using System.Collections.Generic;
using System.Linq;

public class ImageLoader : MonoBehaviour
{
    public string xmlFilePath = "XML/PrefabList"; // Chemin vers votre fichier XML dans le dossier Resources
    public GameObject imageContainer; // Conteneur pour les images
    public GameObject imageTextPrefab; // Prefab pour l'image et le texte
    public TMP_Dropdown dropdown; // Référence au DropdownTMP

    void Start()
    {
        Debug.Log("ImageLoader: Start method called.");
        LoadImagesFromXML();
    }

    void LoadImagesFromXML()
    {
        Debug.Log("ImageLoader: LoadImagesFromXML method called.");
        TextAsset xmlFile = Resources.Load<TextAsset>(xmlFilePath);
        if (xmlFile == null)
        {
            Debug.LogError("ImageLoader: XML file not found at path: " + xmlFilePath);
            return;
        }

        XmlDocument xmlDoc = new XmlDocument();
        try
        {
            Debug.Log("ImageLoader: Attempting to load XML file from path: " + xmlFilePath);
            xmlDoc.LoadXml(xmlFile.text);
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

        List<(Sprite image, string name)> loadedImages = new List<(Sprite, string)>();

        foreach (XmlNode imageNode in imageNodes)
        {
            string imageName = imageNode.Attributes["name"]?.Value;
            string imagePath = imageNode.Attributes["path"]?.Value;

            if (string.IsNullOrEmpty(imageName) || string.IsNullOrEmpty(imagePath))
            {
                Debug.LogWarning("ImageLoader: Invalid image node in XML file.");
                continue;
            }

            Sprite image = Resources.Load<Sprite>(imagePath);
            if (image != null)
            {
                loadedImages.Add((image, imageName));
                Debug.Log("ImageLoader: Loaded image: " + imageName + " from path: " + imagePath);
            }
            else
            {
                Debug.LogWarning("ImageLoader: Image not found at path: " + imagePath);
            }
        }

        // Trier les images par nom
        loadedImages = loadedImages.OrderBy(image => image.name).ToList();
        Debug.Log("ImageLoader: Images sorted.");

        DisplayImages(loadedImages);
    }

    void DisplayImages(List<(Sprite image, string name)> images)
    {
        Debug.Log("ImageLoader: DisplayImages method called.");
        if (images.Count == 0)
        {
            Debug.LogWarning("ImageLoader: No images loaded.");
            return;
        }

        GridLayoutGroup gridLayout = imageContainer.GetComponent<GridLayoutGroup>();
        if (gridLayout == null)
        {
            gridLayout = imageContainer.AddComponent<GridLayoutGroup>();
        }

        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.constraintCount = 4;

        foreach (var (image, name) in images)
        {
            // Instancier le prefab
            GameObject instance = Instantiate(imageTextPrefab, imageContainer.transform);
            Image imageComponent = instance.GetComponent<Image>();
            TextMeshProUGUI textComponent = instance.GetComponentInChildren<TextMeshProUGUI>();
            ImageClickHandler clickHandler = instance.GetComponent<ImageClickHandler>();

            if (imageComponent != null)
            {
                imageComponent.sprite = image;
                imageComponent.preserveAspect = true; // Pour conserver le ratio d'aspect de l'image
                Debug.Log("ImageLoader: Set image sprite for: " + name);
            }
            else
            {
                Debug.LogWarning("ImageLoader: Image component not found in prefab instance.");
            }

            if (textComponent != null)
            {
                textComponent.text = name; // Afficher le nom de l'image dans le texte
                Debug.Log("ImageLoader: Set text for: " + name);
            }
            else
            {
                Debug.LogWarning("ImageLoader: TextMeshProUGUI component not found in prefab instance.");
            }

            if (clickHandler != null)
            {
                clickHandler.imageName = name; // Assigner le nom de l'image
                clickHandler.dropdown = dropdown; // Assigner la référence au DropdownTMP
                Debug.Log("ImageLoader: Set click handler for: " + name);
            }
            else
            {
                Debug.LogWarning("ImageLoader: ImageClickHandler component not found in prefab instance.");
            }

            Debug.Log("ImageLoader: Displayed image: " + name);
        }

        // Ajoutez une ScrollRect si nécessaire
        ScrollRect scrollRect = imageContainer.GetComponentInParent<ScrollRect>();
        if (scrollRect == null)
        {
            scrollRect = imageContainer.AddComponent<ScrollRect>();
            scrollRect.content = imageContainer.GetComponent<RectTransform>();
            scrollRect.viewport = imageContainer.transform.parent.GetComponent<RectTransform>();
            scrollRect.horizontal = false;
            scrollRect.vertical = true;
            scrollRect.movementType = ScrollRect.MovementType.Clamped;
            scrollRect.inertia = false;
            Debug.Log("ImageLoader: ScrollRect added.");
        }
    }
}
