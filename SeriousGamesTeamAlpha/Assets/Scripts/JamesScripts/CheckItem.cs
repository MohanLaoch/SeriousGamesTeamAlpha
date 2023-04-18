using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckItem : MonoBehaviour
{
    public ItemSpawner itemSpawner;

    public string tag;

    public void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
        itemSpawner.InstantiateItem();
        

        if (other.gameObject.tag == tag)
        {
            itemSpawner.score++;
            itemSpawner.GreenAnimation();
        }

        if (other.gameObject.tag != tag)
        {
            itemSpawner.score = 0;
            itemSpawner.RedAnimation();
        }

        itemSpawner.scoreText.text = itemSpawner.score.ToString();
    }


}
