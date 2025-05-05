using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class HoverEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Behaviour outlineToEnable;
    public Shadow shadowToEnable;
    public Shadow shadowToDisable;
    public Toggle toggleToToggle;
    public TMP_Dropdown dropdownToShow;
    public Button buttonToPress;

    [Header("Shadow Animation")]
    public bool enableShadowAnimation = true;
    private float shadowAnimDuration = 0.05f;
    public Vector2 shadowStart = Vector2.zero;
    public Vector2 shadowEnd = new Vector2(2f, -2f);

    private bool isHovered = false;
    private Coroutine shadowAnimCoroutine;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;

        if (outlineToEnable != null)
            outlineToEnable.enabled = true;

        if (shadowToEnable != null)
        {
            shadowToEnable.enabled = true;

            if (enableShadowAnimation)
            {
                if (shadowAnimCoroutine != null)
                    StopCoroutine(shadowAnimCoroutine);

                shadowAnimCoroutine = StartCoroutine(AnimateShadow(shadowToEnable, shadowStart, shadowEnd, shadowAnimDuration));
            }
            else
            {
                shadowToEnable.effectDistance = shadowEnd;
            }
        }

        if (shadowToDisable != null)
            shadowToDisable.enabled = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;

        if (outlineToEnable != null)
            outlineToEnable.enabled = false;

        if (shadowToEnable != null)
        {
            shadowToEnable.enabled = false;

            if (shadowAnimCoroutine != null)
            {
                StopCoroutine(shadowAnimCoroutine);
                shadowAnimCoroutine = null;
            }
        }

        if (shadowToDisable != null)
            shadowToDisable.enabled = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (toggleToToggle != null)
            toggleToToggle.isOn = !toggleToToggle.isOn;

        if (dropdownToShow != null)
        {
            dropdownToShow.gameObject.SetActive(true);
            dropdownToShow.Show();
        }

        if (buttonToPress != null)
            buttonToPress.onClick.Invoke();
    }

    private IEnumerator AnimateShadow(Shadow shadow, Vector2 start, Vector2 end, float duration)
    {
        float time = 0f;

        while (time < duration)
        {
            if (!isHovered)
                yield break;

            float t = time / duration;
            shadow.effectDistance = Vector2.Lerp(start, end, t);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        shadow.effectDistance = end;
        shadowAnimCoroutine = null;
    }
}
