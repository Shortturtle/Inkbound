using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holding_Logic : MonoBehaviour
{
    public Transform Hold_Position;
    private GameObject heldItem = null;

    // Start is called before the first frame update
    void Start()
    {
        if (Hold_Position == null)
        {
            Debug.LogError("Hold Position not assigned on the Leader.");
            enabled = false;
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // 1 is the right mouse button
        {
            DropHeldItem();
        }
    }

    public void SetHeldItem(GameObject item)
    {
        if (heldItem != null)
        {
            DropHeldItem();
        }
        heldItem = item;
    }

    public void ClearHeldItem()
    {
        heldItem = null;
    }

    public void DropHeldItem()
    {
        if (heldItem != null)
        {
            Pickup_Controller grabbableItem = heldItem.GetComponent<Pickup_Controller>();
            if (grabbableItem != null)
            {
                grabbableItem.Drop();
            }
            heldItem = null;
        }
    }

    public GameObject GetHeldItem()
    {
        return heldItem;
    }
}

