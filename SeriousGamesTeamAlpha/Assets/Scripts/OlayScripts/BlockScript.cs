using System.Collections;
using System.Collections.Generic;
using OlayScripts.ItemClassScripts;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    public List<Transform> possibleItemPositions = new List<Transform>();
    public ItemClass[] items;
    private ItemClass previousItems;
    public float itemSpawnRate = 20;

    private List<Transform> itemPositions = new List<Transform>();
    void Start()
    {
        if(RunningGameManager.instance.gameState == GameState.Boosted)
            return;
        SetPossiblePositions();
    }

    void SetPossiblePositions()
    {
        foreach (Transform positions in possibleItemPositions)
        {
            //int x = UnityEngine.Random.Range(0, 100);
            positions.gameObject.SetActive(false);
            /*if (x < (Mathf.FloorToInt(itemSpawnRate / GameManager.instance.GameSpeed)))
            {
                
            }*/
            
            positions.gameObject.SetActive(true);
            int x = UnityEngine.Random.Range(0, 100);

            if (x < 80)
            {
                itemPositions.Add(positions);
            }
           
            

            
            
        }

        if (itemPositions.Count < 2)
        {
            for (int i = 0; i < 2; i++)
            {
                int x = Random.Range(0, possibleItemPositions.Count);
                itemPositions.Add(possibleItemPositions[x]);
            }
            
            
        }
        SpawnItem();
        
        
        
    }

    void SpawnItem()
    {
        foreach (Transform position in itemPositions)
        {
            ItemClass tempItem = null;
            int x = 0;

            if (RunningGameManager.instance.GameSpeed < 1.4f)
            {

                if (previousItems == null)
                {
                    x = Random.Range(0, items.Length);
                }
                else if ((previousItems.GetType() == typeof(HurdleItemClass)) || previousItems.GetType() == typeof(EnergyDrinkItemClass))
                {
                    int count = 0;
                    while ((items[x] == previousItems) && count < 1000)
                    {
                        x = Random.Range(0, items.Length);
                        count++;
                    }
                    
                }
                

                else
                {
                    x = UnityEngine.Random.Range(0, items.Length);

                }
            }

            else
            {
                x = UnityEngine.Random.Range(0, items.Length);
            }

            int y = Random.Range(0, 100);
            float z = 0;
                 
            if (items[x].inverseSpawnRate)
            {
                z = (items[x].spawnChance / RunningGameManager.instance.GameSpeed);
            }

            else
            {
                z = (items[x].spawnChance * RunningGameManager.instance.GameSpeed);
            }

            if (y < z)
            {
                ItemClass item = Instantiate(items[x], position.position, Quaternion.identity);
                item.transform.parent = position;
                RunningGameManager.instance.previousItemSpawned = item;
                previousItems = item;
            }
            
            
            
            

        }
    }

    
}
