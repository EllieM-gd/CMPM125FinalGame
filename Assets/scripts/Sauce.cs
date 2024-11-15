using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sauce : MonoBehaviour
{
    private Renderer pastaRenderer; // var for the renderer since this is what we are changing 
    private Color sauceColor; // Store the color of the sauce we touch
    private bool PlayerInTrigger = false;
    private Transform pasta;
    private PotManager PotManager;

    private void Start()
    {
        // Get the Renderer component of the pasta
        pastaRenderer = GetComponent<Renderer>();
        sauceColor = GetComponent<Renderer>().material.color;
        PotManager = PotManager.Instance;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    // Check if we're colliding with an object tagged as "Sauce"
    //    if (other.CompareTag("Player"))
    //    {
    //        // Check if player is holding object and isn't pot which means it is pasta
    //        if (other.transform.childCount > 1 && !PotManager.IsPickedUp)
    //        {
                
    //            PlayerInTrigger = true;
    //        }
    //    }
    //    if (other.CompareTag("Sauce"))
    //    {
    //        // Get the color of the sauce by accessing the Renderer component on the sauce object
    //        sauceColor = other.GetComponent<Renderer>().material.color;
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        // Ensure one item picked up at a time
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

    void Update()
    {
        if (PlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            pastaRenderer = pasta.GetComponent<Renderer>();
            if (pastaRenderer != null)
            {
                pastaRenderer.material.color = sauceColor;
            }
        }
    }
}