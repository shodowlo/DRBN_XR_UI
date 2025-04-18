using UnityEngine;

public class DashedLine : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float dashLength = 0.1f;
    public float gapLength = 0.05f;
    public Material dashMaterial;
    public float lineWidth = 0.01f;

    public GameObject targetObject;
    public float minY = 0f;
    public float maxY = 10f;

    private Vector3 lastStart;
    private Vector3 lastEnd;

    void Update()
    {
        float targetY = targetObject.transform.localPosition.y;

        // Vérifie si targetY est dans l'intervalle [minY, maxY]
        if (targetY >= minY && targetY <= maxY)
        {
            if (startPoint.position != lastStart || endPoint.position != lastEnd)
            {
                lastStart = startPoint.position;
                lastEnd = endPoint.position;
                Redraw();
            }
        }
        else
        {
            // Supprime les dashes si la condition n'est plus remplie
            if (transform.childCount > 0)
            {
                foreach (Transform child in transform)
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }   

    private void Redraw()
    {
        // Supprimer les anciens dashes
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        Vector3 direction = endPoint.position - startPoint.position;
        float distance = direction.magnitude;
        direction.Normalize();

        float currentLength = 0f;
        int index = 0;

        while (currentLength < distance)
        {
            float segmentLength = Mathf.Min(dashLength, distance - currentLength);

            Vector3 segmentStart = startPoint.position + direction * currentLength;
            Vector3 segmentEnd = segmentStart + direction * segmentLength;

            GameObject dash = new GameObject($"Dash_{index++}");
            dash.transform.parent = transform;

            LineRenderer lr = dash.AddComponent<LineRenderer>();
            lr.material = dashMaterial;
            lr.positionCount = 2;
            lr.SetPosition(0, segmentStart);
            lr.SetPosition(1, segmentEnd);
            lr.startWidth = lineWidth;
            lr.endWidth = lineWidth;
            lr.useWorldSpace = true;
            lr.alignment = LineAlignment.TransformZ;

            currentLength += dashLength + gapLength;
        }
    }
}
