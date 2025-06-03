using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawnDoorHandler : DoorClass
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("doorOpen", doorOpen);

        if(keysPresent == numOfKeysNeeded)
        {
           UnlockDoor();
        }
    }
}
