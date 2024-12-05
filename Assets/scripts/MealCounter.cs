using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MealCounter : MonoBehaviour
{
    public TextMeshProUGUI mealCounterText; 
    private int mealsServed = 0; // ctr for meals served

    private void Start()
    {
        UpdateCounterText(); 
    }

    public void IncrementMealCounter()
    {
        mealsServed++;
        UpdateCounterText();

        PlayerPrefs.SetInt("MealsServed", mealsServed);
        PlayerPrefs.Save();
    }

    private void UpdateCounterText()
    {
        if (mealCounterText != null)
        {
            mealCounterText.text = "Meals Served Correctly: " + mealsServed;
        }
        else
        {
            Debug.LogError("MealCounterText is not assigned in the Inspector!");
        }
    }
}
