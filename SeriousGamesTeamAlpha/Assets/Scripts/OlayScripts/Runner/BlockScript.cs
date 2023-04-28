using System.Collections;
using System.Collections.Generic;
using OlayScripts.ItemClassScripts;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    public List<Transform> possibleItemPositions = new List<Transform>();
    public ItemClass[] items;
    private ItemClass previousItems;
    
    private List<Transform> itemPositions = new List<Transform>();

    public float maxDistanceBetweenItems = 2f;

    private int d = -1;
    private int spawnedItems = 0;

    private bool value;
    [SerializeField]private LayerMask mask;
    public LayerMask ObstacleMask;
    void Start()
    {
        if(RunningGameManager.instance.gameState == GameState.Boosted)
            return;
        SetPossiblePositions();
    }

    void SetPossiblePositions()
    {
        /*foreach (Transform positions in possibleItemPositions)
        {
            //int x = UnityEngine.Random.Range(0, 100);
            positions.gameObject.SetActive(false);
            /*if (x < (Mathf.FloorToInt(itemSpawnRate / GameManager.instance.GameSpeed)))
            {
                
            }#1#
            
            positions.gameObject.SetActive(true);
            int x = UnityEngine.Random.Range(0, 100);
            itemPositions.Add(positions);
            
        }*/
        SpawnItem();
        
        
        
    }

    void SpawnItem()
    {
        for (int i = 0; i < possibleItemPositions.Count; i++)
        {

            int c = -1;
            int r = -1;

           
            
            c = Random.Range(0, items.Length);
            
            
             r = Random.Range(0, 100);
             
             if (isTouching(possibleItemPositions[i].position))
             {
                 c = 0;
                 r = Mathf.FloorToInt(items[c].spawnChance / 2);
             }
            if (r < items[c].spawnChance)
            {
           
                ItemClass item = Instantiate(items[c], possibleItemPositions[i].position, Quaternion.identity);
                item.transform.parent = possibleItemPositions[i];

                
                RunningGameManager.instance.previousItemSpawned = item;
               // RunningGameManager.instance.lastSpawnedItemPos = possibleItemPositions[i];
            
            
           
                spawnedItems += 1;
                d = c;
            }
        }
       
        GenerateItem();
        
    }

    public bool isTouching(Vector2 position)
    {
        StartCoroutine(CheckDistance(position));

        return value;
    }
    private void GenerateItem()
    {
        if(possibleItemPositions.Count < 2)
            return;
        if (spawnedItems > 1)
            return;
        int r = Random.Range(0, 100);
        int count = 0;
        int x = Random.Range(0, items.Length);
        
        
        while (spawnedItems < 2 && count < 200)
        {
            int i = Random.Range(0, possibleItemPositions.Count);
            
            while (possibleItemPositions[i].childCount > 0 && count < 150)
            {
                i = Random.Range(0, possibleItemPositions.Count);
                count++;

            }

            if (x == d)
            {
                x = Random.Range(0, items.Length);
            }

            else
            {
                switch (d)
                {
                    case 0:
                        if (RunningGameManager.instance.GameSpeed > 1)
                        {
                            x = Random.Range(1, 2);
                        }

                        break;
                    default:
                        x = RunningGameManager.instance.GameSpeed > 1 ? Random.Range(0, items.Length) : 0;

                        break;

                }
            }

            r = Random.Range(0, 100);



            if (isTouching(possibleItemPositions[i].position))
            {
                x = 0;
            }
            
            ItemClass item = Instantiate(items[x], possibleItemPositions[i].position, Quaternion.identity);
            item.transform.parent = possibleItemPositions[i];
            RunningGameManager.instance.previousItemSpawned = item;
            //RunningGameManager.instance.lastSpawnedItemPos = possibleItemPositions[i];
            spawnedItems += 1;
            d = x;

        }
        
    }

    private IEnumerator CheckDistance(Vector3 position)
    {
        GameObject empty = new GameObject();
        BoxCollider2D col = empty.AddComponent<BoxCollider2D>();
        Rigidbody2D rb =empty.AddComponent<Rigidbody2D>();
        rb.isKinematic = true;
        empty.layer = 0;
        col.isTrigger = true;
        empty.transform.position = position;
        Debug.Log(col.transform.position);
        Debug.Log(col.bounds);
        yield return new WaitForFixedUpdate();
        if (col.IsTouchingLayers(mask))
        {
            Debug.Log("True");
            Destroy(empty);
            value = true;
        }

        else
        {
            Debug.Log("False");
            Destroy(empty);
            value = false;
        }

        
    }


}