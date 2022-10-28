using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    [SerializeField] TextMeshProUGUI questionText;
    QuestionSO currentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] answers;
    int correctAnswerIndex;
    bool hasAnsweredEarly;

    [Header("Button Colors")]
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite correctSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    void Start()
    {
        timer = FindObjectOfType<Timer>();
        DisplayQuestion();
    }

    void Update() 
    {
        timerImage.fillAmount  = timer.fillFraction;
        
        // Load next question
        if (timer.loadNextQuestion)
        {
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        // Out of time
        else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            ChangeButtonStates(false);
        }
    }

    // Displays question and answers for a question
    void DisplayQuestion()
    {
        // Get and display question
        questionText.text = currentQuestion.GetQuestion();
        correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();

        // Get and display answers
        for (int i = 0; i < answers.Length; i++)
        {
            TextMeshProUGUI buttonText = answers[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    void GetNextQuestion()
    {
        if (questions.Count > 0) 
        {
            // Setup
            ChangeButtonStates(true);
            SetDefaultButtonSprites();

            // Pull random question from list
            GetRandomQuestion();

            // Display question
            DisplayQuestion();
        }
    }

    // Get a random question and remove it from the list
    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];
        if (questions.Contains(currentQuestion)) 
        {
            questions.Remove(currentQuestion);
        }
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
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        ChangeButtonStates(false);
        timer.CancelTimer();
    } 

    public void DisplayAnswer(int index)
    {
        // Correct
        if (index == correctAnswerIndex)
        {
            questionText.text = "Correct!";
            Image buttonImage = answers[index].GetComponent<Image>();
            buttonImage.sprite = correctSprite;
        }
        // Wrong
        else
        {
            questionText.text = "Wrong! \nAnswer: " + currentQuestion.GetAnswer(correctAnswerIndex);
            Image buttonImage = answers[index].GetComponent<Image>();
            buttonImage.sprite = defaultSprite;
        }
    }
}
