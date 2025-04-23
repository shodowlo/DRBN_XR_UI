using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ToggleMenuAnimation : MonoBehaviour
{
    public Transform startTransform;           // Point de sortie (ex: contrôleur)
    public Transform endTransform;             // Position finale du menu
    public GameObject menuCanvas;              // Le canvas à animer
    public float animationDuration = 0.5f;     // Durée de l'animation

    public List<GameObject> objectsToHideOnClose;
    public MonoBehaviour[] scriptsToDeactivateOnClose;
    public MonoBehaviour[] scriptsToActivateOnOpen;

    private bool isAnimating = false;
    private bool isMenuOpen = false;

    public void ToggleMenu()
    {
        if (isMenuOpen)
            CloseMenu();
        else
            ShowMenu();
    }

    public void ShowMenu()
    {
        if (isAnimating || isMenuOpen || startTransform == null || endTransform == null || menuCanvas == null)
            return;

        menuCanvas.SetActive(true);
        menuCanvas.transform.position = startTransform.position;
        menuCanvas.transform.rotation = startTransform.rotation;
        menuCanvas.transform.localScale = Vector3.one * 0.1f;

        Togglescripts(scriptsToActivateOnOpen, true);
        StartCoroutine(AnimateMenu(endTransform, true));
    }

    public void CloseMenu()
    {
        if (isAnimating || !isMenuOpen || startTransform == null || menuCanvas == null)
            return;

        StartCoroutine(AnimateMenu(startTransform, false));
    }

    private System.Collections.IEnumerator AnimateMenu(Transform target, bool opening)
    {
        isAnimating = true;
        float elapsed = 0f;

        Vector3 startPos = menuCanvas.transform.position;
        Quaternion startRot = menuCanvas.transform.rotation;
        Vector3 startScale = menuCanvas.transform.localScale;

        Vector3 targetPos = target.position;
        Quaternion targetRot = target.rotation;
        Vector3 targetScale = opening ? target.localScale : Vector3.one * 0.1f;

        while (elapsed < animationDuration)
        {
            float t = elapsed / animationDuration;

            menuCanvas.transform.position = Vector3.Lerp(startPos, targetPos, t);
            menuCanvas.transform.rotation = Quaternion.Slerp(startRot, targetRot, t);
            menuCanvas.transform.localScale = Vector3.Lerp(startScale, targetScale, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        menuCanvas.transform.position = targetPos;
        menuCanvas.transform.rotation = targetRot;
        menuCanvas.transform.localScale = targetScale;

        isAnimating = false;
        isMenuOpen = opening;

        if (!opening){
            menuCanvas.SetActive(false);
            foreach (GameObject obj in objectsToHideOnClose)
            {
                if (obj != null)
                    obj.SetActive(false);
            }
            Togglescripts(scriptsToDeactivateOnClose, false);
        }
    }

    void Togglescripts(MonoBehaviour[] scripts, bool enable)
    {
        if (scripts != null)
        {
            foreach (var script in scripts)
            {
                if (script != null)
                {
                    script.enabled = enable;
                }
            }
        }
    }
}
