using System;
using System.Collections;
using System.Collections.Generic;
using OlayScripts.ItemClassScripts;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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
    public TextMeshProUGUI scoreText;
    public Slider hydrationMeter;
    public float hydrationSpeed;
    public float hydrationAmount;
    
    public float initialSliderValue;

    [HideInInspector]
    public float totalTime;
    
    
    public GameState gameState;

    [HideInInspector]
    public float GameSpeed = 1;
    

     private float BoostHydrationSpeed = 1;

     public int walkingScore;
     

     [HideInInspector]
     public ItemClass previousItemSpawned;

     [HideInInspector]
     public float hydrationSliderValue;
     
     public float invisibilityFrameTime = 2;

     [Header("ItemStuff")] public float minWaitTime, maxWaitTime;

     public GameObject endCanvas;

     [SerializeField] private LevelLoader Loader;

     public int seed;

     public float minDistance = 2f;

     public List<GameObject> GeneratedItems = new List<GameObject>();

     private bool canSpawn;
     
     float SpawnTimer = 0;

     

     public delegate void onBoost();

     public delegate void onHit();

     public onBoost boostEvent;

     public onHit hitEvent;

     public delegate void OnHitCancel();

     public OnHitCancel hitCancelEvent;

     public float audioPitchRate;

     public GameObject PauseMenu;
     public delegate void OnBoostCancel();

     public OnBoostCancel boostCancelEvent;
     private float audioPitch = 0;

     public delegate void OnGameOver();

     public OnGameOver GameOverEvent;

     public GameObject runningSplash;
     
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

        if (seed != 0)
        {
            seed = (int)DateTime.Now.Ticks;
        }
        
        UnityEngine.Random.InitState(seed);
    }

    // Start is called before the first frame update
    void Start()
    {
        hydrationMeter.value = initialSliderValue;
        totalTime = 0;
        PlayerMovement.instance.canMove = true;
        PauseMenu.SetActive(false);
       
        
        //checks to see if there's a child attached to the block parent, if so add it to the list.
     
    }


    public void OnPlayerBoost()
    {
        if (boostEvent != null)
        {
            boostEvent();
        }
    }

    public void OnHitEvent()
    {
        if (hitEvent != null)
        {
            hitEvent();
        }
    }

    public void OnGameOverEvent()
    {
        if (GameOverEvent != null)
        {
            GameOverEvent();
        }
    }

    public void OnBoostCancelEvent()
    {
        if (boostCancelEvent != null)
        {
            boostCancelEvent();
        }
    }
    public void OnHitCancelEvent()
    {
        if (hitCancelEvent != null)
        {
            hitCancelEvent();
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if(gameState == GameState.Finished)
            return;

        SpawnItems();

        ClockFunc();
        hydrationSliderValue = hydrationMeter.value;
        if(gameState == GameState.Hit)
            return;
        DecreaseHydration(hydrationAmount * Time.deltaTime * (GameSpeed * 0.75f) * hydrationSpeed);
       


    }

    void  SpawnItems()
    {
        SpawnTimer += Time.deltaTime;
        if(SpawnTimer > Random.Range(minWaitTime, maxWaitTime))
        {
            SpawnTimer = 0;
            ObstaclePooler.SharedInstance.SpawnItem();
        }



    }

    bool isAllowedToSpawn()
    {
        if (GeneratedItems.Count > 0)
        {
            
            Debug.Log("Succeed");
            Transform lastSpawned = GeneratedItems[0].transform;
            Debug.Log(lastSpawned.transform.position);
            float seconds = 0;
            
            

            if (lastSpawned.gameObject.activeInHierarchy)
            {
                seconds += Time.deltaTime;
                if ((ObstaclePooler.SharedInstance.maxDefaultXPos - lastSpawned.position.x) >=
                    (minDistance - 0.1f))
                {
                    canSpawn = true;
                }

                else if(seconds >= 4f)
                {
                    canSpawn = true;
                }
                           
            }

            else
            {
                canSpawn = true;
            }
            
        }

        else
        {
            canSpawn = true;
        }

        return canSpawn;

    }

    void ClockFunc()
    {
        //adds the time, so we can determine the game speed
        
        if(!PlayerMovement.instance.canMove)
            return;
        totalTime += Time.deltaTime;
        float minutes = Mathf.FloorToInt(totalTime / 60);


       
        GameSpeed += Time.deltaTime / 60;
       
        
        GameSpeed = Mathf.Clamp(GameSpeed, 1, 2);
        audioPitch += ((GameSpeed - 1) * audioPitchRate);
        
        
        
        AudioManager.instance.PlaySoundAtCertainPitch("Main Runner Game Music", audioPitch);
        //checks to see if the game speed goes to infinity, in case of a divide by 0 situation


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
            hydrationMeter.value -= Amount / 3;
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
        OnHitEvent();
        //wait a certain amount of time 
        yield return new WaitForSeconds(time);
        //reset player status and game state
        SetGameState(GameState.Normal);
        OnHitCancelEvent();
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
                Time.timeScale = 0;
                OnGameOverEvent();
                AudioManager.instance.Stop("Main Runner Game Music");
                //Time.timeScale = 0f;

                
                endCanvas.SetActive(true);
                runningSplash.SetActive(true);

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



    public void ResumeGame(int state)
    {
        switch (state)
        {
            case 0:
                PauseMenu.SetActive(true);
                Time.timeScale = 0;
                PlayerMovement.instance.canMove = false;
                
                break;
            case 1:
                PauseMenu.SetActive(false);
                Time.timeScale = 1;
                PlayerMovement.instance.canMove = true;
                break;
        }
        
    }
  
    
}
