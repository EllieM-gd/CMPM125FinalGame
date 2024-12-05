using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DoorCollider : MonoBehaviour
{
    DoorScript doorScript;
    public DoorScript.direction direction; 
    private bool holdDoor = false;
    void Start(){
        doorScript = GetComponentInParent<DoorScript>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (doorScript.doorOpen == true)
            {
                if (doorScript.doorDirection != direction)
                {
                    holdDoor = true;
                }
                else {
                    StartCoroutine(closeDoorAfterDelay());
                }
            }
            else {
                doorScript.OpenDoor(direction);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (holdDoor)
            {
                doorScript.closedoor();
                holdDoor = false;
            }
            if (doorScript.doorDirection == direction)
            {
                StartCoroutine(closeDoorAfterDelay());
            }
        }
    }


    IEnumerator closeDoorAfterDelay()
    {
        yield return new WaitForSeconds(1.5f);
        if (doorScript.doorOpen) doorScript.closedoor();
    }
}
