using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorClass : MonoBehaviour
{
    public GameObject doorOpenPosition;
    public int numOfKeysNeeded;
    [HideInInspector] public int keysPresent;

    public void KeyInserted()
    {
        keysPresent++;
    }

    public void UnlockDoor()
    {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, doorOpenPosition.transform.position, 0.05f);
    }
}
