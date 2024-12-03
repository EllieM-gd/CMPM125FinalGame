using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; 
public class CountdownTimer : MonoBehaviour
{
    public TMP_Text timerDisplay; 
    public float countdownTime = 180f; // 3 minutes

    private void Start()
    {
        if (timerDisplay == null)
        {
            timerDisplay = GameObject.Find("TimerDisplay").GetComponent<TMP_Text>();
        }
        StartCoroutine(CountdownCoroutine()); 
    }

    private IEnumerator CountdownCoroutine()
    {
        while (countdownTime > 0) 
        {
            yield return new WaitForSeconds(1); 
            countdownTime -= 1; 
            UpdateTimerDisplay(); 
        }

        //the timer reaches 0, trigger Game Over
        TriggerGameOver();
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(countdownTime / 60); 
        int seconds = Mathf.FloorToInt(countdownTime % 60);
        timerDisplay.text = $"Time Left: {minutes:00}:{seconds:00}"; //display in MM:SS format
    }

    private void TriggerGameOver()
    {
        SceneManager.LoadScene("Game Over");
    }
}

