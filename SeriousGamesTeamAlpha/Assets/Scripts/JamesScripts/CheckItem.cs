using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckItem : MonoBehaviour
{
    public ItemSpawner itemSpawner;

    public string tag;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == tag)
        {
            itemSpawner.InsantiateItem();
            Destroy(other.gameObject);
            Debug.Log("Green");
            // add green animation
        }

        if (other.gameObject.tag != tag)
        {
            itemSpawner.InsantiateItem();
            Destroy(other.gameObject);
            Debug.Log("Red");
            // add red animation
        }
    }

}
