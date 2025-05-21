using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pickup_Controller : MonoBehaviour
{
    
    
    // ---------- Refferences -------------------- // 
    public Transform Hold_Position;
    private Camera mainCamera;
    


    // ---------- Bools --------- //
    public bool isHeld = false;
    public bool clickedOn = false;
    public bool isCloseEnough = false;
    public bool isBeingHeld = false;

    
    // --------- Stats ---------- //
    public float pickupRange = 2f;
    public float followSpeed = 5f;



    void Start()

    //------------------ Set Up -------------------------//

    // Personal note: Debug.LogError can be used to pinpoint the point of error
    {
        mainCamera = Camera.main;
        //Main Camara is needed to raycast
        GameObject leader = GameObject.FindGameObjectWithTag("Leader");
        
        if(leader != null)
        {
           Holding_Logic itemHolder = leader.GetComponent<Holding_Logic>();
           if(itemHolder != null)
           {
               Hold_Position = itemHolder.Hold_Position;
           }
           else
           {
              enabled = false;
              return;
           }
        }
        
        
    }

    
    void Update()
    {
        //---------------- Check if and what is being clicked ---------------//

        if (isBeingHeld) return;
        // Check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null && hit.collider.gameObject == gameObject) //check if this object is clicked
            {
                // Check if the Leader is close enough
                if (Vector2.Distance(transform.position, Hold_Position.position) <= pickupRange)
                {
                    PickupItem();
                }
            }
        }
        
        
    }
//---------------- Pick Up item ---------------//
    void PickupItem()
        {
            isBeingHeld = true;
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.isKinematic = true; // Disable physics while held
            }
            transform.SetParent(Hold_Position);
            transform.localPosition = Vector3.zero; // Position it at the hold position

            // Notify the Leader (optional, depending on your game logic)
            if (Hold_Position != null)
            {
                Holding_Logic leaderItemHolder = Hold_Position.GetComponentInParent<Holding_Logic>(); // Find the ItemHolder on the Leader
                if (leaderItemHolder != null)
                {
                    leaderItemHolder.SetHeldItem(gameObject); //send the gameobject to the leader.
                }
            }
            Debug.Log(gameObject.name + " picked up.");
        }

//---------------- Drop Item ---------------//
    public void Drop()
    {
        isBeingHeld = false;
        transform.SetParent(null); // Unparent it
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = false; // Re-enable physics
        }
        // Notify the Leader
        if (Hold_Position != null)
        {
             Holding_Logic leaderItemHolder = Hold_Position.GetComponentInParent<Holding_Logic>(); // Find the ItemHolder on the Leader
             if (leaderItemHolder != null)
             {
                leaderItemHolder.ClearHeldItem();
             }
        }
        Debug.Log(gameObject.name + " dropped.");
    }
}

