using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTriggerLoader : MonoBehaviour
{
    private bool hasSpawned;
    private void OnTriggerEnter2D(Collider2D col)
    {
       
        if (col.CompareTag("Player"))
        { 
            //prevents us from spawning two blocks at the same time
            if(hasSpawned)
                return;
            Vector2 position = transform.parent.position;
            RunningGameManager.instance.SpawnBlock(position);
            hasSpawned = true;
        }
    }
}
