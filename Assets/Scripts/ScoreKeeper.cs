using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctQuestions = 0;
    int totalQuestions = 0;

    public int GetCorrectQuestions()
    {
        return correctQuestions;
    }

    public void IncrementCorrectQuestions()
    {
        correctQuestions++;
    }

    public int GetTotalQuestions()
    {
        return totalQuestions;
    }

    public void IncrementTotalQuestions()
    {
        totalQuestions++;
    }

    public int CalculateScore()
    {
        return Mathf.RoundToInt((correctQuestions /  (float) totalQuestions) * 100);
    }
}
