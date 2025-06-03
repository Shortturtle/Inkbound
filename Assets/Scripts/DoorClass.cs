using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorClass : MonoBehaviour
{
    public bool doorOpen;
    public int numOfKeysNeeded;
    [HideInInspector] public int keysPresent;

    public void KeyInserted()
    {
        keysPresent++;
    }

    public void UnlockDoor()
    {
        doorOpen = true;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
