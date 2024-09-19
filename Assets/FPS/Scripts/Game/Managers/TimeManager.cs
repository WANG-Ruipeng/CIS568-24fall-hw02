using UnityEngine;
using System;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance { get; private set; }

    public float TotalTime;

    public float RemainingTime; 

    public event Action OnTimerEnd;

    private bool isRunning = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            StartTimer();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartTimer()
    {
        RemainingTime = TotalTime;
        isRunning = true;
    }

    private void Update()
    {
        if (isRunning)
        {
            RemainingTime -= Time.deltaTime;
            if (RemainingTime <= 0)
            {
                EndTimer();
            }
        }
    }

    private void EndTimer()
    {
        isRunning = false;
        RemainingTime = 0;
        OnTimerEnd?.Invoke();
    }

    public void PauseTimer()
    {
        isRunning = false;
    }

    public void ResumeTimer()
    {
        isRunning = true;
    }
}