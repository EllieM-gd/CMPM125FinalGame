using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Sauce : MonoBehaviour
{
    private Renderer pastaRenderer; // var for the renderer since this is what we are changing 
    private string sauceColor; // Store the color of the sauce we touch
    private bool PlayerInTrigger = false;
    private Transform pasta;
    private PotManager PotManager;

    private void Start()
    {
        // Get the Renderer component of the pasta
        pastaRenderer = GetComponent<Renderer>();
        sauceColor = GetComponent<Renderer>().material.name;
        PotManager = PotManager.Instance;
        //player.material.color = GameManager.Instance.color;
        //player.material.SetColor("_EmissionColor", GameManager.Instance.color);
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

    void Update()
    {
        if (PlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            // Get the material that's on the pasta mesh
            Material pastaMaterial = pasta.GetComponent<Renderer>().material;
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
                if (pastaMaterial.GetInt(PMaterialName) == 0)
                {
                    pastaMaterial.SetInt(PMaterialName, 1);
                }
            }
        }
    }
}