using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class EnableUI : MonoBehaviour
{
    public GameObject CanvasUI;

    public InputActionReference activateUI;

    public UnityEvent onPressActivateUI;

    private void ShowUI()
    {
        CanvasUI.SetActive(true);
    }

    private void HideUI()
    {
        CanvasUI.SetActive(false);
    }

    private void EnableDisableUI() 
    {

    }

    // Start is called before the first frame update
    private void Start()
    {
        //activateUI.action.performed += EnableDisableUI;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
