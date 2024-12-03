using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

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