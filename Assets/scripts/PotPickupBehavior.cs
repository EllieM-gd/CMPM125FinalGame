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
    public Animator potAnimator;
    public Transform WaterOrigin;
    public GameObject WaterObj;
    private bool CanThrow = true;

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
                // Refill water
                Pot.transform.GetChild(0).gameObject.SetActive(true);
                potAnimator.Play("Pot Idle");
                CanThrow = true;
            }
            else
            {
                Pot.transform.parent = Player.transform;
                Pot.transform.position = Player.transform.position + Player.transform.forward.normalized;
                Pot.transform.rotation = Player.transform.rotation * Quaternion.Euler(0f, 180f, 0f);
                PotManager.IsPickedUp = true;
            }
        }
        if (!PlayerInTrigger && Input.GetButtonDown("Fire2") && PotManager.IsPickedUp)
        {
            if (CanThrow)
            {
                StartCoroutine(ThrowWaterAfterDelay(0.25f));
            }
        }
    }

    private IEnumerator ThrowWaterAfterDelay(float delay)
    {
        potAnimator.Play("Pot Throw");
        // Wait for the specified amount of time to line up with animation
        yield return new WaitForSeconds(delay);
        // Instantiate the water object at the WaterOrigin's position and rotation
        GameObject ThrowWaterObj = Instantiate(WaterObj, WaterOrigin.transform.position, WaterOrigin.transform.rotation);
        // Apply force to the water object
        Rigidbody ThrowWaterObjRb = ThrowWaterObj.GetComponent<Rigidbody>();
        Vector3 throwDirection = WaterOrigin.transform.up;
        throwDirection += WaterOrigin.transform.forward * 0.5f;
        ThrowWaterObjRb.AddForce(throwDirection * 10f, ForceMode.Impulse);
        // Set CanThrow to false to prevent multiple throws
        CanThrow = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ensure one item picked up at a time
        if (other.CompareTag("Player"))
        {
            if (other.transform.childCount == 1 || PotManager.IsPickedUp)
            {
                PlayerInTrigger = true;
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
}
