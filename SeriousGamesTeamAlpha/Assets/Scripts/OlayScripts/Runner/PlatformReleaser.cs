using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformReleaser : MonoBehaviour
{
   

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
           Destroy(other.gameObject);
            
        }
    }
}
