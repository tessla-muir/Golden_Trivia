using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CategoryManager : MonoBehaviour
{
    Quiz quiz;

    void Start()
    {
        quiz = FindObjectOfType<Quiz>();
    }

    // Loads quiz scene
    public void LoadQuiz(int index)
    {
        SceneManager.LoadScene("QuizScene");
        ChangeQuestions(index);
    }

    // Changes questions depending on category
    void ChangeQuestions(int category)
    {
        // Get questions from respective folder
        Object[] categoryQuestions = Resources.LoadAll("Assets/Questions/Easy", typeof(QuestionSO));

        // Add newly loaded questions into quiz question list
        for (int i = 0; i < categoryQuestions.Length; i++)
        {
            quiz.questions.Add((QuestionSO)categoryQuestions[i]);
        }
    }
}
