using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public static PlayerMovement instance { get; set; }
    private float speed;

    private Rigidbody2D rb;
    
    [SerializeField] float walkSpeed = 6f;

    [SerializeField] private float boostRate = 2f;

    [SerializeField] float acceleration;

    private float maxVelocity;
    
    [SerializeField] float maxWalkVelocity = 10f;

    [SerializeField] float maxRunVelocity = 40f;

    [SerializeField] Animator animator;

    private Vector2 moveInput;

    private bool isGrounded;

    [SerializeField] private LayerMask GroundLayer;

    [SerializeField] private float jumpForce;

    [SerializeField] float boostTime;

    [SerializeField] private CircleCollider2D groundCollider;

    private float startSpeed;
   
    private bool isBoosting;

    public bool canMove;
    private static readonly int MoveSpeedHash = Animator.StringToHash("moveSpeed");
    private static readonly int JumpedHash = Animator.StringToHash("Jumped");
    private static readonly int isGroundedHash = Animator.StringToHash("isGrounded");
    private static readonly int HitHash = Animator.StringToHash("isHit");

    public GameObject boostParticleSystem;

    [SerializeField] private float invisibilityRate;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        
        }

        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boostParticleSystem.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.IsTouchingLayers(groundCollider, GroundLayer);
        
        animator.SetBool(isGroundedHash, isGrounded);
        animator.SetFloat(MoveSpeedHash, moveInput.x);

        if(!canMove)
            return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(!isGrounded)
                return;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetTrigger(JumpedHash);
        }
    }

    public void StartBoost()
    {
        StartCoroutine(Boost(boostTime));
    }
    private void FixedUpdate()
    {
        if(!canMove)
            return;
        HandleMovement();
    }

    void HandleMovement()
    {
        
        //if isBoosting is true, set the maxVelocity to maxRunVelocity otherwise set it to maxWalkVelocity.
        maxVelocity = isBoosting ? maxRunVelocity : maxWalkVelocity;

        
        speed = isBoosting ? walkSpeed * boostRate : walkSpeed;

        speed += acceleration * Time.deltaTime * GameManager.instance.GameSpeed;
        
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        
        rb.velocity += (moveInput * (speed  * Time.fixedDeltaTime));

        
        
        
        //prevents velocity from going higher than the Max Velocity.
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);

        int s = Mathf.FloorToInt(GameManager.instance.walkingScoreModifier * rb.velocity.magnitude);
       
        GameManager.instance.UpdateScoreText(s);


    }

    public void PlayHit()
    {
        
        StartCoroutine(HitBlinks());
    }

    IEnumerator HitBlinks()
    {
        
        SpriteRenderer playerSprite = GetComponentInChildren<SpriteRenderer>();
        float time = (GameManager.instance.invisibilityFrameTime /invisibilityRate);

        Color ogColour = playerSprite.color;
        
        while (GameManager.instance.gameState == GameState.Hit)
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
    

    public void ResetAcceleration()
    {
        speed = walkSpeed;
    }

    IEnumerator Boost(float time)
    {
        //waits a single frame
        yield return null;
        isBoosting = true;
        boostParticleSystem.SetActive(true);
        GameManager.instance.SetGameState(GameState.Boosted);
        //Makes the game go twice as fast.
        yield return new WaitForSeconds(time);
        isBoosting = false;
        //Game resumes original speed.
        GameManager.instance.SetGameState(GameState.Normal);
        boostParticleSystem.SetActive(false);
        ResetAcceleration();


    }
}

