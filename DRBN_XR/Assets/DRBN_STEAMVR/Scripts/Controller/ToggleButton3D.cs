using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ToggleButton3D : MonoBehaviour
{
    private bool isPressed = false;

    private Vector3 originalLocalPosition;
    private Vector3 targetPosition;

    private Vector3 originalScale;
    private Vector3 targetScale;

    public float pressDepth = 20f;
    public float moveSpeed = 10f;

    public float pressedScale = 0.9f;

    public Color normalColor = Color.white;
    public Color pressedColor = Color.green;
    private Color targetColor;

    private Image buttonImage;

    public List<GameObject> objectsToHide;
    public List<GameObject> objectsToShow;
    public List<GameObject> objectsToHideOnClose;

    void Start()
    {
        originalLocalPosition = transform.localPosition;
        targetPosition = originalLocalPosition;

        originalScale = transform.localScale;
        targetScale = originalScale;

        buttonImage = GetComponent<Image>();
        if (buttonImage != null)
        {
            buttonImage.color = normalColor;
            targetColor = normalColor;
        }
    }

    void Update()
    {
        // Animation position
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * moveSpeed);

        // Animation couleur
        if (buttonImage != null)
        {
            buttonImage.color = Color.Lerp(buttonImage.color, targetColor, Time.deltaTime * moveSpeed);
        }

        // Animation scale
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * moveSpeed);
    }

    public void ToggleButton()
    {
        isPressed = !isPressed;

        // Nouvelle position cible
        targetPosition = originalLocalPosition + (isPressed ? new Vector3(0, 0, pressDepth) : Vector3.zero);

        // Nouvelle couleur cible
        targetColor = isPressed ? pressedColor : normalColor;

        // Nouveau scale cible
        targetScale = isPressed ? originalScale * pressedScale : originalScale;
        
        ToggleObjects();
    }

    public void ToogleButtonOff()
    {
        isPressed = false;

        targetPosition = originalLocalPosition + (isPressed ? new Vector3(0, 0, pressDepth) : Vector3.zero);

        // Nouvelle couleur cible
        targetColor = isPressed ? pressedColor : normalColor;

        // Nouveau scale cible
        targetScale = isPressed ? originalScale * pressedScale : originalScale;

        ToggleObjects();
    }

    public void ToggleObjects()
    {
        foreach (GameObject obj in objectsToHide)
        {
            if (obj != null)
                obj.SetActive(false);
        }

        foreach (GameObject obj in objectsToShow)
        {
            if (obj != null)
                obj.SetActive(isPressed);
        }

        if(!isPressed){
            foreach (GameObject obj in objectsToHideOnClose)
            {
                if (obj != null)
                    obj.SetActive(false);
            }
        }
    }
}