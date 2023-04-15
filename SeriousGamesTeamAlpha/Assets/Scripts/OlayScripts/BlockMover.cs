using System;
using System.Collections;
using System.Collections.Generic;
using OlayScripts.ItemClassScripts;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlockMover : MonoBehaviour
{
    private enum state
    {
        Normal,
        Boosted,
        Hit
    }
    private Rigidbody2D rb;

    public float speed;

    public List<ItemClass> itemClasses = new List<ItemClass>();

    public bool begin;

    private state itemState;

    private float originalSpeed;
    
    
   
    // Start is called before the first frame update

    private void Awake()
    {
        foreach (Transform item in transform)
        {
            item.TryGetComponent(out ItemClass itemClass);
            itemClasses.Add(itemClass);
            item.gameObject.SetActive(false);
           
        }
    }

    void Start()
    {
        
        
        
       rb = GetComponent<Rigidbody2D>();
      
       
       
       int i = Random.Range(0, itemClasses.Count);
       int r = Random.Range(0, 100);
       bool generated = false || r < 50;


       if (begin)
       {
           generated = true;
       }
       /*while (!generated && count < 5)
       {
           r = Random.Range(0, 100);
           i = Random.Range(0, itemClasses.Count);
           if ((r) < (itemClasses[i].spawnChance))
           {
               
               generated = true;
               
           }

           else
           {
               generated = false;
               count++;
           }
           
           
       }*/


       foreach (var t in itemClasses)
       {
           t.gameObject.SetActive(false);
            
       }

       if (generated)
       {
           itemClasses[i].gameObject.SetActive(true);
            
       }

       else
       {
           gameObject.SetActive(false);
            
       }
       

       generated = false;


       
    }

   

   
    private void OnEnable()
    {
        
        

    }

    private void OnDisable()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        speed = ObstaclePooler.SharedInstance.speed;
        rb.velocity = speed * RunningGameManager.instance.GameSpeed * Vector2.left;
        
        
    }
}
