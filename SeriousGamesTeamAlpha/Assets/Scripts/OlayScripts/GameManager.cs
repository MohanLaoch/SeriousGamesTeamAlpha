using System;
using System.Collections;
using System.Collections.Generic;
using OlayScripts.ItemClassScripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    Normal,
    Hit,
    Boosted
};
public class GameManager : MonoBehaviour
{
    //creates an instance of this class in the scene. Allows us to call it anywhere without having each script have its own reference
    public static GameManager instance { get; set; }
    public Transform player;
    public GameObject[] blockPrefabs;
    private Dictionary<GameObject, Vector2> BlockData = new Dictionary<GameObject, Vector2>();
    public TextMeshProUGUI scoreText;
    public Vector2 spawnOffset;
    public Slider hydrationMeter;
    public float hydrationSpeed;
    public float hydrationAmount;
    public Transform blockParent;
    public int maxDistance;
    public Vector2 startPos;
    public float totalTime;
    
    public GameState gameState;

    [HideInInspector]
    public float GameSpeed = 1;

    [SerializeField] private float GameSpeedRate = 1;

     private int BoostHydrationSpeed = 1;

     private float walkingScore;

     public int walkingScoreModifier = 1;

     [HideInInspector]
     public ItemClass previousItemSpawned;

    
    private void Awake()
    {
        //checks to see if there's already an instance of the class, if not we set the instance. This only should apply at the start of the game since theres no instance of the class yet
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            //deletes GameObject if theres two instances of the class
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        totalTime = 0;
        if (blockParent.childCount > 0)
        {
            foreach (Transform child in blockParent.transform)
            {
                BlockData.Add(child.gameObject, child.position);
            }
        }
        SpawnBlock(startPos);
    }
    
    // Update is called once per frame
    void Update()
    {
        ClockFunc();
        DecreaseHydration(hydrationAmount * Time.deltaTime * GameSpeed * hydrationSpeed * BoostHydrationSpeed);
      
    }

    void ClockFunc()
    {
        totalTime += Time.deltaTime;
        float minutes = Mathf.FloorToInt(totalTime / 60);
        GameSpeed = 1 + ((1 / minutes) * GameSpeedRate);
        if (float.IsPositiveInfinity(GameSpeed) || float.IsNegativeInfinity(GameSpeed))
        {
            GameSpeed = 1;
        }
      
    }

    public void UpdateScoreText(int value)
    {
        walkingScore += value;
        scoreText.text = $"Score: {walkingScore}";
    }
    

    public void IncreaseHydration(float amount)
    {
        hydrationMeter.value += amount;
    }
    public void DecreaseHydration(float amount)
    {
        hydrationMeter.value -= amount;
    }

    public void StartBoost()
    {
        player.GetComponent<PlayerMovement>().StartBoost();
    }

    public void SpawnBlock(Vector2 pos)
    {
        //counter to prevent softlock from while loop
        int count = 0;

        if (BlockData.Count > maxDistance || blockParent.childCount > maxDistance)
        {
            GameObject child = blockParent.GetChild(0).gameObject;
            BlockData.Remove(child);
            Destroy(child);
        }


        while (BlockData.ContainsValue(pos) && count < 1000)
        {
            //keeps adding the offset so we get to a position where a block isn't overlapping another box
            pos.x += spawnOffset.x;
            count++;
        }


        GameObject block = Instantiate(blockPrefabs[0], pos, Quaternion.identity);
        block.transform.parent = blockParent;
        BlockData.Add(block, block.transform.position);
    }

    public void SetGameState(GameState state)
    {
        gameState = state;
        switch (gameState)
        {
            case GameState.Normal:
                Time.timeScale = 1;
                BoostHydrationSpeed = 1;
                break;
            case GameState.Boosted:
                Time.timeScale = 2;
                BoostHydrationSpeed = 2;
                break;
            case GameState.Hit:
                Time.timeScale = 0.75f;
                break;
        }
    }

    
}
