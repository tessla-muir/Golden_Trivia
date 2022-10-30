using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    public List<QuestionSO> questions = new List<QuestionSO>();
    [SerializeField] TextMeshProUGUI questionText;
    QuestionSO currentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] answers;
    int correctAnswerIndex;
    bool hasAnsweredEarly = true;

    [Header("Button Colors")]
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite correctSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("Progress Bar")]
    [SerializeField] Slider progressBar;

    [Header("Sounds")]
    [SerializeField] AudioSource correctSound;
    [SerializeField] AudioSource wrongSound;

    public bool isComplete;


    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        ChangeQuestions(CategoryManager.category);
    }

    void Start()
    {
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
    }

    void Update() 
    {
        timerImage.fillAmount  = timer.fillFraction;
        
        // Load next question
        if (timer.loadNextQuestion)
        {
            // Last question
            if (progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                return;
            }

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

            // Increment progress bar
            progressBar.value++;

            scoreKeeper.IncrementTotalQuestions();
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
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";
    } 

    public void DisplayAnswer(int index)
    {
        Image buttonImage;
        // Correct
        if (index == correctAnswerIndex)
        {
            questionText.text = "Correct!";
            buttonImage = answers[index].GetComponent<Image>();
            buttonImage.sprite = correctSprite;
            correctSound.Play();
            scoreKeeper.IncrementCorrectQuestions();
        }
        // Wrong
        else
        {
            wrongSound.Play();
            questionText.text = "Wrong!\nAnswer: " + currentQuestion.GetAnswer(correctAnswerIndex);
            buttonImage = answers[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctSprite;
        }
    }

    // Changes questions depending on category
    void ChangeQuestions(int category)
    {
        string path = "";

        if (CategoryManager.category == 0)
        {
            path = "Questions/Easy";
        }
        else if (CategoryManager.category == 1)
        {
            path = "Questions/Medium";
        }
        else if (CategoryManager.category == 2)
        {
            path = "Questions/Hard";
        }
        else if (CategoryManager.category == 3)
        {
            path = "Questions/Fanatic";
        }
        else if (CategoryManager.category == 4)
        {
            path = "Questions/Actors";
        }
        else if (CategoryManager.category == 5)
        {
            path = "Questions/Show";
        }
        else
        {
            Debug.Log("Error on category selection");
        }

        // Get questions from respective folder
        Object[] categoryQuestions = Resources.LoadAll(path);

        // Add newly loaded questions into quiz question list
        for (int i = 0; i < categoryQuestions.Length; i++)
        {
            questions.Add((QuestionSO)categoryQuestions[i]);
        }
    }
}
