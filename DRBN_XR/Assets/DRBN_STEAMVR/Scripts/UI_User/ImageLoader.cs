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
    public DropdownSelection dropdownSelection; // Référence au script DropdownSelection

    private Dictionary<string, GameObject> imageToggleObjects = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();

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

        List<(Sprite image, string name, GameObject prefab)> loadedImages = new List<(Sprite, string, GameObject)>();

        foreach (XmlNode imageNode in imageNodes)
        {
            string imageName = imageNode.Attributes["name"]?.Value;
            string imagePath = imageNode.Attributes["path"]?.Value;
            string prefabPath = imageNode.Attributes["prefab"]?.Value;

            if (string.IsNullOrEmpty(imageName) || string.IsNullOrEmpty(imagePath) || string.IsNullOrEmpty(prefabPath))
            {
                Debug.LogWarning("ImageLoader: Invalid image node in XML file.");
                continue;
            }

            Sprite image = Resources.Load<Sprite>(imagePath);
            GameObject prefab = Resources.Load<GameObject>(prefabPath);

            if (image != null && prefab != null)
            {
                loadedImages.Add((image, imageName, prefab));
                prefabs[imageName] = prefab; // Stocker le prefab dans le dictionnaire
                Debug.Log("ImageLoader: Loaded image: " + imageName + " from path: " + imagePath);
            }
            else
            {
                Debug.LogWarning("ImageLoader: Image or prefab not found at path: " + imagePath + " or " + prefabPath);
            }
        }

        // Trier les images par nom
        loadedImages = loadedImages.OrderBy(image => image.name).ToList();
        Debug.Log("ImageLoader: Images sorted.");

        DisplayImages(loadedImages);
    }

    void DisplayImages(List<(Sprite image, string name, GameObject prefab)> images)
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
            gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            gridLayout.constraintCount = 3;
        }

        foreach (var (image, name, prefab) in images)
        {
            // Instancier le prefab
            GameObject instance = Instantiate(imageTextPrefab, imageContainer.transform);
            instance.name = name; // Définir le nom de l'objet instancié

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
                clickHandler.imageLoader = this; // Assigner la référence à ImageLoader
                Debug.Log("ImageLoader: Set click handler for: " + name);
            }
            else
            {
                Debug.LogWarning("ImageLoader: ImageClickHandler component not found in prefab instance.");
            }

            // Ajouter l'instance au dictionnaire pour suivre son état
            imageToggleObjects[name] = instance;

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

    // void Update()
    // {
    //     if (dropdown != null)
    //     {
    //         List<string> activeImageNames = new List<string>();

    //         foreach (var kvp in imageToggleObjects)
    //         {
    //             string imageName = kvp.Key;
    //             GameObject parentObject = kvp.Value;
    //             GameObject toggleObject = parentObject.transform.GetChild(0).gameObject; // Supposons que l'enfant est le premier enfant

    //             if (toggleObject != null && toggleObject.activeSelf)
    //             {
    //                 activeImageNames.Add(imageName);
    //             }
    //         }

    //         // Mettre à jour le Dropdown uniquement si les options actives ont changé
    //         List<string> currentDropdownOptions = dropdown.options.Select(option => option.text).ToList();
    //         if (!activeImageNames.SequenceEqual(currentDropdownOptions))
    //         {
    //             dropdown.ClearOptions();
    //             dropdown.AddOptions(activeImageNames.Select(name => new TMP_Dropdown.OptionData(name)).ToList());
    //             Debug.Log("ImageLoader: Updated dropdown options.");

    //             // Appeler ForceUpdateSpawnPrefab après la mise à jour du Dropdown
    //             if (dropdownSelection != null)
    //             {
    //                 dropdownSelection.ForceUpdateSpawnPrefab();
    //             }
    //             dropdownSelection.OnDropdownValueChanged(dropdown.value); // Mettre à jour la valeur sélectionnée

                
    //         }
    //     }
    // }

    public GameObject GetPrefabByName(string name)
    {
        if (prefabs.TryGetValue(name, out GameObject prefab))
        {
            return prefab;
        }
        return null;
    }

    public void RemoveOptionFromDropdown(string optionText)
    {
        TMP_Dropdown.OptionData option = dropdown.options.Find(o => string.Equals(o.text, optionText));
        if (option != null)
        {
            dropdown.options.Remove(option);
            dropdown.RefreshShownValue(); // Met à jour l'affichage du dropdown
            Debug.Log("ImageLoader: Removed option from dropdown: " + optionText);

            // Appeler ForceUpdateSpawnPrefab après la suppression de l'option
            if (dropdownSelection != null)
            {
                dropdownSelection.ForceUpdateSpawnPrefab();
            }
        }
        else
        {
            Debug.LogWarning("ImageLoader: Option not found in dropdown: " + optionText);
        }
    }


}
