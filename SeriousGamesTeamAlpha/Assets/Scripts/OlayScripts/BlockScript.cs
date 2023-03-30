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
            float distanceNext = 100000000000;
            int c = Random.Range(0, items.Length);

            ItemClass t_item = RunningGameManager.instance.previousItemSpawned;
            if (RunningGameManager.instance.lastSpawnedItemPos != null && d > -1)
            {
                float distance = Vector2.Distance(RunningGameManager.instance.lastSpawnedItemPos.position,
                    possibleItemPositions[i].position);

                if (d > 0)
                {
                    distanceNext = Vector2.Distance(possibleItemPositions[i - 1].position,
                        possibleItemPositions[i].position);
                }


                if (distance < maxDistanceBetweenItems || distanceNext < maxDistanceBetweenItems)
                {
                    float minutes = Mathf.FloorToInt(RunningGameManager.instance.totalTime % 60);


                    if (items[d] == items[c])
                    {
                        switch (d)
                        {
                            case 0:
                                c = Random.Range(1, 2);
                                break;
                            default:
                                c = 0;
                                break;
                        }
                    }

                    switch (d)
                    {
                        case 0:
                            //c = Random.Range(1, 2);
                            break;
                        case 1:
                            c = 0;
                            break;
                        case 2:
                            c = 0;
                            break;
                    }
                }
                
            }


            int r = Random.Range(0, 100);
            if (r < items[c].spawnChance)
            {
           
                ItemClass item = Instantiate(items[CheckDistance(i, c)], possibleItemPositions[i].position, Quaternion.identity);
                item.transform.parent = possibleItemPositions[i];

                item.index = c;
                RunningGameManager.instance.previousItemSpawned = item;
                RunningGameManager.instance.lastSpawnedItemPos = possibleItemPositions[i];
            
            
           
                spawnedItems += 1;
                d = c;
            }
        }
       
        GenerateItem();
        
    }

    private void GenerateItem()
    {
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

            
            
            ItemClass item = Instantiate(items[CheckDistance(i, x)], possibleItemPositions[i].position, Quaternion.identity);
            item.index = x;
            item.transform.parent = possibleItemPositions[i];
            RunningGameManager.instance.previousItemSpawned = item;
            RunningGameManager.instance.lastSpawnedItemPos = possibleItemPositions[i];
            spawnedItems += 1;
            d = x;

        }
        
    }

    private int CheckDistance(int x, int c)
    {
        bool canOperate = true;
        int a = -1;
        int b = -2;


        RaycastHit2D leftRay = Physics2D.Raycast(possibleItemPositions[x].position,
            -possibleItemPositions[x].right, maxDistanceBetweenItems, ObstacleMask);
        RaycastHit2D rightRay = Physics2D.Raycast(possibleItemPositions[x].position,
            possibleItemPositions[x].right, maxDistanceBetweenItems, ObstacleMask);

        ItemClass item;

        if (d > -1)
        {


            if (leftRay.collider == null && rightRay.collider == null)
            {
                canOperate = false;
            }

            if (leftRay.collider == null)
            {
                canOperate = false;
            }

            if (canOperate)
            {


                if (leftRay.collider.TryGetComponent(out ItemClass itemL))
                {
                    item = itemL;
                    a = item.index;

                }

                else if (rightRay.collider.TryGetComponent(out ItemClass itemR))
                {
                    item = itemR;
                    a = item.index;
                }


                if (c == a)
                {
                    b = 0;
                }
                else
                {
                    switch (a)
                    {
                        case 0:
                            b = Random.Range(1, 2);
                            break;
                        case 1:
                            b = 0;
                            break;
                        case 2:
                            b = 0;
                            break;
                        default:
                            break;
                    }

                }
            }

            else
            {
                b = c;
            }
        }

        else
        {
            b = c;
        }

        return b;
    }


}