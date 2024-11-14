using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotPickupBehavior : MonoBehaviour
{
    private PotManager PotManager;
    private PlayerManager PlayerManager;
    private GameObject Pot;
    private GameObject Player;
    private Transform StoveTransform;
    private bool PlayerInTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        PotManager = PotManager.Instance;
        Pot = PotManager.Pot;
        PlayerManager = PlayerManager.Instance;
        Player = PlayerManager.Player;
        StoveTransform = this.gameObject.transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        // If the player is close to the stove and Fire2 (right click / left shift) is pressed, make the pot the child
        // of the correct object based on whether it is picked up or placed down
        if (PlayerInTrigger && Input.GetButtonDown("Fire2"))
        {
            if (PotManager.IsPickedUp)
            {
                Pot.transform.parent = StoveTransform;
                Pot.transform.position = PotManager.PotBasePosition;
                PotManager.IsPickedUp = false;
            }
            else
            {
                Pot.transform.parent = Player.transform;
                Pot.transform.position = Player.transform.position + Player.transform.forward.normalized;
                PotManager.IsPickedUp = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInTrigger = false;
        }
    }
}
