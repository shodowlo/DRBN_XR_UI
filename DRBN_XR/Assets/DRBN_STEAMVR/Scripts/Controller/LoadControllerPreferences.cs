using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;
using UnityEngine.UI;
public class LoadControllerPreferences : MonoBehaviour
{
    public String preferenceKeyJoystick = "joystick";
    public String preferenceKeyTeleport = "teleportation";
    public String preferenceKeyTurn = "turn";

    public String preferenceKeyFly = "fly";

    public TeleportationProvider teleportationProvider;
    public XRRayInteractor rayInteractor;

    public SnapTurnProvider snapTurnProvider;
    public ContinuousTurnProvider continuousTurnProvider;

    public DynamicMoveProvider moveProvider;
    private int isJoystickEnabled;
    private int isTeleportEnabled;

    private int turnValue;
    private int isFlyEnabled;

    [Header("Pour le mode droitier")]
    public GameObject LeftControllerVisual;
    public GameObject RightControllerVisual;
    public GameObject LeftControllerVisualUniversalController;
    public GameObject RightControllerVisualUniversalController;
    public GameObject CanvasButtonOnController;
    public GameObject ControllerSettings;
    public GameObject ControllerHelpMenu;
    public GameObject ControllerSpawnMenu;
    public GameObject Clock;
    public GameObject TextHoverController;
    public GameObject Plus;
    public GameObject Minus;


    private UnityEngine.XR.Interaction.Toolkit.InteractionLayerMask originalInteractionLayers;
    void Start()
    {
        originalInteractionLayers = rayInteractor.interactionLayers;
        
        LoadJoystickPreference();

        LoadTeleportPreference();

        LoadTurnPreference();

        LoadFlyPreference();
        
        //LoadLeftPreference();
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

    private void LoadFlyPreference()
    {
        isFlyEnabled = PlayerPrefs.GetInt(preferenceKeyFly, 0);
        Debug.Log("Loading fly preference: " + isFlyEnabled);
        if (isFlyEnabled == 1)
        {
            moveProvider.enableFly = true;
        }
        else
        {
            moveProvider.enableFly = false;
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

    private void LoadLeftPreference()
    {
        Vector3 newPosition;

        CanvasButtonOnController.transform.SetParent(RightControllerVisual.transform);

        ControllerSettings.transform.SetParent(RightControllerVisual.transform);
        newPosition = ControllerSettings.transform.localPosition;
        newPosition.x = -0.5f;
        // Ajuster la position de ControllerSettings pour qu'il soit à gauche du contrôleur
        ControllerSettings.transform.localPosition = newPosition;

        ControllerHelpMenu.transform.SetParent(RightControllerVisual.transform);
        newPosition = ControllerHelpMenu.transform.localPosition;
        newPosition.x = -0.5f;
        ControllerHelpMenu.transform.localPosition = newPosition;

        ControllerSpawnMenu.transform.SetParent(RightControllerVisual.transform);
        newPosition = ControllerSpawnMenu.transform.localPosition;
        newPosition.x = -0.5f;
        ControllerSpawnMenu.transform.localPosition = newPosition;

        Clock.transform.SetParent(RightControllerVisual.transform);
        TextHoverController.transform.SetParent(RightControllerVisual.transform);

        Minus.transform.SetParent(LeftControllerVisualUniversalController.transform);
        Plus.transform.SetParent(LeftControllerVisualUniversalController.transform);
    }
}
