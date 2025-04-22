using UnityEngine;
using UnityEngine.UI;

public class MoleculeSnapshot : MonoBehaviour
{
    public GameObject moleculePrefab;
    public RawImage targetImage;
    public Vector3 previewPosition = new Vector3(1000, 0, 1000);
    public Vector3 cameraOffset = new Vector3(0, 0, -5);
    public int cameraRotationY = 0;
    public float zoomDistance = 5f;

    public bool debugDontDestroy = false;

    public int textureSize = 1024;

    void Start()
    {
        RenderTexture renderTexture = new RenderTexture(textureSize, textureSize, 16);
        renderTexture.Create();

        GameObject camObj = new GameObject("SnapshotCamera");
        Camera cam = camObj.AddComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = new Color(0, 0, 0, 0);
        cam.targetTexture = renderTexture;
        cam.nearClipPlane = 0.1f;
        cam.farClipPlane = 1000f;

        GameObject molecule = Instantiate(moleculePrefab);
        molecule.transform.position = previewPosition;

        cam.transform.position = previewPosition + cameraOffset;
        cam.transform.LookAt(molecule.transform);

        cam.transform.position += cam.transform.forward * zoomDistance;

        cam.transform.Rotate(0, cameraRotationY, 0);

        cam.Render();

        RenderTexture.active = renderTexture;
        Texture2D snapshot = new Texture2D(textureSize, textureSize, TextureFormat.RGBA32, false);
        snapshot.ReadPixels(new Rect(0, 0, textureSize, textureSize), 0, 0);
        snapshot.Apply();
        RenderTexture.active = null;

        targetImage.texture = snapshot;

        if(!debugDontDestroy)
        {
            DestroyImmediate(molecule);
            DestroyImmediate(camObj);
            renderTexture.Release();
            DestroyImmediate(renderTexture);
        }
    }
}
