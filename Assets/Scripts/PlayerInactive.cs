using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerInactive : MonoBehaviour
{
    [SerializeField] private PlayerData data;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject ground;
    [SerializeField] private Vector2 groundBoxCastSize;
    [SerializeField] private LayerMask realGround;
    private bool groundHit;
    private float lastOnGroundTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
    }

    private void FixedUpdate()
    {
        Run(1);
    }

    private void Run(float lerpAmount)
    {
        float targetSpeed = 0 * data.runMaxSpeed; // calculate target speed
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
        if (Mathf.Abs(0) > 0) // if theres input
        {
            float direction = Mathf.Sign(0);
            transform.localScale = new Vector3(direction, 1, 1); //flip player to face input direciton
        }
    }

    private void CheckGround()
    {
            Vector2 groundBoxCastOrigin = ground.transform.position; // where the cast box originates from

            groundHit = Physics2D.OverlapBox(groundBoxCastOrigin, groundBoxCastSize, 0f, realGround); // grounded check
            if (groundHit)
            {
                lastOnGroundTime = data.coyoteTime; // allows for jump buffer and also acts as ground check
            }
    }
}
