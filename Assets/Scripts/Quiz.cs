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
        DisplayQuestion();
    }

    // Displays question and answers for a question
    void DisplayQuestion()
    {
        questionText.text = question.GetQuestion();
        correctAnswerIndex = question.GetCorrectAnswerIndex();

        for (int i = 0; i < answers.Length; i++)
        {
            TextMeshProUGUI buttonText = answers[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.GetAnswer(i);
        }
    }

    // Check to see if selected answer is correct
    public void OnAnswerSelected(int index)
    {
        // Correct
        if (index == correctAnswerIndex)
        {
            questionText.text = "Correct!";
            Image buttonImage = answers[index].GetComponent<Image>();
            buttonImage.sprite = correctSprite;
        }
        else
        {
            questionText.text = "Wrong! \nAnswer: " + question.GetAnswer(correctAnswerIndex);
            Image buttonImage = answers[index].GetComponent<Image>();
            buttonImage.sprite = defaultSprite;
        }
    }

    
}
