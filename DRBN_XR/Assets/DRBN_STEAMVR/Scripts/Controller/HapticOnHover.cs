using UnityEngine;
using UnityEngine.XR;
using UnityEngine.EventSystems;


public class HapticOnHover : MonoBehaviour, IPointerEnterHandler
{
    public InputDeviceCharacteristics characteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
    public float amplitude = 0.5f;
    public float duration = 0.1f;

    public void OnPointerEnter(PointerEventData eventData)
    {
        var devices = new System.Collections.Generic.List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(characteristics, devices);

        foreach (var device in devices)
        {
            if (device.TryGetHapticCapabilities(out var capabilities) && capabilities.supportsImpulse)
            {
                device.SendHapticImpulse(0u, amplitude, duration);
            }
        }
    }
}
