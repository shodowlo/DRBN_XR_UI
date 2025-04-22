using UnityEngine;

public class ToggleObjects : MonoBehaviour
{
    public GameObject[] objects; // Objects to hide
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleVisibility()
    {
        foreach (GameObject obj in objects)
        {
            if (obj != null)
            {
                obj.SetActive(!obj.activeSelf); // Toggle visibility
            }
        }
    }
}
