using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2, 6)]
    [SerializeField] string question = "Enter new question text here";
    [SerializeField] string[] answers = new string[4];
    [SerializeField] int correctAnswer;

    public string GetQuestion()
    {
        return question;
    }

    public string GetAnswer(int i)
    {
        return answers[i];
    }

    public int GetCorrectAnswerIndex()
    {
        return correctAnswer;
    }
}
