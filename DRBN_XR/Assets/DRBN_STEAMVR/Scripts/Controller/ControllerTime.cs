using UnityEngine;
using TMPro;
using System;

public class ControllerTime : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    private string lastTime = "";

    void Update()
    {
        string currentTime = DateTime.Now.ToString("HH:mm");

        if (currentTime != lastTime)
        {
            lastTime = currentTime;
            timeText.text = currentTime;
        }
    }
}
