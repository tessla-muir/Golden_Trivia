using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    ScoreKeeper scoreKeeper;

    [Header("Sounds")]
    [SerializeField] List<AudioSource> audios;
    bool soundPlayed = false;

    void Awake()
    {
       scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void ShowFinalScore()
    {
        int finalScore = scoreKeeper.CalculateScore();
        if (finalScore == 100)
        {
            finalScoreText.text = "You're golden!\n Final Score: " + finalScore + "%";
        } 
        else if (finalScore >= 80)
        {
            finalScoreText.text = "Bea proud!\n Final Score: " + finalScore + "%";
        }
        else if (finalScore >= 50)
        {
            finalScoreText.text = "At least you passed...\n Final Score: " + finalScore + "%";
        }
        else
        {
            finalScoreText.text = "The girls would Bea ashamed.\n Final Score: " + finalScore + "%";
        }
        PlayEndSound();
    }

    void PlayEndSound()
    {
        int index = Random.Range(0, audios.Count);
        AudioSource currentAudio = audios[index];

        if (!soundPlayed)
        {
            currentAudio.Play();
            soundPlayed = true;
        }
    }
}
