using UnityEngine;
using TMPro;

public class TimerHUDManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private TimerManager timerManager;

    void Start()
    {
        timerManager = TimerManager.Instance;
        if (timerManager == null)
        {
            Debug.LogError("TimerManager instance not found!");
        }

        if (timerText == null)
        {
            Debug.LogError("Timer Text is not assigned in the inspector!");
        }
    }

    void Update()
    {
        if (timerManager != null && timerText != null)
        {
            UpdateTimerDisplay();
        }
    }

    void UpdateTimerDisplay()
    {
        float remainingTime = timerManager.RemainingTime;
        string formattedTime = FormatTime(remainingTime);
        timerText.text = $"Time: {formattedTime} s";
    }

    string FormatTime(float timeInSeconds)
    {
        return timeInSeconds.ToString("F0");
    }
}