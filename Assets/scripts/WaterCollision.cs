using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision with " + collision.gameObject);
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Water"))
        {
            Destroy(gameObject, 0.1f);
        }
    }
}
