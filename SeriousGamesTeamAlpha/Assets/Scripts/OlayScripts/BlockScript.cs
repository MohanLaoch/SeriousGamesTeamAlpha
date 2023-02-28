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

    public List<Transform> itemPositions = new List<Transform>();
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
            Debug.Log(x);
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
            int count = 0;
            int x = UnityEngine.Random.Range(0, items.Length);
            while (x == GameManager.instance.previousItemIndex && count < 1000)
            {
                x = UnityEngine.Random.Range(0, items.Length);
                count++;
            }
            
            ItemClass item = Instantiate(items[x], position.position, Quaternion.identity);
            item.transform.parent = position;
            
            GameManager.instance.previousItemIndex = x;
            
        }
    }

    
}
