using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckItem : MonoBehaviour
{
    public ItemSpawner itemSpawner;

    public string tag;
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        
       
    }


    public void OnCollision(GameObject other)
    {
        other.gameObject.SetActive(false);
        //itemSpawner.InstantiateItem();
        

        if (other.gameObject.CompareTag(tag))
        {
            itemSpawner.score++;
            itemSpawner.GreenAnimation();
            Debug.Log("Success " + Time.frameCount);
            
        }

        else
        {
            itemSpawner.score = 0;
            itemSpawner.RedAnimation();
            Debug.Log("Fail " + Time.frameCount);
            
        }
        
        Destroy(other.gameObject);
        itemSpawner.InstantiateItem();
        

        itemSpawner.scoreText.text = itemSpawner.score.ToString();
    }

}
