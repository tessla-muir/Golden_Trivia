using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Quiz : MonoBehaviour
{
    [SerializeField] QuestionSO question;
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] GameObject[] answers;
    int correctAnswerIndex;
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite correctSprite;

    void Start()
    {
        questionText.text = question.GetQuestion();

        for (int i = 0; i < 4; i++)
        {
            TextMeshProUGUI buttonText = answers[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.GetAnswer(i);
        }
    }

    // Check to see if selected answer is correct
    public void OnAnswerSelected(int index)
    {
        // Correct
        if (index == question.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct!";
            Image buttonImage = answers[index].GetComponent<Image>();
            buttonImage.sprite = correctSprite;
        }
    }

    
}
