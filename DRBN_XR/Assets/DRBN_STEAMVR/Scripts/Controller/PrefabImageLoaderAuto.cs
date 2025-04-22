using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.IO;
using UnityEditor;

public class PrefabImageLoaderAuto : MonoBehaviour
{
    public Vector3 previewPosition = new Vector3(1000, 0, 1000);
    public int textureSize = 1024;
    public bool debugDontDestroy = false;
    private string xmlFilePath = "Resources/XML/PrefabList.xml"; // Chemin relatif au répertoire Assets
    private string imagesFolderPath = "Resources/Images/Molecules"; // Dossier fixe pour enregistrer les images

    void Start()
    {
        // Construire le chemin complet vers le fichier XML
        string fullXmlPath = Path.Combine(Application.dataPath, xmlFilePath);

        // Vérifier si le fichier XML existe
        if (!File.Exists(fullXmlPath))
        {
            Debug.LogError("Fichier XML non trouvé : " + fullXmlPath);
            return;
        }

        // Charger le fichier XML
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(fullXmlPath);

        // Parcourir chaque élément <Image> dans le fichier XML
        XmlNodeList imageNodes = xmlDoc.SelectNodes("//Image");
        foreach (XmlNode imageNode in imageNodes)
        {
            string imageName = imageNode.Attributes["name"]?.Value;
            string prefabPath = imageNode.Attributes["prefab"]?.Value;

            if (string.IsNullOrEmpty(imageName) || string.IsNullOrEmpty(prefabPath))
            {
                Debug.LogError("Attributs manquants dans le fichier XML pour l'image.");
                continue;
            }

            string fullPath = Path.Combine(Application.dataPath, imagesFolderPath, imageName + ".png");

            // Vérifier si l'image existe déjà
            if (File.Exists(fullPath))
            {
                Debug.Log("Image déjà existante : " + fullPath);
                continue;
            }

            // Charger le préfabriqué dynamiquement
            GameObject moleculePrefab = Resources.Load<GameObject>(prefabPath);
            if (moleculePrefab == null)
            {
                Debug.LogError("Préfabriqué non trouvé : " + prefabPath);
                continue;
            }

            // Si l'image n'existe pas, capturer et enregistrer l'image
            CaptureAndSaveImage(moleculePrefab, imageName, fullPath);
        }

        // Re-importer les assets pour appliquer les modifications
        AssetDatabase.Refresh();
    }

    void CaptureAndSaveImage(GameObject moleculePrefab, string imageName, string fullPath)
    {
        // RenderTexture
        RenderTexture renderTexture = new RenderTexture(textureSize, textureSize, 16);
        renderTexture.Create();

        // Instancier la molécule
        GameObject molecule = Instantiate(moleculePrefab);
        molecule.transform.position = previewPosition;

        // Calcul des bounds globaux autour de la molécule
        Bounds bounds = CalculateBounds(molecule);
        Vector3 center = bounds.center;
        float maxSize = Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z);

        // Créer la caméra
        GameObject camObj = new GameObject("SnapshotCamera");
        Camera cam = camObj.AddComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = new Color(0, 0, 0, 0);
        cam.targetTexture = renderTexture;
        cam.nearClipPlane = 0.01f;
        cam.farClipPlane = 1000f;

        // Direction latérale et légère hauteur pour la vue
        Vector3 viewDirection = new Vector3(1, 0.2f, 1).normalized;
        float distance = maxSize * 1.5f;

        // Position de la caméra en fonction du centre calculé
        cam.transform.position = center + viewDirection * distance;
        cam.transform.LookAt(center);

        // Rendu
        cam.Render();

        // Capture l'image
        RenderTexture.active = renderTexture;
        Texture2D snapshot = new Texture2D(textureSize, textureSize, TextureFormat.RGBA32, false);
        snapshot.ReadPixels(new Rect(0, 0, textureSize, textureSize), 0, 0);
        snapshot.Apply();
        RenderTexture.active = null;

        // Nettoyage
        if (!debugDontDestroy)
        {
            DestroyImmediate(molecule);
            DestroyImmediate(camObj);
            renderTexture.Release();
            DestroyImmediate(renderTexture);
        }

        // Convertir la texture en bytes PNG
        byte[] pngData = snapshot.EncodeToPNG();

        // Créer le dossier s'il n'existe pas
        Directory.CreateDirectory(Path.Combine(Application.dataPath, imagesFolderPath));

        // Écrire le fichier
        File.WriteAllBytes(fullPath, pngData);

        // Forcer la mise à jour de la base de données des assets
        AssetDatabase.Refresh();

        // Configurer les paramètres d'importation de la texture
        ConfigureTextureImportSettings(fullPath);

        // Créer un sprite à partir de la texture
        Sprite sprite = CreateSpriteFromTexture(snapshot, imageName);

        // Message console
        Debug.Log("Snapshot sauvegardé : " + fullPath);
        Debug.Log("Sprite créé : " + sprite.name);
    }

    void ConfigureTextureImportSettings(string fullPath)
    {
        // Obtenir le chemin relatif par rapport au répertoire Assets
        string assetPath = Path.GetRelativePath(Application.dataPath, fullPath).Replace("\\", "/");
        assetPath = "Assets/" + assetPath;

        // Charger l'importer de texture
        TextureImporter textureImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;

        if (textureImporter != null)
        {
            // Définir le type de texture sur Sprite (2D and UI)
            textureImporter.textureType = TextureImporterType.Sprite;

            // Configurer les paramètres du sprite
            var textureImporterSettings = new TextureImporterSettings();
            textureImporter.ReadTextureSettings(textureImporterSettings);
            textureImporterSettings.spriteMeshType = SpriteMeshType.FullRect;
            textureImporterSettings.spriteMode = (int)SpriteImportMode.Single;
            textureImporter.SetTextureSettings(textureImporterSettings);

            // Appliquer les modifications
            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
        }
        else
        {
            Debug.LogError("TextureImporter non trouvé pour : " + assetPath);
        }
    }

    Sprite CreateSpriteFromTexture(Texture2D texture, string name)
    {
        // Créer un rectangle pour le sprite
        Rect rect = new Rect(0, 0, texture.width, texture.height);

        // Créer le sprite avec les paramètres spécifiés
        Sprite sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f), 100.0f, 0, SpriteMeshType.FullRect, Vector4.zero, false);
        sprite.name = name;

        return sprite;
    }

    // Calcul des bounds globaux (inclut tous les enfants avec un renderer)
    private Bounds CalculateBounds(GameObject obj)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0)
            return new Bounds(obj.transform.position, Vector3.zero);

        Bounds bounds = renderers[0].bounds;
        foreach (Renderer r in renderers)
        {
            bounds.Encapsulate(r.bounds);
        }
        return bounds;
    }
}
