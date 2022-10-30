using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CategoryManager : MonoBehaviour
{
    static public int category;

    // Loads quiz scene
    public void LoadQuiz(int index)
    {
        category = index;
        SceneManager.LoadScene("QuizScene");
    }
}
