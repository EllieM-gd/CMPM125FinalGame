using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void OpenOptions()
    {
        SceneManager.LoadScene("Options");
    }

    public void ShowCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("Main");
    }

    public void GameOverMenu()
    {
        SceneManager.LoadScene("Game Over");
    }
}