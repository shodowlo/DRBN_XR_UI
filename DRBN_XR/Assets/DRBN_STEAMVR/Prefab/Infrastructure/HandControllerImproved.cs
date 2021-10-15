using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandControllerImproved : MonoBehaviour
{
    [SerializeField] InputActionReference controllerActionGrip;
    [SerializeField] InputActionReference controllerActionTrigger;

    private Animator _handAnimator;

    // Start is called before the first frame update
    private void Awake()
    {
        controllerActionGrip.action.performed += GripPress;
        //controllerActionTrigger.action.performed += TriggerPress;

        _handAnimator = GetComponent<Animator>();
    }

    private void TriggerPress(InputAction.CallbackContext obj)
    {
        //_handAnimator.SetFloat("Trigger",obj.ReadValue<float>());
    }

    private void GripPress(InputAction.CallbackContext obj)
    {
        _handAnimator.SetFloat("Grip", obj.ReadValue<float>());
    }
}
