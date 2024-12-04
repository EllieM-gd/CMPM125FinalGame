using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public enum direction{
        left,right, closed
    }
    public bool doorOpen = false;
    [NonSerialized] public direction doorDirection;
    private Animator doorAnimator;
    void Start()
    {
        doorAnimator = GetComponent<Animator>();
    }

    public void OpenDoor(direction dir)
    {
        doorOpen = true;
        doorDirection = dir;
        if (dir == direction.left)
        {
            doorAnimator.SetTrigger("OpenLeft");       
        }
        else if (dir == direction.right)
        {
            doorAnimator.SetTrigger("OpenRight");
        }
    }

    public void closedoor()
    {
        doorOpen = false;
        doorDirection = direction.closed;
        doorAnimator.SetTrigger("CloseDoor");
    }
    
}
