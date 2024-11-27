using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serve : MonoBehaviour
{
    private bool PlayerInTrigger = false;
    private PotManager PotManager;
    private Transform pasta;
    private Transform TableTransform;
    public Transform targetLocation;

    public AchievementPopUp achievementPopUp;

    // Start is called before the first frame update
    private void Start()
    {
        // Get the Renderer component of the pasta
        PotManager = PotManager.Instance;
        TableTransform = this.gameObject.transform.parent;

        achievementPopUp = FindObjectOfType<AchievementPopUp>();
        if (achievementPopUp == null)
        {
            Debug.LogError("AchievementPopUp script not found in the scene!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if player is holding object and isn't pot which means it is pasta
        if (other.CompareTag("Player"))
        {
            if (other.transform.childCount > 1 && !PotManager.IsPickedUp)
            {
                PlayerInTrigger = true;
                pasta = other.transform.GetChild(1);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInTrigger = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (PlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        //{
        //    pasta.transform.parent = TableTransform;
        //    pasta.transform.position = new Vector3(TableTransform.position.x, TableTransform.position.y + 0.8f, TableTransform.position.z);
        //    // Make pasta disappear after some time so customers eat it
        //    StartCoroutine(DisableAfterDelay());
        //}
        if (PlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            ServeMeal();
        }
    }
    private void ServeMeal()
    {
        // Place pasta on the table
        pasta.transform.parent = TableTransform;
        pasta.transform.localPosition = targetLocation.localPosition;

        // Validate the recipe (using applied sauces from Sauce script)
        bool isCorrectRecipe = RecipeManager.Instance.ValidateRecipe(Sauce.appliedSauces);

        // Debug log the applied sauces and the current recipe
        Debug.Log("Applied Sauces: " + string.Join(", ", Sauce.appliedSauces));
        Debug.Log("Current Recipe: " + string.Join(", ", RecipeManager.Instance.currentRecipe));

        if (isCorrectRecipe)
        {
            RecipeManager.Instance.ClearRecipeBoard(); // Clear the recipe board and generate a new recipe
            if (achievementPopUp != null)
            {
                achievementPopUp.ShowPopup("Just what I ordered, yumm!");
            }
        }
        else
        {
            if (achievementPopUp != null)
            {
                achievementPopUp.ShowPopup("This isn't what I ordered :(");
            }
        }

        Sauce.appliedSauces.Clear();

        // Disable pasta after a delay
        StartCoroutine(DisableAfterDelay());
    }




    private IEnumerator DisableAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        // Disable pasta
        pasta.gameObject.SetActive(false);
    } 
}
