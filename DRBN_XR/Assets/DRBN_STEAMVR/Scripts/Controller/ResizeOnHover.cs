using UnityEngine;
using UnityEngine.EventSystems;

public class ResizeOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector2 normalSize = new Vector2(200f, 200f);
    public Vector2 hoverSize = new Vector2(250f, 250f);
    public float resizeSpeed = 5f;

    private RectTransform rectTransform;
    private Vector2 targetSize;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        targetSize = normalSize;
        rectTransform.sizeDelta = normalSize;
    }

    void Update()
    {
        rectTransform.sizeDelta = Vector2.Lerp(rectTransform.sizeDelta, targetSize, Time.deltaTime * resizeSpeed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetSize = hoverSize;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetSize = normalSize;
    }
}
 