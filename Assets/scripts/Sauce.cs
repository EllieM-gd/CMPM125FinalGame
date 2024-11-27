using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Sauce : MonoBehaviour
{
    private Renderer pastaRenderer;
    private string sauceColor;
    private bool PlayerInTrigger = false;
    private Transform pasta;
    private GameObject sauce;
    public static List<string> appliedSauces = new List<string>(); // List to store applied sauces

    private void Start()
    {
        pastaRenderer = GetComponent<Renderer>();
        sauceColor = GetComponent<Renderer>().material.name;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.transform.childCount > 1)
            {
                PlayerInTrigger = true;
                pasta = other.transform.GetChild(1).GetChild(1);
                sauce = pasta.GetChild(0).GetChild(0).gameObject;
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
            ApplySauce();
        }
    }

    private void ApplySauce()
    {
        // Apply the sauce to pasta
        Material pastaMaterial = sauce.GetComponent<Renderer>().material;
        if (pastaMaterial != null)
        {
            string materialName = pastaMaterial.name;

            // Map the material name to the corresponding sauce name
            string sauceName = "";
            if (materialName.Contains("red"))
            {
                sauceName = "Tomato";  // Red is Tomato
            }
            else if (materialName.Contains("cream"))
            {
                sauceName = "Alfredo";  // Cream is Alfredo
            }
            else if (materialName.Contains("green"))
            {
                sauceName = "Pesto";  // Green is Pesto
            }

            // Add the sauce name to the appliedSauces list if it isn't already there
            if (!string.IsNullOrEmpty(sauceName) && !appliedSauces.Contains(sauceName))
            {
                appliedSauces.Add(sauceName);  // Add the sauce to the list
            }

            sauce.SetActive(true);  // Make the sauce visible on the pasta
        }
        else
        {
            Debug.LogError("Pasta Renderer is null");
        }
    }

}
