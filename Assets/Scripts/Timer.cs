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
        timerValue -= Time.deltaTime;

        if(isAnsweringQuestion)
        {
            if(timerValue > 0)
            {
                fillFraction = timerValue / timeToAnswer;
            }
            else
            {
                isAnsweringQuestion = false;
                timerValue = timeToReviewAnswer;
            }
        }
        else
        {
            if(timerValue > 0)
            {
                fillFraction = timerValue / timeToReviewAnswer;
            }
            else
            {
                isAnsweringQuestion = true;
                timerValue = timeToAnswer;
                loadNextQuestion = true;
            }
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
