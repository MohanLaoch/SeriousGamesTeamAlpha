using System;
using System.Collections;
using System.Collections.Generic;
using OlayScripts.ItemClassScripts;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class ObstaclePooler : MonoBehaviour
{
    public static ObstaclePooler SharedInstance { get; set; }
    public GameObject item;

    public float defaultXPos;
    public float maxDefaultXPos;
    public float minTime;
    
    [SerializeField] private bool collectionChecks = true;
    [SerializeField] private int maxPoolSize = 15;
    
    public float minTimeDelay, maxTimeDelay;

    private int random;
    public IObjectPool<GameObject> itemPool { get; set; }


    public GameObject lastSpawned;
    public List<GameObject> pooledItemObjects;
    public ItemClass lastSpawnedItem;
    public bool hasSpawnedFirstItem;
    private void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {
        
       /*itemPool = new ObjectPool<GameObject>(CreateItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject,
            collectionChecks, 10, maxPoolSize);*/

       //random = Random.Range(0, 2);


       /*pooledItemObjects = new List<GameObject>();

       GameObject newItemObject;

       for (int i = 0; i < maxPoolSize; i++)
       {
           newItemObject = Instantiate(item);
           foreach (Transform child in newItemObject.transform)
           {
              child.gameObject.SetActive(false);
           }
           newItemObject.SetActive(false);
           
           pooledItemObjects.Add(newItemObject);
       }*/



    }
    
   // public void ReleaseItem(GameObject Item) => itemPool.Release(Item);
  
    
    
    private GameObject CreateItem()
    {
        GameObject Item = Instantiate(item, transform);
        if (hasSpawnedFirstItem == false)
        {
            item.GetComponent<BlockMover>().begin = true;
            hasSpawnedFirstItem = true;
            item.SetActive(true);
        }

        else
        {
            Item.SetActive(false);
        }
       

        return Item;
    }

    private void OnTakeFromPool(GameObject Item)
    {
        
        /*
        float a = 0;
        
        
        
        
        a = Random.Range( defaultXPos, maxDefaultXPos);
        Item.transform.position = new Vector3(a, 1, 0);
        Item.gameObject.SetActive(true);
        lastSpawned = item;
        */
        
        
    }
    
    private void OnDestroyPoolObject(GameObject Item)
    {
        //Destroy(Item);
    }

    private void OnReturnedToPool(GameObject Item)
    {
        //Item.gameObject.SetActive(false);
    }

   // public void SpawnItem() => itemPool.Get();


   public void SpawnItem()
   {
       Vector2 position = new Vector2(Random.Range(defaultXPos, maxDefaultXPos), 1);
       GameObject itemObject = Instantiate(item, position, Quaternion.identity, transform);
    
       if (!hasSpawnedFirstItem)
       {
           itemObject.GetComponent<BlockMover>().begin = true;
           hasSpawnedFirstItem = true;
       }
       
   }

    private void Update()
    {
        
    }


    /*public GameObject GetPooledObject()
    {
        for (int i = 0; i < maxPoolSize; i++)
        {
            if (!pooledItemObjects[i].activeInHierarchy)
            {
                return pooledItemObjects[i];
            }
        }

        return null;
    }*/
}