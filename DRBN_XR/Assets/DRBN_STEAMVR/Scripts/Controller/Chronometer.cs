using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Chronometer : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public Button playButton;
    public Button pauseButton;
    public Button resetButton;

    private float elapsedTime = 0f;
    private bool isRunning = false;

    void Start()
    {
        UpdateTimeDisplay();
        UpdateButtons();
    }

    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimeDisplay();
        }
    }

    public void PlayChrono()
    {
        isRunning = true;
        UpdateButtons();
    }

    public void PauseChrono()
    {
        isRunning = false;
        UpdateButtons();
    }

    public void ResetChrono()
    {
        elapsedTime = 0f;
        isRunning = false;
        UpdateTimeDisplay();
        UpdateButtons();
    }

    void UpdateTimeDisplay()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        int hundredths = Mathf.FloorToInt((elapsedTime * 100f) % 100);

        timeText.text = $"{minutes:00}:{seconds:00},{hundredths:00}";
    }

    void UpdateButtons()
    {
        playButton.gameObject.SetActive(!isRunning);
        pauseButton.gameObject.SetActive(isRunning);
    }
}
