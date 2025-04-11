using UnityEngine;
using UnityEngine.UI;

public class togglePanel : MonoBehaviour
{
    public Canvas canvas;

    public void ToggleCanva()
    {
        if (canvas != null)
        {
            canvas.gameObject.SetActive(!canvas.gameObject.activeSelf);
        }
    }
}
