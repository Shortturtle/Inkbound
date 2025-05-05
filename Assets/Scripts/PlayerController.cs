using System.Collections;
using System.Collections.Generic;
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
    private bool isJumpFalling;
    private bool isJumpCut;
    private float lastJumpButtonPress;

    //Player On Box variables
    public bool isOnTopOfBox;

    //Animator variable
    private Animator animator;
    private int xInputInt;

    //Misc variables
    [SerializeField] private PlayerInactive playerInactive;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        playerInactive = GetComponent<PlayerInactive>();

        SetGravityScale(data.gravityScale);

    }

    // Update is called once per frame
    void Update()
    {
        //Animator Update
        AnimatorUpdate();

        //Timer
        lastJumpButtonPress -= Time.deltaTime;
        lastOnGroundTime -= Time.deltaTime;

        //Jump Input
        if (UnityEngine.Input.GetButtonDown("Jump"))
        {
            lastJumpButtonPress = data.jumpInputBufferTime;
        }

        if (UnityEngine.Input.GetButtonUp("Jump"))
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
        if (CanJump() && lastJumpButtonPress > 0) // if jump button has been pressed and not bouncing and is able to jump
        {
            // sets bools to false other than isJumping to ensure correct gravity
            isJumping = true;
            isJumpCut = false;
            isJumpFalling = false;
            Jump();
        }

        //GroundCheck
        CheckGround();

        //Box Check
        PlayerOnBoxCheck();
    }

    private void FixedUpdate()
    {
        //Movement
        Run(1);
    }
    private void CheckGround()
    {
        if (!isJumping)
        {
            Vector2 groundBoxCastOrigin = ground.transform.position; // where the cast box originates from

            groundHit = Physics2D.OverlapBox(groundBoxCastOrigin, groundBoxCastSize, 0f, realGround); // grounded check
            if (groundHit)
            {
                isGrounded = true;
                lastOnGroundTime = data.coyoteTime; // allows for jump buffer and also acts as ground check
            }

            else
            {
                isGrounded = false;
            }
        }
    }

    private void GetInput()
    {
                xInput = UnityEngine.Input.GetAxisRaw("Horizontal");
    }

    private void Run(float lerpAmount)
    {
        GetInput();

        float targetSpeed = xInput * data.runMaxSpeed; // calculate target speed
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
    }

    private void Jump()
    {
        float force = data.jumpForce;

        if (rb.velocity.y < 0) // if falling
        {
            force -= rb.velocity.y; // offsets jump force by falling force to ensure proper jump height
        }

        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);

        // prevents multiple jumps 
        lastOnGroundTime = 0;
        lastJumpButtonPress = 0;
    }

    private void OnDisable()
    {
        rb.velocity = Vector2.zero;
        xInput = 0;
        playerInactive.enabled = true;
    }

    private void OnEnable()
    {
        playerInactive.enabled = false;
    }

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.gameObject.CompareTag("Artist"))
        {
            if(collision.collider.gameObject.CompareTag("Spikes") || collision.collider.gameObject.CompareTag("KillPlane"))
            {

            }
        }

        else if (this.gameObject.CompareTag("Drawing"))
        {
            if (collision.collider.gameObject.CompareTag("Water") || collision.collider.gameObject.CompareTag("KillPlane"))
            {

            }
        }
    }
    */

    private void PlayerOnBoxCheck()
    {
        if(isOnTopOfBox && lastOnGroundTime < 0)
        {
            isOnTopOfBox = false;
            transform.SetParent(null);
        }
    }

    private void SetGravityScale(float scale)
    {
        rb.gravityScale = scale; // sets player gravity to be scale
    }

    private void AnimatorUpdate()
    {
        xInputInt = (int)xInput;
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
        return lastJumpButtonPress > 0 && lastOnGroundTime > 0;
    }
    private bool CanJumpCut()
    {
        return isJumping && rb.velocity.y > 0;
    }
}
