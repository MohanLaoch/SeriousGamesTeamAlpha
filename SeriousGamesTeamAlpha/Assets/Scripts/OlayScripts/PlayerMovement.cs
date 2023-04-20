using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    
    public static PlayerMovement instance { get; set; }
    
    
    //allows us to get the speed from anywhere but not allowing anyone outside this script from changing it
    public float speed { get; private set; }

    private Rigidbody2D rb;
   
    [SerializeField] Animator animator;
    
    
   
    private bool isGrounded;

  

    private float startSpeed;
   
    private bool isBoosting;
    
    public bool canMove { get;  set; }
    private static readonly int MoveSpeedHash = Animator.StringToHash("moveSpeed");
    private static readonly int JumpedHash = Animator.StringToHash("Jumped");
    private static readonly int isGroundedHash = Animator.StringToHash("isGrounded");
    private static readonly int HitHash = Animator.StringToHash("isHit");

    public GameObject boostParticleSystem;

    [SerializeField] private float invisibilityRate;

    private int jumpCount;
    //slider in the inspector
    [Range(0, 6)] public float scoreRatio;

    [Header("Jump Physics")] 
    public float fallMultiplier = 2.5f;

    public float lowJumpMultiplier = 2f;
    
    [SerializeField] private LayerMask GroundLayer;

    [SerializeField] private float jumpForce;

    [SerializeField] public float boostTime;

    [SerializeField] private CircleCollider2D groundCollider;

    private float originaldrag;
    private float currentDrag;

    private Vector2 originalColliderSize;
    private Vector2 originalColliderOffset;

    private BoxCollider2D playerCollider;
    
    private void Awake()
    {

        instance = this;
        /*if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        */


        
        

    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boostParticleSystem.SetActive(false);
        playerCollider = GetComponent<BoxCollider2D>();
        originaldrag = rb.drag;
        currentDrag = originaldrag;
        //temporary walking speed reference allowing us to reference the walking speed at anypoint
        //bruh I legit could've just made the walkingSpeed variable public...
        originalColliderSize = playerCollider.size;
        originalColliderOffset = playerCollider.offset;
        RunningGameManager.instance.boostEvent += OnBoostEvent;
        RunningGameManager.instance.hitEvent += OnHitEvent;
        RunningGameManager.instance.GameOverEvent += OnGameOver;
    }


    void OnGameOver()
    {
        canMove = false;
        RunningGameManager.instance.boostEvent -= OnBoostEvent;
        RunningGameManager.instance.hitEvent -= OnHitEvent;
       // gameObject.SetActive(false);
        RunningGameManager.instance.GameOverEvent -= OnGameOver;
      
    }

    void OnBoostEvent()
    {
        StartCoroutine(Boost(boostTime));
    }

    IEnumerator Boost(float time)
    {
        //waits a single frame
        yield return null;
        isBoosting = true;
        boostParticleSystem.SetActive(true);
        AudioManager.instance.Play("Boost Up");
        
        RunningGameManager.instance.SetGameState(GameState.Boosted);
        //Makes the game go twice as fast.
        yield return new WaitForSeconds(time);
        isBoosting = false;
        //Game resumes original speed.
        AudioManager.instance.Stop("Boost Up");
        AudioManager.instance.Play("Boost Down");
        RunningGameManager.instance.boostCancelEvent();
        //Time.timeScale = 1;
        boostParticleSystem.SetActive(false);
        RunningGameManager.instance.SetGameState(GameState.Normal);


    }
   

    // Update is called once per frame
    void Update()
    {
        //self-explanatory
        isGrounded = Physics2D.IsTouchingLayers(groundCollider, GroundLayer);

        //reset jump counter
        if (isGrounded)
        {
            jumpCount = 0;
            rb.drag = originaldrag;
            playerCollider.size = originalColliderSize;
            playerCollider.offset = originalColliderOffset;
        }

        if (!isGrounded)
        {
            rb.drag = 1.3f;
            playerCollider.size = new Vector2(0.908728123f, 1.62065017f);
            playerCollider.offset = new Vector2(-0.20992589f, 0.164290011f);
        }
        
        
        //makes the player fall faster when jumping
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * (Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime);
        }
        
        
        //sets the isGrounded variable in the animator to be the result of the isGrounded variable here
        animator.SetBool(isGroundedHash, isGrounded);
        //sets the MoveSpeed variable in the animator to be the result of the moveInput x variable
        animator.SetFloat(MoveSpeedHash, 1);

        if(!canMove)
            return;
        if(!isGrounded)
            return;

        //player moves faster when the gamespeed is above 1


        if (Input.GetButtonDown("Vertical") || Input.GetKeyDown(KeyCode.Space))
        {
            JumpFunction();
        }
        
    }

    void JumpFunction()
    {
        
        //prevents jump bug
        if(jumpCount > 0)
            return;
        jumpCount = 1;
        //makes player jump based on its mass
        animator.SetTrigger(JumpedHash);
        rb.AddForce(Vector2.up * (jumpForce), ForceMode2D.Impulse);
        
        

        
        //sets the trigger in animator
        
        
        
        
        
        
        
    }
    
    
    

    
    private void FixedUpdate()
    {
        if(!canMove)
            return;
        //fixed update because physics run better in fixed update compared to update
        HandleMovement();
    }

    void HandleMovement()
    {
        
        /*
        //if isBoosting is true, set the maxVelocity to maxRunVelocity otherwise set it to maxWalkVelocity.
        maxVelocity = isBoosting ? maxRunVelocity : maxWalkVelocity;

        //if isBoosting is true, set the speed to boostSpeed otherwise set it to walkSpeed.
        speed = isBoosting ? walkSpeed * boostRate : walkSpeed;

        //adds acceleration over time
        speed += acceleration * Time.deltaTime;
        
        //moveinput is always going to be one
        moveInput = new Vector2(1, 0);
        
        //adds velocity overtime
        rb.velocity += (moveInput * (speed  * Time.fixedDeltaTime));

        
        
        
        //prevents velocity from going higher than the Max Velocity.
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);
        
        
        //creates score based on the speed of the rigidbody, score modifier and walkingScoreModifier. 
        
        */
        int s = Mathf.FloorToInt(5 * RunningGameManager.instance.GameSpeed * scoreRatio);
       
        RunningGameManager.instance.UpdateScoreText(s);

        
        
        /*
        if (AudioManager.instance.CheckIfPlaying("Walking") == false)
        {
            AudioManager.instance.Play("Walking");
        }
        */
        

       


    }

    public void OnHitEvent()
    {
        
        StartCoroutine(HitBlinks());
    }

    IEnumerator HitBlinks()
    {
        //Creates blinks for sprite
        SpriteRenderer playerSprite = GetComponentInChildren<SpriteRenderer>();
        float time = (RunningGameManager.instance.invisibilityFrameTime /invisibilityRate);

        Color ogColour = playerSprite.color;
        
        
        //while the player is hit, change the opacity of the sprite 3 times in 4 intervals
        while (RunningGameManager.instance.gameState == GameState.Hit)
        {
            playerSprite.color = new Color(1, 1 ,1, 0.1f);

            yield return new WaitForSeconds(time);

            playerSprite.color = new Color(ogColour.r, ogColour.g, ogColour.b, 1);

            yield return new WaitForSeconds(time);

            playerSprite.color = new Color(1, 1, 1, 0.1f);
            
            yield return new WaitForSeconds(time);

            playerSprite.color = new Color(ogColour.r, ogColour.g, ogColour.b, 1f);
        }

    }
    

    

    
}

