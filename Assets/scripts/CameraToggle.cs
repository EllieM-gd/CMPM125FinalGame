using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraToggle : MonoBehaviour
{
    public Camera gameplayCamera; 
    public Camera recipeCamera;   
    private bool isRecipeViewActive = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) // '1' key
        {
            ToggleRecipeView();
        }
    }

    private void ToggleRecipeView()
    {
        isRecipeViewActive = !isRecipeViewActive;

        gameplayCamera.gameObject.SetActive(!isRecipeViewActive);
        recipeCamera.gameObject.SetActive(isRecipeViewActive);

        // pause/resume the game
        // Time.timeScale = isRecipeViewActive ? 0 : 1; // Pause when recipe camera is active
    }
}

