using UnityEngine;
using TMPro;
using System;

public class ControllerTime : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    void Start()
    {
        InvokeRepeating("UpdateTime", 0f, 1f);
    }

    void UpdateTime()
    {
        timeText.text = DateTime.Now.ToString("HH:mm");
    }
}
