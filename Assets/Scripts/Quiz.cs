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
        GetNextQuestion();
        DisplayQuestion();
    }

    // Displays question and answers for a question
    void DisplayQuestion()
    {
        // Get and display question
        questionText.text = question.GetQuestion();
        correctAnswerIndex = question.GetCorrectAnswerIndex();

        // Get and display answers
        for (int i = 0; i < answers.Length; i++)
        {
            TextMeshProUGUI buttonText = answers[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.GetAnswer(i);
        }
    }

    void GetNextQuestion()
    {
        // Setup Buttons
        ChangeButtonStates(true);
        SetDefaultButtonSprites();
    }

    void ChangeButtonStates(bool state)
    {
        for (int i = 0; i < answers.Length; i++)
        {
            Button button = answers[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void SetDefaultButtonSprites()
    {
        for (int i = 0; i < answers.Length; i++)
        {
            Image buttonImage = answers[i].GetComponent<Image>();
            buttonImage.sprite = defaultSprite;
        }
    }

    // Check to see if selected answer is correct
    public void OnAnswerSelected(int index)
    {
        ChangeButtonStates(false);
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
