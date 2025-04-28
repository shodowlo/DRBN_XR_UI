using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class CircularMenu : MonoBehaviour
{
    [System.Serializable]
    public class Button3D
    {
        public GameObject button;
        public MenuController menuController;
        public GameObject[] objectsToHide;
        public GameObject[] objectsToShow;
        [HideInInspector] public bool isPressed = false;
        public Color normalColor = Color.white;
        public Color pressedColor = Color.green;
        // Pour l'animation
        [HideInInspector] public Color targetColor;
        [HideInInspector] public Vector3 originalLocalPosition;
        [HideInInspector] public Vector3 targetPosition;
        [HideInInspector] public Vector3 originalScale;
        [HideInInspector] public Vector3 targetScale;
        [HideInInspector] public Image buttonImage;
        [HideInInspector] public bool isAnimating = false;

        // Pour le Hover (texte qui apparait au survol)
        [HideInInspector] public bool isHovering = false;
        [HideInInspector] public float hoverTimer = 0f;
        public List<GameObject> hoverObjectsToToggle = new List<GameObject>();
        [HideInInspector] public bool hoverObjectsEnabled = false;
        [HideInInspector] public bool isHoverForcedEnabled = false;
        public bool isHelpButton = false;

        public GameObject closeButton; //facultafif, close button menu controller
    }

    public float hoverTime = 1f;
    public Button3D[] button3D; // Array of 3D buttons
    //animation du bouton
    public float pressDepth = 20f;
    public float moveSpeed = 10f;
    public float pressedScale = 0.9f;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        for (int i = 0; i < button3D.Length; i++)
        {
            int index = i;
            button3D[i].button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => toggleButton(index));

            if (button3D[i].closeButton != null)
            {
                button3D[i].closeButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => toggleButton(index));
            }

            button3D[i].originalLocalPosition = button3D[i].button.transform.localPosition;
            button3D[i].targetPosition = button3D[i].originalLocalPosition;

            button3D[i].originalScale = button3D[i].button.transform.localScale;
            button3D[i].targetScale = button3D[i].originalScale;

            button3D[i].buttonImage = button3D[i].button.GetComponent<Image>();
            if (button3D[i].buttonImage != null)
            {
                button3D[i].buttonImage.color = button3D[i].normalColor;
                button3D[i].targetColor = button3D[i].normalColor;
            }

            // Ajout automatique de EventTrigger pour gérer le survol
            AddHoverEvents(button3D[i], index);
        }
    }

    private void AddHoverEvents(Button3D btn, int index)
    {
        EventTrigger trigger = btn.button.GetComponent<EventTrigger>();
        if (trigger == null)
            trigger = btn.button.AddComponent<EventTrigger>();

        // OnPointerEnter
        EventTrigger.Entry entryEnter = new EventTrigger.Entry();
        entryEnter.eventID = EventTriggerType.PointerEnter;
        entryEnter.callback.AddListener((eventData) => { OnHoverEnter(index); });
        trigger.triggers.Add(entryEnter);

        // OnPointerExit
        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((eventData) => { OnHoverExit(index); });
        trigger.triggers.Add(entryExit);
    }


    void Update()
    {
        for (int i = 0; i < button3D.Length; i++)
        {
            if (button3D[i].isHovering)
            {
                button3D[i].hoverTimer += Time.deltaTime;
                if (button3D[i].hoverTimer >= hoverTime && !button3D[i].hoverObjectsEnabled)
                {
                    EnableHoverObjects(i);
                }
            }

            if (!button3D[i].isAnimating)
                continue;

            button3D[i].button.transform.localPosition = Vector3.Lerp(button3D[i].button.transform.localPosition, button3D[i].targetPosition, Time.deltaTime * moveSpeed);
            button3D[i].button.transform.localScale = Vector3.Lerp(button3D[i].button.transform.localScale, button3D[i].targetScale, Time.deltaTime * moveSpeed);

            if (button3D[i].buttonImage != null)
            {
                button3D[i].buttonImage.color = Color.Lerp(button3D[i].buttonImage.color, button3D[i].targetColor, Time.deltaTime * moveSpeed);
            }

            if (Vector3.Distance(button3D[i].button.transform.localPosition, button3D[i].targetPosition) < 0.01f &&
                Vector3.Distance(button3D[i].button.transform.localScale, button3D[i].targetScale) < 0.01f)
            {
                button3D[i].isAnimating = false;
            }
        }
    }

    private void OnHoverEnter(int index)
    {
        button3D[index].isHovering = true;
        button3D[index].hoverTimer = 0f;
    }

    private void OnHoverExit(int index)
    {
        button3D[index].isHovering = false;
        button3D[index].hoverTimer = 0f;
        DisableHoverObjects(index);
    }

    private void EnableHoverObjects(int index)
    {
        foreach (GameObject obj in button3D[index].hoverObjectsToToggle)
        {
            obj.SetActive(true);
        }
        button3D[index].hoverObjectsEnabled = true;
    }

    private void DisableHoverObjects(int index)
    {
        if (button3D[index].isHoverForcedEnabled)
            return;
        foreach (GameObject obj in button3D[index].hoverObjectsToToggle)
        {
            obj.SetActive(false);
        }
        button3D[index].hoverObjectsEnabled = false;
    }

    // Optionnel: méthode publique pour forcer l'activation/désactivation (comme HoverActivate)
    public void ToggleForcedEnabled(int index)
    {
        button3D[index].isHoverForcedEnabled = !button3D[index].isHoverForcedEnabled;
    }
    public void ToggleForcedEnabled()
    {
        for (int i = 0; i < button3D.Length; i++)
        {
            if (button3D[i].isHoverForcedEnabled == true)
            {
                button3D[i].isHoverForcedEnabled = false;
                DisableHoverObjects(i);
            }
            else{
                button3D[i].isHoverForcedEnabled = true;
                EnableHoverObjects(i);
            }

        }
    }

    public void DisableForcedEnabled(int index)
    {
        button3D[index].isHoverForcedEnabled = false;
    }

    public void toggleButton(int index)
    {
        if (index < 0 || index >= button3D.Length)
            return;

        //ne rien faire si un bouton est en train d'animer
        for (int i = 0; i < button3D.Length; i++)
        {
            if (button3D[i].isAnimating == true)
                return;
        }

        Debug.Log("Toggle button " + index);
        if(button3D[index].isHelpButton == true)
        {
            ToggleForcedEnabled();
        }


        // Désactiver les autres boutons
        for (int i = 0; i < button3D.Length; i++)
        {
            if (button3D[i].isPressed == true && i != index)
            {
                Debug.Log("Toggle button " + i + " off");
                toggleButton(i);
            }
        }

        if (button3D[index].objectsToShow != null && button3D[index].isPressed == false)
        {
            foreach (GameObject obj in button3D[index].objectsToShow)
            {
                obj.SetActive(true);
            }
        }
        else if (button3D[index].objectsToHide != null && button3D[index].isPressed == true)
        {
            foreach (GameObject obj in button3D[index].objectsToHide)
            {
                obj.SetActive(false);
            }
        }
        if (button3D[index].menuController != null)
        {
            button3D[index].menuController.ToggleMenu();
        }

        button3D[index].isPressed = !button3D[index].isPressed;
        button3D[index].targetPosition = button3D[index].originalLocalPosition + (button3D[index].isPressed ? new Vector3(0, 0, pressDepth) : Vector3.zero);
        button3D[index].targetColor = button3D[index].isPressed ? button3D[index].pressedColor : button3D[index].normalColor;
        button3D[index].targetScale = button3D[index].isPressed ? button3D[index].originalScale * pressedScale : button3D[index].originalScale;

        button3D[index].isAnimating = true; // Démarrer l'animation pour ce bouton
    }
}
