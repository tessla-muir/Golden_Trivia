using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CategoryManager : MonoBehaviour
{
    static public int category;
    [SerializeField] AudioSource startMusic;

    void Awake()
    {
        startMusic.Play();
    }

    // Loads quiz scene
    public void LoadQuiz(int index)
    {
        category = index;
        SceneManager.LoadScene("QuizScene");
    }

    public void QuitGame(bool quit) 
    {
        if (quit)
        {
            Debug.Log("Exiting Game");
            Application.Quit();
        }
    }
}
