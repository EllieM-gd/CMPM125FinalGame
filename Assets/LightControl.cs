using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    public Light BigLight;
    public bool enableLight = true;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (enableLight)
            {
                BigLight.intensity = 2f;
            }
            else
            {
                BigLight.intensity = 0.5f;
            }
        }

    }
}
