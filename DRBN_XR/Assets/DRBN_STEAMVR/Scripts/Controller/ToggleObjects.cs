using UnityEngine;

public class ToggleObjects : MonoBehaviour
{
    public GameObject[] objects; // Objects to hide

    public void ToggleVisibility()
    {
        foreach (GameObject obj in objects)
        {
            if (obj != null)
            {
                obj.SetActive(!obj.activeSelf);
            }
        }
    }
}