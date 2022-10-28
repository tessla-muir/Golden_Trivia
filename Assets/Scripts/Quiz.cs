using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Quiz : MonoBehaviour
{
    [SerializeField] QuestionSO question;
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] GameObject[] answers;

    void Start()
    {
        questionText.text = question.GetQuestion();

        for (int i = 0; i < 4; i++)
        {
            TextMeshProUGUI buttonText = answers[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.GetAnswer(i);
        }
    }
}
