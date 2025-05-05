using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


public class DestroyOnSelect : MonoBehaviour
{
    private XRBaseInteractable interactable;

    private void Awake()
    {
        interactable = GetComponent<XRBaseInteractable>();
        if (interactable != null)
        {
            interactable.selectEntered.AddListener(OnSelect);
            interactable.activated.AddListener(OnActivated);
        }
    }

    private void OnDestroy()
    {
        if (interactable != null)
        {
            interactable.selectEntered.RemoveListener(OnSelect);
        }
    }

    private void OnSelect(SelectEnterEventArgs args)
    {
        Debug.Log("Selected object: " + gameObject.name);
        Destroy(gameObject);
    }

    private void OnActivated(ActivateEventArgs args)
    {
        Debug.Log("Activated object: " + gameObject.name);
        // Logique de destruction ici si nécessaire
        Destroy(gameObject);
    }
}

