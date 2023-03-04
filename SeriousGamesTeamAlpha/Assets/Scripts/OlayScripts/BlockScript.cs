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
        if(GameManager.instance.gameState == GameState.Boosted)
            return;
        SetPossiblePositions();
    }

    void SetPossiblePositions()
    {
        foreach (Transform positions in possibleItemPositions)
        {
            int x = UnityEngine.Random.Range(0, 100);
            positions.gameObject.SetActive(false);
            if (x < (Mathf.FloorToInt(itemSpawnRate / GameManager.instance.GameSpeed)))
            {
                positions.gameObject.SetActive(true);
                itemPositions.Add(positions);
            }
            

            
            
        }


        SpawnItem();
        
        
        
    }

    void SpawnItem()
    {
        foreach (Transform position in itemPositions)
        {
            if(GameManager.instance.gameState == GameState.Boosted)
               break;
            int x = 0;

            if (GameManager.instance.previousItemSpawned == null)
            {
                x = 0;
            }
            else if ((GameManager.instance.previousItemSpawned.GetType() == typeof(HurdleItemClass)) || GameManager.instance.previousItemSpawned.GetType() == typeof(EnergyDrinkItemClass))
            {
                x = 0;
            }

            else
            {
                 x = UnityEngine.Random.Range(0, items.Length);
            }
            
            ItemClass item = Instantiate(items[x], position.position, Quaternion.identity);
            item.transform.parent = position;
            GameManager.instance.previousItemSpawned = item;
            

        }
    }

    
}
