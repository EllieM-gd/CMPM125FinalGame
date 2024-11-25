using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class AchievementPopUp : MonoBehaviour
{
    public GameObject popupUI; // Reference to the pop-up panel
    public TextMeshProUGUI popupText; // Reference to the pop-up text
    public float displayDuration = 5f; // Duration to display the pop-up
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Method to show the pop-up with a message
    public void ShowPopup(string message)
    {
        Debug.Log($"ShowPopup called with message: {message}");
        popupText.text = message;
        popupUI.SetActive(true);
        Invoke(nameof(HidePopup), displayDuration);
    }

    // Method to hide the pop-up
    private void HidePopup()
    {
        popupUI.SetActive(false);
    }
}
