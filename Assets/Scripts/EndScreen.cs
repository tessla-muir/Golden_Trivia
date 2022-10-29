using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScore;
    ScoreKeeper scoreKeeper;

    void Start()
    {
        finalScore.text = "Congratulations!\n Final Score: " + scoreKeeper.CalculateScore();
    }
}
