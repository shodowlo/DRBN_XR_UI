using UnityEngine;
using UnityEngine.UI;

public class PrefabImageLoaderAuto : MonoBehaviour
{
    public GameObject moleculePrefab;
    public RawImage targetImage;
    public Vector3 previewPosition = new Vector3(1000, 0, 1000);
    public int textureSize = 1024;
    public bool debugDontDestroy = false;

    void Start()
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

        // Affichage
        targetImage.texture = snapshot;

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

        // Créer un chemin de fichier (ici dans le dossier du projet, dans "Snapshots")
        string folderPath = Application.dataPath + "/Snapshots";
        System.IO.Directory.CreateDirectory(folderPath); // Crée le dossier s'il n'existe pas
        string filePath = folderPath + "/molecule_snapshot.png";

        // Écrire le fichier
        System.IO.File.WriteAllBytes(filePath, pngData);

        // Message console
        Debug.Log("Snapshot sauvegardé : " + filePath);
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
