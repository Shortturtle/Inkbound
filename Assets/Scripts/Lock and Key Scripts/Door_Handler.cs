using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Handler : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Lock_Logic lock_Yellow;
    public Lock_Logic lock_Green;
    public Lock_Logic lock_Orange;

    private bool allLocksUnlocked = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!allLocksUnlocked && lock_Yellow.isUnlocked && lock_Green.isUnlocked && lock_Orange.isUnlocked)
        {
            UnlockDoor();
            allLocksUnlocked = true; 
        }
    }
    void UnlockDoor()
    {
     
        Collider2D doorCollider = GetComponent<Collider2D>();
        gameObject.SetActive(false);

       
    }
}
