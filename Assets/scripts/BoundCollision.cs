using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Make sure cannont throw water if touching a wall to prevent water from moving through walls
public class BoundCollision : MonoBehaviour
{
    private PotManager PotManager;
    private bool CouldThrow = false;
    public Animator playerAnimator;

    void Start()
    {
        PotManager = PotManager.Instance;
    }

    // Disable throw
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (PotManager != null)
            {
                if (PotManager.IsPickedUp && PotManager.CanThrow)
                {
                    PotManager.CanThrow = false;
                    CouldThrow = true;
                }
            }
        }
    }
    
    // Re-enable throw
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (PotManager != null)
            {
                if (PotManager.IsPickedUp && CouldThrow)
                {
                    PotManager.CanThrow = true;
                    CouldThrow = false;
                    playerAnimator.Play("Player Idle");
                }
            }
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if (PotManager.IsPickedUp && CouldThrow)
            {
                playerAnimator.Play("Player Bounds");
            }
        }
    }
}
