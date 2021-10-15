using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class ImprovedTeleportController : MonoBehaviour
{
    public GameObject baseControllerGameObject;
    public GameObject teleportationGameObject;
    public GameObject triggerGameObject;

    public InputActionReference teleportActivationReference;
    public InputActionReference triggerActivationReference;

    public UnityEvent onTeleportActivate;
    public UnityEvent onTeleportCancel;
    public UnityEvent onTriggerActivate;
    public UnityEvent onTriggerCancel;

    private void Start()
    {
        teleportActivationReference.action.performed += TeleportModeActivate;
        teleportActivationReference.action.canceled += TeleportModeCancel;

        triggerActivationReference.action.performed += TriggerActivate;
        triggerActivationReference.action.canceled += TriggerCancel;
    }

    private void TeleportModeCancel(InputAction.CallbackContext obj) => Invoke("DeactivateTeleporter", .1f);

    private void TriggerCancel(InputAction.CallbackContext obj) => Invoke("DeactivateTrigger", .1f*100000f);

    void DeactivateTeleporter() => onTeleportCancel.Invoke();

    void DeactivateTrigger() => onTriggerCancel.Invoke();

    private void TeleportModeActivate(InputAction.CallbackContext obj) => onTeleportActivate.Invoke();

    private void TriggerActivate(InputAction.CallbackContext obj) => onTriggerActivate.Invoke();
}
