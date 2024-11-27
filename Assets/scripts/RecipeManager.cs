using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RecipeManager : MonoBehaviour
{
    public List<string> availableSauces = new List<string> { "Tomato", "Alfredo", "Pesto" }; // Sauce options
    public TextMeshPro recipeText; // TextMeshPro on the cube
    public List<string> currentRecipe = new List<string>(); // Current recipe list
    public static RecipeManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        GenerateNewRecipe();
    }

    // Generate a new recipe with random sauces
    public void GenerateNewRecipe()
    {
        currentRecipe.Clear();
        int numberOfSauces = Random.Range(2, 4); // Recipe includes 2-3 sauces

        for (int i = 0; i < numberOfSauces; i++)
        {
            string randomSauce = availableSauces[Random.Range(0, availableSauces.Count)];
            currentRecipe.Add(randomSauce);
        }

        UpdateRecipeText();

        // Log the generated recipe for debugging
        Debug.Log("New Recipe Generated: " + string.Join(", ", currentRecipe));
    }


    // Update the recipe text on the board
    private void UpdateRecipeText()
    {
        if (recipeText != null)
        {
            recipeText.text = "Recipe:\n" + string.Join("\n", currentRecipe); // Display each sauce on a new line
        }
        else
        {
            Debug.LogError("Recipe TextMeshPro is not assigned!");
        }
    }

    // Validate the served pasta against the recipe
    public bool ValidateRecipe(List<string> appliedSauces)
    {
        return new HashSet<string>(appliedSauces).SetEquals(currentRecipe);
    }

    // Clear the recipe board after serving
    public void ClearRecipeBoard()
    {
        if (recipeText != null)
        {
            recipeText.text = "Recipe Served!\nGenerating new recipe...";
        }

        Invoke(nameof(GenerateNewRecipe), 2f); // Delay to display the success message
    }
}
