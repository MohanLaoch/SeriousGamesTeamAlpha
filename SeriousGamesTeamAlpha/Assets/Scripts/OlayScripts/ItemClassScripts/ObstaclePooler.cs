﻿using System;
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
  
    

    [SerializeField] private float hitRate, BoostRate;
    private int random;

    public bool hasSpawnedFirstItem;

    public float speed;

    private bool canSpawn = true;

    private float originalSpeed;
    private void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {
        RunningGameManager.instance.boostEvent += OnBoostEvent;
        RunningGameManager.instance.hitEvent += OnHitEvent;
        RunningGameManager.instance.hitCancelEvent += OnHitCanceled;
        RunningGameManager.instance.boostCancelEvent += OnBoostCanceled;
        RunningGameManager.instance.GameOverEvent += OnGameOver;
        originalSpeed = speed;


    }

    private void OnHitEvent()
    {
        speed = originalSpeed / hitRate;
    }

    private void OnBoostEvent()
    {
        speed = originalSpeed * BoostRate;
    }

    private void OnBoostCanceled()
    {
        speed = originalSpeed;
    }
    private void OnHitCanceled()
    {
        speed = originalSpeed;
    }

    public void SpawnItem()
   {
       if (canSpawn)
       {
           Vector2 position = new Vector2(Random.Range(defaultXPos, maxDefaultXPos), 1);
           GameObject itemObject = Instantiate(item, position, Quaternion.identity, transform);

           BlockMover mover = itemObject.GetComponent<BlockMover>();
           if (!hasSpawnedFirstItem)
           {
               mover.begin = true;
               hasSpawnedFirstItem = true;
           }
       }
      

       

   }

    private void OnGameOver()
    {
        canSpawn = false;
        speed = 0;
        RunningGameManager.instance.GameOverEvent -= OnGameOver;
    }

   

 
}