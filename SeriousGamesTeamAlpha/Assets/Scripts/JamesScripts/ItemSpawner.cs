using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    public GameObject[] foodItems;

    public void InsantiateItem()
    {
        int randomIndex = Random.Range(0, foodItems.Length);

        Vector3 spawnPos = this.transform.position;

        GameObject newItem = Instantiate(foodItems[randomIndex], spawnPos, Quaternion.identity, transform.parent);
    }
}
