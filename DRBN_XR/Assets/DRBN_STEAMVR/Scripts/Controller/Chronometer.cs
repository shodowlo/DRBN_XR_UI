using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class Chronometer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Chronometre
    [Header("Chronometer")]
    public TextMeshProUGUI chronoTimeText;
    public Button playButton;
    public Button pauseButton;
    public Button resetButton;

    private float elapsedTime = 0f;
    private bool isRunning = false;

    // Pour le resize
    [Header("Resize on Hover (chronometer)")]
    public RectTransform target;

    public Vector2 normalSize = new Vector2(225f, 100f);
    public Vector2 hoverSize = new Vector2(270f, 100f);
    public float resizeSpeed = 5f;

    private RectTransform rectTransform;
    private Vector2 targetSize;

    // Horloge
    [Header("Clock")]
    public TextMeshProUGUI clockTimeText;
    private string clockLastTime = "";

    void Start()
    {
        // Pour le chronometre
        UpdateTimeDisplay();
        UpdateButtons();

        playButton.onClick.AddListener(PlayChrono);
        pauseButton.onClick.AddListener(PauseChrono);
        resetButton.onClick.AddListener(ResetChrono);

        // pour le hover
        if (target == null)
            target = GetComponent<RectTransform>();

        targetSize = normalSize;
        target.sizeDelta = normalSize;
    }

    void Update()
    {
        // MAJ du chronometre
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimeDisplay();
        }
    
        // Maj hover
        target.sizeDelta = Vector2.Lerp(target.sizeDelta, targetSize, Time.deltaTime * resizeSpeed);

        // MAJ de l'horloge
        string currentTime = DateTime.Now.ToString("HH:mm");

        if (currentTime != clockLastTime)
        {
            clockLastTime = currentTime;
            clockTimeText.text = currentTime;
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

    void UpdateTimeDisplay() // Pour le chrono
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        int hundredths = Mathf.FloorToInt((elapsedTime * 100f) % 100);

        chronoTimeText.text = $"{minutes:00}:{seconds:00},{hundredths:00}";
    }

    void UpdateButtons() // Pour le chrono
    {
        playButton.gameObject.SetActive(!isRunning);
        pauseButton.gameObject.SetActive(isRunning);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetSize = hoverSize;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetSize = normalSize;
    }
}
