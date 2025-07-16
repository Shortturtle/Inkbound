using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingPickUpHandler : PickUpHandlerClass
{
    private DrawnDoorHandler drawnDoorHandler;
    // Start is called before the first frame update
    void Start()
    {
        pickUpsInRadius = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldItem == null)
            {
                PickUpItem();
            }

            else if (heldItem != null)
            {
                if (heldItem.GetComponent<KeyClass>() && doorNearby == true)
                {
                    drawnDoorHandler.KeyInserted();
                    Destroy(heldItem);
                    heldItem = null;
                }

                else
                {
                    PutDownItem();
                }
            }
        }

        FailSafe();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PickUpClass>() && collision.gameObject.CompareTag("Drawn"))
        {
            pickUpsInRadius.Add(collision.gameObject);
        }

        if (collision.gameObject.GetComponent<DrawnDoorHandler>())
        {
            doorNearby = true;
            drawnDoorHandler = collision.GetComponent<DrawnDoorHandler>();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PickUpClass>() && collision.gameObject.CompareTag("Drawn"))
        {
            pickUpsInRadius.Remove(collision.gameObject);
        }

        if (collision.gameObject.GetComponent<DrawnDoorHandler>())
        {
            doorNearby = false;
            drawnDoorHandler = null;
        }
    }

    public void InteractButtonMobile()
    {
        if (heldItem == null)
        {
            PickUpItem();
        }

        else if (heldItem != null)
        {
            if (heldItem.GetComponent<KeyClass>() && doorNearby == true)
            {
                drawnDoorHandler.KeyInserted();
                Destroy(heldItem);
                heldItem = null;
            }
            else
            {
                PutDownItem();
            }
        }
    }
}
