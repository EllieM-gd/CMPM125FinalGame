using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI mealCountText;  // Reference to the UI Text element

    void Start()
    {
        // Retrieve the stored meal count from PlayerPrefs and display it
        int mealCount = PlayerPrefs.GetInt("MealsServed", 0);  // Default to 0 if not found
        mealCountText.text = "You Served " + mealCount + " Meals Correctly!";
    }

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("Game Over");
    }
}
