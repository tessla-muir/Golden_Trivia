using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToAnswer = 20f;
    [SerializeField] float timeToReviewAnswer = 10f;

    public bool loadNextQuestion = true;
    public bool isAnsweringQuestion = false;
    public float fillFraction;
    private float timerValue;

    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer()
    {
        timerValue = 0;
    }

    void UpdateTimer()
    {
        // Reduce time
        timerValue -= Time.deltaTime;

        // Not answering question
        if (timerValue <= 0 && !isAnsweringQuestion)
        {
            timerValue = timeToAnswer;
            isAnsweringQuestion = true;
            loadNextQuestion = true;
        }
        // Answering a question
        else if (timerValue <= 0 && isAnsweringQuestion)
        {
            timerValue = timeToReviewAnswer;
            isAnsweringQuestion = false;
        }
        // Update timer
        else
        {
            UpdateTimerFraction();
        }
    }

    void UpdateTimerFraction()
    {
        if (isAnsweringQuestion)
        {
            fillFraction = timerValue / timeToAnswer;
        }
        else
        {
            fillFraction = timerValue / timeToReviewAnswer;
        }

    }
}
