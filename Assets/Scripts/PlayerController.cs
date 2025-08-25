using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    //Player Data variable
    public PlayerData data;

    //Player Input variable
    private float xInput;

    //Player Rigidbody variable
    [SerializeField] private Rigidbody2D rb;

    //Player Groundcheck variable
    [SerializeField] private GameObject ground;
    [SerializeField] private Vector2 groundBoxCastSize;
    [SerializeField] private LayerMask realGround;
    private bool groundHit;
    [SerializeField] private bool isGrounded;

    //Player Jump variables
    private bool isJumping;
    private float lastOnGroundTime;
    [SerializeField] private float lastInWaterTime;
    private bool isJumpFalling;
    private bool isJumpCut;
    private float lastJumpButtonPress;
    [SerializeField] private GameObject shadow;

    //Player On Box variables
    public bool isOnTopOfBox;

    //Animator variable
    private Animator animator;
    private int xInputInt;

    //Audio variables
    [SerializeField] private SoundManager soundManager;
    
    //Misc variables
    [SerializeField] private PlayerInactive playerInactive;
    public PickUpHandlerClass pickUpHandler;

    //Debuff variables
    [SerializeField] private float stunTime;
    [SerializeField] private float slowTime;
    private float stunTimer;
    private float slowTimer;
    [SerializeField] private GameObject stunParticles;
    [SerializeField] private GameObject slowParticles;

    [Header("Wwise Events")]
    [SerializeField] private AK.Wwise.Event footstepEvent;
    [SerializeField] private AK.Wwise.Event jumpEvent;
    [SerializeField] private AK.Wwise.Event landEvent;
    [SerializeField] private float timeBetweenFootsteps;
    private bool footstepIsPlaying;
    private float lastFootstepTime;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        playerInactive = GetComponent<PlayerInactive>();

        SetGravityScale(data.gravityScale);

        IgnoreCollisions();

        pickUpHandler = gameObject.GetComponent<PickUpHandlerClass>();

        shadow.SetActive(false);

        slowParticles.SetActive(false);

        stunParticles.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        //Animator Update
        AnimatorUpdate();

        //Timer
        lastJumpButtonPress -= Time.deltaTime;
        lastOnGroundTime -= Time.deltaTime;
        lastInWaterTime -= Time.deltaTime;
        stunTimer -= Time.deltaTime;
        slowTimer -= Time.deltaTime;
        lastFootstepTime -= Time.deltaTime;

        //Jump Input
        if (UnityEngine.Input.GetButtonDown("Jump") && stunTimer <= 0)
        {
            lastJumpButtonPress = data.jumpInputBufferTime;
        }

        if (UnityEngine.Input.GetButtonUp("Jump") && stunTimer <= 0)
        {
            if (CanJumpCut())
            {
                isJumpCut = true;
            }
        }

        //Jump Checks
        if (isJumping && rb.velocity.y < 0) // when player has negative horizontal velocity while jumping
        {
            isJumping = false;
            isJumpFalling = true;
        }

        if (lastOnGroundTime > 0 && !isJumping) // checks if not jumping or wall jumping and on grounf
        {
            isJumpCut = false;

            if (!isJumping)
                isJumpFalling = false;
        }

        if (lastOnGroundTime < 0 && rb.velocity.y < 0 && !isJumpFalling)
        {
            isJumpFalling = true;
            isGrounded = false;
        }

        //Gravity handling
        if (isJumpFalling == true)  // increases gravity by a multiplier if JumpFalling
        {
            SetGravityScale(data.gravityScale * data.fallGravityMultiplier);
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -data.maxFallSpeed));
        }

        else if (isJumpCut) // increases gravity by a multiplier if JumpCut
        {
            SetGravityScale(data.gravityScale * data.jumpCutGravityMultiplier);
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -data.maxFallSpeed));
        }

        else // sets gravity to normal if none of previous conditions met
        {
            SetGravityScale(data.gravityScale);
        }

        //Jump
        if (CanJump() && lastJumpButtonPress > 0 && stunTimer <= 0) // if jump button has been pressed and not bouncing and is able to jump
        {
            // sets bools to false other than isJumping to ensure correct gravity
            isJumping = true;
            isJumpCut = false;
            isJumpFalling = false;
            Jump();
        }

        //GroundCheck
        CheckGround();

        ShadowCheck();

        ParticleCheck();
    }

    private void FixedUpdate()
    {
            //Movement
            if (stunTimer < 0)
            {
                Run(1);
            }

            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }

            //Footstep audio
            FootstepAudio();
    }
    private void CheckGround()
    {
            Vector2 groundBoxCastOrigin = ground.transform.position; // where the cast box originates from

            groundHit = Physics2D.OverlapBox(groundBoxCastOrigin, groundBoxCastSize, 0f, realGround); // grounded check
            if (groundHit)
            {
                isGrounded = true;
            if (!isJumping)
            {
                lastOnGroundTime = data.coyoteTime; // allows for jump buffer and also acts as ground check
            }
            if (isJumpFalling && isGrounded)
            {
                landEvent.Post(gameObject);
            }
            }

            else
            {
                isGrounded = false;
            }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            if (!isJumping)
            {
                lastInWaterTime = data.coyoteTime;
            }
        }
    }

    private void GetInput()
    {
                xInput = UnityEngine.Input.GetAxisRaw("Horizontal");
    }

    private void Run(float lerpAmount)
    {
        float targetSpeed = 0f;

        if(slowTimer >= 0)
        {
            targetSpeed = (xInput * data.runMaxSpeed) * 0.6f; // calculate target speed slowed
        }

        else
        {
            targetSpeed = xInput * data.runMaxSpeed; // calculate target speed
        }
        targetSpeed = Mathf.Lerp(rb.velocity.x, targetSpeed, lerpAmount);
        float speedDiff = targetSpeed - rb.velocity.x; // calculate difference between current speed and target speed

        float accelRate = 0.0f;

        if (lastOnGroundTime > 0)
        {
            if (Mathf.Abs(targetSpeed) > 0.01f) // if accelerating on ground
            {
                accelRate = data.runAccelAmount;

            }

            else if (Mathf.Abs(targetSpeed) == 0f) // if decelerating on ground
            {
                accelRate = data.runDeccelAmount;

            }
        }

        else
        {
            if (Mathf.Abs(targetSpeed) > 0.01f) // if accelerating on air
            {
                accelRate = data.runAccelAmount * data.accelInAir; // multiplies acceleration with multiplier to slow down acceleration in air

            }

            else if (Mathf.Abs(targetSpeed) == 0f) // if decelerating on air
            {
                accelRate = data.runDeccelAmount * data.deccelInAir; // multiplies decceleration with multiplier to slow down decceleration in air

            }
        }

        float movement = speedDiff * accelRate; // calculate force based on speed diff
        rb.AddForce(movement * Vector2.right, ForceMode2D.Force);

        //Flip
        if (Mathf.Abs(xInput) > 0) // if theres input
        {
            float direction = Mathf.Sign(xInput);
            transform.localScale = new Vector3(direction, 1, 1); //flip player to face input direciton
        }

        if (lastOnGroundTime > 0)
        {
            if (MathF.Abs(xInput) > 0)
            {
            }

        }
    }

    private void Jump()
    {
        float force = data.jumpForce;

        if (rb.velocity.y < 0) // if falling
        {
            force -= rb.velocity.y; // offsets jump force by falling force to ensure proper jump height
        }

        else if (lastInWaterTime > 0 && !(rb.velocity.y < 0))
        {
            force -= rb.velocity.y;
        }

        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);

        // prevents multiple jumps 
        lastOnGroundTime = 0;
        lastInWaterTime = 0;
        lastJumpButtonPress = 0;

        jumpEvent.Post(gameObject);
    }

    private void OnDisable()
    {
        playerInactive.enabled = true;

        rb.velocity = Vector2.zero;
        xInput = 0;
        isJumping = false;
        isJumpFalling = false;
        isJumpCut = false;
        shadow.SetActive(false);

        slowParticles.SetActive(false);

        stunParticles.SetActive(false);

        pickUpHandler.enabled = false;
    }

    private void OnEnable()
    {
        playerInactive.enabled = false;

        pickUpHandler.enabled = true;

        lastJumpButtonPress = -1;
        lastOnGroundTime = -1;
    }

    public void Slow()
    {
        slowTimer = slowTime;
    }

    public void Stun()
    {
        stunTimer = stunTime;
    }

    private void IgnoreCollisions()
    {
        GameObject[] tmp;
        tmp = GameObject.FindObjectsOfType<GameObject>();

        foreach(GameObject item in tmp)
        {
            if (item.GetComponent<PickUpClass>() != null)
            {
                Physics2D.IgnoreCollision(this.GetComponent<BoxCollider2D>(), item.GetComponent<BoxCollider2D>());
            }
        }

    }
    private void FootstepAudio()
    {
        if(lastOnGroundTime > 0 && MathF.Abs(xInput) > 0)
        {
            if (!footstepIsPlaying)
            {
                footstepEvent.Post(gameObject);
                footstepIsPlaying = true;
                lastFootstepTime = timeBetweenFootsteps;
            }

            if (footstepIsPlaying)
            {
                if (lastFootstepTime <= 0)
                {
                    footstepIsPlaying = false;
                }
            }
        }
    }

    private void SetGravityScale(float scale)
    {
        rb.gravityScale = scale; // sets player gravity to be scale
    }

    private void AnimatorUpdate()
    {
        if(stunTimer > 0)
        {
            xInputInt = 0;
        }
        else
        {
            xInputInt = (int)xInput;
        }
        animator.SetInteger("Xinput", xInputInt);
        animator.SetBool("Jumping", isJumping);
        animator.SetBool("Falling", isJumpFalling);
        animator.SetBool("Grounded", isGrounded);
        animator.SetFloat("Yvelocity", rb.velocity.y);
    }

    private void OnDrawGizmos()
    {
        // draw boxes for all detections
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(ground.transform.position, groundBoxCastSize);
    }
    private bool CanJump()
    {
        return lastJumpButtonPress > 0 && (lastOnGroundTime > 0 || lastInWaterTime > 0);
    }
    private bool CanJumpCut()
    {
        return isJumping && rb.velocity.y > 0;
    }

    public void JumpMobile()
    {
        lastJumpButtonPress = data.jumpInputBufferTime;
    }

    public void JumpCutMobile()
    {
        if (CanJumpCut())
        {
            isJumpCut = true;
        }
    }

    public void MoveMobile(int x) // Mobile Movement
    {
        {
            xInput = x;
        }

    }

    private void ShadowCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(ground.transform.position, Vector2.down, 99f, realGround);

        if (lastOnGroundTime < 0 &&  hit)
        {
            shadow.SetActive(true);
            shadow.transform.position = hit.point;
        }

        else
        {
            shadow.SetActive(false);
        }
    }

    private void ParticleCheck()
    {
        if (slowTimer > 0)
        {
            slowParticles.SetActive(true);
        }

        else
        {
            slowParticles.SetActive(false);
        }

        if (stunTimer > 0)
        {
            stunParticles.SetActive(true);
        }

        else
        {
            stunParticles.SetActive(false);
        }
    }
}
