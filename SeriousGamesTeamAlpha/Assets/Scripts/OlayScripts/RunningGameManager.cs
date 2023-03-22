using System;
using System.Collections;
using System.Collections.Generic;
using OlayScripts.ItemClassScripts;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState
{
    Normal,
    Hit,
    Boosted,
    Finished
};
public class RunningGameManager : MonoBehaviour
{
    //creates an instance of this class in the scene. Allows us to call it anywhere without having each script have its own reference
    
    public static RunningGameManager instance { get; set; }
    
    [Header("Game Stuff")]
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
    public float boostHydrationSpeed;
    public Vector2 startPos;
    public float initialSliderValue;
    
    [HideInInspector]
    public float totalTime;
    
    [HideInInspector]
    public GameState gameState;

    [HideInInspector]
    public float GameSpeed = 1;

    [SerializeField] private float GameSpeedRate = 1;

     private float BoostHydrationSpeed = 1;

     private float walkingScore;

     public int walkingScoreModifier = 1;

     [HideInInspector]
     public ItemClass previousItemSpawned;

     [HideInInspector]
     public float hydrationSliderValue;
     
     public float invisibilityFrameTime = 2;

     public float distanceCheck = 1000;
     [Header("CutsceneStuff")]
     
     public bool playCutscene;
     
     public PlayableDirector Director;

     public GameObject cutsceneObject;

     public GameObject playerVisuals;

     public float timelineSpeed;

     public GameObject endCanvas;

     [SerializeField] private LevelLoader Loader;

     
     
    private void Awake()
    {
        //checks to see if there's already an instance of the class, if not we set the instance. This only should apply at the start of the game since theres no instance of the class yet
        if (instance == null)
        {
            instance = this;
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
        hydrationMeter.value = initialSliderValue;
        Director.Stop();
        totalTime = 0;
        cutsceneObject.SetActive(false);
        PlayerMovement.instance.canMove = true;
        
        //checks to see if there's a child attached to the block parent, if so add it to the list.
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
        if(gameState == GameState.Finished)
            return;
        ClockFunc();
        hydrationSliderValue = hydrationMeter.value;
        if(gameState == GameState.Hit)
            return;
        DecreaseHydration(hydrationAmount * Time.deltaTime * GameSpeed * hydrationSpeed);
      
    }

    void ClockFunc()
    {
        //adds the time, so we can determine the game speed
        totalTime += Time.deltaTime;
        float minutes = Mathf.FloorToInt(totalTime / 60);


        float Varspeed =  (walkingScore / distanceCheck * PlayerMovement.instance.scoreRatio);

        float t_GameSpeed = 1 + (Varspeed / 10);

        GameSpeed = Mathf.Lerp(GameSpeed, t_GameSpeed, Time.deltaTime);

        GameSpeed = Mathf.Clamp(GameSpeed, 1, 2);
        //checks to see if the game speed goes to infinity, in case of a divide by 0 situation

        Debug.Log(GameSpeed);
      
    }

    public void UpdateScoreText(int value)
    {
        walkingScore += value;
        
        //Another way of writing scoreText.text = walkingScore.ToString();
        scoreText.text = $"{walkingScore}";
    }
    

    public void IncreaseHydration(float amount)
    {
        float Amount = amount / 100;
        hydrationMeter.value += Amount;
        
    }
    public void DecreaseHydration(float amount)
    {
        float Amount = amount / 100;
        
        //if the slider is below 20%, make the hydrationMeter go slowly, allowing players to get a little extra movement
        if (hydrationSliderValue < 0.2)
        {
            hydrationMeter.value -= Amount / 2;
        }

        else
        {
            hydrationMeter.value -= Amount;
        }


        if (hydrationMeter.value <= 0)
        {
            SetGameState(GameState.Finished);
        }
    }

    public void StartBoost()
    {
        PlayerMovement.instance.StartBoost();
    }

    public void StartHurdleHit()
    {
        StartCoroutine(PlayerHit(invisibilityFrameTime));
    }
    
    IEnumerator PlayerHit(float time)
    {
        //wait a frame before resuming   
        yield return null;
        //change game state to hit
        SetGameState(GameState.Hit);
        //play hit function on the player
        PlayerMovement.instance.PlayHit();
        //wait a certain amount of time 
        yield return new WaitForSeconds(time);
        //reset player status and game state
        SetGameState(GameState.Normal);
        PlayerMovement.instance.ResetAcceleration();
    }
    
  

    public void SpawnBlock(Vector2 pos)
    {
        //counter to prevent softlock from while loop
      
        int count = 0;

        
        while (BlockData.ContainsValue(pos) && count < 1000)
        {
            //keeps adding the offset so we get to a position where a block isn't overlapping another box
            pos.x += spawnOffset.x;
            count++;
        }


        GameObject block = Instantiate(blockPrefabs[0], pos, Quaternion.identity);
        block.transform.parent = blockParent;
        //change the scale of the block to be the spawn offset. Allows us to make the level more dynamic 
        block.transform.localScale = new Vector3(spawnOffset.x, 1, 1);
        
        //adds block to the dictionary
        BlockData.Add(block, block.transform.position);
        
        //checks to see if the block size is above the maxLimit if so remove the first child of the parent from the dictionary
        //and delete the block from the scene
        if (BlockData.Count > maxDistance|| blockParent.childCount > maxDistance )
        {
            GameObject child = blockParent.GetChild(0).gameObject;
            BlockData.Remove(child);
            Destroy(child);
        } 

        
        
        

    }


 

    public void SetGameState(GameState state)
    {
        //sets the game state as well as custom properties
        gameState = state;
        switch (gameState)
        {
            case GameState.Normal:
                Time.timeScale = 1;
                BoostHydrationSpeed = 1;
                player.gameObject.layer = 7;
                break;
            case GameState.Boosted:
                //makes the game go twice as fast
                /*ItemClass[] itemsInSight = GameObject.FindObjectsOfType<ItemClass>();
                foreach (var items in itemsInSight)
                {
                    Destroy(items.gameObject);
                }*/
                Time.timeScale = 1;
                BoostHydrationSpeed = 1;
                player.gameObject.layer = 9;
                break;
            case GameState.Hit:
                //makes the game 25% slower
                Time.timeScale = 0.75f;
                player.gameObject.layer = 9;
                break;
            case GameState.Finished:
                PlayerMovement.instance.canMove = false;
                
                //Time.timeScale = 0f;

                if (playCutscene)
                {
                    blockParent.gameObject.SetActive(false);
                    cutsceneObject.SetActive(true);
                    playerVisuals.SetActive(false);
                    Director.Play();
                    Director.playableGraph.GetRootPlayable(0).SetSpeed(timelineSpeed);
                }

                else
                {
                    endCanvas.SetActive(true);
                }

                break;
        }
    }


    public void RetryRunner()
    {
        Loader.LoadLevel(SceneManager.GetActiveScene().name);
    }

    public void QuitRunner()
    {
        Loader.LoadLevel("JamesScene");
    }


  
    
}