using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class HoverActivate : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public List<GameObject> objectsToToggle;
    public float hoverTime = 2f;

    private bool isHovering = false;
    private float hoverTimer = 0f;
    private bool objectsEnabled = false;

    void Update()
    {
        if (isHovering)
        {
            hoverTimer += Time.deltaTime;
            if (hoverTimer >= hoverTime && !objectsEnabled)
            {
                EnableObjects();
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        hoverTimer = 0f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        hoverTimer = 0f;
        DisableObjects();
    }

    private void EnableObjects()
    {
        foreach (GameObject obj in objectsToToggle)
        {
            obj.SetActive(true);
        }
        objectsEnabled = true;
    }

    private void DisableObjects()
    {
        foreach (GameObject obj in objectsToToggle)
        {
            obj.SetActive(false);
        }
        objectsEnabled = false;
    }
}
