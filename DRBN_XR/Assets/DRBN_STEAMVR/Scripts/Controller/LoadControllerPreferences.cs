using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;
public class LoadControllerPreferences : MonoBehaviour
{
    public String preferenceKeyJoystick = "joystick";
    public String preferenceKeyTeleport = "teleportation";
    public String preferenceKeyTurn = "turn";

    public TeleportationProvider teleportationProvider;
    public XRRayInteractor rayInteractor;

    public SnapTurnProvider snapTurnProvider;
    public ContinuousTurnProvider continuousTurnProvider;

    public DynamicMoveProvider moveProvider;
    private int isJoystickEnabled;
    private int isTeleportEnabled;

    private int turnValue;

    private UnityEngine.XR.Interaction.Toolkit.InteractionLayerMask originalInteractionLayers;
    void Start()
    {
        originalInteractionLayers = rayInteractor.interactionLayers;
        
        LoadJoystickPreference();

        LoadTeleportPreference();

        LoadTurnPreference();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadTeleportPreference()
    {
        isTeleportEnabled = PlayerPrefs.GetInt(preferenceKeyTeleport, 1);
        Debug.Log("Loading teleport preference");
        if (isTeleportEnabled == 0)
        {
            teleportationProvider.enabled = false;
            rayInteractor.interactionLayers = 0;
        }
        else
        {
            teleportationProvider.enabled = true;
            rayInteractor.interactionLayers = originalInteractionLayers;
        }
    }

    private void LoadJoystickPreference()
    {
        isJoystickEnabled = PlayerPrefs.GetInt(preferenceKeyJoystick, 1);
        Debug.Log("Loading joystick preference : " + isJoystickEnabled);
        if (isJoystickEnabled == 0)
        {
            moveProvider.enabled = false;
        }
        else
        {
            moveProvider.enabled = true;
        }
        
    }

    private void LoadTurnPreference()
    {
        turnValue = PlayerPrefs.GetInt(preferenceKeyTurn, 0);
        Debug.Log("Loading turn preference: " + turnValue);
        switch (turnValue)
        {
            case 0: // cas continuous
                continuousTurnProvider.enabled = true;
                snapTurnProvider.enabled = false;
                break;
            case 1: // cas snap
                continuousTurnProvider.enabled = false;
                snapTurnProvider.enabled = true;
                break;
            default:
                // desactiver les deux
                continuousTurnProvider.enabled = false;
                snapTurnProvider.enabled = false;
                break;
        }
    }
}
