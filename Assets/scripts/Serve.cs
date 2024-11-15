using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serve : MonoBehaviour
{
    private bool PlayerInTrigger = false;
    private PotManager PotManager;
    private Transform pasta;
    private Transform TableTransform;
    // Start is called before the first frame update
    private void Start()
    {
        // Get the Renderer component of the pasta
        PotManager = PotManager.Instance;
        TableTransform = this.gameObject.transform.parent;
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
        if (PlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            pasta.transform.parent = TableTransform;
            pasta.transform.position = new Vector3(TableTransform.position.x, TableTransform.position.y + 0.8f, TableTransform.position.z);
            // Make pasta disappear after some time so customers eat it
            StartCoroutine(DisableAfterDelay());
        }
    }

    private IEnumerator DisableAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        // Disable pasta
        pasta.gameObject.SetActive(false);
    } 
}
