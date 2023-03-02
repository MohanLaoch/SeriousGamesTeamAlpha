using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
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

    private static readonly int MoveSpeedHash = Animator.StringToHash("moveSpeed");
    private static readonly int JumpedHash = Animator.StringToHash("Jumped");
    private static readonly int isGroundedHash = Animator.StringToHash("isGrounded");

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.IsTouchingLayers(groundCollider, GroundLayer);
        
        animator.SetBool(isGroundedHash, isGrounded);
        animator.SetFloat(MoveSpeedHash, moveInput.x);

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
        HandleMovement();
    }

    void HandleMovement()
    {
        
        //if isBoosting is true, set the maxVelocity to maxRunVelocity otherwise set it to maxWalkVelocity.
        maxVelocity = isBoosting ? maxRunVelocity : maxWalkVelocity;

        speed = walkSpeed * boostRate;

        speed += acceleration * Time.deltaTime * GameManager.instance.GameSpeed;
        
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        
        rb.velocity += (moveInput * (speed  * Time.fixedDeltaTime));

        
        
        
        //prevents velocity from going higher than the Max Velocity.
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);

        int s = Mathf.FloorToInt(GameManager.instance.walkingScoreModifier * rb.velocity.magnitude);
       
        GameManager.instance.UpdateScoreText(s);


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
        GameManager.instance.SetGameState(GameState.Boosted);
        //Makes the game go twice as fast.
        yield return new WaitForSeconds(time);
        isBoosting = false;
        //Game resumes original speed.
        GameManager.instance.SetGameState(GameState.Normal);
        ResetAcceleration();


    }
}
