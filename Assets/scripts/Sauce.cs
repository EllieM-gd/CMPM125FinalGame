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
    private PotManager PotManager;
    private GameObject sauce;
    public static List<string> appliedSauces = new List<string>(); // List to store applied sauces

    private void Start()
    {
        PotManager = PotManager.Instance;
        pastaRenderer = GetComponent<Renderer>();
        sauceColor = GetComponent<Renderer>().material.name;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.transform.childCount > 1 && !PotManager.IsPickedUp)
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
        // Get the material that's on the pasta mesh
        Material pastaMaterial = sauce.GetComponent<Renderer>().material;
        if (pastaRenderer != null)
        {
            // The first time a sauce is applied to the pasta, turn the "None" node on the graph from true to false
            if (pastaMaterial.GetInt("_None") == 1)
            {
                pastaMaterial.SetInt("_None", 0);
            }

            // Get the sauce name from the specific pot, and change that corresponding boolean in the shader graph
            Regex regexExp = new Regex("[a-z]+");
            Match regexOutput = regexExp.Match(GetComponent<Renderer>().material.name);

            string PMaterialName = "_" + regexOutput.Value;
            string sauceName = "";
            if (pastaMaterial.GetInt(PMaterialName) == 0)
            {
                pastaMaterial.SetInt(PMaterialName, 1);
            }
            if (PMaterialName == "_red")
            {
                sauceName = "Tomato";  // Red is Tomato
            }
            if (PMaterialName == "_cream")
            {
                sauceName = "Alfredo";  // Cream is Alfredo
            }
            if (PMaterialName == "_green")
            {
                sauceName = "Pesto";  // Green is Pesto
            }
            // Add the sauce name to the appliedSauces list if it isn't already there
                if (!string.IsNullOrEmpty(sauceName) && !appliedSauces.Contains(sauceName))
            {
                appliedSauces.Add(sauceName);  // Add the sauce to the list
            }
        }
        else Debug.Log("Pasta Renderer is null");
        sauce.SetActive(true);
    }

}
